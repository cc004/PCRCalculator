using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Il2CppInterop.Runtime;

namespace PcrApiDump
{
    /// <summary>
    /// 把 il2cpp 的「方法 → 内存地址(RVA)」全量导出，顺带把解包后的 GameAssembly.dll 镜像拷出来。
    ///
    /// 为什么需要：GameAssembly.dll 文件里 .text 只有 0x1000 字节（rawsize=4096 而 vsize≈4.2MB），
    /// 自定义节 il2cpp（102MB）同理 —— 代码与元数据都是运行时才展开的，所以
    /// Il2CppDumper / Cpp2IL / Ghidra 直接开文件都看不到真正执行的指令。
    /// BepInEx 跑在进程内，能直接问 il2cpp 要方法指针，再把镜像按页读出来。
    ///
    /// 输出（默认 &lt;BepInEx根&gt;\PcrApiDump）：
    ///   methods.csv        image,namespace,class,method,rva
    ///   gameassembly.bin   内存镜像，off == RVA（缺页补 0）
    ///   moduledump.json    模块基址 / 大小 / 统计
    ///
    /// 全程只读。
    /// </summary>
    internal static class MethodDump
    {
        private const int Chunk = 256 * 1024;
        private const long MaxImageBytes = 512L * 1024 * 1024;
        private static int _started;

        [StructLayout(LayoutKind.Sequential)]
        private struct MBI
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MODULEINFO
        {
            public IntPtr lpBaseOfDll;
            public uint SizeOfImage;
            public IntPtr EntryPoint;
        }

        private const uint MEM_COMMIT = 0x1000;
        private const uint PAGE_GUARD = 0x100;
        private const uint READABLE = 0x02 | 0x04 | 0x08 | 0x20 | 0x40 | 0x80;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern UIntPtr VirtualQuery(IntPtr address, out MBI info, UIntPtr length);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr process, IntPtr address, byte[] buffer,
                                                     IntPtr size, out IntPtr read);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern IntPtr GetModuleHandleA(string name);

        /// <summary>直接从已加载模块的 PE 头读 SizeOfImage（不依赖 psapi/K32GetModuleInformation）。</summary>
        private static uint SizeOfImageOf(IntPtr baseAddr)
        {
            int lfanew = Marshal.ReadInt32(baseAddr, 0x3C);
            return (uint)Marshal.ReadInt32(baseAddr, lfanew + 24 + 56);
        }

        internal static void OnceAfterStart(string outDir)
        {
            if (Interlocked.Exchange(ref _started, 1) == 1) return;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Thread.Sleep(3000);
                try { Run(outDir); }
                catch (Exception e) { try { Plugin.Logger.LogError("[method-dump] " + e); } catch { } }
            });
        }

        private static string Ansi(IntPtr p)
        {
            return p == IntPtr.Zero ? "" : (Marshal.PtrToStringAnsi(p) ?? "");
        }

        private static unsafe void Run(string outDir)
        {
            Directory.CreateDirectory(outDir);
            var baseAddr = GetModuleHandleA("GameAssembly.dll");
            if (baseAddr == IntPtr.Zero)
            {
                Plugin.Logger.LogError("[method-dump] GameAssembly.dll 未加载");
                return;
            }
            MODULEINFO mi;
            int lfanew = Marshal.ReadInt32(baseAddr, 0x3C);
            uint imgSize = SizeOfImageOf(baseAddr);
            long imgBase = baseAddr.ToInt64();
            long imgEnd = imgBase + imgSize;
            mi.lpBaseOfDll = baseAddr;
            mi.SizeOfImage = imgSize;
            mi.EntryPoint = IntPtr.Zero;
            Plugin.Logger.LogInfo(string.Format("[method-dump] GameAssembly base=0x{0:X} size=0x{1:X} (lfanew=0x{2:X})", imgBase, imgSize, lfanew));

            // ---- 1) 方法表 ----
            string csv = Path.Combine(outDir, "methods.csv");
            int methods = 0, withPtr = 0, classes = 0;
            using (var w = new StreamWriter(csv, false, new UTF8Encoding(false)))
            {
                w.WriteLine("image,namespace,class,method,rva");
                var domain = IL2CPP.il2cpp_domain_get();
                uint n = 0;
                var asms = IL2CPP.il2cpp_domain_get_assemblies(domain, ref n);
                for (uint a = 0; a < n; a++)
                {
                    var asm = asms[(int)a];
                    if (asm == IntPtr.Zero) continue;
                    var image = IL2CPP.il2cpp_assembly_get_image(asm);
                    if (image == IntPtr.Zero) continue;
                    string imageName = Ansi(IL2CPP.il2cpp_image_get_name(image));
                    int ccount = (int)IL2CPP.il2cpp_image_get_class_count(image);
                    for (int c = 0; c < ccount; c++)
                    {
                        var klass = IL2CPP.il2cpp_image_get_class(image, (uint)c);
                        if (klass == IntPtr.Zero) continue;
                        classes++;
                        string ns = Ansi(IL2CPP.il2cpp_class_get_namespace(klass));
                        string cn = Ansi(IL2CPP.il2cpp_class_get_name(klass));
                        var iter = IntPtr.Zero;
                        while (true)
                        {
                            var m = IL2CPP.il2cpp_class_get_methods(klass, ref iter);
                            if (m == IntPtr.Zero) break;
                            methods++;
                            IntPtr fn = Marshal.ReadIntPtr(m);   // MethodInfo 首字段 = methodPointer
                            long rva = 0;
                            if (fn != IntPtr.Zero)
                            {
                                long v = fn.ToInt64();
                                if (v >= imgBase && v < imgEnd) { rva = v - imgBase; withPtr++; }
                            }
                            w.WriteLine(string.Concat(imageName, ",", ns, ",", cn, ",", Ansi(IL2CPP.il2cpp_method_get_name(m)),
                                                     ",", rva == 0 ? "" : "0x" + rva.ToString("X")));
                        }
                    }
                }
            }
            Plugin.Logger.LogInfo(string.Format("[method-dump] methods.csv: {0} 方法 / {1} 类 / {2} 个拿到 RVA",
                                                methods, classes, withPtr));

            // ---- 2) 模块镜像 ----
            DumpImage(baseAddr, mi.SizeOfImage, Path.Combine(outDir, "gameassembly.bin"));
            using (var j = new StreamWriter(Path.Combine(outDir, "moduledump.json"), false, new UTF8Encoding(false)))
            {
                j.WriteLine("{");
                j.WriteLine(string.Format("  \"module\": \"GameAssembly.dll\","));
                j.WriteLine(string.Format("  \"base\": \"0x{0:X}\",", imgBase));
                j.WriteLine(string.Format("  \"size\": {0},", mi.SizeOfImage));
                j.WriteLine(string.Format("  \"methods\": {0},", methods));
                j.WriteLine(string.Format("  \"methods_with_rva\": {0},", withPtr));
                j.WriteLine(string.Format("  \"classes\": {0}", classes));
                j.WriteLine("}");
            }
        }

        /// <summary>读一块内存；整块失败就退化成 4KB 逐页读（能读到多少算多少）。返回读到的字节数。</summary>
        private static int ReadChunk(IntPtr baseAddr, long off, byte[] buf, int want)
        {
            Array.Clear(buf, 0, want);
            var cur = new IntPtr(baseAddr.ToInt64() + off);
            MBI info;
            if (VirtualQuery(cur, out info, (UIntPtr)(uint)Marshal.SizeOf(typeof(MBI))) != UIntPtr.Zero
                && info.State == MEM_COMMIT && (info.Protect & PAGE_GUARD) == 0 && (info.Protect & READABLE) != 0)
            {
                IntPtr got;
                if (ReadProcessMemory(GetCurrentProcess(), cur, buf, (IntPtr)want, out got)
                    && (long)got == want)
                    return want;
            }
            // 退化：4KB 逐页
            int ok = 0;
            for (int p = 0; p < want; p += 4096)
            {
                int n = Math.Min(4096, want - p);
                var page = new IntPtr(baseAddr.ToInt64() + off + p);
                MBI pi;
                if (VirtualQuery(page, out pi, (UIntPtr)(uint)Marshal.SizeOf(typeof(MBI))) == UIntPtr.Zero)
                    continue;
                if (pi.State != MEM_COMMIT || (pi.Protect & PAGE_GUARD) != 0 || (pi.Protect & READABLE) == 0)
                    continue;
                var tmp = new byte[n];
                IntPtr g3;
                if (ReadProcessMemory(GetCurrentProcess(), page, tmp, (IntPtr)n, out g3))
                {
                    Buffer.BlockCopy(tmp, 0, buf, p, n);
                    ok += n;
                }
            }
            return ok;
        }

        /// <summary>两趟采样：加载后先 dump 一次，20 秒后再 dump 一次做补零合并（il2cpp 段是按需 commit 的）。</summary>
        private static void DumpImage(IntPtr baseAddr, uint size, string path)
        {
            string p1 = path + ".pass1";
            long total = 0;
            var buf = new byte[Chunk];
            using (var fs = new FileStream(p1, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                for (long off = 0; off < size; off += Chunk)
                {
                    int want = (int)Math.Min(Chunk, size - off);
                    ReadChunk(baseAddr, off, buf, want);
                    fs.Write(buf, 0, want);
                    total += want;
                    if (total > MaxImageBytes) { Plugin.Logger.LogWarning("[method-dump] 超过上限，提前结束"); break; }
                }
            }
            Plugin.Logger.LogInfo("[method-dump] pass1 完成 " + total + " 字节，等 20s 让 il2cpp 段继续 commit");

            System.Threading.Thread.Sleep(20000);

            long filled = 0, stillZero = 0, readFail = 0;
            var buf2 = new byte[Chunk];
            using (var src = new FileStream(p1, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var dst = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                for (long off = 0; off < size; off += Chunk)
                {
                    int want = (int)Math.Min(Chunk, size - off);
                    src.Read(buf, 0, want);
                    int ok = ReadChunk(baseAddr, off, buf2, want);
                    if (ok < want) readFail += want - ok;
                    for (int i = 0; i < want; i++)
                    {
                        if (buf[i] == 0 && buf2[i] != 0) { buf[i] = buf2[i]; filled++; }
                        else if (buf[i] == 0 && buf2[i] == 0) stillZero++;
                    }
                    dst.Write(buf, 0, want);
                }
            }
            try { File.Delete(p1); } catch { }
            Plugin.Logger.LogInfo(string.Format(
                "[method-dump] pass2 合并完成: 补入 {0} 字节，仍为零 {1} 字节，读取失败 {2} 字节 -> {3}",
                filled, stillZero, readFail, path));
        }
    }
}

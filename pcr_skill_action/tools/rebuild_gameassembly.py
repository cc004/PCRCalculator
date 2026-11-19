# -*- coding: utf-8 -*-
"""把 BepInEx 插件 dump 出来的 gameassembly.bin（RVA 线性镜像）重建成 Ghidra 能直接打开的 PE。

背景：B:\\...\\GameAssembly.dll 文件里的 .text 只有 0x1000 字节（rawsize=4096 / vsize≈4.2MB），
自定义节 il2cpp（102MB）同理 —— 代码与元数据都是运行时展开的。插件在进程内把镜像按 RVA
逐页读出来（缺页补 0），本脚本再把节表改成「文件偏移 == RVA」，Ghidra 就能按正确地址反编译。

用法：
    python rebuild_gameassembly.py <gameassembly.bin> <输出.dll> [--entry 0x1A2B3C]
"""
import struct
import sys
import os

SRC_DLL = r"B:\IDM\script\SimLaunchers\SimLauncher\GameAssembly.dll"


def read_headers(path):
    with open(path, "rb") as f:
        data = f.read(0x400)
    pe = struct.unpack_from("<I", data, 0x3C)[0]
    with open(path, "rb") as f:
        f.seek(pe + 4)
        machine, nsec = struct.unpack("<HH", f.read(4))
        f.seek(pe + 20)
        optsz, = struct.unpack("<H", f.read(2))
        f.seek(pe + 24)
        opt = f.read(optsz)
        f.seek(pe + 24 + optsz)
        tab = f.read(40 * nsec)
    sections = []
    for i in range(nsec):
        s = tab[i * 40:(i + 1) * 40]
        name = s[:8].rstrip(b"\0").decode("latin1")
        vsize, vaddr, rawsize, rawptr = struct.unpack_from("<IIII", s, 8)
        chars, = struct.unpack_from("<I", s, 36)
        sections.append(dict(name=name, vsize=vsize, vaddr=vaddr,
                             rawsize=rawsize, rawptr=rawptr, chars=chars))
    return pe, machine, nsec, optsz, bytearray(opt), sections


def main():
    if len(sys.argv) < 3:
        print(__doc__)
        sys.exit(2)
    bin_path, out_path = sys.argv[1], sys.argv[2]
    entry = None
    if "--entry" in sys.argv:
        entry = int(sys.argv[sys.argv.index("--entry") + 1], 16 if sys.argv[sys.argv.index("--entry") + 1].startswith("0x") else 10)

    img = open(bin_path, "rb").read()
    pe, machine, nsec, optsz, opt, sections = read_headers(SRC_DLL)
    size_of_image = struct.unpack_from("<I", opt, 56)[0]
    image_base, = struct.unpack_from("<Q", opt, 24)
    print("镜像 %d 字节, section_alignment=0x%x, size_of_image=0x%x, image_base=0x%x"
          % (len(img), struct.unpack_from("<I", opt, 32)[0], size_of_image, image_base))

    SECT_ALIGN = 0x1000
    # 1) 让整个可选头里所有 RVA/大小都按 file==rva 重写
    struct.pack_into("<I", opt, 32, SECT_ALIGN)   # SectionAlignment
    struct.pack_into("<I", opt, 36, SECT_ALIGN)   # FileAlignment
    struct.pack_into("<I", opt, 56, (size_of_image + SECT_ALIGN - 1) & ~(SECT_ALIGN - 1))  # SizeOfImage
    if entry is not None:
        struct.pack_into("<I", opt, 16, entry)     # AddressOfEntryPoint
    # 2) 清掉重定位 / 调试目录（dump 里已按最终地址排布）
    dir_off = 112  # PE32+ NumberOfRvaAndSizes 之后就是目录表
    for i in (5, 6):  # 5 = Relocation, 6 = Debug
        struct.pack_into("<II", opt, dir_off + i * 8, 0, 0)
    struct.pack_into("<I", opt, 108, 16)

    # 3) 输出文件长度 = SizeOfImage，按 RVA 铺数据
    total = struct.unpack_from("<I", opt, 56)[0]
    buf = bytearray(total)
    out = bytearray()
    # 头部
    src = open(SRC_DLL, "rb").read()
    hdr_end = pe + 24 + optsz + 40 * nsec
    out.extend(src[:hdr_end])
    out[pe + 24: pe + 24 + optsz] = opt
    # 节表：文件偏移 = RVA
    for i, s in enumerate(sections):
        off = pe + 24 + optsz + i * 40
        struct.pack_into("<II", out, off + 8, s["vsize"] or s["vaddr"], s["vaddr"])   # vsize, vaddr 保持
        struct.pack_into("<II", out, off + 16, s["vsize"], s["vaddr"])               # rawsize, rawptr
    if len(out) < total:
        out.extend(b"\0" * (total - len(out)))
    # 铺数据
    copied = 0
    for s in sections:
        va, vs = s["vaddr"], s["vsize"]
        if vs == 0:
            continue
        chunk = img[va:va + vs]
        out[va:va + len(chunk)] = chunk
        copied += len(chunk)
    open(out_path, "wb").write(bytes(out))
    print("已写出 %s (%d 字节)，铺入 %d 字节节内容" % (out_path, len(out), copied))
    for s in sections:
        print("  %-8s vaddr=0x%-9x vsize=0x%-9x" % (s["name"], s["vaddr"], s["vsize"]))


if __name__ == "__main__":
    main()

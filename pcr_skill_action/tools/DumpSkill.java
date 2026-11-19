// DumpSkill.java —— 按 RVA 列表批量反编译 il2cpp 方法（Ghidra headless 脚本）
//
// 输入：环境变量 SKILL_LIST = csv(表头 class,method,rva)，rva 形如 0x14BDA70
// 输出：环境变量 SKILL_OUT  = 文本文件，每个方法一段伪 C
//
// 用法（配合 analyzeHeadless）：
//   analyzeHeadless <projDir> <projName> -import GameAssembly_rva.dll -noanalysis \
//       -scriptPath _il2cpp/scripts -postScript DumpSkill.java
//
// 思 路：不跑全量分析（192MB / 46 万方法跑不完），只对列出的地址逐个
//       disassemble + createFunction，再让 DecompInterface 出伪 C。

import ghidra.app.script.GhidraScript;
import ghidra.app.decompiler.*;
import ghidra.program.model.address.Address;
import ghidra.program.model.listing.*;
import ghidra.program.model.symbol.SourceType;

import java.io.*;
import java.util.*;

public class DumpSkill extends GhidraScript {

    @Override
    public void run() throws Exception {
        String inPath = System.getenv("SKILL_LIST");
        String outPath = System.getenv("SKILL_OUT");
        if (inPath == null || outPath == null) {
            println("[DumpSkill] 缺少 SKILL_LIST / SKILL_OUT 环境变量");
            return;
        }
        int budget = 120; // 单函数反编译秒数
        int maxFuncs = Integer.MAX_VALUE;
        String lim = System.getenv("SKILL_MAX");
        if (lim != null && lim.length() > 0) maxFuncs = Integer.parseInt(lim);

        DecompInterface di = new DecompInterface();
        di.toggleCCode(true);
        di.toggleSyntaxTree(true);
        DecompileOptions opts = new DecompileOptions();
        di.setOptions(opts);
        if (!di.openProgram(currentProgram)) {
            println("[DumpSkill] 打开反编译器失败");
            return;
        }

        FunctionManager fm = currentProgram.getFunctionManager();
        long base = currentProgram.getImageBase().getOffset();

        BufferedReader br = new BufferedReader(new FileReader(inPath));
        PrintWriter pw = new PrintWriter(new FileWriter(outPath));
        String line = br.readLine(); // 表头
        int ok = 0, fail = 0, n = 0;
        long t0 = System.currentTimeMillis();
        while ((line = br.readLine()) != null && n < maxFuncs) {
            n++;
            String[] p = line.split(",");
            if (p.length < 3) continue;
            String cls = p[0], meth = p[1], rvaHex = p[2].trim();
            if (!rvaHex.startsWith("0x")) continue;
            long rva;
            try { rva = Long.parseLong(rvaHex.substring(2), 16); } catch (Exception e) { continue; }
            Address addr = toAddr(base + rva);

            Function f = fm.getFunctionContaining(addr);
            if (f == null) {
                // 先把一段范围反汇编好，createFunction 才有指令流可依
                long span = 0x2000;
                String sp = System.getenv("SKILL_SPAN");
                if (sp != null && sp.length() > 0) span = Long.decode(sp.trim());
                try {
                    ghidra.program.disassemble.Disassembler dis =
                        ghidra.program.disassemble.Disassembler.getDisassembler(currentProgram, monitor, null);
                    ghidra.program.model.address.AddressSet restrict =
                        new ghidra.program.model.address.AddressSet(addr, addr.add(span - 1));
                    dis.disassemble(addr, restrict);
                } catch (Exception e) { }
                try { f = createFunction(addr, null); } catch (Exception e) { }
                if (f == null) {
                    // 有些入口是 thunk：试着用 body 起点
                    try { f = fm.getFunctionContaining(addr); } catch (Exception e) { }
                }
            }
            if (f == null) {
                pw.println("/* ==== " + cls + "::" + meth + "  @ 0x" + Long.toHexString(rva) + "  <未能建立函数> ==== */");
                pw.println();
                fail++;
                continue;
            }
            DecompileResults res = di.decompileFunction(f, budget, monitor);
            pw.println("/* ==== " + cls + "::" + meth + "  @ 0x" + Long.toHexString(rva)
                       + "  size=" + f.getBody().getNumAddresses() + " ==== */");
            if (res != null && res.decompileCompleted()) {
                pw.println(res.getDecompiledFunction().getC());
                ok++;
            } else {
                pw.println("/* 反编译失败: " + (res == null ? "null" : res.getErrorMessage()) + " */");
                fail++;
            }
            pw.println();
            pw.flush();
        }
        di.dispose();
        pw.close();
        br.close();
        println("[DumpSkill] 完成 " + n + " 个：成功 " + ok + " 失败 " + fail
                + "，耗时 " + (System.currentTimeMillis() - t0) / 1000 + "s -> " + outPath);
    }
}

// AnalyzeDump.java —— 先建函数 + 限制范围分析（让跳转表/switch 恢复），再批量反编译
//
// 与 DumpSkill.java 的差别：多了一步「只对目标函数体做 AutoAnalysis」。
// 目的：DumpSkill 用 -noanalysis，Ghidra 不会把 switch 的跳转表识别成指针数组，
//       伪 C 里就只剩 117 处 "Could not recover jumptable"、0 个 switch。
//       对 UnitActionController 的 104 个函数做局部分析后，`switch (action_type)` 才能显形。
//
// 环境变量：
//   SKILL_LIST  csv(表头 class,method,rva)
//   SKILL_OUT   输出伪 C
//   SKILL_SPAN  反汇编跨度（默认 0x2000）
//   SKILL_MAX   最多处理几个（调试用）
//   SKILL_ANALYZE  1=做局部分析（默认 1）；0=等同 DumpSkill
//
// 用法：
//   analyzeHeadless <proj> <name> -import GameAssembly_rva.dll -noanalysis \
//       -scriptPath _il2cpp/scripts -postScript AnalyzeDump.java

import ghidra.app.script.GhidraScript;
import ghidra.app.decompiler.*;
import ghidra.app.plugin.core.analysis.AutoAnalysisManager;
import ghidra.program.disassemble.Disassembler;
import ghidra.program.model.address.Address;
import ghidra.program.model.address.AddressSet;
import ghidra.program.model.listing.*;

import java.io.*;
import java.util.*;

public class AnalyzeDump extends GhidraScript {

    @Override
    public void run() throws Exception {
        String inPath  = System.getenv("SKILL_LIST");
        String outPath = System.getenv("SKILL_OUT");
        if (inPath == null || outPath == null) {
            println("[AnalyzeDump] 缺少 SKILL_LIST / SKILL_OUT 环境变量");
            return;
        }
        int maxFuncs = Integer.MAX_VALUE;
        String lim = System.getenv("SKILL_MAX");
        if (lim != null && lim.length() > 0) maxFuncs = Integer.parseInt(lim.trim());

        long span = 0x2000;
        String sp = System.getenv("SKILL_SPAN");
        if (sp != null && sp.length() > 0) span = Long.decode(sp.trim());

        boolean doAnalyze = true;
        String an = System.getenv("SKILL_ANALYZE");
        if (an != null && an.trim().equals("0")) doAnalyze = false;

        FunctionManager fm = currentProgram.getFunctionManager();
        long base = currentProgram.getImageBase().getOffset();
        Disassembler dis = Disassembler.getDisassembler(currentProgram, monitor, null);

        // ---------- 第一遍：按 CSV 建函数，收集函数体范围 ----------
        List<String[]> rows = new ArrayList<>();
        BufferedReader br = new BufferedReader(new FileReader(inPath));
        br.readLine(); // 表头
        String line;
        while ((line = br.readLine()) != null && rows.size() < maxFuncs) {
            String[] p = line.split(",");
            if (p.length < 3) continue;
            if (!p[2].trim().startsWith("0x")) continue;
            rows.add(new String[]{ p[0], p[1], p[2].trim() });
        }
        br.close();

        AddressSet bodies = new AddressSet();
        List<Function> funcs = new ArrayList<>();
        List<String[]> missing = new ArrayList<>();
        long t0 = System.currentTimeMillis();
        for (String[] r : rows) {
            long rva;
            try { rva = Long.decode(r[2]); } catch (Exception e) { continue; }
            Address addr = toAddr(base + rva);
            Function f = fm.getFunctionContaining(addr);
            if (f == null) {
                try {
                    AddressSet restrict = new AddressSet(addr, addr.add(span - 1));
                    dis.disassemble(addr, restrict, true);
                } catch (Throwable t) { }
                try { f = createFunction(addr, null); } catch (Throwable t) { }
                if (f == null) { try { f = fm.getFunctionContaining(addr); } catch (Throwable t) { } }
            }
            if (f == null) { missing.add(r); continue; }
            funcs.add(f);
            bodies.add(f.getBody());
        }
        println("[AnalyzeDump] 建函数 " + funcs.size() + " / " + rows.size()
                + (missing.isEmpty() ? "" : "（未建立 " + missing.size() + "）")
                + "，函数体合计 " + bodies.getNumAddresses() + " 字节，"
                + (System.currentTimeMillis() - t0) / 1000 + "s");

        // ---------- 第二遍：只对函数体做分析（这是开关） ----------
        if (doAnalyze && funcs.size() > 0) {
            long t1 = System.currentTimeMillis();
            try {
                // Ghidra 12 的入口是 startAnalysis + reAnalyzeAll(AddressSetView) + waitForAnalysis
                AutoAnalysisManager am = AutoAnalysisManager.getAnalysisManager(currentProgram);
                am.startAnalysis(monitor);
                am.reAnalyzeAll(bodies);
                am.waitForAnalysis(3000, monitor);   // 等到分析队列空闲（3s 上限）
                println("[AnalyzeDump] reAnalyzeAll(函数体) 完成");
            } catch (Throwable t) {
                println("[AnalyzeDump] 分析阶段失败: " + t);
                t.printStackTrace();
            }
            println("[AnalyzeDump] 分析耗时 " + (System.currentTimeMillis() - t1) / 1000 + "s");
        }

        // ---------- 第三遍：反编译 ----------
        DecompInterface di = new DecompInterface();
        di.toggleCCode(true);
        di.toggleSyntaxTree(true);
        di.setOptions(new DecompileOptions());
        if (!di.openProgram(currentProgram)) { println("[AnalyzeDump] 反编译器打开失败"); return; }

        PrintWriter pw = new PrintWriter(new FileWriter(outPath));
        int ok = 0, fail = 0;
        for (int i = 0; i < funcs.size(); i++) {
            Function f = funcs.get(i);
            String[] r = null;
            // 找对应的 csv 行（按入口地址匹配）
            for (String[] x : rows) {
                long rva;
                try { rva = Long.decode(x[2]); } catch (Exception e) { continue; }
                if (base + rva == f.getEntryPoint().getOffset()) { r = x; break; }
            }
            String tag = (r == null) ? ("?" + f.getEntryPoint()) : (r[0] + "::" + r[1]);
            DecompileResults res = di.decompileFunction(f, 180, monitor);
            pw.println("/* ==== " + tag + "  @ 0x" + Long.toHexString(f.getEntryPoint().getOffset() - base)
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
        for (String[] r : missing) {
            pw.println("/* ==== " + r[0] + "::" + r[1] + "  @ " + r[2] + "  <未能建立函数> ==== */");
            pw.println();
        }
        di.dispose();
        pw.close();
        println("[AnalyzeDump] 反编译 " + (ok + fail) + " 个：成功 " + ok + " 失败 " + fail
                + "，总耗时 " + (System.currentTimeMillis() - t0) / 1000 + "s -> " + outPath);
    }
}

# PCR 技能/战斗逻辑逆向包（v4）

> 目标：让拿到这个包的人**不需要游戏、不需要 Ghidra 也能开始读**「某个 action_type 到底怎么算」。
> 本包含两件东西：①反编译出来的 13 个战斗/技能类的伪 C；②运行时从 il2cpp 直接导出的
> **类/字段偏移/方法签名/枚举值**（`dump.cs` 等价物）。

---

## 0. 先说清楚为什么不是「常规 Il2CppDumper 流程」

| 常规做法 | 本游戏实际情况 |
|---|---|
| `GameAssembly.dll` 里读代码 | 文件里 `.text` 只有 1 页（`rawsize=4096` 而 `vsize≈4.42MB`），`il2cpp` 段（`vsize≈102MB`）同样只有 1 页 → **代码与元数据都是运行时才展开** |
| `global-metadata.dat` + Il2CppDumper | 安装目录**没有**这个文件；内存镜像里 metadata `magic 0xFAB11BAF` **0 命中**；字符串堆在（`0x81B6000–0x85F6000`），但**没有任何 header 的 `stringOffset` 指向它** → header 被加壳器抹掉，Dumper 第一步 sanity 就挂 |
| Ghidra 直接开文件 | 打开的是「壳」，看到的是空页 |

所以走的是**进程内**：BepInEx 插件（`plugin/`）用 il2cpp 自己的 API 拿「方法→RVA」「字段→偏移」「枚举→值」，
再把解包后的模块镜像按页 dump 出来，重建成 `RVA == 文件偏移` 的 PE 交给 Ghidra 反编译。

---

## 1. 包内有什么

```
README.md                  本文件
docs/SKILL_BATTLE_REVERSE.md   逆向笔记（管线、对象模型、ExecAction 实读、局限、复现、踩坑）
docs/ACTION_TYPE_MAP.md        action_type 全表（125 个具名成员 + 数值 + 消费点）
decompiled/*.c             13 个类的伪 C（1,917/1,924 方法已具名，0 个残留 sub_）
analyzed/                  UnitActionController 局部分析版（22 个 caseD_）+ 96 个下游函数
data/                      skill_methods.csv(1,924) / uac.csv(104) / callees.csv(96)
meta/enums.csv             47,977 条枚举字面量（含 action_type 全表）
meta/moduledump.json       本次采集的模块基线信息
tools/                     Ghidra 脚本 + 重建 PE / 改名 / 符号反查脚本
plugin/                    BepInEx 插件源码（MethodDump.cs / FieldDump.cs / Plugin.cs）
```

**另一个包**（如果一并收到）：`nozomi-cb-il2cpp-metadata-*.zip` —— 里面是全量
`dump.cs`（60MB）、`classes.csv`（36,506 类）、`fields.csv`（212,088 字段）、
`methods_sig.csv`（463,026 方法带签名）、`methods.csv`（方法→RVA）。
本包可以独立使用；想深挖任意类时再解那个包。

---

## 2. 直接开读的三条线索

### 2.1 action_type 在哪

```
FEFJGFLKNLK.OKCMLNJFFDB          +0x60   IAGKLCGAFII   ← action_type（就是汇编里的 _action + 0x60）
IAGKLCGAFII                      System.Enum，125 个具名成员（1=ATTACK, 9=SLIP_DAMAGE, 10=BUFF_DEBUFF, …）
Elements.ActionParameterOnPrefab.ActionType   +0x14      ← master/预制体侧同一个枚举
Elements.DamageData.<ActionType>  +0x78 / <AilmentActionType> +0x94
Elements.ForceHitData.<ActionType> +0x18
```

全表见 `docs/ACTION_TYPE_MAP.md`。

### 2.2 消费点

```
Elements.UnitActionController::ExecAction   RVA 0x14BDA70   段长 11,992 字符
  (FEFJGFLKNLK _action, Elements.Skill _skill, Elements.BasePartsData _target,
   System.Int32 _num, System.Single _starttime, System.Boolean _execChildNoFrame,
   Elements.BasePartsData _protectedTarget, System.Boolean _ignoreProtect)
```

`decompiled/UnitActionController.c` 里搜 `ExecAction(` 即可；里面凡是
`*(int *)(param_2 + 0x60)`（也可能显示成 `param_2[0xc]`）就是 action_type。

### 2.3 真正的分派函数

il2cpp 的 x64 分派是 4 字节相对跳转表（`movsxd rax,[table+rax*4]; add rax,base; jmp rax`），
Ghidra 只会标 `caseD_`，**不会**形成 `switch`。所以别指望看到 `switch(action_type)`，
直接顺着下面几个 Cygames 自混淆静态方法走：

```
FEFJGFLKNLK::JFPNODDNPBF     RVA 0x37B2930
FEFJGFLKNLK::MNLBJHHAJHG     RVA 0x37B2D90
FEFJGFLKNLK::OLLAIGFLEBN     RVA 0x37B3470
MGIFJCBCKB::IFDPIEEMDKD      RVA 0x3932570
MGIFJCBCKB::ODPPNGHCIBJ      RVA 0x3932A10
```

反编译里 `FUN_1817b2930` 这类名字可用 `tools/resolve.py` 按 `methods.csv` 反查。

---

## 3. 已知局限（别踩）

1. **没有 `switch`**：如上，jump table 未恢复；`analyzed/*.c` 里有 22 个 `caseD_` 标签可参考。
2. **名字被混淆**：`FEFJGFLKNLK`、`IAGKLCGAFII` 这类是三字母随机名；字段名有的是 `<Xxx>k__BackingField`（属性后备字段）。
3. **master 数据表名/列名也混淆**：`master_raw.db` 里表名全是 `v1_<hash>`，且没有名为 `action_type` 的列，需要另做映射。
4. `decompiled/` 里个别方法来自共享桩（多个方法名指向同一 RVA），已按「唯一 VA 才改名」处理，仍可能有个别名不精确。
5. 数值常量（伤害系数、上限等）多在 master 数据与 `SkillActionValue` 里，不在代码常量里。

---

## 4. 复现方法

### 4.1 采集（需要能运行游戏 + BepInEx）

1. 把 `plugin/*.cs` 放进你的 BepInEx 插件工程，配置：
   ```ini
   [General]
   Enabled = true
   DumpMethodsOnly = true     # 不装任何 hook，只 dump
   DumpMethods = true         # methods.csv + gameassembly.bin
   DumpFields = true          # dump.cs + classes/fields/methods_sig/enums
   OutputDirectory = <你的输出目录>
   ```
2. 启动游戏，进到主界面；约 20 秒后输出齐（`pass2` 会等 20s 二次采样补零页）。

### 4.2 重建可分析的 PE

```bash
python tools/rebuild_gameassembly.py <gameassembly.bin> <GameAssembly_rva.dll>
```

把 `SectionAlignment/FileAlignment` 改成 0x1000、每节 `PointerToRawData = VirtualAddress`、`SizeOfRawData = VirtualSize`，
文件补到 `SizeOfImage`，再把 dump 出来的镜像按 RVA 铺进去。之后任何反汇编器都能直接按 RVA 寻址。

### 4.3 批量反编译（Ghidra headless，需 JDK 21）

```bash
set JAVA_HOME=<jdk-21>
analyzeHeadless <项目目录> PcrGA -import GameAssembly_rva.dll \
  -scriptPath tools -postScript DumpSkill.java -noanalysis
# 局部范围分析（能消掉 jumptable 告警，但仍不生成 switch）
analyzeHeadless ... -postScript AnalyzeDump.java -noanalysis
```

脚本读环境变量 `SKILL_LIST`(csv) / `SKILL_OUT`(输出) / `SKILL_MAX` / `SKILL_ANALYZE`。

---

## 5. 采集基线（本次）

```
module=GameAssembly.dll  base=0x7FFE7C6B0000  size=192,155,648
methods=463,025   methods_with_rva=461,564   classes=36,505
fields=212,088    enum literals=47,977
技能相关方法 1,924 个（BattleManager 750 / UnitUtility 418 / UnitCtrl 383 /
UnitActionController 104 / SkillEffectCtrl 79 / SkillBase 64 / …）→ 全部有代码
```

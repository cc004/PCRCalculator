# PCR 技能 / 战斗逻辑逆向笔记

> 目标：拿到「某个 `action_type` 到底怎么算」的代码。
> 结论先说：**已经能拿到**——客户端把战斗结算放在本地（`Elements.Battle.*`），我们把 `GameAssembly.dll` 运行时解密后的代码整段 dump 出来、重建成可分析的 PE、批量反编译成伪 C，并按 il2cpp 方法表把 `FUN_xxxx` 还原成 `类::方法`。
> 本目录里 `decompiled/` 就是 1,924 个技能/战斗方法的伪 C（0 失败）。

---

## 0. TL;DR

| 问题 | 答案 |
|---|---|
| 为什么 Il2CppDumper / Cpp2IL / Ghidra 直接开 `GameAssembly.dll` 什么都看不到 | 那个 DLL 是**壳**：`.text` 和 `il2cpp` 段在文件里各只有 **4KB**，真正的代码与元数据是加载时展开的 |
| 怎么拿到真代码 | BepInEx 插件在**运行中的进程里**读 `il2cpp_*` API 拿「方法 → 方法指针」，同时把模块镜像整段 dump 下来 |
| 为什么第一次 dump 有 30% 是零 | `il2cpp` 段按需 commit；**改成两趟采样（3s + 20s 补零合并）后覆盖率 100%** |
| 怎么反编译 | 把 dump 重建成「文件偏移 == RVA」的 PE（imagebase 0x180000000），再喂 Ghidra headless 批量出伪 C |
| 可读性 | 用 `methods.csv`（463,025 个方法、461,564 个带 RVA）把 `FUN_1814bda70` 换成 `UnitActionController::ExecAction`（15,242 个共享桩保留 `sub_xxx`） |

---

## 1. 为什么必须绕这一圈：`GameAssembly.dll` 是壳

PE 头实测（`B:\...\SimLauncher\GameAssembly.dll`，183,971,840 B，PE32+ / machine 0x8664 / magic 0x20b，7 节）：

| 节 | vaddr | vsize | rawsize | 说明 |
|---|---|---|---|---|
| `.text` | 0x1000 | 4,415,488 | **4096** | 文件里只有 1 页 |
| `il2cpp` | 0x437000 | **102,334,464** | **4096** | 代码 + 元数据都在这里，文件里同样只有 1 页 |
| `.rdata` | 0x65CF000 | 18,247,680 | 18,239,488 | 正常 |
| `.data` | 0x7736000 | 57,667,584 | 54,898,688 | 正常 |

⇒ 直接静态解包必然失败（`global-metadata.dat` 不存在、`il2cpp_*` 符号 0 命中、`FAB11BAF` magic 0 命中，全都对得上）。

同时这也解释了另一件事：**`libil2cpp.so`（安卓版）和 `GameAssembly.dll`（PC 版）是同一套路**，只是壳不同。

---

## 2. 三段管线

### 2.1 采集（BepInEx 插件 `PcrApiDump` 的 `MethodDump`）

```
IL2CPP.il2cpp_domain_get()
  → il2cpp_domain_get_assemblies(domain, ref n)          // 返回 IntPtr*（不安全指针）
  → il2cpp_assembly_get_image / il2cpp_image_get_class_count / il2cpp_image_get_class
  → il2cpp_class_get_methods(klass, ref iter)             // 遍历每个类的每个方法
  → 方法地址 = Marshal.ReadIntPtr(methodInfo)              // MethodInfo 首字段 = methodPointer
     落到 [moduleBase, moduleBase+SizeOfImage) 内才采信，写成 RVA
```

产物（`BepInEx\PcrApiDump\`）：

| 文件 | 内容 |
|---|---|
| `methods.csv` | `image,namespace,class,method,rva` —— 463,025 行 / 32 MB |
| `gameassembly.bin` | 模块镜像按 RVA 线性排布，192,155,648 B（缺页补 0） |
| `moduledump.json` | `{base, size, methods, methods_with_rva, classes}` |

**两趟采样（关键）**：同一个 `il2cpp` 段在加载后 3 秒仍然是「按需 commit」的状态，一趟采样会得到 **577/1924 = 30% 全零**的技能方法（`UnitActionController` 尤其惨：99/104 全零）。
改法：3 秒 dump 一次 → 等 20 秒 → 再 dump 一次，逐字节「谁非零用谁」；整块 `ReadProcessMemory` 失败就退化成 4KB 逐页读。
效果：**技能方法覆盖率 1347/1924 → 1924/1924**，`ExecAction` 的字节从 `00 00 00…` 变成 `40 53 56 41 55 41 56 41 57 48 81 EC A0 00 00 00`（`push rbx/rsi/r13/r14/r15; sub rsp,0xA0`）。

> 同时也加了 `DumpMethodsOnly` 开关：为 true 时**一个 hook 都不装**，只做 dump。
> 之前抓包 hook 反射读 `NetworkTask.Params` 会 `AccessViolation`，纯取代码时不需要它。

### 2.2 重建 PE（`tools/rebuild_gameassembly.py`）

要做的事就一句：让**文件偏移 == RVA**，这样 Ghidra 加载后地址和 `methods.csv` 里的 RVA 一一对应。

```
SectionAlignment = FileAlignment = 0x1000
每节：PointerToRawData = VirtualAddress, SizeOfRawData = VirtualSize
清空 reloc / debug 目录，文件长度补到 SizeOfImage
按 RVA 把 gameassembly.bin 铺进去
→ GameAssembly_rva.dll（192,155,648 B，imagebase 0x180000000）
```

### 2.3 批量反编译 + 命名还原

Ghidra headless（`tools/DumpSkill.java` + `support/analyzeHeadless.bat`）：

```
analyzeHeadless <projDir> PcrGA -import GameAssembly_rva.dll \
    -scriptPath tools -postScript DumpSkill.java -noanalysis
```

脚本对 CSV 里每个 RVA：`disassemble(addr, addr+0x1000)` → `createFunction(addr)` → `DecompInterface.decompileFunction`。
**不开全量分析**（192 MB / 46 万方法跑不完），只对目标地址按需建函数——1,924 个方法连导入一共 **110 秒，0 失败**。

命名还原（`tools/rename_split.py`）：把伪 C 里的 `FUN_xxxxxxxx` / `func_0x0000000180xxxxxx` 按 `methods.csv` 换成 `类::方法`。
- 251,145 个有名字的 VA 里，**235,903 个唯一** → 直接改名；
- **15,242 个是「多主」共享桩**（同一个 RVA 被好几个方法指向，比如属性访问器共用 `0x43ABE0`）→ v1 一律保留 `sub_xxxxxxxx`，结果 580 个方法名丢掉；**v2 改成「只有当这个 RVA 正好是当前段落自己那个方法时才用段落头名字」**，于是 `get_IsAura`(0x5E9A30) 这类「自己就是共享桩」的也能正名。

最终结果：1,924 个函数定义里 **1,917 个带 `类::方法` 名，0 个残留 `sub_`**（剩下 7 个是 Ghidra 没有给出独立定义的极小桩）。

---

## 3. 产物

```
decompiled/
  UnitActionController.c   596 KB   104 方法   ← ExecAction / ExecSkillWithTargetId / createActionValue …
  BattleManager.c         1178 KB   750 方法   ← 主循环 / 伤害快照 / Wave
  UnitUtility.c           1833 KB   418 方法   ← 目标筛选、位置、属性换算
  UnitCtrl.c               490 KB   383 方法   ← 单位、Spine、HP、异常
  SkillEffectCtrl.c        112 KB    79 方法
  UnitSkill.c               93 KB    30 方法
  SkillUtility.c            69 KB    26 方法
  SkillDefine.c             52 KB    21 方法
  SkillBase.c               24 KB    64 方法
  SkillActionValue.c         5 KB    23 方法
  SkillLimitCounter.c        4 KB    11 方法
  ActionData.c / ActionExecedData.c   —— 表数据载体
  skill_all_named.c        4.2 MB   全部 1,924 个（不拆，方便 grep）
data/
  skill_methods.csv          1,924 行  class,method,rva
  moduledump.json            模块基址/大小/方法数
tools/
  rebuild_gameassembly.py / DumpSkill.java / rename_split.py / audit_dump.py / peek_code.py
plugin/
  MethodDump.cs              采集插件源码（两趟采样 + 逐页降级 + PE 头读 SizeOfImage）
```

---

## 4. 技能 / 战斗对象模型（已确认部分）

### 4.1 `ActionData`（表里一行的载体）

伪 C 里能直接读出的字段：

| 偏移 | 类型 | 含义 |
|---|---|---|
| `+0x10` | int32 | `ActionId`（`get_ActionId` = `return *(int*)(this+0x10)`） |
| `+0x18` | int64 | `ActionValue`（`get_ActionValue` = `return *(longlong*)(this+0x18)`） |
| `+0x2c` | int32 | 在 `ExecAction` 里被当作「非 0xC 分支的取值」 |
| `+0x30`（`[6]` 个 qword） | int32 | 当 `actionType == 0xC` 时改用它 |
| `+0x60`（`[0xc]` 个 qword） | int32 | **疑似 `action_type`**：既参与和 `0xC` 比较，又被当 actionType 传给 `ResistStatus` |
| `+0x1a6` | bool | `ExecAction` 里读的某个标志位 |

> 注意：`ActionData` 的方法体基本都是**共享桩**（`0x43ABE0` 那一族被几十个访问器复用），所以 `ActionData.c` 里的伪 C 看起来像别人的代码——这是 il2cpp 的代码折叠，不是反编译错。

### 4.2 `UnitActionController`（动作执行器，104 个方法）

关键方法（RVA 十六进制）：

| 方法 | RVA |
|---|---|
| `ExecAction` | `0x14BDA70` |
| `ExecActionOnStart` | `0x14BD3F0` / `0x14BD520` |
| `ExecActionOnWaveStart` | `0x14BD790` |
| `ExecActionWithDelayAndTarget` | `0x14BD970` |
| `ExecChildrenAction` | `0x14BE430` |
| `ExecExSkill` | `0x14BEAB0` |
| `ExecSkillWithTargetId` | `0x14BEE20` |
| `ExecSkillWithTargetParts` | `0x14BF390` |
| `ExecSkillWithTargetPartsAndActionList` | `0x14BEFA0` |
| `ExecUnitActionNoDelay` / `WithDelay` | `0x14BFA00` / `0x14BFB90` |
| `CreateExSkill` | `0x14BBF30` |
| `DependActionSolve` | `0x14BCE50` |
| `Initialize` / `InitializeAction` | `0x14C02C0` / `0x14C0210` |
| `SearchTargetByAction` / `searchTargetUnit` | `0x14C20B0` / `0x14CA490` |
| `judgeIsTarget` / `isInActionTargetArea` | `0x14C95B0` / `0x14C9250` |
| `sortTargetListByTargetPattern` | `0x14CC700` |
| `SetAttackValue` | `0x14C25E0` |
| `StartAction` | `0x14C2890` / `0x14C3A20` |
| `createActionValue` | `0x14C4D60` |
| `setSkillParameter` | `0x14CB840` |
| `convertPriorityPattern` | `0x14C4D40` |

私有字段（来自 interop 元数据）：`_action` / `_depend_action` / `_atk_pattern` / `PlateColor` / `_each_odds` / `_ailment` / `_reward_count` / `MainSkillLv` / `_Owner` / `_skillDictionary`。

### 4.3 其它

- `SkillEffectCtrl`（79）：`get_IsAura` = `*(byte*)(this+0x40)`，`set_IsAura` = 写同一偏移 —— 说明 `+0x40` 是「光环/领域」标志。这类**极小的属性访问器非常好用**：它们直接告诉你字段偏移。
- `SkillBase`（64）：`ReadySkill` / `SetLevel` / `_ctor`。
- `SkillDefine`（21）/ `SkillUtility`（26）/ `SkillActionValue`（23）：枚举与数值换算的候选地。
- `BattleManager`（750）：`Update`、伤害快照、`ExistsEnvironment`、`onExecutedActionListCountZero`。

---

## 5. 执行链路（`ExecAction` 实读）

`UnitActionController::ExecAction` 反编译出 **约 12,000 字符**，开头是这样的（已还原命名）：

```c
void UnitActionController::ExecAction(longlong self, longlong *action, longlong target,
                                      ulonglong parts, int param5, ...)
{
  // 一次性初始化：一批 il2cpp 类型句柄（sub_18032f6d0 = il2cpp_class_init 之类）
  if (cRam000000018ab626b0 == '\0') { sub_18032f6d0(0x187b3fb28); ... cRam000000018ab626b0 = 1; }

  ctx = sub_18032f970(...);            // 取/建执行上下文
  FUN_1804383d0(ctx, 0);
  if (ctx == 0) goto fail;

  ctx->self      = self;               // +0x28
  ctx->from_1    = action[0x2e];       // +0x10
  ctx->from_2    = action[0x2f];       // +0x18
  ctx->from_3    = action[0x32];       // +0x20
  ...
  // 七个参数槽（0..6）依次算值并写回
  for (i = 0; i < 7; i++) {
      v = (**(code **)(lVar9 + 0x18))(lVar9->arg0, action, i, lVar9->arg1);
      FUN_1855e0980(lVar7, i, v, ...);
  }

  uVar4 = FUN_1837b2930(action, *(parts + 0x68), param5, 0);   // 某种可执行性/倍率判定
  ...
  // ★ action_type 在这里被取出来
  if ((int)action[0xc] == 0xc)  uVar17 = (int)action[6];       // 0xC 特例走 +0x30
  else                          uVar17 = (int)(action + 0x2c); // 其余走 +0x2c

  cVar5 = Elements::BasePartsData::ResistStatus(parts, (int)action[0xc], uVar17, ...);
  if (cVar5 == 0) {
      FUN_1837b3470(action, *(parts + 0x68), param5, 0);
      ...
      lVar20 = *(longlong *)(*(longlong *)(lRam0000000187b77e20 + 0xb8) + 0x30);
      cVar6 = FUN_1855f08a0(lVar20, (int)action[0xc], ...);    // 又一次按 action_type 分派
  }
}
```

要点：

1. `action` 就是 `ActionData`，`action[0xc]`（= `+0x60`）贯穿全函数当 **action_type** 用；
2. 值来自 `+0x2c` / `+0x30` 两个槽，`0xC` 是特例；
3. 真正「算伤害/抗性」的第一站是 `Elements::BasePartsData::ResistStatus(parts, actionType, value, …)`；
4. 后面还有一次按 action_type 的分派（`FUN_1855f08a0(…, (int)action[0xc], …)`）。

---

## 6. 还没做完的两件事（以及怎么做）

### 6.1 jump table 没恢复 → `switch(action_type)` 目前看不到

伪 C 里有 **117 处** `WARNING: Could not recover jumptable`，`switch`/`case` 一个都没出来。
原因：`-noanalysis` 只按需建函数，跳转表数据没被分析成指针数组。

**已试过的「局部分析」路线（`tools/AnalyzeDump.java`）**：先按 CSV 建函数 → 收集函数体 `AddressSet` → `AutoAnalysisManager.startAnalysis(monitor)` + **`reAnalyzeAll(bodies)`** + `waitForAnalysis(3000, monitor)` → 再反编译。效果：

| 指标 | `-noanalysis`（DumpSkill） | 局部分析（AnalyzeDump） |
|---|---|---|
| `UnitActionController` 里 `Could not recover jumptable` | 117 | **0** |
| `caseD_xxxx` 标签 | 0 | **22**（分布在 `CreateExSkill` 6 / `searchTargetUnit` 7 / `sortTargetListByTargetPattern` 9） |
| 真正的 `switch (...)` 语句 | 0 | **仍然 0** |

⇒ **结论：不是「没分析」，是 Ghidra 解不动 il2cpp 的相对跳转表。** il2cpp 在 x64 上编译出的分派是
`movsxd rax, dword [table + rax*4]` + `add rax, base` + `jmp rax`（4 字节相对偏移表），Ghidra 只把它标成 `caseD_<addr>` 标签 + `goto`，不形成 `switch` 语句。

要继续往下走，只有两条：
- **A（推荐，收益直接）**：不依赖 switch 形状 —— 按「谁读 `ActionData + 0x60`」来定位。`ExecAction` 里读一次，然后把它交给两个**混淆名静态方法**继续算，那两个才是真正的分派器：
  - `FEFJGFLKNLK::JFPNODDNPBF` @ rva `0x37B2930`
  - `FEFJGFLKNLK::MNLBJHHAJHG` @ rva `0x37B2D90`
  - `FEFJGFLKNLK::OLLAIGFLEBN` @ rva `0x37B3470`
  - 以及 `MGIFKJCBCKB::IFDPIEEMDKD` @ `0x3932570`、`MGIFKJCBCKB::ODPPNGHCIBJ` @ `0x3932A10`
  这几个类名是 Cygames 自己混淆过的（不是 Ghidra 的 `FUN_`），说明它们**在方法表里有真名**，只是名字无意义。
- **B（写脚本）**：自己解跳转表 —— 在指令里找 `movsxd reg, [table + idx*4]` 模式，把 `table` 的 4 字节项逐个 `+table基址` 得到 case 目标，再按「比较链里的常量顺序」对上 case 值。`tools/AnalyzeDump.java` 已经把「建函数 + 局部 reAnalyzeAll」这段做好了，脚本只需在 `Function.getBody()` 的指令上扫这个模式。

### 6.2 master 表名/列名被混淆

`master_raw.db` 里 1,012 张表全叫 `v1_<sha256 片段>`，**没有任何列叫 `action_type`**，`skill_action`/`skill` 关键词 0 命中。
所以要拿到「某个 action_type 的数值来自哪张表的哪一列」，还得先做一次表名/列名映射（可用列的形状 + 行数 + 已知样例值反推，或用美术/UI 里出现的字符串对齐）。

---

## 7. 复现步骤

```powershell
# 1) 采集（游戏里进到主页，等 40 秒）
#    插件配置：BepInEx\config\com.analysis.pcrapidump.cfg
#    DumpMethodsOnly = true   ← 只 dump，不装 hook
#    DumpMethods     = true
#    产物：BepInEx\PcrApiDump\{methods.csv, gameassembly.bin, moduledump.json}

# 2) 重建 PE
python tools\rebuild_gameassembly.py gameassembly.bin GameAssembly_rva.dll

# 3) 批量反编译（需要 JDK 21）
$env:JAVA_HOME       = "F:\Java\jdk-21"
$env:SKILL_LIST      = "data\skill_methods.csv"
$env:SKILL_OUT       = "out\skill_all.c"
$env:SKILL_SPAN      = "0x1000"
analyzeHeadless ghidra_proj PcrGA -import GameAssembly_rva.dll `
    -scriptPath tools -postScript DumpSkill.java -noanalysis

# 4) 命名还原 + 按类拆分
python tools\rename_split.py
```

自检点：`SkillEffectCtrl::get_IsAura`（RVA `0x5E9A30`）应当反编译成 `return *(byte *)(param_1 + 0x40);`。

---

## 8. 备忘：踩过的坑

1. **`ReadProcessMemory` 读自己的模块也要两趟**——il2cpp 段按需 commit，一趟采样必然丢 30%。
2. **不要用 `GetModuleInformation`**（不是 `kernel32` 的导出，是 psapi / `K32GetModuleInformation`）；直接读 PE 头的 `SizeOfImage`（`lfanew + 24 + 56`）更省事。
3. **`il2cpp_domain_get_assemblies` 返回 `IntPtr*`**，C# 里必须 `unsafe`，`Marshal.ReadIntPtr` 用不了。
4. **`createFunction` 之前必须先 `disassemble` 一段范围**（`Disassembler.getDisassembler(...).disassemble(addr, restrictSet)`），否则函数体只有 1 字节，反编译报 `Flow exceeded maximum allowable instructions`。
5. **同一个 RVA 可能对应多个方法名**（共享桩），改名时要按「唯一才改」处理，否则 `ActionData::SetActionId` 会显示成 `PointerState::set_ClickCount`。
6. Ghidra 12 需要 **JDK 21+**；`JAVA_HOME` 指到 JDK 8 会 `Failed to find a supported JDK`。
7. 同一个 Ghidra 项目不能重复 `-import`；重建项目目录或用 `-overwrite`。
8. 非 ASCII 路径会让 log4j 报 `Include operation failed`，无害。

## 9. action_type 全表（已解）

`IAGKLCGAFII` 枚举（`FEFJGFLKNLK.OKCMLNJGFDB` 即 **+0x60**）的 125 个具名成员与其数值，
见 **`ACTION_TYPE_MAP.md`**。数据来自插件 `FieldDump` 用 `il2cpp_field_static_get_value`
直接读运行时枚举字面量（本游戏 metadata header 被抹，Il2CppDumper 不可用）。

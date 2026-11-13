# PCRCalculatorServer HTTP API 接入说明

本文面向外部调用方，描述本仓库当前 .NET server 的 HTTP 接口。服务端与 Unity 客户端共享战斗代码，HTTP 层使用 ASP.NET Core。最后核对日期为 2026-11-13。

## 1. 启动与基础约定

### 1.1 启动服务

在包含可执行文件和 `Data/` 的目录执行以下命令。

```bash
./PCRCalculatorServer --server --host 127.0.0.1 --port 8080
```

Windows 使用 `PCRCalculatorServer.exe`。使用框架依赖构建时执行以下命令，并安装相应 .NET 8 运行时。

```bash
dotnet PCRCalculatorServer.dll --server --host 127.0.0.1 --port 8080
```

| 参数 | 默认值 | 含义 |
| --- | --- | --- |
| `--server` | 关闭 | 开启 HTTP 服务，接入时必须传入 |
| `--host` | `localhost` | 监听地址，其他机器访问时可设为 `0.0.0.0` |
| `--port` | `8080` | HTTP 端口 |
| `--debug` | 关闭 | 打印调试信息，并将模拟请求的 `IsSkipping` 设为 `false` |

初始化会加载数据库、缓存及资源，完成后才开始接受 HTTP 请求。桌面分发包还需保留 server 随附的 `Data/StreamingAssets`、`classdata.tpk` 等文件。打包与目录说明见 [BUILDING.md](../BUILDING.md)。

### 1.2 数据版本

模拟使用进程加载的数据，请求中的角色和敌人 ID 也从这些数据中查找。启动前可通过环境变量设置数据目录和版本。

| 环境变量 | 含义 |
| --- | --- |
| `PCRCALC_PERSISTENT_DATA_PATH` | 数据库、资源缓存所在目录 |
| `PCRCALC_VERSION_DATA` | `MainManager.VersionData` 的 JSON，覆盖进程所用版本配置 |

版本 JSON 示例中的数值需替换为已准备的数据版本。

```json
{
  "CharacterVersionJP": 10066000,
  "BossVersionJP": 10066000,
  "BossVersionCN": 202610141431,
  "useQA": false,
  "useJP": true,
  "newAB": false,
  "useLatestProd": false
}
```

复现实战时，需同时固定数据版本、完整角色养成、队伍强化、随机设置与 UB 输入，并确保客户端和 server 的数据版本一致。

### 1.3 HTTP 和 JSON

- 示例服务地址为 `http://127.0.0.1:8080`。
- 请求头使用 `Content-Type: application/json`，文本使用 UTF-8。
- 字段大小写沿用服务端命名。顶层模拟字段如 `PlayerList` 使用 PascalCase，角色字段如 `unitId` 使用小写开头的命名。
- 枚举在 JSON 中用整数传输。省略字段时采用 DTO 的默认值。
- 伤害、适应度使用 C# `long`，上限为有符号 64 位整数。JavaScript 调用方处理超过安全整数范围的值时应使用支持大整数的 JSON 解析方式。
- 当前服务使用普通 HTTP，未配置身份认证和 CORS。浏览器跨域访问时，需要在部署入口配置相应策略。

| 方法 | 路径 | 请求体 | 成功响应 |
| --- | --- | --- | --- |
| POST | `/simulate` | 战斗请求数组 | 与输入顺序对应的结果数组 |
| POST | `/arena/suggest` | 单个竞技场建议请求 | 队伍建议对象 |

以下说明针对 .NET server。源码中 Unity `HttpListener` 使用的任务 UID、`/result/{uid}` 等协议属于另一套服务入口。外部接入本文接口时，等待 `/simulate` 的最终响应即可。

接口路由和序列化配置见 [ApiController.cs](ApiController.cs) 与 [ArenaController.cs](ArenaController.cs)。

## 2. POST /simulate

### 2.1 最小公会战示例

保存为 `battle.json`，将角色和敌人 ID 替换为当前数据库支持的 ID。示例设置了 `ToMaxUnitAttr=true`，服务端会将角色养成设为当前上限，方便先检查服务能否连通、请求能否正常处理。

```json
[
  {
    "PlayerList": [
      { "unitId": 131101 },
      { "unitId": 128301 },
      { "unitId": 100101 },
      { "unitId": 129701 },
      { "unitId": 101101 }
    ],
    "GuildEnemyList": [401610408],
    "IsGuildBattle": true,
    "ToMaxUnitAttr": true,
    "BattleTime": 90,
    "IsAutoMode": false,
    "IsSetMode": true,
    "IsSkipping": true,
    "CalcStats": 1,
    "NeedOnceResult": true,
    "Settings": {
      "UseFixedRandomSeed": true,
      "RandomSeed": 666,
      "ForceNoCritical_player": false,
      "ForceNoCritical_enemy": false,
      "ForceIgnoreDodge_player": false,
      "ForceIgnoreDodge_enemy": false,
      "ForceCritical_player": false,
      "randomSpecialDatas": []
    }
  }
]
```

```bash
curl --fail-with-body \
  -H 'Content-Type: application/json' \
  --data-binary @battle.json \
  http://127.0.0.1:8080/simulate
```

单场请求也需放在数组中。一个请求体可以包含多场不同配置的战斗，服务端将它们排队交给 worker，全部完成后按输入顺序返回。客户端应根据批量规模设置请求超时。

完整养成配置可参考 [request1.json](request1.json) 和 [request2.json](request2.json)。这两个文件各保存一个请求对象，直接调用时需在外层加 `[]`，并按目标战斗调整随机设置、ID 和数据版本。

### 2.2 战斗请求字段

| 字段 | 类型 | 默认值 | 含义 |
| --- | --- | --- | --- |
| `PlayerList` | `UnitData[]` | `null` | 我方队伍，必填且至少一人，常规接入使用五人队伍 |
| `GuildEnemyList` | `int[]` | `null` | 公会战类敌人 `enemy_id`，`IsGuildBattle=true` 时提供有效列表 |
| `EnemyList` | `UnitData[]` | `null` | `IsGuildBattle=false` 时提供敌方角色队伍 |
| `IsGuildBattle` | `bool` | `true` | 通过敌人数据库创建敌方，或直接使用 `EnemyList` |
| `BattleTime` | `int` | `90` | 公会战类战斗时长，单位秒；非公会战路径使用其自身战斗设置 |
| `ToMaxUnitAttr` | `bool` | `false` | 将双方玩家角色数据按当前数据库及计算器设置提升到上限 |
| `IsAutoMode` | `bool` | `false` | AUTO 模式 |
| `IsSetMode` | `bool` | `false` | 公会战我方全员 SET |
| `SetToggles` | `bool[]` | `null` | 按角色槽位设置 SET；`IsSetMode=true` 时全员开启 |
| `IsSemanMode` | `bool` | `false` | 公会战使用语义 UB 输入 |
| `UBExecTimeList` | `float[][]` | `null` | 帧模式的 UB 时间表 |
| `SemanUBExecTimeList` | `int[][]` | `null` | 语义模式的 UB 条件编码，每个角色一行 |
| `UseSequenceUB` | `bool` | `false` | 启用语义 UB 的全局释放顺序 |
| `SemanUBSequenceData` | `int[]` | `null` | 顺序中的角色槽位，使用 `0..4` |
| `IsSkipping` | `bool` | `true` | 快速模拟，批量接入通常保持开启 |
| `Settings` | `GuildRandomData` | 固定种子 `666` | 本场随机设置，结构见下文 |
| `CalcStats` | `int` | `0` | 统计位掩码，详见第 3 节 |
| `ActionProgressFrames` | `int[]` | `null` | 请求采样动作进度的逻辑帧 |
| `NeedOnceResult` | `bool` | `false` | 返回单场伤害与种子摘要 `OnceResult` |
| `RepeatCount` | `int` | `1` | 同一配置重复运行，响应聚合语义见第 5 节 |
| `OptimizationTarget` | `int` | `0` | 单场优化评分目标，详见第 5 节 |
| `OptimizationTpDemand` | `int` | `0` | 攻击力优化目标的 TP 上升需求门槛 |
| `WaveGroupId` | `int` | `0` | 波次及环境上下文，特殊关卡使用对应 wave ID |
| `IsTalentQuest` | `bool` | `false` | 属性关卡标识 |
| `TalentFullInfo` | `bool` | `false` | 共享请求中的兼容字段，当前 `StartByServer` 未读取 |

服务端先按角色 `searchAreaWidth`，再按 `unitId` 升序排列队伍。调用方需预先按这个顺序组织 UB 行、SET 槽位和顺序编码。响应中的 `PlayerList` 按实际使用顺序排列，统计中的玩家数组也与之对应。

时间按每秒 60 帧计算。`UBExecTimeList` 前五行对应五名角色，可包含第六行用于自动释放通道。同帧内的释放顺序用 `300.001` 这类小数编码表示，整数部分是逻辑帧。语义条件沿用计算器的编码，可直接使用计算器保存的 `SemanUBExecTimeList`。例如 `[[0], [0], [0], [0], [0]]` 给每名角色安排一个无额外等待帧的语义 UB 条件；后续是否释放由 TP、状态及队列决定。

请求 DTO 见 [BattleSimulationServer.cs](../Assets/Scrips/Elements/Battle/BattleSimulationServer.cs)，角色排序和初始化见 [MyGameCtrl.cs](../Assets/Scrips/Elements/Battle/MyGameCtrl.cs)，UB 编码见 [SemanUbManager.cs](../Assets/Scrips/Elements/Battle/SemanUbManager.cs)。

### 2.3 UnitData 角色养成

精确回放时，设置 `ToMaxUnitAttr=false` 并提供完整养成配置，字段名按表中拼写传入。

| 字段 | 类型 | 含义 |
| --- | --- | --- |
| `unitId` | `int` | 角色 ID，必填 |
| `level`、`rarity`、`rank` | `int` | 等级、星级、Rank |
| `equipLevel` | `int[6]` | 六个装备槽，`-1` 为未穿戴，`0..5` 为强化等级 |
| `skillLevel` | `int[4]` | 依次为 UB、技能 1、技能 2、EX 技能等级 |
| `playLoveDic` | `object` | 角色好感数据，键为字符串形式的角色 ID，值为好感等级 |
| `uniqueEqLv` | `int` | 专武 1 等级 |
| `uniqueEq2Lv` | `int` | 专武 2 等级，默认 `-1` |
| `uniqueEqPlus`、`uniqueEq2Plus` | `bool` | 对应专武 SP+ 标识 |
| `connectRank`、`connectRankLimit` | `int` | 连结 Rank 及上限 |
| `exEquip`、`exEquipLevel` | `int[3]` | 三个 EX 装备 ID 及强化等级 |
| `exEquipSubs` | `object` | EX 装备槽 `0..2` 对应的八项附加参数数组 |
| `PrefabId` | `int` | 需要明确指定战斗形态时使用，通常按角色默认形态解析 |

`love` 和 `love2` 是只读的派生属性，好感数据通过 `playLoveDic` 写入。下面的角色配置可作为完整 `PlayerList` 中的一项。

```json
{
  "unitId": 100101,
  "level": 325,
  "rarity": 6,
  "rank": 35,
  "equipLevel": [5, 5, 5, 5, 5, 5],
  "skillLevel": [325, 325, 325, 325],
  "playLoveDic": { "100101": 12 },
  "uniqueEqLv": 330,
  "uniqueEq2Lv": -1,
  "connectRank": 0,
  "exEquip": [0, 0, 0],
  "exEquipLevel": [0, 0, 0]
}
```

可用等级、装备和形态由服务端所用数据版本决定。完整模型见 [UnitData.cs](../Assets/Scrips/PCRCalcularor/UnitData.cs)。

### 2.4 队伍强化和特殊关卡

以下字段位于单个战斗请求对象中。

| 字段 | 类型 | 含义 |
| --- | --- | --- |
| `TalentSkillLevels` | `object<int,int>` | 我方属性等级配置 |
| `TalentSkillNodeLevels` | `object<int,int>` | 我方属性技能节点等级 |
| `MasterSkillNodeLevels` | `object<int,int>` | 我方大师技能节点等级 |
| `RoleMastery` | `object<int,[int,int]>` | 我方职能强化，值依次为 `slotLevel`、`enhanceLevel` |
| `EnemyTalentSkillLevels` | `object<int,int>` | 敌方属性等级配置 |
| `EnemyTalentSkillNodeLevels` | `object<int,int>` | 敌方属性技能节点等级 |
| `EnemyMasterSkillNodeLevels` | `object<int,int>` | 敌方大师技能节点等级 |
| `EnemyRoleMastery` | `object<int,[int,int]>` | 敌方职能强化 |
| `LabyrinthEnhancedNodeIds` | `int[]` | 已选迷宫强化节点 |
| `LabyrinthRelicIds` | `int[]` | 已选迷宫遗物 |
| `LabyrinthOwnedUnitIds` | `int[]` | 遗物人数等加成所依据的完整持有/参战角色列表 |
| `LabyrinthAlphaRupee` | `int` | Alpha 卢比数 |
| `LabyrinthQuestId`、`LabyrinthDifficulty` | `int` | 迷宫关卡与难度上下文 |

整数字典的 JSON 键写成字符串，例如 `"RoleMastery": {"1": [10, 20]}`。职能数值对也接受 `{"slotLevel": 10, "enhanceLevel": 20}` 或 `{"Item1": 10, "Item2": 20}`。

要让多次调用的配置可复现，应显式传入所用的强化字典，空配置使用 `{}`。`LabyrinthOwnedUnitIds` 省略或为空时，遗物被动计算回退到出战队伍。兼容字段 `LabyrinthOwnedUnitsCount`、`LabyrinthOwnedUnitRoleCounts`、`LabyrinthOwnedUnitTalentCounts` 仍存在于 DTO，当前初始化路径读取的是 `LabyrinthOwnedUnitIds`。

### 2.5 随机设置

`Settings` 的默认配置为 `UseFixedRandomSeed=true`、`RandomSeed=666`，强制开关默认为 `false`。

| 字段 | 含义 |
| --- | --- |
| `UseFixedRandomSeed` | 使用指定种子；设为 `false` 时由 server 为每场生成种子 |
| `RandomSeed` | 本场种子，实际采用值由响应 `CurrentSeed` 返回 |
| `ForceNoCritical_player`、`ForceNoCritical_enemy` | 强制我方、敌方不暴击 |
| `ForceIgnoreDodge_player`、`ForceIgnoreDodge_enemy` | 强制我方、敌方攻击必中 |
| `ForceCritical_player` | 强制我方暴击 |
| `randomSpecialDatas` | 计算器已有的条件随机覆盖列表，普通接入可用 `[]` |
| `DataName` | 随机方案的显示名称 |

使用自然随机伤害样本时，将强制开关全部设为 `false`，并清空特殊覆盖列表。

### 2.6 响应

以下响应仅示意字段结构，数字用于说明格式，实际返回可能包含更多字段。

```json
[
  {
    "Success": true,
    "Message": "战斗模拟完成",
    "PlayerList": [{ "unitId": 100101 }],
    "EnemyList": [],
    "Stats": {
      "TotalDamage": 123456789,
      "PlayerDamageList": [123456789],
      "UBTime": [[300.0, 900.0]]
    },
    "BattleFrames": 5399,
    "CurrentSeed": 666,
    "Result": 2,
    "SemanUBNum": null,
    "OnceResult": {
      "randomSeed": 666,
      "currentDamage": 123456789,
      "criticalEX": 23456789,
      "exceptDamage": 123456789,
      "backTime": 1,
      "backTimeText": "剩1s",
      "warnings": [],
      "BattleResult": 2
    },
    "OptimizationFitness": null
  }
]
```

| 字段 | 含义 |
| --- | --- |
| `Success`、`Message` | 本项是否成功及说明，必须逐项判断 |
| `PlayerList`、`EnemyList` | 模拟实际使用的双方队伍配置 |
| `BattleFrames` | 战斗结束逻辑帧 |
| `CurrentSeed` | 本场实际采用的随机种子 |
| `Result` | `-1` 无效、`0` 胜利、`1` 失败、`2` 超时、`3` 撤退 |
| `Stats` | 所选统计；通常在 `CalcStats=0` 且没有有效进度采样时为 `null` |
| `SemanUBNum` | 公会战语义模式下，五个角色各自执行的语义 UB 数量 |
| `OnceResult` | `NeedOnceResult=true` 时提供单场摘要，伤害读 `currentDamage` |
| `OptimizationFitness` | `OptimizationTarget` 对应的单场评分，默认 `null` |
| `TalentLevel`、`TalentSkill`、`TeamSkill` | 运行时强化信息描述 |
| `Validation` | 当前 DTO 的预留字段，此控制器未填充 |

## 3. CalcStats 统计选项

`CalcStats` 使用整数。`0` 关闭常规统计，`1` 和 `2` 都映射为基础统计加 UB 时间。其他正整数按位组合，并自动补上基础统计位 `1`。

| 位值 | 名称 | 主要返回内容 |
| ---: | --- | --- |
| 1 | Core | 总伤害、队员伤害/非 UB 伤害/TP 获取，以及攻击防御相关评分 |
| 2 | UBTime | `UBTime`、`UBToleranceFrames` |
| 4 | BuffFlow | `BuffDebuffEvents` |
| 8 | BossDef | `BossDefEvents`、`BossMagicDefEvents` |
| 16 | SkillExecMin | `SkillExecEvents` |
| 32 | HitTrace | `HitTraceEvents` |
| 64 | SkillReadyTrace | `SkillReadyCrossingFrames` |
| 128 | AutoSemanTeacherTrace | `AutoSemanTeacherPayloads`、`AutoSemanUBExecTimeList` |
| 256 | TpChangeTrace | `TpChangeEvents` |

例如 `CalcStats=35`（`1 | 2 | 32`）返回基础统计、UB 时间和命中记录。要开启表中所有统计项，可传 `511`。未开启的事件集合一般返回空数组。

- `SkillExecEvents` 包含 `UnitId`、`SkillId`、`SkillName`、`ActionId`、`StartFrame`、`EndFrame`。尚未记录结束帧时 `EndFrame=-1`。
- `HitTraceEvents` 记录正伤害命中，包含帧、来源/目标、技能、Action、伤害、是否暴击、剩余 HP、部位和伤害类型。
- `BuffDebuffEvents` 记录起止帧、来源/目标、属性类型、数值，以及战斗结束时是否仍有效。
- `TpChangeEvents` 包含变化前后 TP、请求增量、实际增量、溢出量和是否跨过 UB 就绪阈值。角色槽位索引为 `0..4`，无法绑定时使用 `-1`。
- 玩家伤害、TP、UB 等数组按响应 `PlayerList` 对齐。`UBTime` 使用帧单位，可含同帧顺序小数。

当前 .NET server 以 `DONT_USE_EX` 编译，`Stats.TotalDamage`、`OnceResult.currentDamage` 为实战采样伤害，`OnceResult.exceptDamage` 读取的期望类型在此模式下也退化为采样值。`ScorePhysicsExpectation`、`ScoreMagicExpectation` 累计的是逐帧攻防评分。当前版本的普通 `/simulate` 尚未实现用于计算独立蓝字期望的 `UseExpectedDamage` 和 `Stats.ExpectedTotalDamage` 字段。五种子均值的处理见第 5 节。

开关和输出映射见 [BattleStatistics.cs](../Assets/Scrips/Elements/Battle/BattleStatistics.cs)。

## 4. 按帧采样角色动作进度

将以下字段加入完整的战斗请求即可采样。即使 `CalcStats=0`，有效的采样请求也会返回 `Stats.ActionProgress`。

```json
{
  "CalcStats": 0,
  "ActionProgressFrames": [0, 300, 600, 900]
}
```

返回的进度数据结构示例如下。

```json
{
  "Frames": [0, 300, 600],
  "UnitIds": [100101, 100201],
  "StartFrames": [500.0, 600.0],
  "LoopFrames": [900.0, 1000.0],
  "Progress": [
    [0.0, 0.0],
    [150.0, 130.0],
    [430.0, 410.0]
  ]
}
```

- `Frames[i]` 为实际采集到的帧。采样列表会排序、去重，并过滤负数及大于 `min(BattleTime × 60, 5400)` 的值。尚未运行到的帧不会补值。
- `UnitIds[j]` 标识对应角色，仅包含初始化时的我方非召唤、非幻影角色。
- `Progress[i][j]` 表示该角色在该帧累计经过的标准动作帧数，可能超过一个完整循环。
- `StartFrames[j]` 为初始等待、默认起手动作及相关 recast 的标准总长度，`LoopFrames[j]` 为默认单轮循环标准长度。两者在初始化时计算。
- 加减速影响实时进度的推进速度。普通攻击/技能被取消时会结算其标准动作长度，因此进度可向前跳。
- 未请求有效采样时，响应省略 `ActionProgress`。只请求动作采样时，可直接读取进度及 `TotalDamage`，其他依赖常规统计的累计字段可能保持零值。

当 `Progress >= StartFrames` 且 `LoopFrames > 0` 时，可用 `(Progress - StartFrames) % LoopFrames` 得到默认循环中的相位。动作模式发生切换时，固定长度仍以初始化的默认动作序列为准。

## 5. 批量、重复和外部优化

### 5.1 RepeatCount

`RepeatCount>1` 时，服务端会按同一配置展开多场模拟，但返回数组的长度仍与顶层输入请求数量相同。每个聚合结果有 `ResultList`、`BattleFramesList`、`CurrentSeedList`，分别保存各场结果、帧数和种子。

顶层的 `Stats`、`OnceResult`、`OptimizationFitness` 和进度采样取自首个成功场。若全部失败，则取首项。`Success` 表示全部重复是否成功。希望读取各场伤害时，应提交多个顶层请求对象，并将各项 `RepeatCount` 设为 `1`。固定种子配置会重复使用同一个种子。

### 5.2 单场 OptimizationFitness

| `OptimizationTarget` | 含义 | 单场评分 |
| ---: | --- | --- |
| 0 | None | `OptimizationFitness=null` |
| 1 | RealDamage | 获胜时为剩余秒数乘 `1,000,000,000` 后转 `long`，其他结果使用实战伤害 |
| 2 | ExpectedDamage | 当前 .NET 类型退化为实战伤害；获胜评分同目标 1 |
| 3 | PhysicalAttack | 战斗结束时攻击力与 TP 上升门槛相关评分 |
| 4 | MagicAttack | 战斗结束时魔法攻击力与 TP 上升门槛相关评分 |

目标 3、4 的计算先累加攻击力，所有参与角色的 `EnergyRecoveryRate` 均达到 `OptimizationTpDemand` 时才计入攻击力部分，随后加上 TP 上升总和。物攻路径遍历我方单位列表，魔攻路径取前五名。

语义 UB 优化还需传入 `IsGuildBattle=true`、`IsSemanMode=true` 和五行 `SemanUBExecTimeList`，并检查响应中的 `SemanUBNum`。server 为每项请求返回该场的适应度。

### 5.3 五种子均值

当前 Unity 脱机 GA 选择「期望伤害」目标时，客户端一次提交五个不同的种子，使用目标 `1` 获取实战评分，再对五场的 `OptimizationFitness` 求算术平均。相同轮次的候选共用一组种子。外部调用方可以按同样方式接入。

以下 Python 示例从第 2 节的 `battle.json` 读取完整配置，使用 Python 标准库发送五个请求。全部样本成功，且返回的种子与请求一致后，示例会输出平均适应度。计算平均纯伤害时，读取 `OnceResult.currentDamage`。

```python
import copy
import json
from pathlib import Path
from urllib.request import Request, urlopen

base = json.loads(Path("battle.json").read_text(encoding="utf-8"))[0]
seeds = [666, 667, 668, 669, 670]
batch = []
for seed in seeds:
    sample = copy.deepcopy(base)
    sample["RepeatCount"] = 1
    sample["OptimizationTarget"] = 1
    sample["NeedOnceResult"] = True
    sample.setdefault("Settings", {})["UseFixedRandomSeed"] = True
    sample["Settings"]["RandomSeed"] = seed
    batch.append(sample)

request = Request(
    "http://127.0.0.1:8080/simulate",
    data=json.dumps(batch).encode("utf-8"),
    headers={"Content-Type": "application/json"},
)
with urlopen(request, timeout=300) as response:
    results = json.load(response)
if len(results) != len(seeds):
    raise RuntimeError("结果数量与样本数量不符")
for expected_seed, result in zip(seeds, results):
    if not result.get("Success"):
        raise RuntimeError(result.get("Message", "模拟失败"))
    if result["CurrentSeed"] != expected_seed:
        raise RuntimeError("随机种子与请求不符")
    if result.get("OptimizationFitness") is None:
        raise RuntimeError("server 未返回优化适应度")

fitness_mean = sum(r["OptimizationFitness"] for r in results) / len(results)
damage_mean = sum(r["OnceResult"]["currentDamage"] for r in results) / len(results)
print("平均适应度", fitness_mean)
print("平均实战伤害", damage_mean)
```

自然随机样本的强制暴击、必中及特殊随机覆盖设置应关闭。外部 GA 应保留平均值的小数，并取各场 `SemanUBNum` 中每个角色的最大值，为该角色保留足够长的 UB 基因。五场的原始结果由调用方保存。

## 6. POST /arena/suggest

该接口使用 `Models/Arena` 中的 ONNX 模型，根据防守阵容和可用角色池预测攻击队伍，输入只包含角色 ID。

```bash
curl --fail-with-body \
  -H 'Content-Type: application/json' \
  --data-binary '{"Defense":[100101,100201,100301,100401,100501],"Roster":[100101,100201,100301,100401,100501,100601,100701,100801],"Candidates":64,"Count":3,"Seed":42}' \
  http://127.0.0.1:8080/arena/suggest
```

| 请求字段 | 类型 | 默认值 | 约束 |
| --- | --- | --- | --- |
| `Defense` | `int[]` | 必填 | 恰好五个不同且模型已知的角色 |
| `Roster` | `int[]` | 必填 | 最多 4096 项，去重并过滤未知角色后至少五人 |
| `Candidates` | `int` | 256 | 1–512，候选数量上限 |
| `Count` | `int` | 8 | 1–20，返回队伍数量上限 |
| `Seed` | `int` | 42 | 候选采样种子 |

| 响应字段 | 含义 |
| --- | --- |
| `Teams` | 按 `Score` 降序排列的建议队伍 |
| `Teams[].Team` | 五个不同角色 ID，来自可用角色池 |
| `Teams[].PredictedWinRate` | 胜利四分类概率之和，范围 `0..1` |
| `Teams[].Score` | 六分类概率按模型效用权重加权后的排序分数 |
| `Teams[].Probabilities` | 顺序为 `win_0_15`、`win_15_30`、`win_30_60`、`win_60_90`、`loss`、`timeout` 的六项概率 |
| `GeneratedCandidates` | 实际生成的不同候选队伍数 |
| `ElapsedMilliseconds` | 本次候选生成及评分耗时，单位毫秒 |
| `ModelVersion` | 当前加载的模型版本 |
| `UnknownRoster` | 可用角色池中被过滤的未知角色 ID |

可用组合较少时，候选和返回队伍数量可能低于请求上限。模型清单中的 `unit_ids` 列出支持的角色。可将预测队伍作为 `/simulate` 的输入，补全养成后再模拟验证。

模型输入与输出见 [ArenaRecommendationEngine.cs](ArenaRecommendationEngine.cs)，角色词表见 [Models/Arena/manifest.json](Models/Arena/manifest.json)。

## 7. 错误处理

| 场景 | HTTP/响应 | 调用方处理 |
| --- | --- | --- |
| 正常模拟完成 | 200，结果数组 | 检查每项 `Success` |
| 某场模拟失败 | 通常仍是 200，该项 `Success=false` | 记录该项 `Message`；其余项独立处理 |
| JSON、类型或请求体形状错误 | 通常 400，ASP.NET 错误对象 | 检查顶层数组和字段类型 |
| 建议请求无效或角色不支持 | 400，`{"message": "..."}` | 修正防守阵容、角色池或数量 |
| 建议模型无法加载 | 503，`{"message": "..."}` | 核对 ONNX 文件、manifest 和运行依赖 |
| 未处理的初始化或服务端异常 | 5xx | 保存状态码与响应正文，结合 server 日志定位 |

客户端应先判断 HTTP 状态，再按对应端点解析业务响应。某个候选有一场模拟失败时，整组评分也应判为失败，避免将失败项按零伤害计入均值。

模拟请求由进程内共享的 worker 队列调度执行。客户端停止等待或断开 HTTP 连接后，已经派发的模拟仍可能继续运行。重发请求会产生新的计算任务，调用方可按自己生成的请求 ID 关联日志。

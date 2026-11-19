# PCRCalculator 仓库工作说明

## 从哪里开始

| 要改的功能 | 主要入口 |
|---|---|
| 战斗生命周期、伤害、状态和 TP | `Assets/Scrips/Elements/Battle/`、`UnitCtrl.cs`、`BattleActions/`（后两项在 `Assets/Scrips/Elements/` 下） |
| 角色面板、EX 装备与属性 | `Assets/Scrips/PCRCalcularor/UnitData.cs`、`ChoosePannelManager.cs`、`Assets/Scrips/Elements/ExEquips/` |
| Boss、队伍保存、关卡简称和头像标题 | `Assets/Scrips/PCRCalcularor/Guild/GuildManager.cs`、`Assets/Scenes/GuildScene.unity` |
| 遗物 UI 和战斗效果 | `Assets/Scrips/PCRCalcularor/RelicManager.cs`、同目录的 `LabyrinthRelic*.cs`、`Assets/Scrips/Elements/LabyrinthRelicTempData.cs` |
| 数据表、加载、缓存与更新 | `Assets/Scrips/SQL/`、`Assets/Scrips/PCRCalcularor/DatabaseCacheContract.cs`、同目录的 `DataSources/` 和 `UpdateTool/` |
| 远程模拟 | `RemoteSimulator.cs`、`Elements/Battle/BattleSimulationServer.cs` 及 [服务端接口](PCRCalculatorServer/API.md) |

## 修改与验证

- 战斗代码由 Unity 和 .NET 服务端共用。修改共享字段或玩法时，检查客户端组队、本地模拟、GA 和远程请求是否传入相同的波次、玩法参数及遗物。改动数据库缓存结构时，检查旧缓存失效和旧数据库缺表时的加载行为。
- 关卡用已保存的 `waveGroupId` 恢复。下拉框与主界面标题共用期数和关卡格式。修改界面后，在实际场景走一次选择、确认、切换队伍和重新载入，并检查收起与展开时文字是否完整。
- 回放还原数据库、RANK 奖励、技能版本、角色装备、骑士强化和起始 HP。同一逻辑帧内保留玩家、Boss 和召唤物的 UB 顺序。表内期望伤害、单种子总伤和逐击吻合分别记录。


- 服务端改动可用 `dotnet build PCRCalculatorServer/PCRCalculatorServer.csproj -c Release` 检查。Windows 玩家包使用 `.\build.ps1 -Target windows -Mono`，Unity 版本以 `ProjectSettings/ProjectVersion.txt` 为准。图标、布局和场景绑定需要在 Unity 中核对。
- 本地测试输入可能放在 `测试/`、`新轴/` 或仓库外，临时比对通常在 `.tmp_scene/`，构建产物在 `Builds/`。提交前逐项核对暂存区，将代码、文档和可复用脚本与本机数据库、个人存档、原始回放及生成物区分开。

# MZQ (PCRCalculator)

PCRCalculator 是《公主连结 Re:Dive》的战斗轴计算器。Unity 客户端用于配置队伍、选择 Boss、安排 UB 和查看战斗日志，.NET 8 服务端使用同一套战斗代码运行模拟。

## 开始使用

1. 解压 Windows 客户端，保留程序旁的数据目录。打开「更新数据」，选择日服数据库来源并填写版本号。保存设置后，重启时会按选定来源和版本准备数据库。「下载最新数据库」单独执行。版本输入框右上角的书卷可查看会战与数据库版本的对照。
2. 在主界面选择 Boss 和队伍。讨魔战线按期数、难度和战目选择，例如 `第9期 EX4`。难度依次为 N、H、VH、EX。挑战任务按期数和层数选择。确认后，Boss 头像上方显示当前关卡。
3. 讨魔遗物从 **Boss 头像右侧的「遗物」** 打开。点卡片可选择或取消，最多五件。点击「强化 +3」等系列按钮会换上当期该系列的三星 +3 遗物，点击「保存」写入当前队伍。讨魔和黎明界各自保存遗物配置。
4. 设置 UB 轴后运行战斗。复现同一场实战时使用回放的实际种子、起始 HP 和 UB 顺序。战斗 Debug 窗口可输入页码后按回车跳转。

## 从源码构建

Windows 客户端使用项目指定的 Unity 编辑器和 Windows Build Support，本次验证版本为 `6000.0.58f2`。双击仓库根目录的 `build.bat` 即可构建 Windows Mono 包，也可从终端运行。服务端使用 .NET 8 SDK。

```powershell
.\build.bat
dotnet build PCRCalculatorServer/PCRCalculatorServer.csproj -c Release
```

客户端压缩包在 `Builds/`，Unity 构建日志在 `Builds/unity-build.log`。服务端运行需要对应版本的数据库和资源，启动参数、模拟请求及逐击记录字段见 [HTTP API 文档](PCRCalculatorServer/API.md)。

## 战斗核对与维护

本次逐帧核定2025年12月到2026年11月一年会战、深域10图、部分深渊讨伐战、讨魔战线、挑战任务等近200份时间轴,但仍无法保证所有战斗效果都与真实游戏一致，有问题请提ISSUE或PR。后续开发入口和复验步骤见 [AGENTS.md](AGENTS.md)。

## License

采用 [MZQ Noncommercial and Source-Sharing License 1.0](LICENSE)：允许非商业学习、研究及二次开发，禁止商业使用。本体修改须公开源码；仅通过 API 开发的独立项目无需向公众公开源码，但对外提供服务时须向实际用户提供对应源码。具体以 LICENSE 为准。

## 数据源
数据源采用

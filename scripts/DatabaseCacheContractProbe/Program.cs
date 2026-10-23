using Elements;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Mono.Data.Sqlite;
using PCRApi.CN;
using PCRCaculator;
using PCRCaculator.Guild;
using PCRCaculator.SQL;

static void Require(bool condition, string message)
{
    if (!condition)
        throw new InvalidOperationException(message);
}

static unit_promotion CreatePromotion(int rank, int equipmentCount, int unitId = 100101)
{
    int[] slots = Enumerable.Repeat(999999, 6).ToArray();
    int[] releaseOrder = [1, 3, 5, 4, 2, 0];
    foreach (int slot in releaseOrder.Take(equipmentCount))
        slots[slot] = 10010001 + slot;
    return new unit_promotion
    {
        unit_id = unitId,
        promotion_level = rank,
        equip_slot_1 = slots[0], equip_slot_2 = slots[1], equip_slot_3 = slots[2],
        equip_slot_4 = slots[3], equip_slot_5 = slots[4], equip_slot_6 = slots[5],
    };
}

foreach (var (rank, count, expected) in new[]
{
    (8, 5, 10), (9, 3, 11), (9, 5, 12),
    (10, 3, 13), (10, 4, 14), (10, 5, 15),
    (38, 5, 99), (42, 3, 109), (42, 4, 110), (42, 5, 111),
})
{
    int actual = SQLData.CalculatePlayerProcessLimit([CreatePromotion(rank, count)]);
    Require(actual == expected, $"R{rank}-{count}: expected process {expected}, got {actual}");
    var settings = new PlayerSetting();
    settings.ApplyPlayerProcessLimit(actual);
    Require(settings.GetMaxRank() == rank, $"Progression encoding changed rank for {expected}");
    Require(settings.GetEquipmentIsAble().Count(enabled => enabled) == count,
        $"Progression encoding changed equipment count for {expected}");
}
Require(SQLData.CalculatePlayerProcessLimit(
    [CreatePromotion(42, 4), CreatePromotion(99, 5, 600101), CreatePromotion(43, 0), CreatePromotion(38, 5)]) == 110,
    "Progression limit included enemy rows or unreleased ranks");
Require(SQLData.CalculatePlayerProcessLimit([]) == 0, "Missing progression data must not invent a limit");
var savedSettings = new PlayerSetting { playerProcess = 111 };
savedSettings.ApplyPlayerProcessLimit(99);
Require(savedSettings.playerProcess == 99, "Older database did not clamp saved progression");
savedSettings.playerProcess = 80;
savedSettings.ApplyPlayerProcessLimit(110);
Require(savedSettings.playerProcess == 80, "Valid lower progression was overwritten");
savedSettings.ApplyPlayerProcessLimit(0);
Require(savedSettings.playerProcess == 80, "Progression changed before database was loaded");
Require(SQLData.CalculatePlayerLevelLimit(
    [new experience_team { team_level = 359 }, new experience_team { team_level = 1 }]) == 358,
    "Team experience level 359 must configure player level 358");
Require(SQLData.CalculatePlayerLevelLimit([new experience_team { team_level = 392 }]) == 391,
    "Team experience level 392 must configure player level 391");
Require(SQLData.CalculatePlayerLevelLimit([]) == 0, "Missing experience data must not invent a level limit");
var levelSettings = new PlayerSetting { playerLevel = 300 };
Require(!levelSettings.enableGuildLevelBonus, "Guild level bonus must default to disabled");
levelSettings.ApplyPlayerLevelLimit(358, setToMaximum: true);
Require(levelSettings.playerLevel == 358, "Database loading did not replace the saved player level");
foreach (var (mode, connectRank, expected) in new[]
{
    (0, 0, 358), (0, 8, 358), (0, 9, 358), (0, 10, 358),
    (1, 0, 368), (1, 8, 368), (1, 9, 373), (1, 10, 373),
    (2, 0, 368), (2, 8, 368), (2, 9, 373),
})
{
    Require(levelSettings.GetLevelMax(mode, connectRank) == expected,
        $"Wrong level limit for battle mode {mode}, connect rank {connectRank}");
}
levelSettings.enableGuildLevelBonus = true;
foreach (var (mode, connectRank, expected) in new[]
{
    (0, 0, 368), (0, 8, 368), (0, 9, 373), (0, 10, 373),
    (1, 8, 368), (1, 9, 373), (2, 8, 368), (2, 9, 373),
})
{
    Require(levelSettings.GetLevelMax(mode, connectRank) == expected,
        $"Wrong enabled guild bonus for battle mode {mode}, connect rank {connectRank}");
}
levelSettings.enableGuildLevelBonus = false;
Require(levelSettings.GetLevelMax(0, 9) == 358, "Disabling guild level bonus did not restore the guild limit");
var connectedUnit = new PCRCaculator.UnitData { level = 373, skillLevel = [373, 373, 373, 0] };
connectedUnit.ClampLevel(levelSettings.GetLevelMax(2, 8));
Require(connectedUnit.level == 368 && connectedUnit.skillLevel.SequenceEqual([368, 368, 368, 0]),
    "Connection rank decrease did not clamp character and skill levels");
levelSettings.ApplyPlayerLevelLimit(391, setToMaximum: true);
Require(levelSettings.playerLevel == 391, "Database update did not refresh the player level");
levelSettings.ApplyPlayerLevelLimit(358);
Require(levelSettings.playerLevel == 358, "Database level limit did not clamp excessive levels");
levelSettings.playerLevel = 300;
levelSettings.ApplyPlayerLevelLimit(358);
Require(levelSettings.playerLevel == 300, "Manually selected lower player level was overwritten");
levelSettings.ApplyPlayerLevelLimit(0, setToMaximum: true);
Require(levelSettings.playerLevel == 300, "Player level changed before database was loaded");

Require(SQLData.CalculateUniqueEqLevelLimit(
    [new unique_equipment_enhance_data { equip_slot = 1, enhance_level = 360 },
     new unique_equipment_enhance_data { equip_slot = 1, enhance_level = 370 },
     new unique_equipment_enhance_data { equip_slot = 2, enhance_level = 999 }]) == 370,
    "Unique equipment 1 limit included another slot or subtracted a level");
Require(SQLData.CalculateUniqueEqLevelLimit([]) == 0,
    "Missing unique equipment enhancement data must not invent a level limit");
Require(SQLData.CalculateUniqueEqLevelLimit(
    [new unique_equipment_enhance_data { equip_slot = 2, enhance_level = 5 }]) == 0,
    "Unique equipment 2 data must not define the unique equipment 1 limit");
var uniqueEqSettings = new PlayerSetting();
uniqueEqSettings.ApplyUniqueEqLevelLimit(360);
Require(uniqueEqSettings.maxUniqueEqLv == 360, "New settings did not use the database unique equipment limit");
uniqueEqSettings.ApplyUniqueEqLevelLimit(370, setToMaximum: true);
Require(uniqueEqSettings.maxUniqueEqLv == 370, "Database update did not refresh the unique equipment limit");
uniqueEqSettings.ApplyUniqueEqLevelLimit(360);
Require(uniqueEqSettings.maxUniqueEqLv == 360, "Older database did not clamp the unique equipment limit");
uniqueEqSettings.maxUniqueEqLv = 300;
uniqueEqSettings.ApplyUniqueEqLevelLimit(370);
Require(uniqueEqSettings.maxUniqueEqLv == 300, "Manually selected lower unique equipment level was overwritten");
uniqueEqSettings.ApplyUniqueEqLevelLimit(0, setToMaximum: true);
Require(uniqueEqSettings.maxUniqueEqLv == 300, "Unique equipment level changed before database was loaded");
uniqueEqSettings.ApplyUniqueEqLevelLimit(360, setToMaximum: true);
Require(uniqueEqSettings.maxUniqueEqLv == 360, "Reloading did not restore the database unique equipment limit");

Require(JsonConvert.DeserializeObject<PlayerSetting>("{}").playerProcess == 0,
    "Missing saved progression must default to the database limit after loading");
Require(JsonConvert.DeserializeObject<PlayerSetting>("{}").maxUniqueEqLv == 0,
    "Missing saved unique equipment level must select the database maximum after loading");
Require(!JsonConvert.DeserializeObject<PlayerSetting>("{}").enableGuildLevelBonus,
    "Existing settings must default to disabled guild level bonus");
var enabledGuildSettings = JsonConvert.DeserializeObject<PlayerSetting>(
    JsonConvert.SerializeObject(new PlayerSetting { enableGuildLevelBonus = true }));
Require(enabledGuildSettings.enableGuildLevelBonus, "Saved guild level bonus was not restored");

static MainManager.DatabaseCache CreateValidCache(string fingerprint)
{
    var enemy = new EnemyData
    {
        detailData = new EnemyDetailData(),
        baseData = new BaseData(),
        main_skill_lvs = Enumerable.Repeat(0, 10).ToList(),
        ex_skill_lvs = Enumerable.Repeat(0, 5).ToList(),
        skillData = new EnemySkillData
        {
            raw_data = new unit_skill_data(),
            MainSkills = Enumerable.Repeat(0, 10).ToList(),
            enemyAttackPatterns =
            [
                new UnitAttackPattern { loop_start = 1, loop_end = 1 },
            ],
        },
    };
    return new MainManager.DatabaseCache
    {
        cacheSchemaVersion = DatabaseCacheContract.CurrentSchemaVersion,
        playerProcessLimit = 99,
        playerLevelLimit = 358,
        uniqueEqLevelLimit = 360,
        cacheBuildComplete = true,
        databaseSourceFingerprint = fingerprint,
        unitRarityDic =
        {
            [1] = new UnitRarityData
            {
                detailData = new UnitDetailData(),
                rankData = new UnitRankData([new int[6]], [new BaseData()]),
                skillData = new UnitSkillData
                {
                    loopStart = 1,
                    loopEnd = 1,
                    atkPatterns = new int[20],
                },
                raritydatas = [new BaseData()],
                ratitydatas_rate = [new BaseData()],
            },
        },
        ailmentDataDic = { [1] = new ailment_data { ailment_id = 1 } },
        resistDataDic = { [1] = new int[1] },
        skillDataDic = { [1] = new SkillData() },
        skillActionDic = { [1] = new SkillAction { details = new int[3], values = new double[7] } },
        allUnitAttackPatternDic =
        {
            [1] = new UnitAttackPattern { loop_start = 1, loop_end = 1 },
        },
        enemyDataDic = { [1] = enemy },
        guildEnemyDatas = { [1] = new GuildEnemyData { enemyIds = [[1, 1, 1, 1, 1]] } },
        talentQuestWaveGroupDataDic =
        {
            [1] = new WaveGroupDataBase { id = 1, wave_group_id = 810010011, enemy_ids = [42] },
        },
        talentQuestEnemyParameterDic = { [42] = enemy },
        masterUnitSkillDataRf = new MasterUnitSkillDataRf
        {
            dict = new Dictionary<int, List<MasterUnitSkillDataRf.UnitSkillDataRf>>
            {
                [1] = [],
            },
        },
        unitExEquips =
        {
            [1] =
            [
                new Dictionary<int, ex_equipment_data>(),
                new Dictionary<int, ex_equipment_data>(),
                new Dictionary<int, ex_equipment_data>(),
            ],
        },
    };
}

var tempDirectory = Path.Combine(Path.GetTempPath(), $"pcr-cache-contract-{Guid.NewGuid():N}");
Directory.CreateDirectory(tempDirectory);
try
{
    var sourceA = Path.Combine(tempDirectory, "source-a.db");
    var sourceB = Path.Combine(tempDirectory, "source-b.db");
    File.WriteAllText(sourceA, "database-A");
    File.WriteAllText(sourceB, "database-B");

    Require(DatabaseCacheContract.TryComputeSourceFingerprint(
        [new DatabaseSourceDescriptor("character", "jp123", sourceA)],
        out var fingerprintA,
        out var fingerprintReasonA), fingerprintReasonA);
    Require(DatabaseCacheContract.TryComputeSourceFingerprint(
        [new DatabaseSourceDescriptor("character", "jp123", sourceB)],
        out var fingerprintB,
        out var fingerprintReasonB), fingerprintReasonB);
    Require(fingerprintA != fingerprintB, "相同版本号的不同 DB 内容产生了相同指纹");

    var validCache = CreateValidCache(fingerprintA);
    Require(DatabaseCacheContract.TryValidate(validCache, fingerprintA, out var validReason), validReason);
    var missingProgressionCache = CreateValidCache(fingerprintA);
    missingProgressionCache.playerProcessLimit = 0;
    Require(!DatabaseCacheContract.TryValidate(missingProgressionCache, fingerprintA, out _),
        "Cache missing JP progression limit was accepted");
    var missingLevelCache = CreateValidCache(fingerprintA);
    missingLevelCache.playerLevelLimit = 0;
    Require(!DatabaseCacheContract.TryValidate(missingLevelCache, fingerprintA, out _),
        "Cache missing JP player level limit was accepted");
    var missingUniqueEqLevelCache = CreateValidCache(fingerprintA);
    missingUniqueEqLevelCache.uniqueEqLevelLimit = 0;
    Require(!DatabaseCacheContract.TryValidate(missingUniqueEqLevelCache, fingerprintA, out _),
        "Cache missing JP unique equipment 1 level limit was accepted");

    var nullEntryCache = CreateValidCache(fingerprintA);
    nullEntryCache.unitRarityDic[1] = null;
    Require(!DatabaseCacheContract.TryValidate(nullEntryCache, fingerprintA, out var nullEntryReason),
        "含 null 核心条目的缓存未被拒绝");
    Require(nullEntryReason.Contains("unitRarityDic"), $"null 条目拒绝原因不明确: {nullEntryReason}");

    var invalidRankStructureCache = CreateValidCache(fingerprintA);
    invalidRankStructureCache.unitRarityDic[1].rankData.rankEquipments[0] = new int[5];
    Require(!DatabaseCacheContract.TryValidate(
            invalidRankStructureCache,
            fingerprintA,
            out var invalidRankStructureReason),
        "装备槽长度不足的角色缓存未被拒绝");
    Require(invalidRankStructureReason.Contains("unitRarityDic"),
        $"角色 rank 结构拒绝原因不明确: {invalidRankStructureReason}");

    var mismatchedRarityGrowthCache = CreateValidCache(fingerprintA);
    mismatchedRarityGrowthCache.unitRarityDic[1].raritydatas.Add(new BaseData());
    Require(!DatabaseCacheContract.TryValidate(
            mismatchedRarityGrowthCache,
            fingerprintA,
            out var mismatchedRarityGrowthReason),
        "星级基础值与成长值数量不一致的缓存未被拒绝");
    Require(mismatchedRarityGrowthReason.Contains("unitRarityDic"),
        $"星级成长结构拒绝原因不明确: {mismatchedRarityGrowthReason}");

    var invalidUnitSkillCache = CreateValidCache(fingerprintA);
    invalidUnitSkillCache.unitRarityDic[1].skillData.atkPatterns = null;
    Require(!DatabaseCacheContract.TryValidate(
            invalidUnitSkillCache,
            fingerprintA,
            out var invalidUnitSkillReason),
        "攻击循环缺失的角色缓存未被拒绝");
    Require(invalidUnitSkillReason.Contains("unitRarityDic"),
        $"角色技能结构拒绝原因不明确: {invalidUnitSkillReason}");

    var invalidAttackPatternCache = CreateValidCache(fingerprintA);
    invalidAttackPatternCache.allUnitAttackPatternDic[1].loop_start = 0;
    Require(!DatabaseCacheContract.TryValidate(
            invalidAttackPatternCache,
            fingerprintA,
            out var invalidAttackPatternReason),
        "循环边界越界的攻击模式缓存未被拒绝");
    Require(invalidAttackPatternReason.Contains("allUnitAttackPatternDic"),
        $"攻击模式结构拒绝原因不明确: {invalidAttackPatternReason}");

    var invalidEnemySkillCache = CreateValidCache(fingerprintA);
    invalidEnemySkillCache.enemyDataDic[1].skillData.raw_data = null;
    Require(!DatabaseCacheContract.TryValidate(
            invalidEnemySkillCache,
            fingerprintA,
            out var invalidEnemySkillReason),
        "技能原始数据缺失的敌人缓存未被拒绝");
    Require(invalidEnemySkillReason.Contains("enemyDataDic"),
        $"敌人技能结构拒绝原因不明确: {invalidEnemySkillReason}");

    var invalidRfCache = CreateValidCache(fingerprintA);
    invalidRfCache.masterUnitSkillDataRf.dict[1] = null;
    Require(!DatabaseCacheContract.TryValidate(invalidRfCache, fingerprintA, out var invalidRfReason),
        "RF 技能映射含 null value 的缓存未被拒绝");
    Require(invalidRfReason.Contains("masterUnitSkillDataRf"),
        $"RF 技能映射拒绝原因不明确: {invalidRfReason}");

    var invalidExEquipmentCache = CreateValidCache(fingerprintA);
    invalidExEquipmentCache.unitExEquips[1] = [new Dictionary<int, ex_equipment_data>()];
    Require(!DatabaseCacheContract.TryValidate(
            invalidExEquipmentCache,
            fingerprintA,
            out var invalidExEquipmentReason),
        "EX 装备槽长度不足的缓存未被拒绝");
    Require(invalidExEquipmentReason.Contains("unitExEquips"),
        $"EX 装备结构拒绝原因不明确: {invalidExEquipmentReason}");

    var missingRelicExecutionCache = CreateValidCache(fingerprintA);
    missingRelicExecutionCache.RelicSkillExecTimeDic = null;
    Require(!DatabaseCacheContract.TryValidate(
            missingRelicExecutionCache,
            fingerprintA,
            out var missingRelicExecutionReason),
        "迷宫遗物执行表缺失的缓存未被拒绝");
    Require(missingRelicExecutionReason.Contains(nameof(MainManager.DatabaseCache.RelicSkillExecTimeDic)),
        $"迷宫遗物执行表拒绝原因不明确: {missingRelicExecutionReason}");

    var sharedPrimary = new object();
    var separateFactoryCalls = 0;
    var resolvedShared = DatabaseCacheContract.ResolveSharedDatabase(
        sharedPrimary,
        () =>
        {
            separateFactoryCalls++;
            return new object();
        },
        sharesPrimary: true);
    Require(ReferenceEquals(resolvedShared, sharedPrimary), "共用 manager 时没有复用已加载的 SQLData");
    Require(separateFactoryCalls == 0, "共用 manager 时仍创建了未初始化的 SQLData wrapper");

    var legacyJson = JObject.Parse(JsonConvert.SerializeObject(validCache));
    legacyJson.Remove(nameof(MainManager.DatabaseCache.cacheSchemaVersion));
    legacyJson.Remove(nameof(MainManager.DatabaseCache.cacheBuildComplete));
    legacyJson.Remove(nameof(MainManager.DatabaseCache.databaseSourceFingerprint));
    var legacyCache = JsonConvert.DeserializeObject<MainManager.DatabaseCache>(legacyJson.ToString());
    Require(!DatabaseCacheContract.TryValidate(legacyCache, fingerprintA, out var legacyReason),
        "缺少元数据的旧缓存未被拒绝");
    Require(legacyReason.Contains("schema"), $"旧缓存拒绝原因不明确: {legacyReason}");

    var versionTwoCache = CreateValidCache(fingerprintA);
    versionTwoCache.cacheSchemaVersion = 2;
    Require(!DatabaseCacheContract.TryValidate(versionTwoCache, fingerprintA, out var versionTwoReason),
        "缺少迷宫遗物执行表语义的 v2 缓存未被拒绝");
    Require(versionTwoReason.Contains("schema"), $"v2 缓存拒绝原因不明确: {versionTwoReason}");

    var partialCache = CreateValidCache(fingerprintA);
    partialCache.skillDataDic.Clear();
    Require(!DatabaseCacheContract.TryValidate(partialCache, fingerprintA, out var partialReason),
        "核心数据为空的缓存未被拒绝");
    Require(partialReason.Contains("skillDataDic"), $"残缺缓存拒绝原因不明确: {partialReason}");

    var badReferenceCache = CreateValidCache(fingerprintA);
    badReferenceCache.talentQuestEnemyParameterDic.Clear();
    badReferenceCache.talentQuestEnemyParameterDic[7] = new EnemyData { detailData = new EnemyDetailData() };
    Require(!DatabaseCacheContract.TryValidate(badReferenceCache, fingerprintA, out var referenceReason),
        "属性深域坏引用缓存未被拒绝");
    Require(referenceReason.Contains("42"), $"坏引用拒绝原因不明确: {referenceReason}");

    Require(!DatabaseCacheContract.TryValidate(validCache, fingerprintB, out var mismatchReason),
        "DB 内容指纹不匹配的缓存未被拒绝");
    Require(mismatchReason.Contains("指纹不匹配"), $"指纹拒绝原因不明确: {mismatchReason}");

    var cachePath = Path.Combine(tempDirectory, "cache_test.json");
    Require(DatabaseCacheContract.TryWriteAtomically(cachePath, "complete-cache", out var writeError), writeError);
    Require(File.ReadAllText(cachePath) == "complete-cache", "原子写入内容错误");
    Require(!DatabaseCacheContract.TryWriteAtomically(cachePath, "must-not-overwrite", out _),
        "原子写入覆盖了意外存在的缓存");
    Require(File.ReadAllText(cachePath) == "complete-cache", "写入失败后原缓存内容被修改");

    Require(DatabaseCacheContract.TryQuarantineRejectedCache(
        cachePath,
        out var quarantinedPath,
        out var quarantineError), quarantineError);
    Require(!File.Exists(cachePath), "被拒绝缓存仍留在原路径");
    Require(File.Exists(quarantinedPath), "被拒绝缓存没有保留副本");
    Require(File.ReadAllText(quarantinedPath) == "complete-cache", "被拒绝缓存的证据内容被修改");

    var continuationCalled = false;
    var propagated = false;
    try
    {
        await DatabaseCacheContract.RunAfterSuccessAsync(
            Task.FromException(new InvalidDataException("obfuscated database")),
            () => continuationCalled = true);
    }
    catch (InvalidDataException)
    {
        propagated = true;
    }
    Require(propagated, "数据库前置任务异常未向上传播");
    Require(!continuationCalled, "数据库前置任务异常后仍执行了缓存组装");

    var originalPersistentDataPath = ABExTool.persistentDataPath;
    var originalSql = SQLiteTool.sql;
    var invalidDataVersion = $"probe-obfuscated-{Guid.NewGuid():N}";
    var invalidDatabasePath = Path.Combine(tempDirectory, $"{invalidDataVersion}.db");
    File.WriteAllText(invalidDatabasePath, "not-a-sqlite-database");
    try
    {
        ABExTool.persistentDataPath = tempDirectory;
        SQLiteTool.sql = "SELECT 1;";
        var invalidDatabase = SQLData.OpenDB(new ProbeDatabaseManager(invalidDataVersion));
        var prepareFailed = false;
        try
        {
            invalidDatabase.Prepare();
        }
        catch (InvalidDataException exception)
        {
            prepareFailed = exception.InnerException != null;
        }
        finally
        {
            invalidDatabase.CloseDB();
        }

        Require(prepareFailed, "无法解析的 DB 没有以带原始原因的异常失败");
        Require(File.ReadAllText(invalidDatabasePath) == "not-a-sqlite-database",
            "解析失败修改或删除了来源 DB 证据");

        var missingCoreDataVersion = $"probe-missing-core-{Guid.NewGuid():N}";
        var missingCoreDatabasePath = Path.Combine(tempDirectory, $"{missingCoreDataVersion}.db");
        SqliteConnection.CreateFile(missingCoreDatabasePath);
        var missingCoreDatabase = SQLData.OpenDB(new ProbeDatabaseManager(missingCoreDataVersion));
        var parallelLoadFailed = false;
        try
        {
            missingCoreDatabase.Prepare();
            await missingCoreDatabase.ParallelGetAll();
        }
        catch (InvalidDataException exception)
        {
            parallelLoadFailed = exception.Message.Contains("table=");
        }
        finally
        {
            missingCoreDatabase.CloseDB();
        }

        Require(parallelLoadFailed, "ParallelGetAll 的核心表读取失败仍被吞掉");

        var stagedDataVersion = $"probe-staged-{Guid.NewGuid():N}";
        var stagedDatabasePath = Path.Combine(tempDirectory, $"{stagedDataVersion}.db");
        var stagedManager = new ProbeDatabaseManager(
            stagedDataVersion,
            () => System.Text.Encoding.UTF8.GetBytes("downloaded-obfuscated-database"));
        for (var attempt = 1; attempt <= 2; attempt++)
        {
            var stagedDatabase = SQLData.OpenDB(stagedManager);
            var stagedPrepareFailed = false;
            try
            {
                stagedDatabase.Prepare();
            }
            catch (InvalidDataException exception)
            {
                stagedPrepareFailed = exception.InnerException != null &&
                                      exception.Message.Contains(stagedDataVersion);
            }
            finally
            {
                stagedDatabase.CloseDB();
            }

            Require(stagedPrepareFailed,
                $"第 {attempt} 次下载后的反哈希失败没有保留原始异常上下文");
            Require(!File.Exists(stagedDatabasePath),
                $"第 {attempt} 次失败把残缺数据库发布到了正式来源路径");
            Require(stagedManager.ResolveCount == attempt,
                $"第 {attempt} 次启动没有重新下载数据库");
        }
    }
    finally
    {
        ABExTool.persistentDataPath = originalPersistentDataPath;
        SQLiteTool.sql = originalSql;
    }

    var resolved = GuildManager.TryResolveTalentQuestSelection(
        validCache.talentQuestWaveGroupDataDic,
        validCache.talentQuestEnemyParameterDic,
        810010011,
        out var enemyIds,
        out var resolveError);
    Require(resolved, resolveError);
    Require(enemyIds.SequenceEqual([42]), "属性深域有效选择解析错误");

    var previousSelection = new[] { 99 };
    var missingResolved = GuildManager.TryResolveTalentQuestSelection(
        validCache.talentQuestWaveGroupDataDic,
        new Dictionary<int, EnemyData>(),
        810010011,
        out var missingEnemyIds,
        out var missingError);
    Require(!missingResolved, "属性深域缺敌人数据时仍解析成功");
    Require(missingEnemyIds.Length == 0, "属性深域解析失败时返回了半成品敌人列表");
    Require(previousSelection.SequenceEqual([99]), "属性深域解析失败修改了既有选择");
    Require(missingError.Contains("42"), $"属性深域失败原因不明确: {missingError}");

    var realDatabaseResult = "SKIP";
    if (args.Length == 2 && args[0] == "--real-db")
    {
        var realDatabasePath = Path.GetFullPath(args[1]);
        Require(File.Exists(realDatabasePath), $"真实 DB 不存在: {realDatabasePath}");
        Require(Path.GetDirectoryName(realDatabasePath) == Directory.GetCurrentDirectory(),
            "--real-db 当前只接受仓库根目录 DB，避免改变 SQLiteTool 的来源选择语义");

        var realDataVersion = Path.GetFileNameWithoutExtension(realDatabasePath);
        var savedSql = SQLiteTool.sql;
        try
        {
            SQLiteTool.sql = File.ReadAllText(Path.Combine("Assets", "StreamingAssets", "dbdiff.sql"));
            var realDatabase = SQLData.OpenDB(new ProbeDatabaseManager(realDataVersion));
            try
            {
                realDatabase.Prepare();
                await realDatabase.ParallelGetAll();
                Require(realDatabase.Dic1?.Count > 0, "真实 DB 缺 unit rarity 结果");
                Require(realDatabase.Dic2?.Count > 0, "真实 DB 缺 enemy 结果");
                Require(realDatabase.Dic3?.Count > 0, "真实 DB 缺 skill 结果");
                Require(realDatabase.Dic4?.Count > 0, "真实 DB 缺 skill action 结果");
                Require(realDatabase.Dic5?.Count > 0, "真实 DB 缺 attack pattern 结果");
                Require(realDatabase.Dic6?.Count > 0, "真实 DB 缺 guild enemy 结果");
                Require(realDatabase.TalentQuestWaveGroupDataDic?.Count > 0,
                    "真实 DB 缺 Talent Quest wave 结果");
                Require(realDatabase.TalentQuestEnemyParameterDic?.Count > 0,
                    "真实 DB 缺 Talent Quest enemy 结果");

                var realCache = new MainManager.DatabaseCache
                {
                    cacheSchemaVersion = DatabaseCacheContract.CurrentSchemaVersion,
                    playerProcessLimit = realDatabase.PlayerProcessLimit,
                    playerLevelLimit = realDatabase.PlayerLevelLimit,
                    uniqueEqLevelLimit = realDatabase.UniqueEqLevelLimit,
                    cacheBuildComplete = true,
                    databaseSourceFingerprint = "real-db-probe",
                    unitRarityDic = realDatabase.Dic1,
                    skillDataDic = realDatabase.Dic3,
                    skillActionDic = realDatabase.Dic4,
                    allUnitAttackPatternDic = realDatabase.Dic5,
                    enemyDataDic = realDatabase.Dic2,
                    guildEnemyDatas = realDatabase.Dic6,
                    talentQuestWaveGroupDataDic = realDatabase.TalentQuestWaveGroupDataDic,
                    talentQuestEnemyParameterDic = realDatabase.TalentQuestEnemyParameterDic,
                    masterUnitSkillDataRf = realDatabase.masterUnitSkillDataRf,
                    unitExEquips = realDatabase.unitExEquips,
                };
                Require(DatabaseCacheContract.TryValidate(realCache, "real-db-probe", out var realCacheReason),
                    $"真实 DB 结果未通过 cache 结构契约: {realCacheReason}");
                realDatabaseResult = "PASS";
            }
            finally
            {
                realDatabase.CloseDB();
            }
        }
        finally
        {
            SQLiteTool.sql = savedSql;
        }
    }

    Console.WriteLine(
        "database_cache_contract_probe=PASS " +
        "legacy_reject=PASS partial_reject=PASS null_entry_reject=PASS deep_structure_reject=PASS reference_reject=PASS " +
        "same_version_content_fingerprint=PASS shared_manager_alias=PASS " +
        "obfuscated_db_failure=PASS staged_unhash_retry=PASS parallel_read_failure=PASS fault_propagation=PASS " +
        $"atomic_publish=PASS quarantine=PASS talent_quest_state_guard=PASS real_db={realDatabaseResult}");
}
finally
{
    if (Directory.Exists(tempDirectory))
        Directory.Delete(tempDirectory, true);
}

sealed class ProbeDatabaseManager : IDataBaseManager
{
    private readonly Func<byte[]> resolver;

    public ProbeDatabaseManager(string dataVersion, Func<byte[]> resolver = null)
    {
        DataVer = dataVersion;
        this.resolver = resolver;
    }

    public int ResolveCount { get; private set; }
    public string DataVer { get; }

    public byte[] ResolveDatabase()
    {
        ResolveCount++;
        return resolver?.Invoke() ??
               throw new InvalidOperationException("probe source should already exist");
    }
}

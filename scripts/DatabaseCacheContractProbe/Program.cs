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

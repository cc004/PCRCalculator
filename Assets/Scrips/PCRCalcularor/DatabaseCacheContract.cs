using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PCRCaculator
{
    public readonly struct DatabaseSourceDescriptor
    {
        public DatabaseSourceDescriptor(string role, string dataVersion, string path)
        {
            Role = role;
            DataVersion = dataVersion;
            Path = path;
        }

        public string Role { get; }
        public string DataVersion { get; }
        public string Path { get; }
    }

    public static class DatabaseCacheContract
    {
        public const int CurrentSchemaVersion = 14;

        public static T ResolveSharedDatabase<T>(T primary, Func<T> createSeparate, bool sharesPrimary)
            where T : class
        {
            if (primary == null)
                throw new ArgumentNullException(nameof(primary));
            if (createSeparate == null)
                throw new ArgumentNullException(nameof(createSeparate));
            return sharesPrimary ? primary : createSeparate();
        }

        public static bool TryValidate(
            MainManager.DatabaseCache cache,
            string expectedSourceFingerprint,
            out string reason)
        {
            if (cache == null)
                return Reject("缓存内容为空", out reason);
            if (cache.cacheSchemaVersion != CurrentSchemaVersion)
                return Reject($"缓存 schema 不匹配: {cache.cacheSchemaVersion} != {CurrentSchemaVersion}", out reason);
            if (!cache.cacheBuildComplete)
                return Reject("缓存未标记为完整构建", out reason);
            if (cache.playerProcessLimit < PlayerSetting.MinimumPlayerProcess)
                return Reject("缓存缺少 BossVersionJP 的有效装备进度上限", out reason);
            if (cache.playerLevelLimit <= 0)
                return Reject("缓存缺少 BossVersionJP 的有效玩家等级上限", out reason);
            if (cache.uniqueEqLevelLimit <= 0)
                return Reject("缓存缺少 BossVersionJP 的有效专武 1 等级上限", out reason);
            if (string.IsNullOrWhiteSpace(cache.databaseSourceFingerprint))
                return Reject("缓存缺少数据库来源指纹", out reason);
            if (string.IsNullOrWhiteSpace(expectedSourceFingerprint))
                return Reject("当前数据库来源指纹不可用", out reason);
            if (!string.Equals(cache.databaseSourceFingerprint, expectedSourceFingerprint, StringComparison.Ordinal))
                return Reject("数据库来源指纹不匹配", out reason);

            var coreCounts = new[]
            {
                ("unitRarityDic", cache.unitRarityDic?.Count ?? 0),
                ("ailmentDataDic", cache.ailmentDataDic?.Count ?? 0),
                ("resistDataDic", cache.resistDataDic?.Count ?? 0),
                ("skillDataDic", cache.skillDataDic?.Count ?? 0),
                ("skillActionDic", cache.skillActionDic?.Count ?? 0),
                ("allUnitAttackPatternDic", cache.allUnitAttackPatternDic?.Count ?? 0),
                ("enemyDataDic", cache.enemyDataDic?.Count ?? 0),
                ("guildEnemyDatas", cache.guildEnemyDatas?.Count ?? 0),
                ("talentQuestWaveGroupDataDic", cache.talentQuestWaveGroupDataDic?.Count ?? 0),
                ("talentQuestEnemyParameterDic", cache.talentQuestEnemyParameterDic?.Count ?? 0),
            };
            var emptyCore = coreCounts.Where(item => item.Item2 <= 0).Select(item => item.Item1).ToArray();
            if (emptyCore.Length > 0)
                return Reject(
                    $"核心数据为空: {string.Join(", ", emptyCore)}; counts={FormatCounts(coreCounts)}",
                    out reason);

            if (cache.ailmentDataDic.Any(pair =>
                    pair.Value == null || pair.Key <= 0 || pair.Value.ailment_id != pair.Key))
                return Reject("ailmentDataDic 包含不完整条目", out reason);
            // The client iterates the actual resistance array length. Metadata may
            // contain later IDs; do not reject or extend a valid shorter source array.
            if (cache.resistDataDic.Any(pair => pair.Value == null || pair.Value.Length == 0))
                return Reject("resistDataDic 包含空抗性数组", out reason);

            if (cache.masterUnitSkillDataRf?.dict == null)
                return Reject("masterUnitSkillDataRf 结构缺失", out reason);
            if (cache.masterUnitSkillDataRf.dict.Any(pair =>
                    pair.Value == null || pair.Value.Any(item => item == null)))
                return Reject("masterUnitSkillDataRf 包含不完整条目", out reason);
            if (cache.unitExEquips == null)
                return Reject("unitExEquips 结构缺失", out reason);
            if (cache.unitExEquips.Any(pair =>
                    pair.Value == null || pair.Value.Length != 3 || pair.Value.Any(slot => slot == null)))
                return Reject("unitExEquips 包含不完整条目", out reason);

            var missingLabyrinthRelicContainers = new[]
            {
                (nameof(cache.LabyrinthRelicMarkEffectDic), cache.LabyrinthRelicMarkEffectDic != null),
                (nameof(cache.RelicActionExecTimeDic), cache.RelicActionExecTimeDic != null),
                (nameof(cache.RelicSkillExecTimeDic), cache.RelicSkillExecTimeDic != null),
            }.Where(item => !item.Item2).Select(item => item.Item1).ToArray();
            if (missingLabyrinthRelicContainers.Length > 0)
                return Reject(
                    $"迷宫遗物缓存结构缺失: {string.Join(", ", missingLabyrinthRelicContainers)}",
                    out reason);

            if (cache.unitRarityDic.Any(pair =>
                    pair.Value?.detailData == null ||
                    pair.Value.rankData == null ||
                    pair.Value.rankData.rankEquipments == null ||
                    pair.Value.rankData.rankEquipments.Count == 0 ||
                    pair.Value.rankData.rankEquipments.Any(row => row == null || row.Length != 6) ||
                    pair.Value.rankData.datas == null ||
                    pair.Value.rankData.datas.Count == 0 ||
                    pair.Value.rankData.datas.Any(data => data == null) ||
                    pair.Value.skillData == null ||
                    pair.Value.skillData.atkPatterns == null ||
                    pair.Value.skillData.atkPatterns.Length != 20 ||
                    pair.Value.skillData.loopStart < 1 ||
                    pair.Value.skillData.loopEnd < pair.Value.skillData.loopStart ||
                    pair.Value.skillData.loopEnd > pair.Value.skillData.atkPatterns.Length ||
                    pair.Value.raritydatas == null ||
                    pair.Value.raritydatas.Count == 0 ||
                    pair.Value.raritydatas.Any(data => data == null) ||
                    pair.Value.ratitydatas_rate == null ||
                    pair.Value.ratitydatas_rate.Count == 0 ||
                    pair.Value.raritydatas.Count != pair.Value.ratitydatas_rate.Count ||
                    pair.Value.ratitydatas_rate.Any(data => data == null)))
                return Reject("unitRarityDic 包含不完整条目", out reason);
            if (cache.skillDataDic.Any(pair =>
                    pair.Value?.skillactions == null || pair.Value.skillactions.Length != 20 ||
                    pair.Value.dependactions == null || pair.Value.dependactions.Length != 20))
                return Reject("skillDataDic 包含不完整条目", out reason);
            if (cache.skillActionDic.Any(pair =>
                    pair.Value?.details == null || pair.Value.details.Length != 3 ||
                    pair.Value.values == null || pair.Value.values.Length != 7))
                return Reject("skillActionDic 包含不完整条目", out reason);
            if (cache.allUnitAttackPatternDic.Any(pair => !IsAttackPatternComplete(pair.Value)))
                return Reject("allUnitAttackPatternDic 包含不完整条目", out reason);
            if (cache.enemyDataDic.Any(pair => !IsEnemyDataComplete(pair.Value)))
                return Reject("enemyDataDic 包含不完整条目", out reason);
            if (cache.towerEnemyParameterDic == null ||
                cache.towerEnemyParameterDic.Any(pair =>
                    pair.Key != pair.Value?.enemy_id || !IsEnemyDataComplete(pair.Value)))
                return Reject("towerEnemyParameterDic 包含不完整条目", out reason);
            if (cache.guildEnemyDatas.Any(pair =>
                    pair.Value?.enemyIds == null ||
                    pair.Value.enemyIds.Length == 0 ||
                    pair.Value.enemyIds.Any(row =>
                        row == null || row.Length != 5 || row.Any(enemyId => !cache.enemyDataDic.ContainsKey(enemyId)))))
                return Reject("guildEnemyDatas 包含不完整条目", out reason);

            foreach (var pair in cache.talentQuestWaveGroupDataDic)
            {
                var wave = pair.Value;
                if (wave?.enemy_ids == null || wave.enemy_ids.Count == 0)
                    return Reject($"属性深域波次 {pair.Key} 缺少敌人引用", out reason);

                var missingEnemyIds = wave.enemy_ids
                    .Where(enemyId =>
                        !cache.talentQuestEnemyParameterDic.TryGetValue(enemyId, out var enemy) ||
                        !IsEnemyDataComplete(enemy))
                    .Distinct()
                    .ToArray();
                if (missingEnemyIds.Length > 0)
                    return Reject(
                        $"属性深域波次 {pair.Key} 引用了缺失敌人: {string.Join(",", missingEnemyIds)}",
                        out reason);
            }

            reason = $"cache schema={cache.cacheSchemaVersion}; counts={FormatCounts(coreCounts)}";
            return true;
        }

        public static bool TryComputeSourceFingerprint(
            IEnumerable<DatabaseSourceDescriptor> sources,
            out string fingerprint,
            out string reason)
        {
            fingerprint = null;
            try
            {
                var orderedSources = sources?.OrderBy(source => source.Role, StringComparer.Ordinal).ToArray();
                if (orderedSources == null || orderedSources.Length == 0)
                    return Reject("没有可用于计算指纹的数据库来源", out reason);

                var identityBuilder = new StringBuilder();
                foreach (var source in orderedSources)
                {
                    if (string.IsNullOrWhiteSpace(source.Path) || !File.Exists(source.Path))
                        return Reject($"数据库来源不存在: role={source.Role}, dataVersion={source.DataVersion}", out reason);

                    var fileInfo = new FileInfo(source.Path);
                    using var stream = File.Open(source.Path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using var sha256 = SHA256.Create();
                    var fileHash = ToHex(sha256.ComputeHash(stream));
                    identityBuilder.Append(source.Role)
                        .Append('|').Append(source.DataVersion)
                        .Append('|').Append(fileInfo.Length)
                        .Append('|').Append(fileHash)
                        .Append('\n');
                }

                using var identitySha256 = SHA256.Create();
                fingerprint = ToHex(identitySha256.ComputeHash(Encoding.UTF8.GetBytes(identityBuilder.ToString())));
                reason = "数据库来源指纹计算完成";
                return true;
            }
            catch (Exception exception)
            {
                return Reject($"数据库来源指纹计算失败: {exception}", out reason);
            }
        }

        public static bool TryQuarantineRejectedCache(string path, out string quarantinedPath, out string error)
        {
            quarantinedPath = null;
            error = null;
            try
            {
                if (!File.Exists(path))
                    return true;

                var directory = Path.GetDirectoryName(path) ?? string.Empty;
                var fileName = Path.GetFileNameWithoutExtension(path);
                var extension = Path.GetExtension(path);
                quarantinedPath = Path.Combine(
                    directory,
                    $"{fileName}.invalid-{DateTime.UtcNow:yyyyMMddTHHmmssfffZ}-{Guid.NewGuid():N}{extension}");
                File.Move(path, quarantinedPath);
                return true;
            }
            catch (Exception exception)
            {
                error = exception.ToString();
                quarantinedPath = null;
                return false;
            }
        }

        public static bool TryWriteAtomically(string path, string contents, out string error)
        {
            error = null;
            var directory = Path.GetDirectoryName(path) ?? string.Empty;
            var tempPath = Path.Combine(directory, $".{Path.GetFileName(path)}.{Guid.NewGuid():N}.tmp");
            try
            {
                if (File.Exists(path))
                    throw new IOException($"拒绝覆盖意外出现的缓存文件: {path}");

                Directory.CreateDirectory(directory);
                File.WriteAllText(tempPath, contents);
                File.Move(tempPath, path);
                return true;
            }
            catch (Exception exception)
            {
                error = exception.ToString();
                return false;
            }
            finally
            {
                try
                {
                    if (File.Exists(tempPath))
                        File.Delete(tempPath);
                }
                catch
                {
                    // 临时文件清理失败不覆盖原始写入错误。
                }
            }
        }

        public static async Task RunAfterSuccessAsync(Task prerequisite, Action continuation, Action cleanup = null)
        {
            try
            {
                await prerequisite.ConfigureAwait(false);
                continuation();
            }
            finally
            {
                cleanup?.Invoke();
            }
        }

        private static string FormatCounts(IEnumerable<(string, int)> counts)
        {
            return string.Join(",", counts.Select(item => $"{item.Item1}={item.Item2}"));
        }

        private static bool IsEnemyDataComplete(EnemyData enemy)
        {
            return enemy?.detailData != null &&
                   enemy.baseData != null &&
                   enemy.skillData?.raw_data != null &&
                   enemy.main_skill_lvs?.Count == 10 &&
                   enemy.ex_skill_lvs?.Count == 5 &&
                   enemy.skillData.MainSkills?.Count == 10 &&
                   enemy.skillData.enemyAttackPatterns != null &&
                   enemy.skillData.enemyAttackPatterns.Count > 0 &&
                   enemy.skillData.enemyAttackPatterns.All(IsAttackPatternComplete);
        }

        private static bool IsAttackPatternComplete(UnitAttackPattern pattern)
        {
            return pattern?.atk_patterns != null &&
                   pattern.atk_patterns.Length == 20 &&
                   pattern.loop_start >= 1 &&
                   pattern.loop_end >= pattern.loop_start &&
                   pattern.loop_end <= pattern.atk_patterns.Length;
        }

        private static string ToHex(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLowerInvariant();
        }

        private static bool Reject(string rejectionReason, out string reason)
        {
            reason = rejectionReason;
            return false;
        }
    }
}

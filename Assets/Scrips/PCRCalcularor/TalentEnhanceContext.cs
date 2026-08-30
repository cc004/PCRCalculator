using Elements;
using PCRCaculator.Guild;

namespace PCRCaculator
{
    /// <summary>
    /// Keeps the attribute-enhancement editor focused on either the player team or
    /// the optional arena opponent configuration.
    /// </summary>
    public static class TalentEnhanceContext
    {
        private static bool isEditingArenaEnemy;
        public static bool IsEditingArenaEnemy => isEditingArenaEnemy && HasArenaEnemyConfiguration;

        private static GuildPlayerGroupData PlayerGroup => GuildManager.Instance?.SettingData?.GetCurrentPlayerGroup();

        public static TeamEnhanceConfiguration ActiveConfiguration
        {
            get
            {
                var playerGroup = PlayerGroup;
                if (playerGroup == null)
                    return null;

                return IsEditingArenaEnemy
                    ? playerGroup.arenaEnemyTeamEnhance
                    : playerGroup;
            }
        }

        public static TeamEnhanceParameter ActiveTeamEnhanceData => IsEditingArenaEnemy
            ? MainManager.Instance.ArenaEnemyTeamEnhanceData
            : MainManager.Instance.TeamEnhanceData;

        public static bool HasArenaEnemyConfiguration => PlayerGroup?.arenaEnemyTeamEnhance != null;

        public static void BeginArenaEnemyEditing()
        {
            var playerGroup = PlayerGroup;
            if (playerGroup == null)
                return;

            playerGroup.arenaEnemyTeamEnhance ??= playerGroup.Clone();
            isEditingArenaEnemy = true;
            MainManager.Instance.RefreshArenaEnemyTeamEnhanceData();
        }

        public static void EndArenaEnemyEditing()
        {
            isEditingArenaEnemy = false;
        }

        public static void ClearArenaEnemyConfiguration()
        {
            var playerGroup = PlayerGroup;
            if (playerGroup != null)
                playerGroup.arenaEnemyTeamEnhance = null;

            isEditingArenaEnemy = false;
            MainManager.Instance.RefreshArenaEnemyTeamEnhanceData();
        }
    }
}

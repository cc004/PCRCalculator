using System.Collections.Generic;


namespace Elements
{
    public class AllNetData
    {
        public NetHead data_headers;
        public ArenaStartReceiveParam data;
    }
    public class NetHead
    {
        public string sid;
        public string request_id;
        public string short_udid;
        public long viewer_id;
        public long servertime;
        public int result_code;
    }
    
    public class ArenaStartReceiveParam : BaseReceiveParam
    {
        public int MyViewerId { get; private set; }

        public int BattleViewerId { get; private set; }

        public int BattleId { get; private set; }

        public int BattleSpeed { get; private set; }

        public List<ArenaWaveInfo> WaveInfoList { get; private set; }
    }
    public class ArenaWaveInfo
    {
        public List<UnitData> UserArenaDeck { get; private set; }

        public List<UnitData> VsUserArenaDeck { get; private set; }

        public int Seed { get; private set; }

        public int BattleLogId { get; private set; }

        public int WaveNum { get; private set; }

    }
    public class BaseReceiveParam
    {
        public Notification Notification { get; private set; }
    }
    public class Notification
    {
        //public EquipDonateNotification EquipDonation { get; private set; }

        public List<MissionNotice> Mission;//{ get; private set; }
    }
    public class MissionNotice
    {
        public int MissionId;

        public int Count;

        public int MaxTimes;
    }
}
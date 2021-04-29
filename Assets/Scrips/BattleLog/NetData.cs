using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        public ObscuredInt MyViewerId { get; private set; }

        public ObscuredInt BattleViewerId { get; private set; }

        public ObscuredInt BattleId { get; private set; }

        public ObscuredInt BattleSpeed { get; private set; }

        public List<ArenaWaveInfo> WaveInfoList { get; private set; }
    }
    public class ArenaWaveInfo
    {
        public List<UnitData> UserArenaDeck { get; private set; }

        public List<UnitData> VsUserArenaDeck { get; private set; }

        public ObscuredInt Seed { get; private set; }

        public ObscuredInt BattleLogId { get; private set; }

        public ObscuredInt WaveNum { get; private set; }

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
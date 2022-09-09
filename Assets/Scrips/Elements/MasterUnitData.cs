// Decompiled with JetBrains decompiler
// Type: Elements.MasterUnitData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll


//using Sqlite3Plugin;

namespace Elements
{
  public class MasterUnitData : AbstractMasterData
  {
    /*public const string TABLE_NAME = "unit_data";
    private MasterUnitDatabase _db;
    private Dictionary<int, MasterUnitData.UnitData> _strictPrimaryKeyDictionary;
    private Dictionary<int, MasterUnitData.UnitData> _lazyPrimaryKeyDictionary;

    public static implicit operator Dictionary<int, MasterUnitData.UnitData>(
      MasterUnitData data) => data.dictionary;

    public bool ContainsKey(int id) => this.dictionary.ContainsKey(id);

    public MasterUnitData.UnitData this[int id] => this.Get(id);

    public void Add(MasterUnitData.UnitData unitData) => this.dictionary.Add((int) unitData.UnitId, unitData);

    public int Count => this.dictionary.Count;

    public Dictionary<int, MasterUnitData.UnitData> dictionary
    {
      get
      {
        if (this._strictPrimaryKeyDictionary == null)
          this._StrictLoadAllEntries();
        return this._strictPrimaryKeyDictionary;
      }
    }

    public MasterUnitData(MasterUnitDatabase db)
      : base((AbstractMasterDatabase) db)
    {
      this._lazyPrimaryKeyDictionary = new Dictionary<int, MasterUnitData.UnitData>();
      this._db = db;
    }

    public MasterUnitData.UnitData Get(int UnitId)
    {
      int key = UnitId;
      MasterUnitData.UnitData unitData = (MasterUnitData.UnitData) null;
      if (this._strictPrimaryKeyDictionary != null)
      {
        this._strictPrimaryKeyDictionary.TryGetValue(key, out unitData);
        return unitData;
      }
      if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out unitData))
      {
        unitData = this._db != null ? this._SelectOne(UnitId) : (MasterUnitData.UnitData) null;
        this._lazyPrimaryKeyDictionary[key] = unitData;
      }
      return unitData;
    }

    public bool HasKey(int UnitId) => this.Get(UnitId) != null;

    private MasterUnitData.UnitData _SelectOne(int UnitId)
    {
      NAOCHNBMGCB selectQueryUnitData = this._db.GetSelectQuery_UnitData();
      if (selectQueryUnitData == null)
        return (MasterUnitData.UnitData) null;
      if (!selectQueryUnitData.BindInt(1, UnitId))
        return (MasterUnitData.UnitData) null;
      MasterUnitData.UnitData unitData = (MasterUnitData.UnitData) null;
      if (selectQueryUnitData.Step())
        unitData = this._CreateCachedOrmByQueryResult((ODBKLOJPCHG) selectQueryUnitData);
      selectQueryUnitData.Reset();
      return unitData;
    }

    private MasterUnitData.UnitData _CreateCachedOrmByQueryResult(ODBKLOJPCHG query)
    {
      MasterUnitData.UnitData unitData = (MasterUnitData.UnitData) null;
      int UnitId = query.GetInt(0);
      int key = UnitId;
      if (this._strictPrimaryKeyDictionary != null)
      {
        this._strictPrimaryKeyDictionary.TryGetValue(key, out unitData);
        return unitData;
      }
      if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out unitData))
      {
        string text1 = query.GetText(1);
        string text2 = query.GetText(2);
        int PrefabId = query.GetInt(3);
        int IsLimited = query.GetInt(4);
        int Rarity = query.GetInt(5);
        int MotionType = query.GetInt(6);
        int SeType = query.GetInt(7);
        int MoveSpeed = query.GetInt(8);
        int SearchAreaWidth = query.GetInt(9);
        int AtkType = query.GetInt(10);
        double AtkCastTime = query.GetDouble(11);
        int CutIn1 = query.GetInt(12);
        int CutIn2 = query.GetInt(13);
        int cutin1_star6 = query.GetInt(14);
        int cutin2_star6 = query.GetInt(15);
        int GuildId = query.GetInt(16);
        int ExskillDisplay = query.GetInt(17);
        string text3 = query.GetText(18);
        int OnlyDispOwned = query.GetInt(19);
        string text4 = query.GetText(20);
        string text5 = query.GetText(21);
        unitData = new MasterUnitData.UnitData(UnitId, text1, text2, PrefabId, IsLimited, Rarity, MotionType, SeType, MoveSpeed, SearchAreaWidth, AtkType, AtkCastTime, CutIn1, CutIn2, cutin1_star6, cutin2_star6, GuildId, ExskillDisplay, text3, OnlyDispOwned, text4, text5);
        this._lazyPrimaryKeyDictionary.Add(key, unitData);
      }
      return unitData;
    }

    public override void Unload()
    {
      this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterUnitData.UnitData>) null;
      this._strictPrimaryKeyDictionary = (Dictionary<int, MasterUnitData.UnitData>) null;
    }

    private void _StrictLoadAllEntries()
    {
      Dictionary<int, MasterUnitData.UnitData> dictionary = new Dictionary<int, MasterUnitData.UnitData>();
      using (ODBKLOJPCHG allQueryUnitData = this._db.GetSelectAllQuery_UnitData())
      {
        while (allQueryUnitData.Step())
        {
          int key = allQueryUnitData.GetInt(0);
          dictionary.Add(key, this._CreateCachedOrmByQueryResult(allQueryUnitData));
        }
      }
      this._strictPrimaryKeyDictionary = dictionary;
      this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterUnitData.UnitData>) null;
    }
    */
    public class UnitData
    {
      protected int _UnitId;
      protected string _UnitName;
      protected string _Kana;
      protected int _PrefabId;
      protected int _IsLimited;
      protected int _Rarity;
      protected int _MotionType;
      protected int _SeType;
      protected int _MoveSpeed;
      protected int _SearchAreaWidth;
      protected int _AtkType;
      protected double _AtkCastTime;
      protected int _CutIn1;
      protected int _CutIn2;
      protected int _cutin1_star6;
      protected int _cutin2_star6;
      protected int _GuildId;
      protected int _ExskillDisplay;
      protected string _Comment;
      protected int _OnlyDispOwned;
      protected string _StartTime;
      protected string _EndTime;
            protected int _OriginalUnitId;

            public int VisualChangeFlag { get; private set; }

      /*public UnitData(MasterUnitEnemyData.UnitEnemyData unitEnemyData)
      {
        this._UnitId = unitEnemyData.unit_id;
        this._UnitName = unitEnemyData.unit_name;
        this._PrefabId = unitEnemyData.prefab_id;
        this._MotionType = unitEnemyData.motion_type;
        this._SeType = unitEnemyData.se_type;
        this._MoveSpeed = unitEnemyData.move_speed;
        this._SearchAreaWidth = unitEnemyData.search_area_width;
        this._AtkType = unitEnemyData.atk_type;
        this._AtkCastTime = unitEnemyData.normal_atk_cast_time;
        this._CutIn1 = unitEnemyData.cutin;
        this._cutin1_star6 = unitEnemyData.cutin_star6;
        this.VisualChangeFlag = (int) unitEnemyData.visual_change_flag;
        this._Comment = unitEnemyData.comment;
      }

      public string UnitNameOnly() => TextUtil.DeleteVersionText((string) this.UnitName);
      */
      public int UnitId => _UnitId;

      public string UnitName => _UnitName;

      public string Kana => _Kana;

      public int PrefabId => _PrefabId;

      public int IsLimited => _IsLimited;

      public int Rarity => _Rarity;

      public int MotionType => _MotionType;

      public int SeType => _SeType;

      public int MoveSpeed => _MoveSpeed;

      public int SearchAreaWidth => _SearchAreaWidth;

      public int AtkType => _AtkType;

      public double AtkCastTime => _AtkCastTime;

      public int CutIn1 => _CutIn1;

      public int CutIn2 => _CutIn2;

      public int cutin1_star6 => _cutin1_star6;

      public int cutin2_star6 => _cutin2_star6;

      public int GuildId => _GuildId;

      public int ExskillDisplay => _ExskillDisplay;

      public string Comment => _Comment;

      public int OnlyDispOwned => _OnlyDispOwned;

      public string StartTime => _StartTime;

      public string EndTime => _EndTime;
            public int OriginalUnitId => _OriginalUnitId;
            public UnitData(
        int UnitId = 0,
        string UnitName = "",
        string Kana = "",
        int PrefabId = 0,
        int IsLimited = 0,
        int Rarity = 0,
        int MotionType = 0,
        int SeType = 0,
        int MoveSpeed = 0,
        int SearchAreaWidth = 0,
        int AtkType = 0,
        double AtkCastTime = 0.0,
        int CutIn1 = 0,
        int CutIn2 = 0,
        int cutin1_star6 = 0,
        int cutin2_star6 = 0,
        int GuildId = 0,
        int ExskillDisplay = 0,
        string Comment = "",
        int OnlyDispOwned = 0,
        string StartTime = "",
        string EndTime = "",
        int originalUnitID = 0)
      {
        _UnitId = UnitId;
        _UnitName = UnitName;
        _Kana = Kana;
        _PrefabId = PrefabId;
        _IsLimited = IsLimited;
        _Rarity = Rarity;
        _MotionType = MotionType;
        _SeType = SeType;
        _MoveSpeed = MoveSpeed;
        _SearchAreaWidth = SearchAreaWidth;
        _AtkType = AtkType;
        _AtkCastTime = AtkCastTime;
        _CutIn1 = CutIn1;
        _CutIn2 = CutIn2;
        _cutin1_star6 = cutin1_star6;
        _cutin2_star6 = cutin2_star6;
        _GuildId = GuildId;
        _ExskillDisplay = ExskillDisplay;
        _Comment = Comment;
        _OnlyDispOwned = OnlyDispOwned;
        _StartTime = StartTime;
        _EndTime = EndTime;
                _OriginalUnitId = originalUnitID;
      }
    }
  }
}

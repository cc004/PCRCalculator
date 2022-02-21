// Decompiled with JetBrains decompiler
// Type: Elements.MasterResistData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

//using Sqlite3Plugin;

namespace Elements
{
  public class MasterResistData : AbstractMasterData
  {
    public const string TABLE_NAME = "resist_data";
    //private MasterEnemyDatabase _db;
    private Dictionary<int, ResistData> _lazyPrimaryKeyDictionary;

    /*public MasterResistData(MasterEnemyDatabase db)
      : base((AbstractMasterDatabase) db)
    {
      this._lazyPrimaryKeyDictionary = new Dictionary<int, MasterResistData.ResistData>();
      this._db = db;
    }*/

    /*public MasterResistData.ResistData Get(int resist_status_id)
    {
      int key = resist_status_id;
      MasterResistData.ResistData resistData = (MasterResistData.ResistData) null;
      if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out resistData))
      {
        resistData = this._db != null ? this._SelectOne(resist_status_id) : (MasterResistData.ResistData) null;
        this._lazyPrimaryKeyDictionary[key] = resistData;
      }
      return resistData;
    }*/

    //public bool HasKey(int resist_status_id) => this.Get(resist_status_id) != null;

    /*private MasterResistData.ResistData _SelectOne(int resist_status_id)
    {
      NAOCHNBMGCB selectQueryResistData = this._db.GetSelectQuery_ResistData();
      if (selectQueryResistData == null)
        return (MasterResistData.ResistData) null;
      if (!selectQueryResistData.BindInt(1, resist_status_id))
        return (MasterResistData.ResistData) null;
      MasterResistData.ResistData resistData = (MasterResistData.ResistData) null;
      if (selectQueryResistData.Step())
        resistData = this._CreateCachedOrmByQueryResult((ODBKLOJPCHG) selectQueryResistData);
      selectQueryResistData.Reset();
      return resistData;
    }*/

    /*private MasterResistData.ResistData _CreateCachedOrmByQueryResult(
      ODBKLOJPCHG query)
    {
      MasterResistData.ResistData resistData = (MasterResistData.ResistData) null;
      int resist_status_id = query.GetInt(0);
      int key = resist_status_id;
      if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out resistData))
      {
        int ailment_1 = query.GetInt(1);
        int ailment_2 = query.GetInt(2);
        int ailment_3 = query.GetInt(3);
        int ailment_4 = query.GetInt(4);
        int ailment_5 = query.GetInt(5);
        int ailment_6 = query.GetInt(6);
        int ailment_7 = query.GetInt(7);
        int ailment_8 = query.GetInt(8);
        int ailment_9 = query.GetInt(9);
        int ailment_10 = query.GetInt(10);
        int ailment_11 = query.GetInt(11);
        int ailment_12 = query.GetInt(12);
        int ailment_13 = query.GetInt(13);
        int ailment_14 = query.GetInt(14);
        int ailment_15 = query.GetInt(15);
        int ailment_16 = query.GetInt(16);
        int ailment_17 = query.GetInt(17);
        int ailment_18 = query.GetInt(18);
        int ailment_19 = query.GetInt(19);
        int ailment_20 = query.GetInt(20);
        int ailment_21 = query.GetInt(21);
        int ailment_22 = query.GetInt(22);
        int ailment_23 = query.GetInt(23);
        int ailment_24 = query.GetInt(24);
        int ailment_25 = query.GetInt(25);
        int ailment_26 = query.GetInt(26);
        int ailment_27 = query.GetInt(27);
        int ailment_28 = query.GetInt(28);
        int ailment_29 = query.GetInt(29);
        int ailment_30 = query.GetInt(30);
        int ailment_31 = query.GetInt(31);
        int ailment_32 = query.GetInt(32);
        int ailment_33 = query.GetInt(33);
        int ailment_34 = query.GetInt(34);
        int ailment_35 = query.GetInt(35);
        int ailment_36 = query.GetInt(36);
        int ailment_37 = query.GetInt(37);
        int ailment_38 = query.GetInt(38);
        int ailment_39 = query.GetInt(39);
        int ailment_40 = query.GetInt(40);
        int ailment_41 = query.GetInt(41);
        int ailment_42 = query.GetInt(42);
        int ailment_43 = query.GetInt(43);
        int ailment_44 = query.GetInt(44);
        int ailment_45 = query.GetInt(45);
        int ailment_46 = query.GetInt(46);
        int ailment_47 = query.GetInt(47);
        int ailment_48 = query.GetInt(48);
        int ailment_49 = query.GetInt(49);
        int ailment_50 = query.GetInt(50);
        resistData = new MasterResistData.ResistData(resist_status_id, ailment_1, ailment_2, ailment_3, ailment_4, ailment_5, ailment_6, ailment_7, ailment_8, ailment_9, ailment_10, ailment_11, ailment_12, ailment_13, ailment_14, ailment_15, ailment_16, ailment_17, ailment_18, ailment_19, ailment_20, ailment_21, ailment_22, ailment_23, ailment_24, ailment_25, ailment_26, ailment_27, ailment_28, ailment_29, ailment_30, ailment_31, ailment_32, ailment_33, ailment_34, ailment_35, ailment_36, ailment_37, ailment_38, ailment_39, ailment_40, ailment_41, ailment_42, ailment_43, ailment_44, ailment_45, ailment_46, ailment_47, ailment_48, ailment_49, ailment_50);
        this._lazyPrimaryKeyDictionary.Add(key, resistData);
      }
      return resistData;
    }*/

    //public override void Unload() => this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterResistData.ResistData>) null;

    public class ResistData
    {
      protected int _resist_status_id;
      protected int _ailment_1;
      protected int _ailment_2;
      protected int _ailment_3;
      protected int _ailment_4;
      protected int _ailment_5;
      protected int _ailment_6;
      protected int _ailment_7;
      protected int _ailment_8;
      protected int _ailment_9;
      protected int _ailment_10;
      protected int _ailment_11;
      protected int _ailment_12;
      protected int _ailment_13;
      protected int _ailment_14;
      protected int _ailment_15;
      protected int _ailment_16;
      protected int _ailment_17;
      protected int _ailment_18;
      protected int _ailment_19;
      protected int _ailment_20;
      protected int _ailment_21;
      protected int _ailment_22;
      protected int _ailment_23;
      protected int _ailment_24;
      protected int _ailment_25;
      protected int _ailment_26;
      protected int _ailment_27;
      protected int _ailment_28;
      protected int _ailment_29;
      protected int _ailment_30;
      protected int _ailment_31;
      protected int _ailment_32;
      protected int _ailment_33;
      protected int _ailment_34;
      protected int _ailment_35;
      protected int _ailment_36;
      protected int _ailment_37;
      protected int _ailment_38;
      protected int _ailment_39;
      protected int _ailment_40;
      protected int _ailment_41;
      protected int _ailment_42;
      protected int _ailment_43;
      protected int _ailment_44;
      protected int _ailment_45;
      protected int _ailment_46;
      protected int _ailment_47;
      protected int _ailment_48;
      protected int _ailment_49;
      protected int _ailment_50;

      public int resist_status_id => _resist_status_id;

      public int ailment_1 => _ailment_1;

      public int ailment_2 => _ailment_2;

      public int ailment_3 => _ailment_3;

      public int ailment_4 => _ailment_4;

      public int ailment_5 => _ailment_5;

      public int ailment_6 => _ailment_6;

      public int ailment_7 => _ailment_7;

      public int ailment_8 => _ailment_8;

      public int ailment_9 => _ailment_9;

      public int ailment_10 => _ailment_10;

      public int ailment_11 => _ailment_11;

      public int ailment_12 => _ailment_12;

      public int ailment_13 => _ailment_13;

      public int ailment_14 => _ailment_14;

      public int ailment_15 => _ailment_15;

      public int ailment_16 => _ailment_16;

      public int ailment_17 => _ailment_17;

      public int ailment_18 => _ailment_18;

      public int ailment_19 => _ailment_19;

      public int ailment_20 => _ailment_20;

      public int ailment_21 => _ailment_21;

      public int ailment_22 => _ailment_22;

      public int ailment_23 => _ailment_23;

      public int ailment_24 => _ailment_24;

      public int ailment_25 => _ailment_25;

      public int ailment_26 => _ailment_26;

      public int ailment_27 => _ailment_27;

      public int ailment_28 => _ailment_28;

      public int ailment_29 => _ailment_29;

      public int ailment_30 => _ailment_30;

      public int ailment_31 => _ailment_31;

      public int ailment_32 => _ailment_32;

      public int ailment_33 => _ailment_33;

      public int ailment_34 => _ailment_34;

      public int ailment_35 => _ailment_35;

      public int ailment_36 => _ailment_36;

      public int ailment_37 => _ailment_37;

      public int ailment_38 => _ailment_38;

      public int ailment_39 => _ailment_39;

      public int ailment_40 => _ailment_40;

      public int ailment_41 => _ailment_41;

      public int ailment_42 => _ailment_42;

      public int ailment_43 => _ailment_43;

      public int ailment_44 => _ailment_44;

      public int ailment_45 => _ailment_45;

      public int ailment_46 => _ailment_46;

      public int ailment_47 => _ailment_47;

      public int ailment_48 => _ailment_48;

      public int ailment_49 => _ailment_49;

      public int ailment_50 => _ailment_50;

      public ResistData(
        int resist_status_id = 0,
        int ailment_1 = 0,
        int ailment_2 = 0,
        int ailment_3 = 0,
        int ailment_4 = 0,
        int ailment_5 = 0,
        int ailment_6 = 0,
        int ailment_7 = 0,
        int ailment_8 = 0,
        int ailment_9 = 0,
        int ailment_10 = 0,
        int ailment_11 = 0,
        int ailment_12 = 0,
        int ailment_13 = 0,
        int ailment_14 = 0,
        int ailment_15 = 0,
        int ailment_16 = 0,
        int ailment_17 = 0,
        int ailment_18 = 0,
        int ailment_19 = 0,
        int ailment_20 = 0,
        int ailment_21 = 0,
        int ailment_22 = 0,
        int ailment_23 = 0,
        int ailment_24 = 0,
        int ailment_25 = 0,
        int ailment_26 = 0,
        int ailment_27 = 0,
        int ailment_28 = 0,
        int ailment_29 = 0,
        int ailment_30 = 0,
        int ailment_31 = 0,
        int ailment_32 = 0,
        int ailment_33 = 0,
        int ailment_34 = 0,
        int ailment_35 = 0,
        int ailment_36 = 0,
        int ailment_37 = 0,
        int ailment_38 = 0,
        int ailment_39 = 0,
        int ailment_40 = 0,
        int ailment_41 = 0,
        int ailment_42 = 0,
        int ailment_43 = 0,
        int ailment_44 = 0,
        int ailment_45 = 0,
        int ailment_46 = 0,
        int ailment_47 = 0,
        int ailment_48 = 0,
        int ailment_49 = 0,
        int ailment_50 = 0)
      {
        _resist_status_id = resist_status_id;
        _ailment_1 = ailment_1;
        _ailment_2 = ailment_2;
        _ailment_3 = ailment_3;
        _ailment_4 = ailment_4;
        _ailment_5 = ailment_5;
        _ailment_6 = ailment_6;
        _ailment_7 = ailment_7;
        _ailment_8 = ailment_8;
        _ailment_9 = ailment_9;
        _ailment_10 = ailment_10;
        _ailment_11 = ailment_11;
        _ailment_12 = ailment_12;
        _ailment_13 = ailment_13;
        _ailment_14 = ailment_14;
        _ailment_15 = ailment_15;
        _ailment_16 = ailment_16;
        _ailment_17 = ailment_17;
        _ailment_18 = ailment_18;
        _ailment_19 = ailment_19;
        _ailment_20 = ailment_20;
        _ailment_21 = ailment_21;
        _ailment_22 = ailment_22;
        _ailment_23 = ailment_23;
        _ailment_24 = ailment_24;
        _ailment_25 = ailment_25;
        _ailment_26 = ailment_26;
        _ailment_27 = ailment_27;
        _ailment_28 = ailment_28;
        _ailment_29 = ailment_29;
        _ailment_30 = ailment_30;
        _ailment_31 = ailment_31;
        _ailment_32 = ailment_32;
        _ailment_33 = ailment_33;
        _ailment_34 = ailment_34;
        _ailment_35 = ailment_35;
        _ailment_36 = ailment_36;
        _ailment_37 = ailment_37;
        _ailment_38 = ailment_38;
        _ailment_39 = ailment_39;
        _ailment_40 = ailment_40;
        _ailment_41 = ailment_41;
        _ailment_42 = ailment_42;
        _ailment_43 = ailment_43;
        _ailment_44 = ailment_44;
        _ailment_45 = ailment_45;
        _ailment_46 = ailment_46;
        _ailment_47 = ailment_47;
        _ailment_48 = ailment_48;
        _ailment_49 = ailment_49;
        _ailment_50 = ailment_50;
        SetUp();
      }

      public int[] Ailments { get; set; }

      public void SetUp()
      {
        Ailments = new int[50];
        Ailments[0] = ailment_1;
        Ailments[1] = ailment_2;
        Ailments[2] = ailment_3;
        Ailments[3] = ailment_4;
        Ailments[4] = ailment_5;
        Ailments[5] = ailment_6;
        Ailments[6] = ailment_7;
        Ailments[7] = ailment_8;
        Ailments[8] = ailment_9;
        Ailments[9] = ailment_10;
        Ailments[10] = ailment_11;
        Ailments[11] = ailment_12;
        Ailments[12] = ailment_13;
        Ailments[13] = ailment_14;
        Ailments[14] = ailment_15;
        Ailments[15] = ailment_16;
        Ailments[16] = ailment_17;
        Ailments[17] = ailment_18;
        Ailments[18] = ailment_19;
        Ailments[19] = ailment_20;
        Ailments[20] = ailment_21;
        Ailments[21] = ailment_22;
        Ailments[22] = ailment_23;
        Ailments[23] = ailment_24;
        Ailments[24] = ailment_25;
        Ailments[25] = ailment_26;
        Ailments[26] = ailment_27;
        Ailments[27] = ailment_28;
        Ailments[28] = ailment_29;
        Ailments[29] = ailment_30;
        Ailments[30] = ailment_31;
        Ailments[31] = ailment_32;
        Ailments[32] = ailment_33;
        Ailments[33] = ailment_34;
        Ailments[34] = ailment_35;
        Ailments[35] = ailment_36;
        Ailments[36] = ailment_37;
        Ailments[37] = ailment_38;
        Ailments[38] = ailment_39;
        Ailments[39] = ailment_40;
        Ailments[40] = ailment_41;
        Ailments[41] = ailment_42;
        Ailments[42] = ailment_43;
        Ailments[43] = ailment_44;
        Ailments[44] = ailment_45;
        Ailments[45] = ailment_46;
        Ailments[46] = ailment_47;
        Ailments[47] = ailment_48;
        Ailments[48] = ailment_49;
        Ailments[49] = ailment_50;
      }
    }
  }
}

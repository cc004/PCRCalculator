// Decompiled with JetBrains decompiler
// Type: Elements.MasterResistData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using Sqlite3Plugin;
using System.Collections.Generic;

namespace Elements
{
  public class MasterResistData : AbstractMasterData
  {
    public const string TABLE_NAME = "resist_data";
    //private MasterEnemyDatabase _db;
    private Dictionary<int, MasterResistData.ResistData> _lazyPrimaryKeyDictionary;

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
      protected ObscuredInt _resist_status_id;
      protected ObscuredInt _ailment_1;
      protected ObscuredInt _ailment_2;
      protected ObscuredInt _ailment_3;
      protected ObscuredInt _ailment_4;
      protected ObscuredInt _ailment_5;
      protected ObscuredInt _ailment_6;
      protected ObscuredInt _ailment_7;
      protected ObscuredInt _ailment_8;
      protected ObscuredInt _ailment_9;
      protected ObscuredInt _ailment_10;
      protected ObscuredInt _ailment_11;
      protected ObscuredInt _ailment_12;
      protected ObscuredInt _ailment_13;
      protected ObscuredInt _ailment_14;
      protected ObscuredInt _ailment_15;
      protected ObscuredInt _ailment_16;
      protected ObscuredInt _ailment_17;
      protected ObscuredInt _ailment_18;
      protected ObscuredInt _ailment_19;
      protected ObscuredInt _ailment_20;
      protected ObscuredInt _ailment_21;
      protected ObscuredInt _ailment_22;
      protected ObscuredInt _ailment_23;
      protected ObscuredInt _ailment_24;
      protected ObscuredInt _ailment_25;
      protected ObscuredInt _ailment_26;
      protected ObscuredInt _ailment_27;
      protected ObscuredInt _ailment_28;
      protected ObscuredInt _ailment_29;
      protected ObscuredInt _ailment_30;
      protected ObscuredInt _ailment_31;
      protected ObscuredInt _ailment_32;
      protected ObscuredInt _ailment_33;
      protected ObscuredInt _ailment_34;
      protected ObscuredInt _ailment_35;
      protected ObscuredInt _ailment_36;
      protected ObscuredInt _ailment_37;
      protected ObscuredInt _ailment_38;
      protected ObscuredInt _ailment_39;
      protected ObscuredInt _ailment_40;
      protected ObscuredInt _ailment_41;
      protected ObscuredInt _ailment_42;
      protected ObscuredInt _ailment_43;
      protected ObscuredInt _ailment_44;
      protected ObscuredInt _ailment_45;
      protected ObscuredInt _ailment_46;
      protected ObscuredInt _ailment_47;
      protected ObscuredInt _ailment_48;
      protected ObscuredInt _ailment_49;
      protected ObscuredInt _ailment_50;

      public ObscuredInt resist_status_id => this._resist_status_id;

      public ObscuredInt ailment_1 => this._ailment_1;

      public ObscuredInt ailment_2 => this._ailment_2;

      public ObscuredInt ailment_3 => this._ailment_3;

      public ObscuredInt ailment_4 => this._ailment_4;

      public ObscuredInt ailment_5 => this._ailment_5;

      public ObscuredInt ailment_6 => this._ailment_6;

      public ObscuredInt ailment_7 => this._ailment_7;

      public ObscuredInt ailment_8 => this._ailment_8;

      public ObscuredInt ailment_9 => this._ailment_9;

      public ObscuredInt ailment_10 => this._ailment_10;

      public ObscuredInt ailment_11 => this._ailment_11;

      public ObscuredInt ailment_12 => this._ailment_12;

      public ObscuredInt ailment_13 => this._ailment_13;

      public ObscuredInt ailment_14 => this._ailment_14;

      public ObscuredInt ailment_15 => this._ailment_15;

      public ObscuredInt ailment_16 => this._ailment_16;

      public ObscuredInt ailment_17 => this._ailment_17;

      public ObscuredInt ailment_18 => this._ailment_18;

      public ObscuredInt ailment_19 => this._ailment_19;

      public ObscuredInt ailment_20 => this._ailment_20;

      public ObscuredInt ailment_21 => this._ailment_21;

      public ObscuredInt ailment_22 => this._ailment_22;

      public ObscuredInt ailment_23 => this._ailment_23;

      public ObscuredInt ailment_24 => this._ailment_24;

      public ObscuredInt ailment_25 => this._ailment_25;

      public ObscuredInt ailment_26 => this._ailment_26;

      public ObscuredInt ailment_27 => this._ailment_27;

      public ObscuredInt ailment_28 => this._ailment_28;

      public ObscuredInt ailment_29 => this._ailment_29;

      public ObscuredInt ailment_30 => this._ailment_30;

      public ObscuredInt ailment_31 => this._ailment_31;

      public ObscuredInt ailment_32 => this._ailment_32;

      public ObscuredInt ailment_33 => this._ailment_33;

      public ObscuredInt ailment_34 => this._ailment_34;

      public ObscuredInt ailment_35 => this._ailment_35;

      public ObscuredInt ailment_36 => this._ailment_36;

      public ObscuredInt ailment_37 => this._ailment_37;

      public ObscuredInt ailment_38 => this._ailment_38;

      public ObscuredInt ailment_39 => this._ailment_39;

      public ObscuredInt ailment_40 => this._ailment_40;

      public ObscuredInt ailment_41 => this._ailment_41;

      public ObscuredInt ailment_42 => this._ailment_42;

      public ObscuredInt ailment_43 => this._ailment_43;

      public ObscuredInt ailment_44 => this._ailment_44;

      public ObscuredInt ailment_45 => this._ailment_45;

      public ObscuredInt ailment_46 => this._ailment_46;

      public ObscuredInt ailment_47 => this._ailment_47;

      public ObscuredInt ailment_48 => this._ailment_48;

      public ObscuredInt ailment_49 => this._ailment_49;

      public ObscuredInt ailment_50 => this._ailment_50;

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
        this._resist_status_id = (ObscuredInt) resist_status_id;
        this._ailment_1 = (ObscuredInt) ailment_1;
        this._ailment_2 = (ObscuredInt) ailment_2;
        this._ailment_3 = (ObscuredInt) ailment_3;
        this._ailment_4 = (ObscuredInt) ailment_4;
        this._ailment_5 = (ObscuredInt) ailment_5;
        this._ailment_6 = (ObscuredInt) ailment_6;
        this._ailment_7 = (ObscuredInt) ailment_7;
        this._ailment_8 = (ObscuredInt) ailment_8;
        this._ailment_9 = (ObscuredInt) ailment_9;
        this._ailment_10 = (ObscuredInt) ailment_10;
        this._ailment_11 = (ObscuredInt) ailment_11;
        this._ailment_12 = (ObscuredInt) ailment_12;
        this._ailment_13 = (ObscuredInt) ailment_13;
        this._ailment_14 = (ObscuredInt) ailment_14;
        this._ailment_15 = (ObscuredInt) ailment_15;
        this._ailment_16 = (ObscuredInt) ailment_16;
        this._ailment_17 = (ObscuredInt) ailment_17;
        this._ailment_18 = (ObscuredInt) ailment_18;
        this._ailment_19 = (ObscuredInt) ailment_19;
        this._ailment_20 = (ObscuredInt) ailment_20;
        this._ailment_21 = (ObscuredInt) ailment_21;
        this._ailment_22 = (ObscuredInt) ailment_22;
        this._ailment_23 = (ObscuredInt) ailment_23;
        this._ailment_24 = (ObscuredInt) ailment_24;
        this._ailment_25 = (ObscuredInt) ailment_25;
        this._ailment_26 = (ObscuredInt) ailment_26;
        this._ailment_27 = (ObscuredInt) ailment_27;
        this._ailment_28 = (ObscuredInt) ailment_28;
        this._ailment_29 = (ObscuredInt) ailment_29;
        this._ailment_30 = (ObscuredInt) ailment_30;
        this._ailment_31 = (ObscuredInt) ailment_31;
        this._ailment_32 = (ObscuredInt) ailment_32;
        this._ailment_33 = (ObscuredInt) ailment_33;
        this._ailment_34 = (ObscuredInt) ailment_34;
        this._ailment_35 = (ObscuredInt) ailment_35;
        this._ailment_36 = (ObscuredInt) ailment_36;
        this._ailment_37 = (ObscuredInt) ailment_37;
        this._ailment_38 = (ObscuredInt) ailment_38;
        this._ailment_39 = (ObscuredInt) ailment_39;
        this._ailment_40 = (ObscuredInt) ailment_40;
        this._ailment_41 = (ObscuredInt) ailment_41;
        this._ailment_42 = (ObscuredInt) ailment_42;
        this._ailment_43 = (ObscuredInt) ailment_43;
        this._ailment_44 = (ObscuredInt) ailment_44;
        this._ailment_45 = (ObscuredInt) ailment_45;
        this._ailment_46 = (ObscuredInt) ailment_46;
        this._ailment_47 = (ObscuredInt) ailment_47;
        this._ailment_48 = (ObscuredInt) ailment_48;
        this._ailment_49 = (ObscuredInt) ailment_49;
        this._ailment_50 = (ObscuredInt) ailment_50;
        this.SetUp();
      }

      public int[] Ailments { get; set; }

      public void SetUp()
      {
        this.Ailments = new int[50];
        this.Ailments[0] = (int) this.ailment_1;
        this.Ailments[1] = (int) this.ailment_2;
        this.Ailments[2] = (int) this.ailment_3;
        this.Ailments[3] = (int) this.ailment_4;
        this.Ailments[4] = (int) this.ailment_5;
        this.Ailments[5] = (int) this.ailment_6;
        this.Ailments[6] = (int) this.ailment_7;
        this.Ailments[7] = (int) this.ailment_8;
        this.Ailments[8] = (int) this.ailment_9;
        this.Ailments[9] = (int) this.ailment_10;
        this.Ailments[10] = (int) this.ailment_11;
        this.Ailments[11] = (int) this.ailment_12;
        this.Ailments[12] = (int) this.ailment_13;
        this.Ailments[13] = (int) this.ailment_14;
        this.Ailments[14] = (int) this.ailment_15;
        this.Ailments[15] = (int) this.ailment_16;
        this.Ailments[16] = (int) this.ailment_17;
        this.Ailments[17] = (int) this.ailment_18;
        this.Ailments[18] = (int) this.ailment_19;
        this.Ailments[19] = (int) this.ailment_20;
        this.Ailments[20] = (int) this.ailment_21;
        this.Ailments[21] = (int) this.ailment_22;
        this.Ailments[22] = (int) this.ailment_23;
        this.Ailments[23] = (int) this.ailment_24;
        this.Ailments[24] = (int) this.ailment_25;
        this.Ailments[25] = (int) this.ailment_26;
        this.Ailments[26] = (int) this.ailment_27;
        this.Ailments[27] = (int) this.ailment_28;
        this.Ailments[28] = (int) this.ailment_29;
        this.Ailments[29] = (int) this.ailment_30;
        this.Ailments[30] = (int) this.ailment_31;
        this.Ailments[31] = (int) this.ailment_32;
        this.Ailments[32] = (int) this.ailment_33;
        this.Ailments[33] = (int) this.ailment_34;
        this.Ailments[34] = (int) this.ailment_35;
        this.Ailments[35] = (int) this.ailment_36;
        this.Ailments[36] = (int) this.ailment_37;
        this.Ailments[37] = (int) this.ailment_38;
        this.Ailments[38] = (int) this.ailment_39;
        this.Ailments[39] = (int) this.ailment_40;
        this.Ailments[40] = (int) this.ailment_41;
        this.Ailments[41] = (int) this.ailment_42;
        this.Ailments[42] = (int) this.ailment_43;
        this.Ailments[43] = (int) this.ailment_44;
        this.Ailments[44] = (int) this.ailment_45;
        this.Ailments[45] = (int) this.ailment_46;
        this.Ailments[46] = (int) this.ailment_47;
        this.Ailments[47] = (int) this.ailment_48;
        this.Ailments[48] = (int) this.ailment_49;
        this.Ailments[49] = (int) this.ailment_50;
      }
    }
  }
}

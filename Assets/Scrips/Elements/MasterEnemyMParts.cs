// Decompiled with JetBrains decompiler
// Type: Elements.MasterEnemyMParts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
//using Sqlite3Plugin;
using System.Collections.Generic;

namespace Elements
{
    public class MasterEnemyMParts : AbstractMasterData
    {
        public const string TABLE_NAME = "enemy_m_parts";
        //private MasterEnemyDatabase _db;
        //private Dictionary<int, MasterEnemyMParts.EnemyMParts> _strictPrimaryKeyDictionary;
        //private Dictionary<int, MasterEnemyMParts.EnemyMParts> _lazyPrimaryKeyDictionary;

        /*public Dictionary<int, MasterEnemyMParts.EnemyMParts> dictionary
        {
          get
          {
            if (this._strictPrimaryKeyDictionary == null)
              this._StrictLoadAllEntries();
            return this._strictPrimaryKeyDictionary;
          }
        }*/

        /*public MasterEnemyMParts(MasterEnemyDatabase db)
          : base((AbstractMasterDatabase) db)
        {
          this._lazyPrimaryKeyDictionary = new Dictionary<int, MasterEnemyMParts.EnemyMParts>();
          this._db = db;
        }*/

        /*public MasterEnemyMParts.EnemyMParts Get(int enemy_id)
        {
          int key = enemy_id;
          MasterEnemyMParts.EnemyMParts enemyMparts = (MasterEnemyMParts.EnemyMParts) null;
          if (this._strictPrimaryKeyDictionary != null)
          {
            this._strictPrimaryKeyDictionary.TryGetValue(key, out enemyMparts);
            return enemyMparts;
          }
          if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out enemyMparts))
          {
            enemyMparts = this._db != null ? this._SelectOne(enemy_id) : (MasterEnemyMParts.EnemyMParts) null;
            this._lazyPrimaryKeyDictionary[key] = enemyMparts;
          }
          return enemyMparts;
        }*/

        //public bool HasKey(int enemy_id) => this.Get(enemy_id) != null;

        /*private MasterEnemyMParts.EnemyMParts _SelectOne(int enemy_id)
        {
          NAOCHNBMGCB queryEnemyMparts = this._db.GetSelectQuery_EnemyMParts();
          if (queryEnemyMparts == null)
            return (MasterEnemyMParts.EnemyMParts) null;
          if (!queryEnemyMparts.BindInt(1, enemy_id))
            return (MasterEnemyMParts.EnemyMParts) null;
          MasterEnemyMParts.EnemyMParts enemyMparts = (MasterEnemyMParts.EnemyMParts) null;
          if (queryEnemyMparts.Step())
            enemyMparts = this._CreateCachedOrmByQueryResult((ODBKLOJPCHG) queryEnemyMparts);
          queryEnemyMparts.Reset();
          return enemyMparts;
        }*/

        /*private MasterEnemyMParts.EnemyMParts _CreateCachedOrmByQueryResult(
          ODBKLOJPCHG query)
        {
          MasterEnemyMParts.EnemyMParts enemyMparts = (MasterEnemyMParts.EnemyMParts) null;
          int enemy_id = query.GetInt(0);
          int key = enemy_id;
          if (this._strictPrimaryKeyDictionary != null)
          {
            this._strictPrimaryKeyDictionary.TryGetValue(key, out enemyMparts);
            return enemyMparts;
          }
          if (!this._lazyPrimaryKeyDictionary.TryGetValue(key, out enemyMparts))
          {
            int child_enemy_parameter_1 = query.GetInt(1);
            int child_enemy_parameter_2 = query.GetInt(2);
            int child_enemy_parameter_3 = query.GetInt(3);
            int child_enemy_parameter_4 = query.GetInt(4);
            int child_enemy_parameter_5 = query.GetInt(5);
            enemyMparts = new MasterEnemyMParts.EnemyMParts(enemy_id, child_enemy_parameter_1, child_enemy_parameter_2, child_enemy_parameter_3, child_enemy_parameter_4, child_enemy_parameter_5);
            this._lazyPrimaryKeyDictionary.Add(key, enemyMparts);
          }
          return enemyMparts;
        }*/

        /*public override void Unload()
        {
          this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterEnemyMParts.EnemyMParts>) null;
          this._strictPrimaryKeyDictionary = (Dictionary<int, MasterEnemyMParts.EnemyMParts>) null;
        }*/

        /*private void _StrictLoadAllEntries()
        {
          Dictionary<int, MasterEnemyMParts.EnemyMParts> dictionary = new Dictionary<int, MasterEnemyMParts.EnemyMParts>();
          using (ODBKLOJPCHG queryEnemyMparts = this._db.GetSelectAllQuery_EnemyMParts())
          {
            while (queryEnemyMparts.Step())
            {
              int key = queryEnemyMparts.GetInt(0);
              dictionary.Add(key, this._CreateCachedOrmByQueryResult(queryEnemyMparts));
            }
          }
          this._strictPrimaryKeyDictionary = dictionary;
          this._lazyPrimaryKeyDictionary = (Dictionary<int, MasterEnemyMParts.EnemyMParts>) null;
        }*/

        public class EnemyMParts
        {
            /*protected ObscuredInt _enemy_id;
            protected ObscuredInt _child_enemy_parameter_1;
            protected ObscuredInt _child_enemy_parameter_2;
            protected ObscuredInt _child_enemy_parameter_3;
            protected ObscuredInt _child_enemy_parameter_4;
            protected ObscuredInt _child_enemy_parameter_5;*/

            public int enemy_id;// => this._enemy_id;

            public int child_enemy_parameter_1;// => this._child_enemy_parameter_1;

            public int child_enemy_parameter_2;// => this._child_enemy_parameter_2;

            public int child_enemy_parameter_3;// => this._child_enemy_parameter_3;

            public int child_enemy_parameter_4;// => this._child_enemy_parameter_4;

            public int child_enemy_parameter_5;// => this._child_enemy_parameter_5;

            public EnemyMParts(
              int enemy_id = 0,
              int child_enemy_parameter_1 = 0,
              int child_enemy_parameter_2 = 0,
              int child_enemy_parameter_3 = 0,
              int child_enemy_parameter_4 = 0,
              int child_enemy_parameter_5 = 0)
            {
                this.enemy_id = enemy_id;
                this.child_enemy_parameter_1 = child_enemy_parameter_1;
                this.child_enemy_parameter_2 = child_enemy_parameter_2;
                this.child_enemy_parameter_3 = child_enemy_parameter_3;
                this.child_enemy_parameter_4 = child_enemy_parameter_4;
                this.child_enemy_parameter_5 = child_enemy_parameter_5;
            }
        }
    }
}

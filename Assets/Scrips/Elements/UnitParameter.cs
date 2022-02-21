// Decompiled with JetBrains decompiler
// Type: Elements.UnitParameter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using PCRCaculator;

namespace Elements
{
    public class UnitParameter
    {
        private int _enemyColor;

        public UnitData UniqueData { get; set; }

        public UnitDataForView UniqueDataForView { get; set; }

        public MasterUnitData.UnitData MasterData { get; set; }

        public MasterUnitSkillData.UnitSkillData SkillData { get; set; }
        public EnemyData EnemyData { get; set; }

        public int EnemyColor
        {
            get => _enemyColor;
            set => _enemyColor = value;
        }

        public UnitParameter()
        {
        }

        public UnitParameter(UnitData _unitData)
        {
            UniqueData = _unitData;
            //if (ManagerSingleton<MasterDataManager>.Instance.masterUnitData == null)
            //  return;
            //this.SetMasterData();
        }

        public UnitParameter(UnitData uniqueData, UnitDataForView uniqueDataForView, MasterUnitData.UnitData masterData, MasterUnitSkillData.UnitSkillData skillData,EnemyData enemyData=null) : this(uniqueData)
        {
            UniqueDataForView = uniqueDataForView;
            MasterData = masterData;
            SkillData = skillData;
            EnemyData = enemyData;
        }

        /*public UnitParameter(UnitDataForView _unitData)
        {
          this.UniqueDataForView = _unitData;
          if (ManagerSingleton<MasterDataManager>.Instance.masterUnitData == null)
            return;
          this.SetMasterDataForView();
        }*/

        /*public void SetMasterData()
        {
          int unitResourceId = UnitUtility.GetUnitResourceId((int) this.UniqueData.Id);
          this.MasterData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData.Get(unitResourceId);
          this.SkillData = ManagerSingleton<MasterDataManager>.Instance.masterUnitSkillData[unitResourceId];
        }*/

        /*public void SetMasterDataForView()
        {
          int unitResourceId = UnitUtility.GetUnitResourceId((int) this.UniqueDataForView.Id);
          this.MasterData = ManagerSingleton<MasterDataManager>.Instance.masterUnitData.Get(unitResourceId);
          this.SkillData = ManagerSingleton<MasterDataManager>.Instance.masterUnitSkillData[unitResourceId];
        }*/

        //public void CalcUnitData() => UnitUtility.CalcParamAndSkill(this.UniqueData);
    }
}

// Decompiled with JetBrains decompiler
// Type: Elements.AttackFieldData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using Elements.Battle;

namespace Elements
{
    public class AttackFieldData : AbnormalStateDataBase
    {
        public DamageData.eDamageType DamageType { get; set; }

        public int EffectId { get; set; }

        public FloatWithEx Value { get; set; }

        public int ActionId { get; set; }

        public float EnergyChargeMultiple { get; set; } = 1f;

        public int AbsorberValue { get; set; }

        public override void StartField()
        {
            /*if ((UnityEngine.Object) this.HGMNJJBLJIO != (UnityEngine.Object) null)
            {
              this.skillEffect = this.IPEGKPHMBBN.GetEffect(this.PPOJKIDHGNJ.IsLeftDir ? this.LALMMFAOJDP : this.HGMNJJBLJIO);
              this.initializeSkillEffect();
            }
            else if (this.EffectId > 0)
            {
              this.skillEffect = this.IPEGKPHMBBN.GetEffect(Singleton<LCEGKJFKOPD>.Instance.GetAttaclFieldEffect(this.EffectId));
              this.initializeSkillEffect();
            }*/
            base.StartField();
        }

        public override void OnRepeat()
        {
            int index = 0;
            for (int count = TargetList.Count; index < count; ++index)
            {
                if (TargetList[index].Owner.CurrentState != UnitCtrl.ActionState.DIE)
                {
                    DamageData damageData = new DamageData();
                    damageData.Target = TargetList[index];
                    damageData.Damage = BattleUtil.FloatToInt(Value);
                    damageData.DamageType = DamageType;
                    damageData.Source = PPOJKIDHGNJ;
                    damageData.ActionType = eActionType.ATTACK_FIELD;
                    damageData.ExecAbsorber = _damage =>
                    {
                        if (_damage > AbsorberValue)
                        {
                            int num = _damage - AbsorberValue;
                            BattleManager.SubstructEnemyPoint(AbsorberValue);
                            AbsorberValue = 0;
                            return num;
                        }
                        AbsorberValue -= _damage;
                        BattleManager.SubstructEnemyPoint(_damage);
                        return 0;
                    };
                    UnitCtrl owner = TargetList[index].Owner;
                    DamageData _damageData = damageData;
                    Skill pmhdbojmead = PMHDBOJMEAD;
                    int actionId = ActionId;
                    Skill _skill = pmhdbojmead;
                    double energyChargeMultiple = EnergyChargeMultiple;
                    //add
                    Action<string> action0 = a =>
                    {
                        string msg = "(" + BattleHeaderController.CurrentFrameCount + ")目标：" + owner.UnitName + a + "\n";
                        onExec?.Invoke(msg);
                    };
                    //end add
                    owner.SetDamage(_damageData, false, actionId, _skill: _skill, _noMotion: true, _energyChargeMultiple: ((float)energyChargeMultiple), callBack: action0);
                }
            }
        }
    }
}

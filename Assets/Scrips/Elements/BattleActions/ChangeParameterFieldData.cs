// Decompiled with JetBrains decompiler
// Type: Elements.ChangeParameterFieldData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Elements.Battle;
using System.Collections.Generic;

namespace Elements
{
    public class ChangeParameterFieldData : AbnormalStateDataBase
    {
        private const float PERCENT_DIGIT = 100f;

        public UnitCtrl.BuffParamKind BuffParamKind { get; set; }

        public ChangeParameterFieldData.eEffectType EffectType { get; set; }

        public float Value { get; set; }

        public BuffDebuffAction.eChangeParameterType ValueType { get; set; }

        public bool IsBuff { get; set; }

        public Dictionary<UnitCtrl, int> AlreadyExecedTargetCount { get; set; }

        protected override int getClearedIndex(UnitCtrl _unit) => !this.IsBuff ? _unit.ClearedDebuffFieldIndex : _unit.ClearedBuffFieldIndex;

        public override void StartField()
        {
            /*switch (this.EffectType)
            {
              case ChangeParameterFieldData.eEffectType.DEBUFF:
                this.skillEffect = this.IPEGKPHMBBN.GetEffect(Singleton<LCEGKJFKOPD>.Instance.CBGICKAAHBB);
                this.initializeSkillEffect();
                break;
              case ChangeParameterFieldData.eEffectType.UNIQUE:
                this.skillEffect = this.IPEGKPHMBBN.GetEffect(this.PPOJKIDHGNJ.IsLeftDir ? this.LALMMFAOJDP : this.HGMNJJBLJIO);
                this.initializeSkillEffect();
                break;
            }*/
            PCRCaculator.Battle.BattleUIManager.Instance.StartFieldEffect(this);

            base.StartField();
        }

        public override void OnRepeat()
        {
        }

        public override void OnExit(BasePartsData _parts)
        {
            base.OnExit(_parts);
            this.AlreadyExecedTargetCount[_parts.Owner]--;
            if (this.AlreadyExecedTargetCount[_parts.Owner] != 0)
                return;
            Dictionary<BasePartsData, FloatWithEx> dictionary = new Dictionary<BasePartsData, FloatWithEx>();
            if (_parts.Owner.IsPartsBoss && this.BuffParamKind != UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE && (this.BuffParamKind != UnitCtrl.BuffParamKind.MOVE_SPEED && this.BuffParamKind != UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE) && this.BuffParamKind != UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE)
            {
                for (int index = 0; index < _parts.Owner.BossPartsListForBattle.Count; ++index)
                    dictionary.Add((BasePartsData)_parts.Owner.BossPartsListForBattle[index], BuffDebuffAction.CalculateBuffDebuffParam((BasePartsData)_parts.Owner.BossPartsListForBattle[index], this.Value, this.ValueType, this.BuffParamKind, !this.IsBuff));
            }
            else
                dictionary.Add(_parts.Owner.DummyPartsData, BuffDebuffAction.CalculateBuffDebuffParam(_parts, this.Value, this.ValueType, this.BuffParamKind, !this.IsBuff));
            _parts.Owner.EnableBuffParam(this.BuffParamKind, dictionary, false, (UnitCtrl)null, this.IsBuff, false, 90);
        }

        public override void OnEnter(BasePartsData _parts)
        {
            UnitCtrl owner = _parts.Owner;
            base.OnEnter(_parts);
            if (this.AlreadyExecedTargetCount.ContainsKey(_parts.Owner))
            {
                this.AlreadyExecedTargetCount[_parts.Owner]++;
                if (this.AlreadyExecedTargetCount[_parts.Owner] != 1)
                    return;
            }
            else
                this.AlreadyExecedTargetCount.Add(_parts.Owner, 1);
            Dictionary<BasePartsData, FloatWithEx> dictionary = new Dictionary<BasePartsData, FloatWithEx>();
            if (_parts.Owner.IsPartsBoss && this.BuffParamKind != UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE && (this.BuffParamKind != UnitCtrl.BuffParamKind.MOVE_SPEED && this.BuffParamKind != UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE) && this.BuffParamKind != UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE)
            {
                for (int index = 0; index < _parts.Owner.BossPartsListForBattle.Count; ++index)
                    dictionary.Add((BasePartsData)_parts.Owner.BossPartsListForBattle[index], BuffDebuffAction.CalculateBuffDebuffParam((BasePartsData)_parts.Owner.BossPartsListForBattle[index], this.Value, this.ValueType, this.BuffParamKind, !this.IsBuff));
            }
            else
                dictionary.Add(_parts.Owner.DummyPartsData, BuffDebuffAction.CalculateBuffDebuffParam(_parts, this.Value, this.ValueType, this.BuffParamKind, !this.IsBuff));
            owner.SetBuffParam(UnitCtrl.BuffParamKind.NUM, dictionary, 0.0f, 0, (UnitCtrl)null, true, Elements.eEffectType.COMMON, this.IsBuff, false);
            owner.EnableBuffParam(this.BuffParamKind, dictionary, true, (UnitCtrl)null, this.IsBuff, false, 90);
        }

        public enum eValueType
        {
            FIXED = 1,
            PERCENTAGE = 2,
        }

        public enum eEffectType
        {
            NONE,
            BUFF,
            DEBUFF,
            UNIQUE,
        }
    }
}

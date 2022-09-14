// Decompiled with JetBrains decompiler
// Type: Elements.ChangeParameterFieldData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Battle;

namespace Elements
{
    public class ChangeParameterFieldData : AbnormalStateDataBase
    {
        private const float PERCENT_DIGIT = 100f;

        public UnitCtrl.BuffParamKind BuffParamKind { get; set; }

        public eEffectType EffectType { get; set; }

        public float Value { get; set; }

        public BuffDebuffAction.eChangeParameterType ValueType { get; set; }

        public bool IsBuff { get; set; }

        public Dictionary<UnitCtrl, int> AlreadyExecedTargetCount { get; set; }

        protected override int getClearedIndex(UnitCtrl _unit) => !IsBuff ? _unit.ClearedDebuffFieldIndex : _unit.ClearedBuffFieldIndex;
        public bool ShowsIcon = true;

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
            BattleUIManager.Instance.StartFieldEffect(this);

            base.StartField();
        }

        public override void OnRepeat()
        {
        }

        public override void OnExit(BasePartsData _parts)
        {
            base.OnExit(_parts);
            AlreadyExecedTargetCount[_parts.Owner]--;
            if (AlreadyExecedTargetCount[_parts.Owner] != 0)
                return;
            Dictionary<BasePartsData, FloatWithEx> dictionary = new Dictionary<BasePartsData, FloatWithEx>();
            if (_parts.Owner.IsPartsBoss && BuffParamKind != UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE && (BuffParamKind != UnitCtrl.BuffParamKind.MOVE_SPEED && BuffParamKind != UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE) && BuffParamKind != UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE)
            {
                for (int index = 0; index < _parts.Owner.BossPartsListForBattle.Count; ++index)
                    dictionary.Add(_parts.Owner.BossPartsListForBattle[index], BuffDebuffAction.CalculateBuffDebuffParam(_parts.Owner.BossPartsListForBattle[index], Value, ValueType, BuffParamKind, !IsBuff));
            }
            else
                dictionary.Add(_parts.Owner.DummyPartsData, BuffDebuffAction.CalculateBuffDebuffParam(_parts, Value, ValueType, BuffParamKind, !IsBuff));
            _parts.Owner.EnableBuffParam(BuffParamKind, dictionary, _enable: false, null, IsBuff, _additional: false, ShowsIcon);
        }

        public override void OnEnter(BasePartsData _parts)
        {
            UnitCtrl owner = _parts.Owner;
            base.OnEnter(_parts);
            if (AlreadyExecedTargetCount.ContainsKey(_parts.Owner))
            {
                AlreadyExecedTargetCount[_parts.Owner]++;
                if (AlreadyExecedTargetCount[_parts.Owner] != 1)
                    return;
            }
            else
                AlreadyExecedTargetCount.Add(_parts.Owner, 1);
            Dictionary<BasePartsData, FloatWithEx> dictionary = new Dictionary<BasePartsData, FloatWithEx>();
            if (_parts.Owner.IsPartsBoss && BuffParamKind != UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE && (BuffParamKind != UnitCtrl.BuffParamKind.MOVE_SPEED && BuffParamKind != UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE) && BuffParamKind != UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE)
            {
                for (int index = 0; index < _parts.Owner.BossPartsListForBattle.Count; ++index)
                    dictionary.Add(_parts.Owner.BossPartsListForBattle[index], BuffDebuffAction.CalculateBuffDebuffParam(_parts.Owner.BossPartsListForBattle[index], Value, ValueType, BuffParamKind, !IsBuff));
            }
            else
                dictionary.Add(_parts.Owner.DummyPartsData, BuffDebuffAction.CalculateBuffDebuffParam(_parts, Value, ValueType, BuffParamKind, !IsBuff));
            owner.SetBuffParam(UnitCtrl.BuffParamKind.NUM, BuffParamKind, dictionary, 0f, 0, null, _despelable: true, Elements.eEffectType.COMMON, IsBuff, _additional: false, _isShowIcon: false);
            owner.EnableBuffParam(BuffParamKind, dictionary, _enable: true, null, IsBuff, _additional: false, ShowsIcon);
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

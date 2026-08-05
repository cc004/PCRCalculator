using System.Collections.Generic;
using Cute;
using UnityEngine;

namespace Elements
{
    public class SealAction : ActionParameter
    {

        public enum eCancelTriggerType // TypeDefIndex: 20470
        {
            NONE = 0,
            DAMAGED_COUNT = 1,
        }


        private const int DISPLAY_COUNT_NUM = 1;
        private const int REMOVE_EFFECT_NUM = 1;

        public override void Initialize()
        {
            base.Initialize();
            //Singleton<LCEGKJFKOPD>.Instance.LoadSealActionEffect();
        }

        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController) => base.ExecActionOnStart(_skill, _source, _sourceActionController);

        public override void ExecAction(
          UnitCtrl _source,
          BasePartsData _target,
          int _num,
          UnitActionController _sourceActionController,
          Skill _skill,
          float _starttime,
          Dictionary<int, bool> _enabledChildAction,
          ValueDictionary _valueDictionary)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
            AppendIsAlreadyExeced(_target.Owner, _num);
            eStateIconType eStateIconType = (eStateIconType)(double)_valueDictionary[eValueNumber.VALUE_2];

            if (!_target.Owner.SealDictionary.ContainsKey(eStateIconType))
            {
                SealData sealData = new SealData
                {
                    Max = (int)_valueDictionary[eValueNumber.VALUE_1],
                    DisplayCount = ActionDetail1 == 1
                };
                _target.Owner.SealDictionary.Add(eStateIconType, sealData);
            }
            else
                _target.Owner.SealDictionary[eStateIconType].Max = Mathf.Max((int)_valueDictionary[eValueNumber.VALUE_1], _target.Owner.SealDictionary[eStateIconType].Max);

            if (ActionDetail3 == (int)eCancelTriggerType.DAMAGED_COUNT)
            {
                _valueDictionary.TryGetValue(eValueNumber.VALUE_5, out var rawCancel);

                int cancelInt = double.IsInfinity(rawCancel) ? int.MinValue : (int)rawCancel;
                System.Action cancelAction = () =>
                {
                    _target.Owner.SealDictionary[eStateIconType].RemoveSeal(1, true);
                };

                var cancelData = new SealDamageCountCancelData(
                    cancelInt, cancelAction
                );

                var sealCancelDict = _target.Owner.SealCancelDictionary;

                if (!sealCancelDict.ContainsKey(eCancelTriggerType.DAMAGED_COUNT))
                    sealCancelDict[eCancelTriggerType.DAMAGED_COUNT] = new Dictionary<eStateIconType, SealCancelData>();

                sealCancelDict[eCancelTriggerType.DAMAGED_COUNT][eStateIconType] = cancelData;
            }

            if ((double)_valueDictionary[eValueNumber.VALUE_4] == 0.0)
                    return;
            SealData seal = _target.Owner.SealDictionary[eStateIconType];
            if (seal.GetCurrentCount() == 0 && (double)_valueDictionary[eValueNumber.VALUE_4] > 0.0)
            {
                _target.Owner.OnChangeState.Call(_target.Owner, eStateIconType, true);
                // _target.Owner.MyOnChangeAbnormalState?.Invoke(_target.Owner, eStateIconType, true, _valueDictionary[eValueNumber.VALUE_3], eStateIconType.ToString());

                /*if (this.ActionEffectList.Count != 0)
                {
                  NormalSkillEffect actionEffect = this.ActionEffectList[0];
                  GameObject MDOJNMEMHLN = _target.Owner.IsLeftDir ? actionEffect.PrefabLeft : actionEffect.Prefab;
                  SkillEffectCtrl skillEffectCtrl = seal.Effect = this.battleEffectPool.GetEffect(MDOJNMEMHLN);
                  skillEffectCtrl.transform.parent = ExceptNGUIRoot.Transform;
                  skillEffectCtrl.SortTarget = _target.Owner;
                  skillEffectCtrl.InitializeSort();
                  skillEffectCtrl.SetPossitionAppearanceType(actionEffect, _target, _target.Owner, _skill);
                  skillEffectCtrl.ExecAppendCoroutine();
                }*/
            }
            if ((double)_valueDictionary[eValueNumber.VALUE_4] > 0.0)
                seal.AddSeal((float)_valueDictionary[eValueNumber.VALUE_3], _target.Owner, eStateIconType, (int)_valueDictionary[eValueNumber.VALUE_4]);
            if ((double)_valueDictionary[eValueNumber.VALUE_4] >= 0.0)
                return;
            seal.RemoveSeal(-(int)_valueDictionary[eValueNumber.VALUE_4], false);
        }
    }
}

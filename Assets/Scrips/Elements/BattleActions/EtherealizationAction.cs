using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{

    public class EtherealizationAction : ActionParameter // TypeDefIndex: 21647
    {
        public enum eTargetAttackType // TypeDefIndex: 21644
        {
            // Fields
            PHYSICAL = 1,
            MAGIC = 2,
            BOTH = 3,
        }
        // Methods

        // RVA: 0x3592E44 Offset: 0x3592E44 VA: 0x3592E44 Slot: 6
        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source, UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
        }

        // RVA: 0x3592ED8 Offset: 0x3592ED8 VA: 0x3592ED8 Slot: 8
        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController, Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, ValueDictionary _valueDictionary,  Action<string> callBack = null)
        {
            base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary, callBack);
            AppendIsAlreadyExeced(_target.Owner, _num);
            if (!_target.Owner.SealDictionary.ContainsKey((eStateIconType)ActionDetail2))
            {
                _target.Owner.SealDictionary.Add((eStateIconType)ActionDetail2, new SealData()
                {
                    OnAddAction = createOnAddSeal((eTargetAttackType)ActionDetail1, _target.Owner),
                    OnRemoveAction = createOnRemoveSeal((eTargetAttackType)ActionDetail1, _target.Owner),
                    DisplayCount = true,
                    IsEtherealization = true,
                });
            }
            else
            {
                var sealData = _target.Owner.SealDictionary[(eStateIconType)ActionDetail2];
                if (sealData.IsEtherealization == false)
                {
                    _target.Owner.AddEtherealizationCount((eTargetAttackType)ActionDetail1);
                    battleManager.StartAllUnitEtherealizationCheck(_target.Owner);
                    sealData.IsEtherealization = true;
                    sealData.OnAddAction = createOnAddSeal((eTargetAttackType)ActionDetail1, _target.Owner);
                    sealData.OnRemoveAction = createOnRemoveSeal((eTargetAttackType)ActionDetail1, _target.Owner);
                }
            }
        }

        // RVA: 0x35933D4 Offset: 0x35933D4 VA: 0x35933D4
        // private Action<int, bool> createOnRemoveSeal(EtherealizationAction.eTargetAttackType _targetAttackType, UnitComponentAbnormal _abnormal) { }
        private Action<int, bool> createOnRemoveSeal(EtherealizationAction.eTargetAttackType _targetAttackType, UnitCtrl _target)
        {
            return (int _id, bool _oneToZero) =>
            {
                if (_oneToZero)
                {
                    switch (_targetAttackType)
                    {
                        case eTargetAttackType.PHYSICAL:
                            _target.EtherealizationPhysicalCount --;
                            break;
                        case eTargetAttackType.MAGIC:
                            _target.EtherealizationMagicCount --;
                            break;
                        case eTargetAttackType.BOTH:
                            _target.EtherealizationBothCount --;
                            break;
                    }
                }
            };
        }

        // RVA: 0x3593288 Offset: 0x3593288 VA: 0x3593288
        // private Action<int, bool> createOnAddSeal(EtherealizationAction.eTargetAttackType _targetAttackType, UnitComponentAbnormal _abnormal, UnitCtrl _target) { }
        private Action<int, bool> createOnAddSeal(EtherealizationAction.eTargetAttackType _targetAttackType, UnitCtrl _target)
        {
            return (int _id, bool _zeroToOne) => {
                _target.AddEtherealizationCount(_targetAttackType);
                battleManager.StartAllUnitEtherealizationCheck(_target);
            };
        }

        // RVA: 0x35934FC Offset: 0x35934FC VA: 0x35934FC Slot: 9
        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
        }

    }
}
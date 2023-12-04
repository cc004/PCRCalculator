using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Audio;

namespace Elements
{
    public class CopyAtkParamAction : ActionParameter
    {
        private enum eAtkType // TypeDefIndex: 1526
        {
            PHYSICAL = 1, 
            MAGIC = 2
        }

        private AttackActionBase targetAction; // 0x1A0

        public override void ExecActionOnStart(Skill _skill, UnitCtrl _source,
            UnitActionController _sourceActionController)
        {
            targetAction = _skill.ActionParameters.Find(ap => ap.ActionId == ActionDetail2) as AttackActionBase;
            targetAction.UseCopyAtkParam = true;
        }

        // RVA: 0x12AC320 Offset: 0x12AC320 VA: 0x7FFD9D34C320 Slot: 10
        public override void ReadyAction(UnitCtrl _source, UnitActionController _sourceActionController, Skill _skill)
        {
            targetAction.CopyAtk = 0;
        }
            

        public override void ExecAction(UnitCtrl _source, BasePartsData _target, int _num, UnitActionController _sourceActionController,
            Skill _skill, float _starttime, Dictionary<int, bool> _enabledChildAction, Dictionary<eValueNumber, FloatWithEx> _valueDictionary,
            Action<string> callBack = null)
        {
            FloatWithEx val = 0;

            var type = (eAtkType) ActionDetail1;
            if (type == eAtkType.PHYSICAL) val = _target.GetAtkZeroEx();
            if (type == eAtkType.MAGIC) val = _target.GetMagicStrZeroEx();

            targetAction.CopyAtk = val.Floor();

            callBack?.Invoke($"将动作{targetAction.ActionId}的参考攻击力替换为{val}");
        }

        // RVA: 0x12A4E50 Offset: 0x12A4E50 VA: 0x7FFD9D344E50 Slot: 9
        public override void SetLevel(float _level)
        {

        }
    }
}

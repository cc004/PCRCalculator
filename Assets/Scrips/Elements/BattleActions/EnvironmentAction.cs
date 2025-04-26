using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    public class EnvironmentAction : ActionParameter // TypeDefIndex: 2488
    {
        private List<ActionParameter> targetActions { get; set; }

        public override void ExecActionOnStart(
            Skill skill,
            UnitCtrl source,
            UnitActionController sourceActionController)
        {
            base.ExecActionOnStart(skill, source, sourceActionController);

            var indexes = this.ActionChildrenIndexes;
            if (indexes == null || indexes.Count == 0)
                return;

            if (this.targetActions == null)
                this.targetActions = new List<ActionParameter>();

            var parameters = skill?.ActionParameters;
            if (parameters == null)
                return;

            foreach (int idx in indexes)
            {
                if (idx < 0 || idx >= parameters.Count)
                    continue;

                var child = parameters[idx];
                if (child == null)
                    continue;

                this.targetActions.Add(child);
            }
        }

        public override void ReadyAction(
        UnitCtrl source,
        UnitActionController sourceActionController,
        Skill skill)
        {
            base.ReadyAction(source, sourceActionController, skill);

            if (targetActions == null || targetActions.Count == 0)
                return;

            foreach (var child in targetActions)
            {
                if (child == null)
                    continue;

                child.ReadyAction(source, sourceActionController, skill);
            }
        }

        public override void ExecAction(
        UnitCtrl source,
        BasePartsData target,
        int num,
        UnitActionController sourceActionController,
        Skill skill,
        float starttime,
        Dictionary<int, bool> enabledChildAction,
        Dictionary<eValueNumber, FloatWithEx> valueDictionary)
        {
            // SkillEffectCtrl wrapper = null;
            // if (this.ActionEffectList != null && this.ActionEffectList.Count > 0)
            // {
            //     var effect = this.ActionEffectList[0];
            //     if (effect != null)
            //         wrapper = effect.VMWrapper;      
            // }

            // 2. 新建 EnvironmentData，用它承载所有上下文
            var data = new EnvironmentData
            {
                Skill = skill,
                SourceUnit = source,
                EnvironmentType = this.ActionDetail2,
                TargetActions = this.targetActions,
                SourceActionController = sourceActionController,
                // 对应 C++ 里 *(float*)(obj+0x44) = valueDictionary[0]
                LimitTime = (valueDictionary != null && valueDictionary.TryGetValue((eValueNumber)0, out var t))
                                         ? t
                                         : 0f,
            };
            // data.InitializeSkillEffect(wrapper);

            data.TargetList = new List<BasePartsData>();

            var mgr = data.battleManager;
            if (mgr == null)
                return;

            bool isEnemy = source != null && !source.IsPlayerUnit;

            mgr.ExecEnvironment(data, isEnemy);
        }

        public override void SetLevel(float level)
        {
            base.SetLevel(level);

            var master = this.MasterData;
            if (master == null) return;

            var valueDict = this.Value;
            if (valueDict == null) return;

            double baseValue = master.action_value_1;
            double incrementValue = master.action_value_2;

            float result = (float)(baseValue + incrementValue * level);

            valueDict[(eValueNumber)0] = result;
        }
    }

}
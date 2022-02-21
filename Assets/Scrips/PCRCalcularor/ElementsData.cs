using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{

    [Serializable]
    public class FirearmCtrlData
    {
        public float HitDelay = 0.5f;
        public float MoveRate = 1;
        public float duration;
        public PCRCaculator.Battle.eMoveTypes MoveType = PCRCaculator.Battle.eMoveTypes.LINEAR;
        public float startRotate;
        public float endRotate;
        //public Bounds ColliderBox;
        public float[] ColliderBoxCentre = new float[3] { 0, 0, 0 };
        public float[] ColliderBoxSize = new float[3] { 0.2f, 0.2f, 0 };

        public bool ignoreFirearm = false;
        public float fixedExecTime = 0;

        public FirearmCtrlData() { }

        public FirearmCtrlData(float hitDelay, float moveRate, float duration, PCRCaculator.Battle.eMoveTypes moveType, float startRotate, float endRotate, Bounds colliderBox)
        {
            HitDelay = hitDelay;
            MoveRate = moveRate;
            this.duration = duration;
            MoveType = moveType;
            this.startRotate = startRotate;
            this.endRotate = endRotate;
            //ColliderBox = colliderBox;
            ColliderBoxCentre = new[] { colliderBox.center.x, colliderBox.center.y, colliderBox.center.z };
            ColliderBoxSize = new[] { colliderBox.extents.x, colliderBox.extents.y, colliderBox.extents.z };
        }

    }
    public class UnitActionControllerData2
    {
        public ActionParameterOnPrefabDetail AttackDetail;
        public bool UseDefaultDelay;
        public Skill Attack;
        public List<Skill> UnionBurstList;
        public List<Skill> MainSkillList;
        public List<Skill> SpecialSkillList;
        public List<Skill> SpecialSkillEvolutionList;
        public List<Skill> UnionBurstEvolutionList;
        public List<Skill> MainSkillEvolutionList;
        public Skill Annihilation;

        public UnitActionControllerData2(ActionParameterOnPrefabDetail attackDetail,
            bool useDefaultDelay, Skill attack, List<Skill> unionBurstList, List<Skill> mainSkillList,
            List<Skill> specialSkillList, List<Skill> specialSkillEvList,
            List<Skill> unionBurstEvolutionList, List<Skill> mainSkillEvolutionList, Skill annihilation)
        {
            AttackDetail = attackDetail;
            UseDefaultDelay = useDefaultDelay;
            Attack = attack;
            UnionBurstList = unionBurstList;
            MainSkillList = mainSkillList;
            SpecialSkillList = specialSkillList;
            SpecialSkillEvolutionList = specialSkillEvList;
            UnionBurstEvolutionList = unionBurstEvolutionList;
            MainSkillEvolutionList = mainSkillEvolutionList;
            Annihilation = annihilation;
        }
    }
    [Serializable]
    public class UnitPrefabData
    {
        public UnitActionControllerData2 UnitActionControllerData;
        public Dictionary<string, List<FirearmCtrlData>> unitFirearmDatas;
    }
}
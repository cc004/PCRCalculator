using System;

namespace ActionParameterSerializer.Actions
{
    public class RevivalAction : ActionParameter
    {

        public enum RevivalType
        {
            unknown = 0,
            normal = 1,
            phoenix = 2
        }

        private RevivalType revivalType;

        public
            override void childInit()
        {
            revivalType = (RevivalType)(actionDetail1);
        }

        public
            override string localizedDetail(int level, Property property)
        {
            switch (revivalType)
            {
                case RevivalType.normal:
                    return Utils.JavaFormat(Utils.GetString("Revive_s1_with_d2_HP"),
                        targetParameter.buildTargetClause(), Math.Round(actionValue2.value * 100));
                default:
                    return base.localizedDetail(level, property);
            }
        }
    }
}

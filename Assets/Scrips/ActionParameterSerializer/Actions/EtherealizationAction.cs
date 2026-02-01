using System.Collections.Generic;
using Elements;

namespace ActionParameterSerializer.Actions
{
    public class EtherealizationAction : ActionParameter
    {
        public enum eTargetAttackType // TypeDefIndex: 21644
        {
            // Fields
            PHYSICAL = 1,
            MAGIC = 2,
            BOTH = 3,
        }
        public override string localizedDetail(int level, Property property)
        {
            eStateIconType iconType = (eStateIconType)actionDetail2;
            eTargetAttackType eTargetAttackType = (eTargetAttackType)actionDetail1;

            return Utils.JavaFormat(Utils.GetString("etherealization_s1_with_s2_seal_by_s3_unit"),
                targetParameter.buildTargetClause(),
                iconType.GetDescription(),
                eTargetAttackType == eTargetAttackType.PHYSICAL ? Utils.GetString("physics") :
                eTargetAttackType == eTargetAttackType.MAGIC ? Utils.GetString("magic") :
                "");
        }
    }
}
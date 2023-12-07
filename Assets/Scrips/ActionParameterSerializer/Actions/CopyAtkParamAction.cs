using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActionParameterSerializer.Actions
{
    class CopyAtkParamAction : ActionParameter
    {
        private enum eAtkType
        {
            ATK = 1, Magic_STR = 2
        }

        private int targetAction;
        private eAtkType propType;

        public override void childInit()
        {
            propType = (eAtkType) actionDetail1;
            targetAction = actionDetail2 % 10;
            base.childInit();
        }

        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Use_param_s1_of_s2_for_action_d3"),
                propType.rawDescription(),
                targetParameter.buildTargetClause(),
                targetAction
            );
        }
    }
}

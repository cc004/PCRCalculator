using System.Text;

namespace ActionParameterSerializer.Actions
{
    public class ChangeEnergyRecoveryRatioByDamageAction : ActionParameter
    {

        public
            override void childInit()
        {
            base.childInit();
        }

        public string getChildrenActionString()
        {
            StringBuilder childrenActionString = new StringBuilder();
            // TODO
            /*
            if (childrenAction != null) {
                for (SkillAction action : childrenAction) {
                    childrenActionString.Append(action.getActionId() % 10).Append(", ");
                }
                childrenActionString.delete(childrenActionString.lastIndexOf(", "), childrenActionString.Count);
            }*/
            return childrenActionString.ToString();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("change_energy_recovery_ratio_of_action_s1_to_s2_when_s3_get_damage"),
                getChildrenActionString(),
                actionValue1.valueString(),
                targetParameter.buildTargetClause());
        }
    }
}

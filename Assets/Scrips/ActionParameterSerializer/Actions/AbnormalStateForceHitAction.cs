using System.Linq;

namespace ActionParameterSerializer.Actions
{
    public class AbnormalStateForceHitAction : ActionParameter
    {
        public override string localizedDetail(int level, Property property)
        {
            var states = new[] { actionValue4.value, actionValue5.value, actionValue6.value, actionValue7.value }
                .Select(value => (int)value).Where(value => value != -1).Distinct().Select(describeState).ToArray();
            if (states.Length == 0)
                return base.localizedDetail(level, property);

            return Utils.JavaFormat(Utils.GetString("Skip_s1_resistance_for_s2"),
                targetParameter.buildTargetClause(), string.Join("、", states));
        }

        private string describeState(int detail)
        {
            if (actionDetail1 == 11 && detail == 0)
                return Utils.GetString("Charm");
            if (actionDetail1 == 11 && detail == 1)
                return Utils.GetString("Confuse");
            if (actionDetail1 == 8 || actionDetail1 == 9)
                return new Ailment(actionDetail1, detail).description();

            return Utils.JavaFormat(Utils.GetString("Abnormal_action_d1_detail_d2"), actionDetail1, detail);
        }
    }
}

using System.Linq;

namespace ActionParameterSerializer.Actions
{
    public class ChangeEnergyRecoveryRatioByDamageAction : ActionParameter
    {

        public override string localizedDetail(int level, Property property)
        {
            string ratio = Utils.roundIfNeed(actionValue1.value * 100);
            if (descriptionContext != null)
            {
                var children = descriptionContext.GetDescendants(actionId).Select(id => describeActionSlot(id)).ToArray();
                if (children.Length > 0)
                {
                    return Utils.JavaFormat(Utils.GetString("Scale_hit_TP_from_related_actions"),
                        string.Join("、", children), ratio);
                }
            }
            return Utils.JavaFormat(Utils.GetString("Scale_hit_TP_from_related_damage"), ratio);
        }
    }
}

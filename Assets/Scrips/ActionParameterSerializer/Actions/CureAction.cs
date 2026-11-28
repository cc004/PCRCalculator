namespace ActionParameterSerializer.Actions
{
    public class CureAction : ActionParameter
    {

        public
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            var state = (Elements.CureAction.eCureActionType)actionDetail1;
            string key = state switch
            {
                Elements.CureAction.eCureActionType.CHAINED => "Bind",
                Elements.CureAction.eCureActionType.POISONE => "Poison",
                _ => state.ToString().ToLowerInvariant()
            };
            string description = Utils.GetString(key);
            return description == key
                ? base.localizedDetail(level, property)
                : Utils.JavaFormat(Utils.GetString("Cure_s1_of_s2"), targetParameter.buildTargetClause(), description);
        }
    }
}

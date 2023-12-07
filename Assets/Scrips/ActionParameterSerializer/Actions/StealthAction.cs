namespace ActionParameterSerializer.Actions
{
    public class StealthAction : ActionParameter
    {
        public
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Stealth_for_s_sec"), actionValue1.valueString());
        }
    }
}

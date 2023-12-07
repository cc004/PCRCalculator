namespace ActionParameterSerializer.Actions
{
    public class WaveStartIdleAction : ActionParameter
    {
        public 
            override void childInit()
        {
            base.childInit();
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Appear_after_s_sec_since_wave_start"),
                actionValue1.valueString());
        }
    }
}

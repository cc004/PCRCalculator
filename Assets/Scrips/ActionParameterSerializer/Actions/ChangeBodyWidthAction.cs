namespace ActionParameterSerializer.Actions
{
    public class ChangeBodyWidthAction : ActionParameter
    {
        public override void childInit()
        {
            base.childInit();
        }
    
        public override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Change_body_width_to_s"), actionValue1.valueString());
        }
    }
}

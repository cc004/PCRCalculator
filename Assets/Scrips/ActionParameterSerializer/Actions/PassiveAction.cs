namespace ActionParameterSerializer.Actions
{
    public class PassiveAction : ActionParameter
    {

        public PropertyKey propertyKey;

        public
            override void childInit()
        {
            switch (actionDetail1)
            {
                case 1:
                    propertyKey = PropertyKey.hp; break;
                case 2:
                    propertyKey = PropertyKey.atk; break;
                case 3:
                    propertyKey = PropertyKey.def; break;
                case 4:
                    propertyKey = PropertyKey.magicStr; break;
                case 5:
                    propertyKey = PropertyKey.magicDef; break;
                default:
                    propertyKey = PropertyKey.unknown; break;
            }
            actionValues.Add(new ActionValue(actionValue2, actionValue3, null));
        }

        public
            override string localizedDetail(int level, Property property)
        {
            return Utils.JavaFormat(Utils.GetString("Raise_s1_s2"), buildExpression(level, property), propertyKey.description());
        }

        public Property propertyItem(int level)
        {
            return Property.getPropertyWithKeyAndValue(null, propertyKey, actionValue2.value + actionValue3.value * level);
        }
    }
}

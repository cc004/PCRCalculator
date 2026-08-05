namespace Elements
{
    public class AccumulativeDamageSourceData
    {
        private eAccumulativeDamageType AccumulativeDamageType;
        private double effectValue;
        private int countLimit;

        public AccumulativeDamageSourceData(eAccumulativeDamageType damageType, double effectValue, int countLimit)
        {
            AccumulativeDamageType = damageType;
            this.effectValue = effectValue;
            this.countLimit = countLimit;
        }

        public AccumulativeDamageData CreateAccumulativeDamageData()
        {
            if (AccumulativeDamageType == eAccumulativeDamageType.FIXED)
            {
                return new AccumulativeDamageData
                {
                    AccumulativeDamageType = eAccumulativeDamageType.FIXED,
                    FixedValue = effectValue,
                    CountLimit = countLimit
                };
            }

            return new AccumulativeDamageData
            {
                AccumulativeDamageType = eAccumulativeDamageType.PERCENTAGE,
                PercentageValue = effectValue,
                CountLimit = countLimit
            };
        }
    }
}

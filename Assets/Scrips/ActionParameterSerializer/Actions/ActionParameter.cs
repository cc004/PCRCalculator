using System;
using System.Collections.Generic;
using System.Text;

namespace ActionParameterSerializer.Actions
{
    public class ActionParameter
    {
        public static ActionParameter type(int rawType)
        {
            switch (rawType)
            {
                case 1:
                    return new DamageAction();
                case 2:
                    return new MoveAction();
                case 3:
                    return new KnockAction();
                case 4:
                    return new HealAction();
                case 5:
                    return new CureAction();
                case 6:
                    return new BarrierAction();
                case 7:
                    return new ReflexiveAction();
                case 8:
                case 9:
                case 12:
                case 13:
                    return new AilmentAction();
                case 10:
                    return new AuraAction();
                case 11:
                    return new CharmAction();
                case 14:
                    return new ModeChangeAction();
                case 15:
                    return new SummonAction();
                case 16:
                    return new ChangeEnergyAction();
                case 17:
                    return new TriggerAction();
                case 18:
                    return new DamageChargeAction();
                case 19:
                    return new ChargeAction();
                case 20:
                    return new DecoyAction();
                case 21:
                    return new NoDamageAction();
                case 22:
                    return new ChangePatternAction();
                case 23:
                    return new IfForChildrenAction();
                case 24:
                    return new RevivalAction();
                case 25:
                    return new ContinuousAttackAction();
                case 26:
                    return new AdditiveAction();
                case 27:
                    return new MultipleAction();
                case 28:
                    return new IfForAllAction();
                case 29:
                    return new SearchAreaChangeAction();
                case 30:
                    return new DestroyAction();
                case 31:
                    return new ContinuousAttackNearbyAction();
                case 32:
                    return new EnchantLifeStealAction();
                case 33:
                    return new EnchantStrikeBackAction();
                case 34:
                    return new AccumulativeDamageAction();
                case 35:
                    return new SealAction();
                case 36:
                    return new AttackFieldAction();
                case 37:
                    return new HealFieldAction();
                case 38:
                    return new ChangeParameterFieldAction();
                case 39:
                    return new AbnormalStateFieldAction();
                case 40:
                    return new ChangeSpeedFieldAction();
                case 41:
                    return new UBChangeTimeAction();
                case 42:
                    return new LoopTriggerAction();
                case 43:
                    return new IfHasTargetAction();
                case 44:
                    return new WaveStartIdleAction();
                case 45:
                    return new SkillExecCountAction();
                case 46:
                    return new RatioDamageAction();
                case 47:
                    return new UpperLimitAttackAction();
                case 48:
                    return new RegenerationAction();
                case 49:
                    return new DispelAction();
                case 50:
                    return new ChannelAction();
                case 52:
                    return new ChangeBodyWidthAction();
                case 53:
                    return new IFExistsFieldForAllAction();
                case 54:
                    return new StealthAction();
                case 55:
                    return new MovePartsAction();
                case 56:
                    return new CountBlindAction();
                case 57:
                    return new CountDownAction();
                case 58:
                    return new StopFieldAction();
                case 59:
                    return new InhibitHealAction();
                case 60:
                    return new AttackSealAction();
                case 61:
                    return new FearAction();
                case 62:
                    return new AweAction();
                case 63:
                    return new LoopMotionRepeatAction();
                case 69:
                    return new ToadAction();
                case 71:
                    return new KnightGuardAction();
                case 72:
                    return new DamageCutAction();
                case 73:
                    return new LogBarrierAction();
                case 74:
                    return new DivideAction();
                case 75:
                    return new ActionByHitCountAction();
                case 76:
                    return new HealDownAction();
                case 77:
                    return new PassiveSealAction();
                case 78:
                    return new PassiveDamageUpAction();
                case 79:
                    return new DamageByBehaviourAction();
                case 83:
                    return new ChangeSpeedOverlapAction();
                case 90:
                    return new PassiveAction();
                case 91:
                    return new PassiveInermittentAction();
                case 92:
                    return new ChangeEnergyRecoveryRatioByDamageAction();
                case 93:
                    return new IgnoreDecoyAction();
                case 94:
                    return new EffectAction();
                case 95:
                    return new SpyAction();
                case 96:
                    return new ChangeEnergyFieldAction();
                case 97:
                    return new ChargeEnergyByDamageAction();
                case 98:
                    return new EnergyDamageReduceAction();
                case 99:
                    return new ChangeSpeedOverwriteFieldAction();
                case 100:
                    return new UnableStateGuardAction();
                case 101:
                    return new AttackSealActionForAllEnemy();
                case 102:
                    return new AccumulativeDamageActionForAllEnemy();
                case 103:
                    return new CopyAtkParamAction();
                case 104:
                    return new EveryAttackCriticalAction();
                case 105:
                    return new EnvironmentAction();
                case 106:
                    return new ProtectAction();
                case 107:
                    return new ChangeCriticalReferenceAction();
                case 108:
                    return new IfContainsUnitGroupAction();
                /*
                case 901:
                    return new ExStartPassiveAction();
                case 902:
                    return new ExConditionPassiveAction();*/
                default:
                    return new ActionParameter();
            }
        }

        public bool isEnemySkill;
        public int dependActionId;
        public List<SkillAction> childrenAction;

        public int actionId;
        public int classId;
        public int rawActionType;

        public int actionDetail1;
        public int actionDetail2;
        public int actionDetail3;
        public List<int> actionDetails = new();

        public class DoubleValue
        {
            public double value;
            public string description;
            public eActionValue index;

            public DoubleValue(double value, eActionValue index)
            {
                this.value = value;
                this.index = index;
                description = index.description();
            }

            public string valueString()
            {
                return Utils.roundIfNeed(value);
            }
        }

        public enum eActionValue
        {
            VALUE1,
            VALUE2,
            VALUE3,
            VALUE4,
            VALUE5,
            VALUE6,
            VALUE7
        }

        public DoubleValue actionValue1;
        public DoubleValue actionValue2;
        public DoubleValue actionValue3;
        public DoubleValue actionValue4;
        public DoubleValue actionValue5;
        public DoubleValue actionValue6;
        public DoubleValue actionValue7;
        public List<double> rawActionValues = new();

        public ActionType actionType;

        public TargetParameter targetParameter;

        public ActionParameter init(bool isEnemySkill, int actionId, int dependActionId, int classId, int actionType, int actionDetail1, int actionDetail2, int actionDetail3, double actionValue1, double actionValue2, double actionValue3, double actionValue4, double actionValue5, double actionValue6, double actionValue7, int targetAssignment, int targetArea, int targetRange, int targetType, int targetNumber, int targetCount, SkillAction dependAction, List<SkillAction> childrenAction)
        {
            this.isEnemySkill = isEnemySkill;
            this.actionId = actionId;
            this.dependActionId = dependActionId;
            this.classId = classId;
            rawActionType = actionType;
            this.actionType = (ActionType)(actionType);
            this.actionDetail1 = actionDetail1;
            this.actionDetail2 = actionDetail2;
            this.actionDetail3 = actionDetail3;
            if (actionDetail1 != 0)
            {
                actionDetails.Add(actionDetail1);
            }

            if (actionDetail2 != 0)
            {
                actionDetails.Add(actionDetail2);
            }

            if (actionDetail3 != 0)
            {
                actionDetails.Add(actionDetail3);
            }

            this.actionValue1 = new DoubleValue(actionValue1, eActionValue.VALUE1);
            this.actionValue2 = new DoubleValue(actionValue2, eActionValue.VALUE2);
            this.actionValue3 = new DoubleValue(actionValue3, eActionValue.VALUE3);
            this.actionValue4 = new DoubleValue(actionValue4, eActionValue.VALUE4);
            this.actionValue5 = new DoubleValue(actionValue5, eActionValue.VALUE5);
            this.actionValue6 = new DoubleValue(actionValue6, eActionValue.VALUE6);
            this.actionValue7 = new DoubleValue(actionValue7, eActionValue.VALUE7);
            if (actionValue1 != 0)
            {
                rawActionValues.Add(actionValue1);
            }

            if (actionValue2 != 0)
            {
                rawActionValues.Add(actionValue2);
            }

            if (actionValue3 != 0)
            {
                rawActionValues.Add(actionValue3);
            }

            if (actionValue4 != 0)
            {
                rawActionValues.Add(actionValue4);
            }

            if (actionValue5 != 0)
            {
                rawActionValues.Add(actionValue5);
            }

            if (actionValue6 != 0)
            {
                rawActionValues.Add(actionValue6);
            }

            if (actionValue7 != 0)
            {
                rawActionValues.Add(actionValue7);
            }

            if (childrenAction != null)
            {
                this.childrenAction = childrenAction;
            }
            targetParameter = new TargetParameter(targetAssignment, targetNumber, targetType, targetRange, targetArea, targetCount, dependAction);
            childInit();
            return this;
        }

        public virtual void childInit()
        {
        }

        private string bracesIfNeeded(string content)
        {
            if (content.Contains("+"))
            {
                return Utils.JavaFormat("(%s)", content);
            }
            else
            {
                return content;
            }
        }

        public virtual string localizedDetail(int level, Property property)
        {
            if (rawActionType == 0)
            {
                return Utils.JavaFormat(Utils.GetString("no_effect"));
            }
            return Utils.JavaFormat(Utils.GetString("Unknown_effect_d1_to_s2_with_details_s3_values_s4"),
                rawActionType,
                targetParameter.buildTargetClause(),
                string.Join(",", actionDetails),
                string.Join(",", rawActionValues));
        }

        public string buildExpression(int level, Property property)
        {
            return buildExpression(level, actionValues, null, property, false, false, false);
        }

        public string buildExpression(int level, RoundingMode? roundingMode, Property property)
        {
            return buildExpression(level, actionValues, roundingMode, property, false, false, false);
        }

        public string buildExpression(int level, List<ActionValue> actionValues, RoundingMode? roundingMode, Property property)
        {
            return buildExpression(level, actionValues, roundingMode, property, false, false, false);
        }

        public String buildExpression(int level, RoundingMode? roundingMode, Property property, bool isConstant)
        {
            return buildExpression(level, actionValues, roundingMode, property, false, false, false, isConstant);
        }

        public string buildExpression(int level,
            List<ActionValue> actionValues,
            RoundingMode? roundingMode,
            Property property,
            bool isHealing,
            bool isSelfTPRestoring,
            bool hasBracesIfNeeded,
            params bool[] redundancy)
        {
            bool isConstant = redundancy.Length > 0 && redundancy[0];
            if (actionValues == null)
            {
                actionValues = this.actionValues;
            }

            if (roundingMode == null)
            {
                roundingMode = RoundingMode.DOWN;
            }

            if (property == null)
            {
                property = new Property();
            }

            if (UserSettings.get().getExpression() == UserSettings.EXPRESSION_EXPRESSION)
            {
                StringBuilder expression = new StringBuilder();
                foreach (ActionValue value in actionValues)
                {
                    StringBuilder part = new StringBuilder();
                    if (value.initial != null && value.perLevel != null)
                    {
                        double initialValue = double.Parse(value.initial);
                        double perLevelValue = double.Parse(value.perLevel);
                        if (initialValue == 0 && perLevelValue == 0)
                        {
                            continue;
                        }
                        else if (initialValue == 0)
                        {
                            part.Append(Utils.JavaFormat("%s * %s", perLevelValue, Utils.JavaFormat(Utils.GetString("SLv"))));
                        }
                        else if (perLevelValue == 0)
                        {
                            if (value.key == null && roundingMode != RoundingMode.UNNECESSARY)
                            {
                                part.Append((int)Math.Round(initialValue));
                            }
                            else
                            {
                                part.Append(initialValue);
                            }
                        }
                        else
                        {
                            part.Append(Utils.JavaFormat("%s + %s * %s", initialValue, perLevelValue, Utils.JavaFormat(Utils.GetString("SLv"))));
                        }
                        if (value.key != null)
                        {
                            if (initialValue == 0 && perLevelValue == 0)
                            {
                                continue;
                            }
                            else if (initialValue == 0 || perLevelValue == 0)
                            {
                                part.Append(Utils.JavaFormat(" * %s", value.key.description()));
                            }
                            else
                            {
                                string c = part.ToString();
                                part.Clear();
                                part.Append(Utils.JavaFormat("(%s) * %s", c, value.key.description()));
                            }
                        }
                    }
                    if (part.Length != 0)
                    {
                        expression.Append(part).Append(" + ");
                    }
                }
                if (expression.Length == 0)
                {
                    return "0";
                }
                else
                {
                    expression.RemoveRange(expression.ToString().LastIndexOf(" +"), expression.Length);
                    return hasBracesIfNeeded ? bracesIfNeeded(expression.ToString()) : expression.ToString();
                }
            }
            else if (UserSettings.get().getExpression() == UserSettings.EXPRESSION_ORIGINAL && !isConstant)
            {
                StringBuilder expression = new StringBuilder();
                foreach (ActionValue value in actionValues)
                {
                    StringBuilder part = new StringBuilder();
                    if (value.initial != null && value.perLevel != null)
                    {
                        double initialValue = double.Parse(value.initial);
                        double perLevelValue = double.Parse(value.perLevel);
                        if (initialValue == 0 && perLevelValue == 0)
                        {
                            continue;
                        }
                        else if (initialValue == 0)
                        {
                            part.Append(Utils.JavaFormat("<sub><color=blue>%s</color></sub>%s * %s", value.perLevelValue.description, perLevelValue, Utils.JavaFormat(Utils.GetString("SLv"))));
                        }
                        else if (perLevelValue == 0)
                        {
                            if (value.key == null && roundingMode != RoundingMode.UNNECESSARY)
                            {
                                double bigDecimal = initialValue;
                                part.Append(Utils.JavaFormat("<sub><color=blue>%s</color></sub>%s", value.initialValue.description, Utils.Round(bigDecimal, roundingMode)));
                            }
                            else
                            {
                                part.Append(Utils.JavaFormat("<sub><color=blue>%s</color></sub>%s", value.initialValue.description, initialValue));
                            }
                        }
                        else
                        {
                            part.Append(Utils.JavaFormat("<sub><color=blue>%s</color></sub>%s + <sub><color=blue>%s</color></sub>%s * %s", value.initialValue.description, initialValue, value.perLevelValue.description, perLevelValue, Utils.JavaFormat(Utils.GetString("SLv"))));
                        }
                        if (value.key != null)
                        {
                            if (initialValue == 0 && perLevelValue == 0)
                            {
                                continue;
                            }
                            else if (initialValue == 0 || perLevelValue == 0)
                            {
                                part.Append(Utils.JavaFormat(" * %s", value.key.description()));
                            }
                            else
                            {
                                string c = part.ToString();
                                part.Clear();
                                part.Append(Utils.JavaFormat("(%s) * %s", c, value.key.description()));
                            }
                        }
                    }
                    if (part.Length != 0)
                    {
                        expression.Append(part).Append(" + ");
                    }
                }
                if (expression.Length == 0)
                {
                    return "0";
                }
                else
                {
                    expression.RemoveRange(expression.ToString().LastIndexOf(" +"), expression.Length);
                    return hasBracesIfNeeded ? bracesIfNeeded(expression.ToString()) : expression.ToString();
                }
            }
            else
            {
                double fixedValue = 0.0;
                foreach (ActionValue value in actionValues)
                {
                    double part = .0;
                    if (value.initial != null && value.perLevel != null)
                    {
                        double initialValue = double.Parse(value.initial);
                        double perLevelValue = double.Parse(value.perLevel);
                        part = initialValue + (perLevelValue * (level));
                    }
                    if (value.key != null)
                    {
                        part = part * ((property.getItem(value.key)));
                    }
                    //                int num = (int)part;
                    //                if (UnitUtils.Companion.approximately(part, (double)num)) {
                    //                    part = num;
                    //                }
                    fixedValue = fixedValue + (part);
                }
                if (roundingMode == RoundingMode.UNNECESSARY)
                {
                    return fixedValue.ToString();
                }

                return Utils.Round(fixedValue, roundingMode);
            }
        }

        public List<ActionValue> actionValues = new();

        public void setActionValues(List<ActionValue> actionValues)
        {
            this.actionValues = actionValues;
        }
        public List<ActionValue> getActionValues()
        {
            return actionValues;
        }

        public class ActionValue
        {
            public string initial;
            public string perLevel;
            public PropertyKey? key;
            public DoubleValue initialValue;
            public DoubleValue perLevelValue;

            public ActionValue(DoubleValue initial, DoubleValue perLevel, PropertyKey? key)
            {
                initialValue = initial;
                perLevelValue = perLevel;
                this.initial = initial.valueString();
                this.perLevel = perLevel.valueString();
                this.key = key;
            }

            public ActionValue(double initial, double perLevel, eActionValue vInitial, eActionValue vPerLevel, PropertyKey? key)
            {
                initialValue = new DoubleValue(initial, vInitial);
                perLevelValue = new DoubleValue(perLevel, vPerLevel);
                this.initial = (initial).ToString();
                this.perLevel = (perLevel).ToString();
                this.key = key;
            }
        }
    }

    public enum PercentModifier
    {
        percent,
        number
    }

    public enum ClassModifier
    {
        unknown = 0,
        physical = 1,
        magical = 2,
        inevitablePhysical = 3
    }

    public enum CriticalModifier
    {
        normal = 0,
        critical = 1
    }

    public enum PropertyKey
    {
        atk,
        def,
        dodge,
        energyRecoveryRate,
        energyReduceRate,
        hp,
        hpRecoveryRate,
        lifeSteal,
        magicCritical,
        magicDef,
        magicPenetrate,
        magicStr,
        physicalCritical,
        physicalPenetrate,
        waveEnergyRecovery,
        waveHpRecovery,
        accuracy,
        unknown
    }

    public enum RoundingMode
    {
        UNNECESSARY,
        FLOOR,
        CEILING,
        DOWN,
        UP,
        HALF_UP
    }
}
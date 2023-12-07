using System;
using ActionParameterSerializer.Actions;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ActionParameterSerializer
{
    public class Property
    {
        public double getItem(PropertyKey? key)
        {
            switch (key)
            {
                case PropertyKey.atk:
                    return this.atk;
                case PropertyKey.def:
                    return this.def;

                case PropertyKey.dodge:
                    return this.dodge;

                case PropertyKey.energyRecoveryRate:
                    return this.energyRecoveryRate;

                case PropertyKey.energyReduceRate:
                    return this.energyReduceRate;

                case PropertyKey.hp:
                    return this.hp;

                case PropertyKey.hpRecoveryRate:
                    return this.hpRecoveryRate;

                case PropertyKey.lifeSteal:
                    return this.lifeSteal;

                case PropertyKey.magicCritical:
                    return this.magicCritical;

                case PropertyKey.magicDef:
                    return this.magicDef;

                case PropertyKey.magicPenetrate:
                    return this.magicPenetrate;

                case PropertyKey.magicStr:
                    return this.magicStr;

                case PropertyKey.physicalCritical:
                    return this.physicalCritical;

                case PropertyKey.physicalPenetrate:
                    return this.physicalPenetrate;

                case PropertyKey.waveEnergyRecovery:
                    return this.waveEnergyRecovery;

                case PropertyKey.waveHpRecovery:
                    return this.waveHpRecovery;

                case PropertyKey.accuracy:
                    return this.accuracy;

                default:
                    return 0;
            }
        }

        public static Property getPropertyWithKeyAndValue(Property property, PropertyKey key, double value)
        {
            if (property == null)
            {
                property = new Property();
            }

            switch (key)
            {
                case PropertyKey.atk:
                    property.atk += value;
                    return property;
                case PropertyKey.def:
                    property.def += value;
                    return property;
                case PropertyKey.dodge:
                    property.dodge += value;
                    return property;
                case PropertyKey.energyRecoveryRate:
                    property.energyRecoveryRate += value;
                    return property;
                case PropertyKey.energyReduceRate:
                    property.energyReduceRate += value;
                    return property;
                case PropertyKey.hp:
                    property.hp += value;
                    return property;
                case PropertyKey.hpRecoveryRate:
                    property.hpRecoveryRate += value;
                    return property;
                case PropertyKey.lifeSteal:
                    property.lifeSteal += value;
                    return property;
                case PropertyKey.magicCritical:
                    property.magicCritical += value;
                    return property;
                case PropertyKey.magicDef:
                    property.magicDef += value;
                    return property;
                case PropertyKey.magicPenetrate:
                    property.magicPenetrate += value;
                    return property;
                case PropertyKey.magicStr:
                    property.magicStr += value;
                    return property;
                case PropertyKey.physicalCritical:
                    property.physicalCritical += value;
                    return property;
                case PropertyKey.physicalPenetrate:
                    property.physicalPenetrate += value;
                    return property;
                case PropertyKey.waveEnergyRecovery:
                    property.waveEnergyRecovery += value;
                    return property;
                case PropertyKey.waveHpRecovery:
                    property.waveHpRecovery += value;
                    return property;
                case PropertyKey.accuracy:
                    property.accuracy += value;
                    return property;
                default:
                    return property;
            }
        }

        public double hp;
        public double atk;
        public double magicStr;
        public double def;
        public double magicDef;
        public double physicalCritical;
        public double magicCritical;
        public double waveHpRecovery;
        public double waveEnergyRecovery;
        public double dodge;
        public double physicalPenetrate;
        public double magicPenetrate;
        public double lifeSteal;
        public double hpRecoveryRate;
        public double energyRecoveryRate;
        public double energyReduceRate;
        public double accuracy;

    }

    public class SkillAction
    {
        public ActionParameter parameter;
        public int getActionId()
        {
            return 0;
        }
    }

    public class UserSettings
    {
        public const int EXPRESSION_EXPRESSION = 0;
        public const int EXPRESSION_ORIGINAL = 1;
        public const int EXPRESSION_VALUE = 2;
        public int getExpression()
        {
            return expression;
        }

        public static int expression = EXPRESSION_ORIGINAL;

        public static UserSettings get()
        {
            return new UserSettings();
        }
    }
    public static class EnumEx
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<long, string>> descDictionary = new ConcurrentDictionary<Type, Dictionary<long, string>>();

        public static string GetDescription(this Enum input)
        {
            return input.GetDescription<Attribute>();
        }

        public static string GetDescription<TArrtibute>(this Enum input, string attrPropName = "Description") where TArrtibute : Attribute
        {
            RegisterDescription(input.GetType(), typeof(TArrtibute), out Dictionary<long, string>? dictionary, attrPropName);
            long key = Convert.ToInt64(input);
            if (dictionary != null && dictionary.Count > 0 && dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            return input?.ToString();
        }

        public static IDictionary<TEnum, string> GetDescriptions<TEnum>() where TEnum : struct
        {
            Type typeFromHandle = typeof(TEnum);
            if (typeFromHandle.IsEnum && descDictionary.TryGetValue(typeFromHandle, out Dictionary<long, string>? value) && value != null)
            {
                return value.ToDictionary((KeyValuePair<long, string> k) => Enum.TryParse<TEnum>(k.Key.ToString(), ignoreCase: true, out TEnum result) ? result : result, (KeyValuePair<long, string> v) => v.Value);
            }
            return null;
        }

        public static void RegisterDescription<TEnum, TArrtibute>(string attrPropName = "Description") where TEnum : struct where TArrtibute : Attribute
        {
            RegisterDescription(typeof(TEnum), typeof(TArrtibute), out Dictionary<long, string> _, attrPropName);
        }

        public static void RegisterDescription<TEnum>() where TEnum : struct
        {
            RegisterDescription<TEnum, DescriptionAttribute>();
        }

        public static void RegisterDescription<TEnum>(IDictionary<TEnum, string> dictionary) where TEnum : struct
        {
            Type typeFromHandle = typeof(TEnum);
            if (typeFromHandle.IsEnum)
            {
                Dictionary<long, string> dic = dictionary.ToDictionary((KeyValuePair<TEnum, string> k) => Convert.ToInt64(k.Key), (KeyValuePair<TEnum, string> v) => v.Value);
                descDictionary.AddOrUpdate(typeFromHandle, dic, (Type k, Dictionary<long, string> v) => dic);
            }
        }

        private static void RegisterDescription(Type enumType, Type attrType, out Dictionary<long, string> dictionary, string attrPropName)
        {
            dictionary = null;
            if (!enumType.IsEnum || descDictionary.TryGetValue(enumType, out dictionary))
            {
                return;
            }
            Array values = Enum.GetValues(enumType);
            dictionary = new Dictionary<long, string>();
            foreach (object item in values)
            {
                string text = item.ToString();
                object[] customAttributes = enumType.GetField(text).GetCustomAttributes(attrType, inherit: false);
                if (customAttributes.Length != 0)
                {
                    PropertyInfo property = customAttributes[0].GetType().GetProperty(attrPropName);
                    if (property != null)
                    {
                        text = property.GetValue(customAttributes[0]).ToString();
                    }
                }
                if (!dictionary.ContainsKey(Convert.ToInt64(item)))
                {
                    dictionary.Add(Convert.ToInt64(item), text);
                }
            }
            descDictionary.TryAdd(enumType, dictionary);
        }
    }
    public static class Utils
    {
        public static ExclusiveAllType exclusiveWithAll(this TargetType type)
        {
            switch (type)
            {
                case TargetType.unknown:
                case TargetType.magic:
                case TargetType.physics:
                case TargetType.summon:
                case TargetType.boss:
                    return ExclusiveAllType.not;
                case TargetType.nearWithoutSelf:
                    return ExclusiveAllType.halfExclusive;
                default:
                    return ExclusiveAllType.exclusive;
            }
        }
        public static AuraAction.AuraActionType toggle(this AuraAction.AuraActionType type)
        {
            switch (type)
            {
                case AuraAction.AuraActionType.raise: return AuraAction.AuraActionType.reduce;
                case AuraAction.AuraActionType.reduce: return AuraAction.AuraActionType.raise;
            }
            return AuraAction.AuraActionType.raise;
        }

        private static Regex re = new Regex(@"%((\d)\$)?[ds]", RegexOptions.Compiled);

        public static string JavaFormat(string format, params object[] args)
        {
            var i = 0;
            return re.Replace(format, match =>
            {
                if (match.Groups[1].Success) return args[int.Parse(match.Groups[2].Value) - 1].ToString();
                return args[i++].ToString();
            }).Replace("%%", "%");
            //$"{format}({string.Join(",", args)})");
        }

        public static PluralModifier pluralModifier(this TargetCount count)
        {
            if (count == TargetCount.one)
            {
                return PluralModifier.one;
            }
            else
            {
                return PluralModifier.many;
            }
        }

        public static string path = Path.Combine(Application.streamingAssetsPath, "string.json");

        private static Dictionary<string, string> cache;

        public static string GetString(string name)
        {
            if (cache == null)
                cache = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path));

            if (cache.TryGetValue(name, out var val)) return val;
            if (cache.TryGetValue(name.ToUpper(), out val)) return val;
            if (cache.TryGetValue(name[..1].ToUpper() + name[1..], out val)) return val;

            return name;
#if DEBUG
            throw new NotImplementedException();
#else
            return "不明效果";
#endif
        }

        private static readonly Regex re2 = new Regex($"[A-Z]", RegexOptions.Compiled);

        public static string description(this Enum t)
        {
            return GetString(re2.Replace(t.GetDescription(), match => "_" + match.Groups[0].Value.ToLower()));
        }
        public static string rawDescription(this Enum t)
        {
            return GetString(t.GetDescription());
        }

        public static string description(this AuraAction.AuraType val)
        {
            switch (val)
            {
                case AuraAction.AuraType.moveSpeed: return Utils.GetString("Move_Speed");
                case AuraAction.AuraType.physicalCriticalDamage: return Utils.GetString("Physical_Critical_Damage");
                case AuraAction.AuraType.magicalCriticalDamage: return Utils.GetString("Magical_Critical_Damage");
                case AuraAction.AuraType.accuracy: return PropertyKey.accuracy.description();
                case AuraAction.AuraType.receivedCriticalDamage: return Utils.GetString("Received_Critical_Damage");
                case AuraAction.AuraType.receivedDamage: return Utils.GetString("received_damage");
                case AuraAction.AuraType.receivedPhysicalDamage: return Utils.GetString("received_physical_damage");
                case AuraAction.AuraType.receivedMagicalDamage: return Utils.GetString("received_magical_damage");
                case AuraAction.AuraType.maxHP: return Utils.GetString("max_HP");
                default:
                    if (Enum.TryParse<PropertyKey>(val.ToString(), out var key))
                        return key.description();
                    return ((Enum) val).description();
            }
        }

        public static string description(this PercentModifier val)
        {
            switch (val)
            {
                case PercentModifier.percent: return "%";
            }

            return "";
        }

        public static string description(this AuraAction.AuraActionType val)
        {
            switch (val)
            {
                case AuraAction.AuraActionType.raise: return GetString("Raise");
                case AuraAction.AuraActionType.reduce: return GetString("Reduce");
            }
            return GetString("Raise");
        }

        public static string description(this ActionParameter.eActionValue val)
        {
            return GetString(val.ToString().ToLower());
        }

        public static string description(this PropertyKey? key)
        {
            return key.Value.description();
        }

        public static string description(this PropertyKey key)
        {
            switch (key)
            {
                case PropertyKey.atk: return Utils.GetString("ATK");
                case PropertyKey.def: return Utils.GetString("DEF");
                case PropertyKey.dodge: return Utils.GetString("Dodge");
                case PropertyKey.energyRecoveryRate: return Utils.GetString("Energy_Recovery_Rate");
                case PropertyKey.energyReduceRate: return Utils.GetString("Energy_Reduce_Rate");
                case PropertyKey.hp: return Utils.GetString("HP");
                case PropertyKey.hpRecoveryRate: return Utils.GetString("HP_Recovery_Rate");
                case PropertyKey.lifeSteal: return Utils.GetString("Life_Steal");
                case PropertyKey.magicCritical: return Utils.GetString("Magic_Critical");
                case PropertyKey.magicDef: return Utils.GetString("Magic_DEF");
                case PropertyKey.magicPenetrate: return Utils.GetString("Magic_Penetrate");
                case PropertyKey.magicStr: return Utils.GetString("Magic_STR");
                case PropertyKey.physicalCritical: return Utils.GetString("Physical_Critical");
                case PropertyKey.physicalPenetrate: return Utils.GetString("Physical_Penetrate");
                case PropertyKey.waveEnergyRecovery: return Utils.GetString("Wave_Energy_Recovery");
                case PropertyKey.waveHpRecovery: return Utils.GetString("Wave_HP_Recovery");
                case PropertyKey.accuracy: return Utils.GetString("Accuracy");
                default: return Utils.GetString("Unknown");
            }
        }

        public static bool ignoresOne(this TargetType type)
        {
            switch (type)
            {
                case TargetType.unknown:
                case TargetType.random:
                case TargetType.randomOnce:
                case TargetType.absolute:
                case TargetType.summon:
                case TargetType.selfSummonRandom:
                case TargetType.allSummonRandom:
                case TargetType.magic:
                case TargetType.physics:
                    return false;
                default:
                    return true;
            }
        }
        public static string roundDownDouble(double value)
        {
            return (Math.Floor(value)).ToString();
        }
        public static string roundUpDouble(double value)
        {
            return (Math.Ceiling(value)).ToString();
        }
        public static string roundDouble(double value)
        {
            return (Math.Round(value)).ToString();
        }
        public static string roundIfNeed(double value)
        {
            if (value % 1 == 0)
            {
                return roundDouble(value);
            }
            else
            {
                return (value).ToString();
            }
        }

        public static StringBuilder RemoveRange(this StringBuilder sb, int start, int end) =>
            sb.Remove(start, end - start);

        public static string description(this TargetType type)
        {
            switch (type)
            {
                case TargetType.unknown:
                    return Utils.JavaFormat(Utils.GetString("unknown"));
                case TargetType.random:
                case TargetType.randomOnce:
                    return Utils.JavaFormat(Utils.GetString("random"));
                case TargetType.zero:
                case TargetType.near:
                case TargetType.none:
                    return Utils.JavaFormat(Utils.GetString("the_nearest"));
                case TargetType.far:
                    return Utils.JavaFormat(Utils.GetString("the_farthest"));
                case TargetType.hpAscending:
                case TargetType.hpAscendingOrNear:
                    return Utils.JavaFormat(Utils.GetString("the_lowest_HP_ratio"));
                case TargetType.hpAscendingOrNearForward:
                    return Utils.JavaFormat(Utils.GetString("the_lowest_HP"));
                case TargetType.hpDescendingOrNearForward:
                    return Utils.JavaFormat(Utils.GetString("the_highest_HP"));
                case TargetType.hpDescending:
                case TargetType.hpDescendingOrNear:
                    return Utils.JavaFormat(Utils.GetString("the_highest_HP_ratio"));
                case TargetType.self:
                    return Utils.JavaFormat(Utils.GetString("self"));
                case TargetType.forward:
                    return Utils.JavaFormat(Utils.GetString("the_most_backward"));
                case TargetType.backward:
                    return Utils.JavaFormat(Utils.GetString("the_most_forward"));
                case TargetType.absolute:
                    return Utils.JavaFormat(Utils.GetString("targets_within_the_scope"));
                case TargetType.tpDescending:
                case TargetType.tpDescendingOrNear:
                case TargetType.tpDescendingOrMaxForward:
                    return Utils.JavaFormat(Utils.GetString("the_highest_TP"));
                case TargetType.tpAscending:
                case TargetType.tpReducing:
                case TargetType.tpAscendingOrNear:
                    return Utils.JavaFormat(Utils.GetString("the_lowest_TP"));
                case TargetType.atkDescending:
                case TargetType.atkDescendingOrNear:
                case TargetType.atkDecForwardWithoutOwner:
                    return Utils.JavaFormat(Utils.GetString("the_highest_ATK"));
                case TargetType.atkAscending:
                case TargetType.atkAscendingOrNear:
                    return Utils.JavaFormat(Utils.GetString("the_lowest_ATK"));
                case TargetType.magicSTRDescending:
                case TargetType.magicSTRDescendingOrNear:
                    return Utils.JavaFormat(Utils.GetString("the_highest_Magic_STR"));
                case TargetType.magicSTRAscending:
                case TargetType.magicSTRAscendingOrNear:
                    return Utils.JavaFormat(Utils.GetString("the_lowest_Magic_STR"));
                case TargetType.summon:
                    return Utils.JavaFormat(Utils.GetString("minion"));
                case TargetType.physics:
                    return Utils.JavaFormat(Utils.GetString("physics"));
                case TargetType.magic:
                    return Utils.JavaFormat(Utils.GetString("magic"));
                case TargetType.allSummonRandom:
                    return Utils.JavaFormat(Utils.GetString("random_minion"));
                case TargetType.selfSummonRandom:
                    return Utils.JavaFormat(Utils.GetString("random_self_minion"));
                case TargetType.boss:
                    return Utils.JavaFormat(Utils.GetString("boss"));
                case TargetType.shadow:
                    return Utils.JavaFormat(Utils.GetString("shadow"));
                case TargetType.nearWithoutSelf:
                    return Utils.JavaFormat(Utils.GetString("nearest_without_self"));
                case TargetType.bothAtkDescending:
                    return Utils.JavaFormat(Utils.GetString("the_highest_ATK_or_Magic_STR"));
                case TargetType.bothAtkAscending:
                    return Utils.JavaFormat(Utils.GetString("the_lowest_ATK_or_Magic_STR"));
                case TargetType.energyAscBackWithoutOwner:
                    return Utils.JavaFormat(Utils.GetString("the_lowest_TP_except_self"));
                case TargetType.atkDefAscForward:
                    return Utils.JavaFormat(Utils.GetString("the_lowest_DEF"));
                case TargetType.magicDefAscForward:
                    return Utils.JavaFormat(Utils.GetString("the_lowest_Magic_DEF"));
                default:
                    return Utils.JavaFormat(((Enum)type).description());
            }
        }
        
        public static string description(this TargetType type, TargetNumber targetNumber, string localizedNumber)
        {

            if (targetNumber == TargetNumber.second
                || targetNumber == TargetNumber.third
                || targetNumber == TargetNumber.fourth
                || targetNumber == TargetNumber.fifth)
            {

                string localizedModifier = localizedNumber == null ? targetNumber.description() : localizedNumber;
                switch (type)
                {
                    case TargetType.unknown:
                        return Utils.JavaFormat(Utils.GetString("the_s_unknown_type"), localizedModifier);
                    case TargetType.zero:
                    case TargetType.near:
                    case TargetType.none:
                        return Utils.JavaFormat(Utils.GetString("the_s_nearest"), localizedModifier);
                    case TargetType.far:
                        return Utils.JavaFormat(Utils.GetString("the_s_farthest"), localizedModifier);
                    case TargetType.hpAscending:
                    case TargetType.hpAscendingOrNear:
                        return Utils.JavaFormat(Utils.GetString("the_s_lowest_HP_ratio"), localizedModifier);
                    case TargetType.hpDescending:
                    case TargetType.hpDescendingOrNear:
                        return Utils.JavaFormat(Utils.GetString("the_s_highest_HP_ratio"), localizedModifier);
                    case TargetType.hpAscendingOrNearForward:
                        return Utils.JavaFormat(Utils.GetString("the_s_lowest_HP"), localizedModifier);
                    case TargetType.hpDescendingOrNearForward:
                        return Utils.JavaFormat(Utils.GetString("the_s_highest_HP"), localizedModifier);
                    case TargetType.forward:
                        return Utils.JavaFormat(Utils.GetString("the_s_most_backward"), localizedModifier);
                    case TargetType.backward:
                        return Utils.JavaFormat(Utils.GetString("the_s_most_forward"), localizedModifier);
                    case TargetType.tpDescending:
                    case TargetType.tpDescendingOrNear:
                        return Utils.JavaFormat(Utils.GetString("the_s_highest_TP"), localizedModifier);
                    case TargetType.tpAscending:
                    case TargetType.tpReducing:
                    case TargetType.tpAscendingOrNear:
                        return Utils.JavaFormat(Utils.GetString("the_s_lowest_TP"), localizedModifier);
                    case TargetType.atkDescending:
                    case TargetType.atkDescendingOrNear:
                        return Utils.JavaFormat(Utils.GetString("the_s_highest_ATK"), localizedModifier);
                    case TargetType.atkAscending:
                    case TargetType.atkAscendingOrNear:
                        return Utils.JavaFormat(Utils.GetString("the_s_lowest_ATK"), localizedModifier);
                    case TargetType.magicSTRDescending:
                    case TargetType.magicSTRDescendingOrNear:
                        return Utils.JavaFormat(Utils.GetString("the_s_highest_Magic_STR"), localizedModifier);
                    case TargetType.magicSTRAscending:
                    case TargetType.magicSTRAscendingOrNear:
                        return Utils.JavaFormat(Utils.GetString("the_s_lowest_Magic_STR"), localizedModifier);
                    case TargetType.nearWithoutSelf:
                        return Utils.JavaFormat(Utils.GetString("the_s_nearest_without_self"));
                    case TargetType.bothAtkDescending:
                        return Utils.JavaFormat(Utils.GetString("the_s_highest_ATK_or_Magic_STR"), localizedModifier);
                    case TargetType.bothAtkAscending:
                        return Utils.JavaFormat(Utils.GetString("the_s_lowest_ATK_or_Magic_STR"), localizedModifier);
                    case TargetType.energyAscBackWithoutOwner:
                        return Utils.JavaFormat(Utils.GetString("the_s_th_lowest_TP_except_self"), localizedModifier);
                    case TargetType.atkDefAscForward:
                        return Utils.JavaFormat(Utils.GetString("the_s_lowest_DEF"), localizedModifier);
                    case TargetType.magicDefAscForward:
                        return Utils.JavaFormat(Utils.GetString("the_s_lowest_Magic_DEF"), localizedModifier);
                    default:
                        return Utils.JavaFormat("s_" + ((Enum)type).description(), localizedModifier);
                }
            }
            else
            {
                return type.description();
            }
        }

        public static string Round(double bigDecimal, RoundingMode? roundingMode)
        {
            return bigDecimal.ToString();
        }
    }
}

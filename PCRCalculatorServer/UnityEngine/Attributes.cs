using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    namespace Internal
    {

        [Serializable]
        [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
        public class DefaultValueAttribute : Attribute
        {
            private object DefaultValue;

            public object Value => DefaultValue;

            public DefaultValueAttribute(string value)
            {
                DefaultValue = value;
            }

            public override bool Equals(object obj)
            {
                if (!(obj is DefaultValueAttribute defaultValueAttribute))
                {
                    return false;
                }
                if (DefaultValue == null)
                {
                    return defaultValueAttribute.Value == null;
                }
                return DefaultValue.Equals(defaultValueAttribute.Value);
            }

            public override int GetHashCode()
            {
                if (DefaultValue == null)
                {
                    return base.GetHashCode();
                }
                return DefaultValue.GetHashCode();
            }
        }

    }
    public class Header(string _) : Attribute { }
    public class SpineAnimation(params object[] _) : Attribute
    {

    }
    public sealed class TextArea : Attribute
    {
    }

    public sealed class ContextMenu : Attribute
    {
        public ContextMenu(string _) { }
    }
    public sealed class AddComponentMenu : Attribute
    {
        private string m_AddComponentMenu;

        private int m_Ordering;

        public string componentMenu => m_AddComponentMenu;

        public int componentOrder => m_Ordering;

        public AddComponentMenu(string menuName)
        {
            m_AddComponentMenu = menuName;
            m_Ordering = 0;
        }

        public AddComponentMenu(string menuName, int order)
        {
            m_AddComponentMenu = menuName;
            m_Ordering = order;
        }
    }

    public sealed class ExecuteAlways : Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class CreateAssetMenuAttribute : Attribute
    {
        public string menuName { get; set; }

        public string fileName { get; set; }

        public int order { get; set; }
    }

    [VisibleToOtherModules]
    [AttributeUsage(AttributeTargets.Parameter)]
    internal class NotNullAttribute : Attribute
    {
        public string Exception { get; set; }

        public NotNullAttribute(string exception = "ArgumentNullException")
        {
            Exception = exception;
        }
    }

    [Serializable]
    public class ExcludeFromDocsAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property)]
    [VisibleToOtherModules]
    internal class NativeConditionalAttribute : Attribute
    {
        public string Condition { get; set; }

        public string StubReturnStatement { get; set; }

        public bool Enabled { get; set; }

        public NativeConditionalAttribute()
        {
        }

        public NativeConditionalAttribute(string condition)
        {
            Condition = condition;
            Enabled = true;
        }

        public NativeConditionalAttribute(bool enabled)
        {
            Enabled = enabled;
        }

        public NativeConditionalAttribute(string condition, bool enabled)
            : this(condition)
        {
            Enabled = enabled;
        }

        public NativeConditionalAttribute(string condition, string stubReturnStatement, bool enabled)
            : this(condition, stubReturnStatement)
        {
            Enabled = enabled;
        }

        public NativeConditionalAttribute(string condition, string stubReturnStatement)
            : this(condition)
        {
            StubReturnStatement = stubReturnStatement;
        }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public abstract class PropertyAttribute : Attribute
    {
        public int order { get; set; }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class TooltipAttribute : PropertyAttribute
    {
        public readonly string tooltip;

        public TooltipAttribute(string tooltip)
        {
            this.tooltip = tooltip;
        }
    }
    [VisibleToOtherModules]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
    internal class VisibleToOtherModulesAttribute : Attribute
    {
        public VisibleToOtherModulesAttribute()
        {
        }

        public VisibleToOtherModulesAttribute(params string[] modules)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
    [VisibleToOtherModules]
    internal class NativeMethodAttribute : Attribute
    {
        public string Name { get; set; }

        public bool IsThreadSafe { get; set; }

        public bool IsFreeFunction { get; set; }

        public bool ThrowsException { get; set; }

        public bool HasExplicitThis { get; set; }

        public bool WritableSelf { get; set; }

        public NativeMethodAttribute()
        {
        }

        public NativeMethodAttribute(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (name == "")
            {
                throw new ArgumentException("name cannot be empty", "name");
            }
            Name = name;
        }

        public NativeMethodAttribute(string name, bool isFreeFunction)
            : this(name)
        {
            IsFreeFunction = isFreeFunction;
        }

        public NativeMethodAttribute(string name, bool isFreeFunction, bool isThreadSafe)
            : this(name, isFreeFunction)
        {
            IsThreadSafe = isThreadSafe;
        }

        public NativeMethodAttribute(string name, bool isFreeFunction, bool isThreadSafe, bool throws)
            : this(name, isFreeFunction, isThreadSafe)
        {
            ThrowsException = throws;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    [VisibleToOtherModules]
    internal class FreeFunctionAttribute : NativeMethodAttribute
    {
        public FreeFunctionAttribute()
        {
            base.IsFreeFunction = true;
        }

        public FreeFunctionAttribute(string name)
            : base(name, isFreeFunction: true)
        {
        }

        public FreeFunctionAttribute(string name, bool isThreadSafe)
            : base(name, isFreeFunction: true, isThreadSafe)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    [RequiredByNativeCode]
    public sealed class RequireComponent : Attribute
    {
        public Type m_Type0;

        public Type m_Type1;

        public Type m_Type2;

        public RequireComponent(Type requiredComponent)
        {
            m_Type0 = requiredComponent;
        }

        public RequireComponent(Type requiredComponent, Type requiredComponent2)
        {
            m_Type0 = requiredComponent;
            m_Type1 = requiredComponent2;
        }

        public RequireComponent(Type requiredComponent, Type requiredComponent2, Type requiredComponent3)
        {
            m_Type0 = requiredComponent;
            m_Type1 = requiredComponent2;
            m_Type2 = requiredComponent3;
        }
    }

    [VisibleToOtherModules]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, Inherited = false)]
    internal class RequiredByNativeCodeAttribute : Attribute
    {
        public string Name { get; set; }

        public bool Optional { get; set; }

        public bool GenerateProxy { get; set; }

        public RequiredByNativeCodeAttribute()
        {
        }

        public RequiredByNativeCodeAttribute(string name)
        {
            Name = name;
        }

        public RequiredByNativeCodeAttribute(bool optional)
        {
            Optional = optional;
        }

        public RequiredByNativeCodeAttribute(string name, bool optional)
        {
            Name = name;
            Optional = optional;
        }
    }

    public sealed class SerializeField : Attribute
    {
    }
    public enum TypeInferenceRules
    {
        TypeReferencedByFirstArgument,
        TypeReferencedBySecondArgument,
        ArrayOfTypeReferencedByFirstArgument,
        TypeOfFirstArgument
    }

    [Serializable]
    [AttributeUsage(AttributeTargets.Method)]
    public class TypeInferenceRuleAttribute : Attribute
    {
        private readonly string _rule;

        public TypeInferenceRuleAttribute(TypeInferenceRules rule)
            : this(rule.ToString())
        {
        }

        public TypeInferenceRuleAttribute(string rule)
        {
            _rule = rule;
        }

        public override string ToString()
        {
            return _rule;
        }
    }


}

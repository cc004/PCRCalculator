using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    public static class Utils
    {
        public static IEnumerable<FieldInfo> GetAllSerializedFields(this Type type)
        {
            BindingFlags flags = BindingFlags.Instance |
                                 BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.DeclaredOnly;

            foreach (var field in type.GetFields(flags))
            {
                if (field.IsPublic && !Attribute.IsDefined(field, typeof(NonSerializedAttribute)) || Attribute.IsDefined(field, typeof(SerializeField)))
                    yield return field;
            }

            if (type.BaseType != typeof(object))
            {
                foreach (var baseField in GetAllSerializedFields(type.BaseType))
                    yield return baseField;
            }
        }

    }
}

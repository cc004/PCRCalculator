﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace AssetsTools.Dynamic {
    /// <summary>
    /// Dynamic Asset Object.
    /// </summary>
    public partial class DynamicAsset : DynamicObject, IDynamicAssetBase {
#if DEBUG
        public
#else
        internal
#endif
            Dictionary<string, object> objects;
        private string proto_name;

        /// <summary>
        /// TypeName(Path) of this object.
        /// </summary>
        public string TypeName => proto_name;

        public DynamicAsset() {
        }

#if DEBUG
        public
#else
        internal
#endif
            DynamicAsset(Dictionary<string, object> dic, string proto_name) {
            objects = dic;
            this.proto_name = proto_name;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            return objects.TryGetValue(binder.Name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value) {
            IDynamicAssetBase ival = value as IDynamicAssetBase;
            object target = null;

            // If member exists
            if (!objects.TryGetValue(binder.Name, out target))
                return false;
            // If primitive type matches
            else if (target.GetType() != value.GetType())
                throw new TypeMismatchException("The type of `" + binder.Name + "` is `" + target.GetType().GetCSharpName() + "` but got `" + value.GetType().GetCSharpName() + "`");
            // If object type matches
            else if((ival != null && ival.TypeName != ((IDynamicAssetBase)target).TypeName))
                throw new TypeMismatchException("The type of `" + binder.Name + "` is `" + ((IDynamicAssetBase)target).TypeName + "` but got `" + ival.TypeName + "`");

            // Passed all checkes, now update the value
            objects[binder.Name] = value;
            return true;
        }

        /// <summary>
        /// Determines whether this object has the specified member.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool HasMember(string name) => objects.ContainsKey(name);

        /// <summary>
        /// Cast to dynamic(equivalent of (dynamic)this)
        /// </summary>
        /// <returns></returns>
        public dynamic AsDynamic() => this;

        public override int GetHashCode() {
            int hash = proto_name.GetHashCode();
            foreach (var kv in objects) {
                hash ^= kv.Key.GetHashCode() ^ kv.Value.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        /// Get the Prototype of this object.
        /// </summary>
        /// <returns></returns>
        public DynamicAsset GetPrototype() {
            return PrototypeDic[proto_name];
        }

        public class TypeMismatchException : Exception {
            public TypeMismatchException() { }

            public TypeMismatchException(string message) : base(message) { }
        }
    }
}

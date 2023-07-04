using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetsTools {
    public static class IOLibExtensions {
        public static void Align(this UnityBinaryReader reader, int align)
        {
            int pad = align - (reader.Position % align);
            if (pad != align) reader.Position += pad;
        }

        public static void Align(this UnityBinaryWriter writer, int align)
        {
            int pad = align - (writer.Position % align);
            if (pad != align) writer.Position += pad;
        }
        
        public static string ReadAlignedString(this UnityBinaryReader reader) {
            string str = reader.ReadString(reader.ReadInt());
            reader.Align(4);
            return str;
        }

        public static void WriteAlignedString(this UnityBinaryWriter writer, string str) {
            writer.WriteString(str);
            writer.Align(4);
        }
    }
}

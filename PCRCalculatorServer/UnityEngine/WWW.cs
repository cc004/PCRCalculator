using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityEngine
{
    public class WWW
    {
        internal bool isDone = true;
        internal AssetBundle assetBundle => AssetBundle.LoadFromMemory(bytes);

        public WWW(string url)
        {
            try
            {
                bytes = File.ReadAllBytes(url.Replace("file://", ""));
            }
            catch
            {
                // Console.WriteLine($"www impl: failed to read file: " + url);
            }
        }

        public byte[] bytes { get; private set; }
    }
}

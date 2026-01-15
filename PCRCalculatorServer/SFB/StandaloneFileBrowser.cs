using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFB
{
    public class ExtensionFilter(params object[] _)
    {

    }
    public class StandaloneFileBrowser
    {
        internal static string[] OpenFilePanel(string v1, string empty, ExtensionFilter[] extensionFilters, bool v2)
        {
            throw new NotImplementedException();
        }

        internal static string[] OpenFilePanel(string v1, string empty, string v2, bool v3)
        {
            throw new NotImplementedException();
        }

        internal static string? SaveFilePanel(string v1, string empty, string fileName, string v2)
        {
            throw new NotImplementedException();
        }
    }
}

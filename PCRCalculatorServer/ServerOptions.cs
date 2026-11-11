using System;
using System.IO;
using Newtonsoft.Json;
using PCRCaculator;

namespace PCRCalculatorServer
{
    /// <summary>
    /// Holds server runtime switches derived from command line arguments.
    /// </summary>
    public sealed class ServerOptions
    {
        public bool DebugMode { get; init; }

        public static void ApplyClientContext()
        {
            var version = Environment.GetEnvironmentVariable("PCRCALC_VERSION_DATA");
            if (!string.IsNullOrWhiteSpace(version))
                MainManager.Instance.Version = JsonConvert.DeserializeObject<MainManager.VersionData>(version)
                    ?? throw new InvalidDataException("PCRCALC_VERSION_DATA cannot be null.");
            var persistentPath = Environment.GetEnvironmentVariable("PCRCALC_PERSISTENT_DATA_PATH");
            if (!string.IsNullOrWhiteSpace(persistentPath))
                UnityEngine.Application.persistentDataPath = Path.GetFullPath(persistentPath);
        }
    }
}

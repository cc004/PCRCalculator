using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft0.Json;

namespace PCRCaculator
{
    public static class SaveManager
    {
        private static Dictionary<string, string> objDict;

        private static string path => MainManager.GetSaveDataPath() + "/../../存档.json";
        private static void LoadAll()
        {
            try
            {
                objDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    File.ReadAllText(path));
            }
            catch (Exception e)
            {
                objDict = new Dictionary<string, string>();
            }
        }

        private static void SaveAll()
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(objDict));
        }

        public static T Load<T>()
        {
            if (objDict == null) LoadAll();
            return JsonConvert.DeserializeObject<T>(objDict[typeof(T).FullName ?? nameof(T)]);
        }

        public static void Save<T>(T t)
        {
            objDict[typeof(T).FullName ?? nameof(T)] = JsonConvert.SerializeObject(t);
            SaveAll();
        }
    }
}
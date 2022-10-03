using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using System.Linq;
#if !UNITY_EDITOR
using System.IO;
#endif

namespace PCRCaculator.SQL
{
    public class SQLiteTool
    {
        public static string DatabaseName = "redive_jp.db";
        public static string DatabaseName_cn = "redive_cn.db";

        public static string GetDBPath(bool cn = false)
        {
#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", cn ? DatabaseName_cn : DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            return dbPath;
        }
        public static SQLiteTool OpenDB(bool cn = false)
        {

            //_connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            //Debug.Log("Final PATH: " + dbPath);
            SQLiteTool temp = new SQLiteTool(GetDBPath(cn), true);
            return temp;
        }   


        SQLiteConnection db = null;
        private SQLiteTool(string path, bool readOnly)
        {
            ConnectDB(path, readOnly);
        }
        private void ConnectDB(string path, bool readOnly)
        {
            // Get an absolute path to the database file
            //var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyData.db");

            db = new SQLiteConnection(path, readOnly ? SQLiteOpenFlags.ReadOnly : SQLiteOpenFlags.ReadWrite);
        }
        public List<T> GetDatas<T>(Func<T, bool> func = null) where T : new()
        {
            
            List<T> list = new List<T>(func == null ? db.Table<T>() : db.Table<T>().Where<T>(func));
            return list;
        }
        public Dictionary<int, T> GetDatasDic<T>(Func<T, int> func, Func<T, bool> select = null) where T : new()
        {
            Dictionary<int, T> dict = new Dictionary<int, T>();
            var list = GetDatas<T>(select);
            foreach (var item in list)
                dict[func(item)] = item;
            return dict;
        }
        public Dictionary<int,List<T>> GetDatasDicList<T>(Func<T, int> func, Func<T, bool> select = null) where T : new()
        {
            Dictionary<int, List<T>> dic = new Dictionary<int, List<T>>();
            var list = GetDatas<T>(select);
            foreach(var dd in list)
            {
                if (dic.TryGetValue(func(dd), out var list1))
                {
                    list1.Add(dd);
                }
                else
                    dic[func(dd)] = new List<T> { dd };
            }
            return dic;
        }


        public int UpdateDB(List<object> list)
        {
            return db.UpdateAll(list);
        }
        public void Commit()
        {
            db.Commit();
        }
        public void CloseDB()
        {
            db.Close();
            db = null;
        }
    }
}
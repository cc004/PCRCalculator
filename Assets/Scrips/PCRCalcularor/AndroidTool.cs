using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Application = UnityEngine.Application;

namespace PCRCaculator
{
    public static class AndroidTool
    {
        public static readonly string libName = "com.pcrfans.mylibrary";
        public static void p()
        {
            AndroidJavaObject Main = new AndroidJavaObject("com.pcrfans.mylibrary.ToolActivity");
            int s = Main.Call<int>("Add", 2, 8);
            Debug.Log($"��������{s}(5)");
        }
        public static void q()
        {
            AndroidJavaObject jc = new AndroidJavaObject("com.pcrfans.mylibrary.TestClass");
            var jo = jc.CallStatic<AndroidJavaObject>("Instance");
            int s = jo.Call<int>("Sub", 3, 2);
            Debug.Log($"��������{s}(1)");
        }

        public static void OpenAndroidFileBrower()
        {
            int ver;
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
                ver = version.GetStatic<int>("SDK_INT");

            Debug.Log($"using sdk version = {ver}");

            if (ver >= 30)
            {
                MainManager.Instance.WindowConfigMessage(
                    "������ʹ��android11�����ϵİ汾����֧���ڲ��洢�Ķ�ȡ��" +
                    "�뽫excel�ļ�����/sdcard/Android/data/com.PCRFans.PCRGuildCalculator/files" +
                    "�£����򽫻�����������",
                    OpenAndroidFileBrowerInternal);
            }
            else OpenAndroidFileBrowerInternal();
        }

        private static void OpenAndroidFileBrowerInternal()
        {
            MainManager.Instance.WindowConfigMessage(
                "�ֻ�ѡȡ�ļ�ʱ����ʹ���ļ���������ļ�ѡȡ�������\"��������\"��ѡȡ�ļ���������",
                OpenAndroidFileBrowerInternal2);
        }

        private static void OpenAndroidFileBrowerInternal2()
        {
            try
            {
                AndroidJavaObject jc = new AndroidJavaObject($"{libName}.GetFilePath");
                var jo = jc.CallStatic<AndroidJavaObject>("GetInstance");
                jo.Call("UnityFunc", "GuildManager", "ImportExcelFileByAndroidNative");
                jo.Call("FileBrower");
            }
            catch (Exception ex)
            {
                Debug.LogError($"{ex.Message}\n{ex.StackTrace}");
            }
        }
        public static void ShowExcelFile(string path)
        {
            try
            {
                AndroidJavaObject Main = new AndroidJavaObject($"{libName}.ToolActivity");
                Main.Call("Show", path);
            }
            catch (Exception ex)
            {
                Debug.LogError($"{ex.Message}\n{ex.StackTrace}");
            }
        }
        public static void UpdateTotalAPK(string newapkPath)
        {
            AndroidJavaObject Main = new AndroidJavaObject($"{libName}.ToolActivity");
            Main.Call("InstallApk2", newapkPath);
        }
        public static string GetExcelPath()
        {
            p();
            q();
            return null;
        }
        public static IEnumerator s()
        {
            string remote = "http://192.168.1.1:5569/Root/3.apk";
            string down = Application.persistentDataPath + "/3.apk";
            UnityWebRequest request = UnityWebRequest.Get(remote);
            yield return request.SendWebRequest();
            byte[] d = request.downloadHandler.data;
            File.WriteAllBytes(down, d);

            //���ð�׿����
            //AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            //AndroidJavaObject context = activity.GetStatic<AndroidJavaObject>("currentActivity");
            string downapk = Application.persistentDataPath + "/3.apk";
            AndroidJavaObject Main = new AndroidJavaObject($"{libName}.MainActivity");
            //Main.Call("InstallApk", context, downapk);

            Main.Call("InstallApk2", downapk);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;

namespace PCRCaculator
{

    public class LoginTool : MonoBehaviour
    {
        public Text msgText;
        public string verSTR;

        private Version version;

        static string? RoutePrefix;
        private const string serverRoot = "ichigo.icu";


        private bool requireCheck;
        private bool isChecking = false;
        private byte[] data;
        private string dataSTR;
        
        public void AddCheckGameStartPostParam()
        {
            CheckGameStartPostParam checkGameStartPostParam = new CheckGameStartPostParam();
            checkGameStartPostParam.device_id = SystemInfo.deviceUniqueIdentifier;
            //checkGameStartPostParam.device_id = new DeviceIdBuilder().AddMachineName().AddMacAddress().ToString();
            dataSTR = Convert.ToBase64String(Encoding.UTF8.GetBytes((JsonConvert.SerializeObject(checkGameStartPostParam))));
            data = Encoding.UTF8.GetBytes(dataSTR);
        }

        public void UpdateButton()
        {
            Check();
        }

        private void Start()
        {
            AddCheckGameStartPostParam();
            version = Version.Parse(verSTR);
            RoutePrefix = "/" + version.Major + "." + version.Minor + "." + version.Build;
            requireCheck = true;

            Check();
        }
        private void Check()
        {
            if (isChecking)
                return;
            StartCoroutine(Check_0());
        }
        private IEnumerator Check_0()
        {
            isChecking = true;
            if (!requireCheck)
            {
                isChecking = false;
                requireCheck = false;
                gameObject.SetActive(false);
                yield break;
            }
            string url = $"http://{serverRoot}{RoutePrefix}/check/game_start";
            var web = UnityWebRequest.Post(url,dataSTR);
            web.SetRequestHeader("User-Agent", $"PCRCalculaltor");
            web.timeout = 30;
            yield return web.SendWebRequest();
            string response = "";
            if (web.result == UnityWebRequest.Result.Success)
            {
                response = web.downloadHandler.text;

                if(string.IsNullOrEmpty(response))
                {
                    requireCheck = false;
                    gameObject.SetActive(false);
                    response = "通过";
                }
            }
            else
            {
                response = "连接服务器失败！";
            }
            msgText.text = response;
            isChecking = false;
        }

        private class CheckGameStartPostParam
        {
            public string device_id { get; set; }
        }

    }
}
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Collections;
using Newtonsoft.Json;
namespace PCRCaculator.Update
{
    public class UpdateManager : MonoBehaviour
    {
        public InputField verCharaJP, verBossJP, verBossCN;
        public Toggle qa, jp, newab;
        public Button prodUpdateButton, jpBossIsShow;
        private bool isProdUpdated = false;
        public GameObject jpBossPanel;
        private const string Url = "https://wthee.xyz/pcr/api/v1/db/info/v2";
        public void StartCheck()
        {
            this.gameObject.SetActive(true);
            verCharaJP.text = MainManager.Instance.Version.CharacterVersionJP.ToString();
            verBossJP.text = MainManager.Instance.Version.BossVersionJP.ToString();
            verBossCN.text = MainManager.Instance.Version.BossVersionCN.ToString();
            qa.isOn = MainManager.Instance.Version.useQA;
            jp.isOn = MainManager.Instance.Version.useJP;
            newab.isOn = MainManager.Instance.Version.newAB;
        }

        public void CancelButton()
        {
            gameObject.SetActive(false);
            prodUpdateButton.GetComponentInChildren<Text>().text = "获取";
            prodUpdateButton.interactable = true;
            isProdUpdated = false;
        }

        public void UpdateButton()
        {
            MainManager.Instance.Version.useQA = qa.isOn;
            MainManager.Instance.Version.useJP = jp.isOn;
            MainManager.Instance.Version.newAB = newab.isOn;
            MainManager.Instance.Version.CharacterVersionJP = long.Parse(verCharaJP.text);
            MainManager.Instance.Version.BossVersionJP = long.Parse(verBossJP.text);
            MainManager.Instance.Version.BossVersionCN = long.Parse(verBossCN.text);

            SaveManager.Save(MainManager.Instance.Version);
            /*
            string path = Application.streamingAssetsPath + "/../.ABExt2";
            if (Application.platform == RuntimePlatform.Android)
            {
                path = Application.persistentDataPath + "/AB";
            }

            Directory.Delete(path, true);
            Directory.CreateDirectory(path);
            */

            gameObject.SetActive(false);
            MainManager.Instance.WindowConfigMessage("更新完成，重启摸轴器生效", Application.Quit, Application.Quit);
        }

        public void ProdUpdateButton()
        {
            if (!isProdUpdated)
            {
              // await ProdUpdate();
                StartCoroutine(SendPostRequest());
            }
            else
            {
                verBossCN.text = prodUpdateButton.GetComponentInChildren<Text>().text;
                qa.isOn = false;
                prodUpdateButton.GetComponentInChildren<Text>().text = "已覆盖版本号并取消QA";
                prodUpdateButton.interactable = false;
            }
        }

        public void jpBossIsShowButton()
        {
            jpBossPanel.SetActive(!jpBossPanel.activeSelf);
        }
        public IEnumerator SendPostRequest()
        {
            prodUpdateButton.GetComponentInChildren<Text>().text = "获取中...";
            using UnityWebRequest www = new(Url, UnityWebRequest.kHttpVerbPOST);
            byte[] formdata = new System.Text.UTF8Encoding().GetBytes("{\"regionCode\":\"cn\"}");

            // 设置请求头部
            www.SetRequestHeader("Content-Type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(formdata);
            www.downloadHandler = new DownloadHandlerBuffer();

            // 发送请求
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
                prodUpdateButton.GetComponentInChildren<Text>().text = "获取失败（重试）";
                prodUpdateButton.interactable = true;
            }
            else
            {
                var responseText = www.downloadHandler.text;
                var response = JsonConvert.DeserializeObject<ApiResponse>(responseText);
                // 输出truthVersion
                prodUpdateButton.GetComponentInChildren<Text>().text = response.data.truthVersion;
                prodUpdateButton.interactable = true;
                isProdUpdated = true;
            }
        }
    }

    // 定义模型类
    [Serializable]
    public class ApiResponse
    {
        public ResponseData data;
        public string message;
        public int status;
    }

    [Serializable]
    public class ResponseData
    {
        public string desc;
        public string hash;
        public string time;
        public string truthVersion;
    }
}
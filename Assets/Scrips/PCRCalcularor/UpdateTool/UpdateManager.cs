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
        public Toggle qa, jp, newab, useLatestProd;
        public Button prodUpdateButton, jpBossIsShow;
        private bool isProdUpdated = false;
        public GameObject jpBossPanel;
        public void StartCheck()
        {
            this.gameObject.SetActive(true);
            verCharaJP.text = MainManager.Instance.Version.CharacterVersionJP.ToString();
            verBossJP.text = MainManager.Instance.Version.BossVersionJP.ToString();
            verBossCN.text = MainManager.Instance.Version.BossVersionCN.ToString();
            qa.isOn = MainManager.Instance.Version.useQA;
            jp.isOn = MainManager.Instance.Version.useJP;
            newab.isOn = MainManager.Instance.Version.newAB;
            useLatestProd.isOn = MainManager.Instance.Version.useLatestProd;
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
            MainManager.Instance.Version.useLatestProd = useLatestProd.isOn;
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

        public async void ProdUpdateButton()
        {
            if (!isProdUpdated)
            {
                prodUpdateButton.GetComponentInChildren<Text>().text = "获取中...";
                // await ProdUpdate();
                await MainManager.Instance.SendPostRequestAsync(() =>
                {
                    prodUpdateButton.GetComponentInChildren<Text>().text = MainManager.Instance.truthVersion;
                    prodUpdateButton.interactable = true;
                    isProdUpdated = true;
                });
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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Update
{
    public class UpdateManager : MonoBehaviour
    {
        public InputField verCharaJP, verBossJP, verBossCN;
        public Toggle qa, jp;
        
        public void StartCheck()
        {
            this.gameObject.SetActive(true);

            verCharaJP.text = MainManager.Instance.Version.CharacterVersionJP.ToString();
            verBossJP.text = MainManager.Instance.Version.BossVersionJP.ToString();
            verBossCN.text = MainManager.Instance.Version.BossVersionCN.ToString();
            qa.isOn = MainManager.Instance.Version.useQA;
            jp.isOn = MainManager.Instance.Version.useJP;

        }

        public void CancelButton()
        {
            gameObject.SetActive(false);
        }

        public void UpdateButton()
        {
            MainManager.Instance.Version.useQA = qa.isOn;
            MainManager.Instance.Version.useJP = jp.isOn;
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
            MainManager.Instance.WindowConfigMessage("更新完成，重启摸轴器以应用更改", Application.Quit, Application.Quit);
        }
    }
}
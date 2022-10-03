using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Update
{
    public class UpdateManager : MonoBehaviour
    {
        public Text text;
        public Button button;
        public Text buttonText;

        public int GameVersion;
        public int AssetVer;
        public int AssetVerOld;
        private int code => CheckUpdate.StateCode;
        public void StartCheck()
        {
            text.text = "";
            this.gameObject.SetActive(true);
            Log.LogAction = SetLog;
            StartCoroutine(Check_0());
        }
        private IEnumerator Check_0()
        {
            button.interactable = false;
            buttonText.text = "��ȴ�...";
            yield return CheckUpdate.GetGithubData(GameVersion);
            button.interactable = true;
            buttonText.text = "ȷ��";

        }
        public void UpdateButton()
        {
            if (code < 100)
            {
                Application.Quit();
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
        public void SetLog(string str,Color color)
        {
            text.text += $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{str}</color>\n";
        }
        [ContextMenu("���ɸ����ļ�")]
        public void CreateUpdateFile()
        {
            CheckUpdate.ExportUpdateHashDataInEditor(GameVersion,AssetVer,AssetVerOld);
        }
    }
}
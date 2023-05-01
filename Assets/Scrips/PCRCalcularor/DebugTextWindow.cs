using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator.Battle
{
    public class DebugTextWindow : MonoBehaviour
    {
        public Text debugText;
        public Text pageText;
        public int messageCount;

        private List<string> debugLogs;
        private int pageMax = 1;
        private int page = 1;

        private void Start()
        {
            if (BattleUIManager.Instance.DebugStrList == null)
            {
                return;
            }
            debugLogs = BattleUIManager.Instance.DebugStrList;
            pageMax = Mathf.Max(1, Mathf.CeilToInt((float)debugLogs.Count / messageCount));
            page = pageMax;
            Refresh();
        }
        public void Refresh()
        {
            string str = "";
            for(int i = (page - 1) * messageCount; i < page * messageCount; i++)
            {
                if (i < debugLogs.Count)
                {
                    str += debugLogs[i];
                }
            }
            debugText.text = str;
            pageText.text = page + "/" + pageMax;
        }
        public void Close()
        {
            Destroy(gameObject);
        }
        public void PreviousButton()
        {
            if (page > 1)
            {
                page--;
            }
            Refresh();
        }
        public void NextButton()
        {
            if (page < pageMax)
            {
                page++;
            }
            Refresh();
        }
    }
}

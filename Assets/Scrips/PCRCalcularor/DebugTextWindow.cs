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

#if UNITY_5_3_OR_NEWER
        private InputField jumpInput;
        private void AddPageInput()
        {
            var go = new GameObject("跳至页码", typeof(RectTransform), typeof(Image), typeof(InputField));
            var rect = (RectTransform)go.transform;
            rect.SetParent(pageText.transform.parent, false);
            rect.anchorMin = pageText.rectTransform.anchorMin;
            rect.anchorMax = pageText.rectTransform.anchorMax;
            rect.pivot = pageText.rectTransform.pivot;
            rect.anchoredPosition = pageText.rectTransform.anchoredPosition + new Vector2(248, 0);
            rect.sizeDelta = new Vector2(132, 32);
            var label = Instantiate(pageText, rect, false);
            label.rectTransform.anchorMin = Vector2.zero;
            label.rectTransform.anchorMax = Vector2.one;
            label.rectTransform.offsetMin = label.rectTransform.offsetMax = Vector2.zero;
            label.color = Color.black;
            label.alignment = TextAnchor.MiddleCenter;
            label.raycastTarget = false;
            var hint = Instantiate(label, rect, false);
            hint.text = "跳至页码";
            hint.color = Color.gray;
            jumpInput = go.GetComponent<InputField>();
            jumpInput.textComponent = label;
            jumpInput.placeholder = hint;
            jumpInput.targetGraphic = go.GetComponent<Image>();
            jumpInput.contentType = InputField.ContentType.IntegerNumber;
            jumpInput.onEndEdit.AddListener(value =>
            {
                if (!jumpInput.wasCanceled && int.TryParse(value, out int target))
                {
                    page = target;
                    Refresh();
                }
                jumpInput.SetTextWithoutNotify("");
            });
        }
#endif

        private void Start()
        {
            if (BattleUIManager.Instance.DebugStrList == null)
            {
                return;
            }
            debugLogs = BattleUIManager.Instance.DebugStrList;
            messageCount = Mathf.Max(1, messageCount);
#if UNITY_5_3_OR_NEWER
            AddPageInput();
#endif
            pageMax = Mathf.Max(1, Mathf.CeilToInt((float)debugLogs.Count / messageCount));
            page = pageMax;
            Refresh();
        }
        public void Refresh()
        {
            if (debugLogs == null) return;
            pageMax = Mathf.Max(1, Mathf.CeilToInt((float)debugLogs.Count / messageCount));
            page = Mathf.Clamp(page, 1, pageMax);
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
            if (debugLogs != null && page * (long)messageCount < debugLogs.Count)
            {
                page++;
            }
            Refresh();
        }
    }
}

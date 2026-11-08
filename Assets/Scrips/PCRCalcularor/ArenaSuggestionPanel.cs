#if UNITY_5_3_OR_NEWER
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    /// <summary>Attack-only controls. Uses the existing local companion; never starts a battle.</summary>
    public sealed class ArenaSuggestionPanel : MonoBehaviour
    {
        private ChoosePannelManager owner;
        private int[] defense;
        private Button suggestButton;
        private Button nextButton;
        private Text status;
        private CancellationTokenSource pending;
        private Coroutine routine;
        private JObject[] suggestions = new JObject[0];
        private int current;
        private RectTransform rosterRect;
        private RectTransform selectedRect;
        private Vector2 initialRosterSize;
        private Vector2 initialRosterPosition;
        private Vector2 initialSelectedPosition;

        public static void Bind(ChoosePannelManager owner, AddedPlayerData opponent, bool attack)
        {
            var panel = owner.chooseBack_A.GetComponentInChildren<ArenaSuggestionPanel>(true);
            if (panel == null && !attack) return;
            if (panel == null)
            {
                var root = new GameObject("ArenaSuggestions", typeof(RectTransform));
                root.transform.SetParent(owner.chooseBack_A.transform, false);
                root.layer = owner.chooseBack_A.layer;
                panel = root.AddComponent<ArenaSuggestionPanel>();
                panel.CreateControls(owner);
            }
            panel.CancelPending();
            panel.SetAttackLayout(attack);
            panel.gameObject.SetActive(attack);
            if (!attack) return;
            panel.owner = owner;
            panel.defense = opponent?.playrCharacters.Select(unit => unit.unitId).ToArray();
            panel.suggestions = new JObject[0];
            panel.nextButton.interactable = false;
            panel.suggestButton.interactable = true;
            panel.status.text = "根据当前防守阵容\n推荐进攻五人队";
        }

        private void CreateControls(ChoosePannelManager selection)
        {
            owner = selection;
            rosterRect = selection.parent.GetComponentInParent<ScrollRect>().GetComponent<RectTransform>();
            initialRosterSize = rosterRect.sizeDelta;
            initialRosterPosition = rosterRect.anchoredPosition;
            var selected = selection.selectedChars[0].transform;
            while (selected.parent != selection.chooseBack_A.transform) selected = selected.parent;
            selectedRect = (RectTransform)selected;
            initialSelectedPosition = selectedRect.anchoredPosition;
            var rect = (RectTransform)transform;
            rect.anchorMin = rect.anchorMax = rect.pivot = Vector2.zero;
            rect.anchoredPosition = new Vector2(20, 24);
            rect.sizeDelta = new Vector2(590, 82);
            var template = selection.nextButtonText;
            suggestButton = CreateButton("建议解", new Vector2(0, 12), new Vector2(140, 56), template);
            suggestButton.onClick.AddListener(Request);
            nextButton = CreateButton("下一组", new Vector2(152, 12), new Vector2(120, 56), template);
            nextButton.onClick.AddListener(() => Apply((current + 1) % suggestions.Length));
            status = CreateText("Status", transform, template, 18);
            var textRect = status.rectTransform;
            textRect.anchorMin = textRect.anchorMax = textRect.pivot = Vector2.zero;
            textRect.anchoredPosition = new Vector2(284, 0);
            textRect.sizeDelta = new Vector2(306, 82);
            status.alignment = TextAnchor.MiddleLeft;
            status.color = new Color(.2f, .2f, .2f, 1);
        }

        private void SetAttackLayout(bool attack)
        {
            // Reserve a footer within the original panel bounds; restore other selection modes.
            rosterRect.sizeDelta = initialRosterSize + new Vector2(0, attack ? -110 : 0);
            rosterRect.anchoredPosition = initialRosterPosition + new Vector2(0, attack ? 55 : 0);
            selectedRect.anchoredPosition = initialSelectedPosition + new Vector2(0, attack ? 110 : 0);
        }

        private Button CreateButton(string label, Vector2 position, Vector2 size, Text template)
        {
            var root = new GameObject(label, typeof(RectTransform), typeof(Image), typeof(Button));
            root.transform.SetParent(transform, false);
            root.layer = gameObject.layer;
            var rect = (RectTransform)root.transform;
            rect.anchorMin = rect.anchorMax = rect.pivot = Vector2.zero;
            rect.anchoredPosition = position;
            rect.sizeDelta = size;
            var image = root.GetComponent<Image>();
            image.color = new Color(.23f, .48f, .72f, 1);
            var button = root.GetComponent<Button>();
            button.targetGraphic = image;
            var text = CreateText("Label", root.transform, template, 24);
            text.text = label;
            text.rectTransform.anchorMin = Vector2.zero;
            text.rectTransform.anchorMax = Vector2.one;
            text.rectTransform.sizeDelta = Vector2.zero;
            return button;
        }

        private static Text CreateText(string name, Transform parent, Text template, int size)
        {
            var root = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            root.transform.SetParent(parent, false);
            root.layer = parent.gameObject.layer;
            var text = root.GetComponent<Text>();
            text.font = template.font;
            text.fontSize = size;
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;
            text.raycastTarget = false;
            return text;
        }

        private void Request()
        {
            CancelPending();
            if (defense == null || defense.Length != 5 || defense.Distinct().Count() != 5)
            {
                MainManager.Instance.WindowMessage("模型建议需要五个不同的防守角色，请先编辑防守阵容。");
                return;
            }
            pending = new CancellationTokenSource(TimeSpan.FromSeconds(60));
            routine = StartCoroutine(WaitForSuggestion(pending.Token));
        }

        private IEnumerator WaitForSuggestion(CancellationToken token)
        {
            Task<string> task;
            try
            {
                var request = new JObject
                {
                    ["Defense"] = JArray.FromObject(defense),
                    ["Roster"] = JArray.FromObject(owner.GetArenaSuggestionRoster()),
                    ["Candidates"] = 256, ["Count"] = 8, ["Seed"] = Environment.TickCount
                };
                task = OfflineServerManager.Acquire().Send(request.ToString(), token, "/arena/suggest");
                // Observe a late failure even when closing the selection panel stops this coroutine.
                task.ContinueWith(failed => { var error = failed.Exception; },
                    CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);
            }
            catch (Exception error)
            {
                ShowError(error);
                yield break;
            }
            suggestButton.interactable = nextButton.interactable = false;
            status.text = "正在生成建议…";
            while (!task.IsCompleted) yield return null;
            try
            {
                var response = JObject.Parse(task.GetAwaiter().GetResult());
                suggestions = (response["Teams"] as JArray)?.OfType<JObject>().ToArray() ?? new JObject[0];
                if (suggestions.Length == 0) throw new InvalidOperationException("模型未返回可用阵容。");
                Apply(0);
                nextButton.interactable = suggestions.Length > 1;
            }
            catch (Exception error) { ShowError(error); }
            suggestButton.interactable = true;
            pending?.Dispose();
            pending = null;
            routine = null;
        }

        private void Apply(int index)
        {
            if (suggestions.Length == 0) return;
            var team = suggestions[index]["Team"]?.ToObject<int[]>();
            if (team == null || !owner.ApplyArenaSuggestion(team))
                throw new InvalidOperationException("建议包含当前版本不可用的角色，请更新模型。");
            current = index;
            var win = suggestions[index].Value<double>("PredictedWinRate");
            status.text = $"模型预测胜率 {win:P0}\n第 {index + 1}/{suggestions.Length} 组 · 未模拟";
        }

        private void ShowError(Exception error)
        {
            status.text = "建议失败，可重试";
            suggestButton.interactable = true;
            nextButton.interactable = false;
            MainManager.Instance.WindowMessage(error is OperationCanceledException
                ? "获取建议已取消或超时，请重试。" : "获取竞技场建议失败：\n" + error.Message);
        }

        private void CancelPending()
        {
            pending?.Cancel();
            pending?.Dispose();
            pending = null;
            if (routine != null) StopCoroutine(routine);
            routine = null;
        }

        private void OnDisable() { CancelPending(); }
    }
}
#endif

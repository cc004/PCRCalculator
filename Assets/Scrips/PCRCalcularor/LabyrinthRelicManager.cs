using System.Collections.Generic;
using System.Collections;
using System.Linq;
using PCRCaculator.Guild;
using PCRCaculator.SQL;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{
    public class LabyrinthRelicManager : MonoBehaviour
    {
        public static LabyrinthRelicManager Instance;
        public LabyrinthRelicSoltPrefab relicSlotPrefab;
        public RectTransform Content;
        public InputField relicAlphaRupee;
        public InputField labyrinthOwnedUnitsCount;
        public List<Image> relicMarkImage;
        public List<Text> relicMarkCount;
        public List<Sprite> relicMarkSprites;
        public GameObject relicMarkEffect;
        public event System.Action<IReadOnlyList<int>> MarkCountsChanged;

        private const int RelicMarkTypeCount = 5;

        private readonly List<LabyrinthRelicSoltPrefab> relicSlots = new List<LabyrinthRelicSoltPrefab>();
        private Coroutine loadCoroutine;
        private GuildPlayerGroupData selectionGroup;
        private readonly HashSet<int> selectedRelicIds = new HashSet<int>();
        private GuildPlayerGroupData PlayerGroup => GuildManager.Instance?.SettingData?.GetCurrentPlayerGroup();

        private int scheduleId;
        private Text selectedCount;
        private readonly Dictionary<int, Button> seriesButtons = new Dictionary<int, Button>();
        private EventBattleData Events => MainManager.Instance?.databaseCache?.EventBattles;
        private int CurrentSchedule => Events != null && Events.Invasion.TryGetValue(PlayerGroup?.waveGroupId ?? 0, out var quest)
            ? quest.schedule_id : 0;
        public bool IsInvasion => CurrentSchedule != 0;
        internal Dictionary<int, List<labyrinth_relic_mark_effect>> MarkEffects =>
            IsInvasion ? Events.Marks : MainManager.Instance?.LabyrinthRelicMarkEffectDic;

        public void LoadSavedSelection()
        {
            selectionGroup = PlayerGroup;
            selectedRelicIds.Clear();
            scheduleId = CurrentSchedule;
            if (scheduleId != 0)
                selectedRelicIds.UnionWith(Events.SelectRelics(scheduleId, selectionGroup.invasionRelicIds));
            else if (selectionGroup?.labyrinthRelicIds != null)
                selectedRelicIds.UnionWith(selectionGroup.labyrinthRelicIds.Where(id => id > 0));

            if (relicAlphaRupee != null)
            {
                relicAlphaRupee.contentType = InputField.ContentType.IntegerNumber;
                relicAlphaRupee.SetTextWithoutNotify(Mathf.Max(0, selectionGroup?.labyrinthAlphaRupee ?? 0).ToString());
            }
            if (labyrinthOwnedUnitsCount != null)
            {
                labyrinthOwnedUnitsCount.contentType = InputField.ContentType.IntegerNumber;
                labyrinthOwnedUnitsCount.SetTextWithoutNotify(Mathf.Max(0, selectionGroup?.labyrinthOwnedUnitsCount ?? 0).ToString());
            }

            UpdateRelicMarks();
        }

        public void SaveSelectedRelics()
        {
            var group = PlayerGroup;
            if (group == null)
                return;

            if (selectionGroup != group || scheduleId != CurrentSchedule)
                LoadSavedSelection();

            if (scheduleId != 0)
            {
                group.invasionRelicIds = Events.SelectRelics(scheduleId, selectedRelicIds.OrderBy(id => id));
                return;
            }
            group.labyrinthRelicIds = selectedRelicIds.OrderBy(id => id).ToList();
            if (relicAlphaRupee != null)
            {
                group.labyrinthAlphaRupee = int.TryParse(relicAlphaRupee.text, out int alphaRupee)
                    ? Mathf.Max(0, alphaRupee)
                    : 0;
                relicAlphaRupee.SetTextWithoutNotify(group.labyrinthAlphaRupee.ToString());
            }
            if (labyrinthOwnedUnitsCount != null)
            {
                group.labyrinthOwnedUnitsCount = int.TryParse(labyrinthOwnedUnitsCount.text, out int ownedUnitsCount)
                    ? Mathf.Max(0, ownedUnitsCount)
                    : 0;
                labyrinthOwnedUnitsCount.SetTextWithoutNotify(group.labyrinthOwnedUnitsCount.ToString());
            }
            UpdateRelicMarks();
        }

        private LabyrinthRelicSoltPrefab CreateRelicSlot()
        {
            var slot = Instantiate(relicSlotPrefab, Content, false);
            slot.SelectionChanged += OnRelicSelectionChanged;
            return slot;
        }

        private void OnRelicSelectionChanged(int relicId, bool isSelected)
        {
            if (relicId <= 0)
                return;

            if (isSelected)
            {
                if (scheduleId == 0 || selectedRelicIds.Count < 5)
                    selectedRelicIds.Add(relicId);
            }
            else
                selectedRelicIds.Remove(relicId);

            UpdateRelicMarks();
        }

        public IReadOnlyList<int> GetRelicMarkCounts()
        {
            var counts = new int[RelicMarkTypeCount];
            var relics = scheduleId != 0 ? Events.Relics : MainManager.Instance?.LabyrinthRelicDic;
            if (relics != null)
            {
                foreach (int relicId in selectedRelicIds)
                {
                    if (relics.TryGetValue(relicId, out var relic) && relic != null &&
                        relic.relic_mark_id >= 1 && relic.relic_mark_id <= RelicMarkTypeCount)
                    {
                        counts[relic.relic_mark_id - 1] += relic.relic_mark_count;
                    }
                }
            }
            return counts;
        }

        private void UpdateRelicMarks()
        {
            foreach (var slot in relicSlots)
            {
                if (slot == null) continue;
                bool selected = selectedRelicIds.Contains(slot.RelicId);
                slot.SetSelected(selected);
                slot.relicToggle.interactable = scheduleId == 0 || selected || selectedRelicIds.Count < 5;
            }
            if (selectedCount != null)
                selectedCount.text = $"已选 {selectedRelicIds.Count} / 5";
            var counts = GetRelicMarkCounts();

            // 按印记 ID 1–5 固定升序显示：三个列表的索引 0 都对应 ID 1。
            if (relicMarkCount != null)
            {
                for (int i = 0; i < relicMarkCount.Count; i++)
                {
                    if (relicMarkCount[i] != null)
                        relicMarkCount[i].text = i < counts.Count ? counts[i].ToString() : string.Empty;
                }
            }

            if (relicMarkImage != null)
            {
                for (int i = 0; i < relicMarkImage.Count; i++)
                {
                    var image = relicMarkImage[i];
                    if (image == null)
                        continue;

                    image.sprite = i < RelicMarkTypeCount && relicMarkSprites != null && i < relicMarkSprites.Count
                        ? relicMarkSprites[i]
                        : null;
                    image.enabled = image.sprite != null;
                }
            }
            MarkCountsChanged?.Invoke(counts);
        }

        public void MarkEffectButton()
        {
            if (relicMarkEffect == null || BaseBackManager.Instance?.latestUIback == null)
                return;

            var panel = Instantiate(relicMarkEffect, BaseBackManager.Instance.latestUIback.transform, false);
            panel.GetComponent<LabyrinthRelicMark>()?.Initialize(this);
        }

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            if (MainManager.Instance != null && MainManager.Instance.LoadFinished)
                Refresh();
            else
                loadCoroutine = StartCoroutine(LoadRelics());
        }

        private IEnumerator LoadRelics()
        {
            while (MainManager.Instance == null || !MainManager.Instance.LoadFinished)
                yield return null;

            Refresh();
            loadCoroutine = null;
        }

        public void Refresh()
        {
            if (relicSlotPrefab == null || Content == null)
            {
                Debug.LogWarning("请绑定遗物列表的 relicSlotPrefab 和 Content。", this);
                return;
            }

            if (selectionGroup != PlayerGroup || scheduleId != CurrentSchedule)
                LoadSavedSelection();
            bool invasion = scheduleId != 0;
            relicAlphaRupee.gameObject.SetActive(!invasion);
            labyrinthOwnedUnitsCount.gameObject.SetActive(!invasion);
            var relics = invasion ? Events.SelectRelicsForSchedule(scheduleId)
                : MainManager.Instance?.LabyrinthRelicDic?.Values.ToList();
            if (relics == null) return;
            UpdateSeriesButtons(relics);

            int index = 0;
            foreach (var relic in relics.OrderBy(relic => relic.relic_id))
            {
                if (index >= relicSlots.Count)
                    relicSlots.Add(CreateRelicSlot());
                else if (relicSlots[index] == null)
                    relicSlots[index] = CreateRelicSlot();

                var slot = relicSlots[index];
                slot.SetData(relic, invasion);
                slot.SetSelected(selectedRelicIds.Contains(relic.relic_id));
                slot.gameObject.SetActive(true);
                index++;
            }

            for (int i = index; i < relicSlots.Count; i++)
            {
                if (relicSlots[i] != null)
                    relicSlots[i].gameObject.SetActive(false);
            }
            UpdateRelicMarks();
        }

        private void UpdateSeriesButtons(List<labyrinth_relic> available)
        {
            if (scheduleId != 0 && selectedCount == null)
            {
                // 使用原下方角色数量、金币输入框的位置。
                selectedCount = Instantiate(relicAlphaRupee.textComponent, transform, false);
                var rect = selectedCount.rectTransform;
                rect.anchorMin = rect.anchorMax = rect.pivot = new Vector2(.5f, .5f);
                rect.sizeDelta = new Vector2(152, 44);
                rect.anchoredPosition = new Vector2(480, -263);
                selectedCount.alignment = TextAnchor.MiddleCenter;
                selectedCount.raycastTarget = false;
                for (int i = 0; i < RelicMarkTypeCount; i++)
                {
                    int mark = i + 1;
                    var button = GuildManager.Instance.EventButton(transform, LabyrinthRelicMark.MarkNames[i] + " +3",
                        -212 + i * 130, -263, 122, () => EquipSeries(mark));
                    seriesButtons.Add(mark, button);
                }
            }
            if (selectedCount != null) selectedCount.gameObject.SetActive(scheduleId != 0);
            foreach (var pair in seriesButtons)
                pair.Value.gameObject.SetActive(scheduleId != 0 && available.Any(r =>
                    r.relic_mark_id == pair.Key && r.rarity == 3 && r.relic_mark_count == 3));
        }

        private void EquipSeries(int mark)
        {
            if (scheduleId == 0) return;
            var ids = Events.SelectRelicsForSchedule(scheduleId)
                .Where(r => r.relic_mark_id == mark && r.rarity == 3 && r.relic_mark_count == 3)
                .OrderBy(r => r.relic_id).Take(5).Select(r => r.relic_id);
            selectedRelicIds.Clear();
            selectedRelicIds.UnionWith(ids);
            UpdateRelicMarks();
        }

        private void OnDisable()
        {
            if (loadCoroutine != null)
            {
                StopCoroutine(loadCoroutine);
                loadCoroutine = null;
            }
        }

        private void OnDestroy()
        {
            foreach (var slot in relicSlots)
            {
                if (slot != null)
                    slot.SelectionChanged -= OnRelicSelectionChanged;
            }

            if (Instance == this)
                Instance = null;
        }
    }
}

using System.Collections.Generic;
using System.Collections;
using System.Linq;
using PCRCaculator.Guild;
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

        public void LoadSavedSelection()
        {
            selectionGroup = PlayerGroup;
            selectedRelicIds.Clear();
            if (selectionGroup?.labyrinthRelicIds != null)
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

            foreach (var slot in relicSlots)
            {
                if (slot != null)
                    slot.SetSelected(selectedRelicIds.Contains(slot.RelicId));
            }
            UpdateRelicMarks();
        }

        private void CaptureSelection()
        {
            // 未打开遗物页时保留已保存的数据；只更新实际渲染过的条目。
            foreach (var slot in relicSlots)
            {
                if (slot == null || !slot.gameObject.activeSelf || slot.relicToggle == null || slot.RelicId <= 0)
                    continue;

                if (slot.relicToggle.isOn)
                    selectedRelicIds.Add(slot.RelicId);
                else
                    selectedRelicIds.Remove(slot.RelicId);
            }
        }

        public void SaveSelectedRelics()
        {
            var group = PlayerGroup;
            if (group == null)
                return;

            if (selectionGroup != group)
                LoadSavedSelection();

            CaptureSelection();
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
                selectedRelicIds.Add(relicId);
            else
                selectedRelicIds.Remove(relicId);

            UpdateRelicMarks();
        }

        public IReadOnlyList<int> GetRelicMarkCounts()
        {
            var counts = new int[RelicMarkTypeCount];
            var relics = MainManager.Instance?.LabyrinthRelicDic;
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

            if (selectionGroup != PlayerGroup)
                LoadSavedSelection();
            else
                CaptureSelection();

            var relics = MainManager.Instance?.LabyrinthRelicDic;
            if (relics == null)
                return;

            // 复用已有条目，重复打开或刷新不会累积重复的遗物。
            int index = 0;
            foreach (var relic in relics.Values.Where(relic => relic != null).OrderBy(relic => relic.relic_id))
            {
                if (index >= relicSlots.Count)
                    relicSlots.Add(CreateRelicSlot());
                else if (relicSlots[index] == null)
                    relicSlots[index] = CreateRelicSlot();

                var slot = relicSlots[index];
                slot.SetData(relic);
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PCRCaculator.Guild;
using UnityEngine;

namespace PCRCaculator
{
    public class LabyrinthEnhanceManager : MonoBehaviour
    {
        public static LabyrinthEnhanceManager Instance;
        public GameObject labyrinthEnhancePrefab;
        public RectTransform Content;
        // 节点边缘之间的水平、垂直间隔，以及内容边缘留白。
        public Vector2 nodeSpacing = new Vector2(40f, 40f);
        public Vector2 contentPadding = new Vector2(20f, 20f);

        private readonly Dictionary<int, labyrinthEnhancePrefab> nodeViews =
            new Dictionary<int, labyrinthEnhancePrefab>();
        private Coroutine loadCoroutine;
        private GuildPlayerGroupData selectionGroup;
        private readonly HashSet<int> selectedNodeIds = new HashSet<int>();
        private GuildPlayerGroupData PlayerGroup => GuildManager.Instance?.SettingData?.GetCurrentPlayerGroup();

        public void LoadSavedSelection()
        {
            selectionGroup = PlayerGroup;
            selectedNodeIds.Clear();
            if (selectionGroup?.labyrinthEnhancedNodeIds != null)
                selectedNodeIds.UnionWith(selectionGroup.labyrinthEnhancedNodeIds.Where(id => id > 0));

            AddSelectedPrerequisites();
            UpdateNodeSelection();
        }

        public void SaveSelectedNodes()
        {
            var group = PlayerGroup;
            if (group == null)
                return;

            if (selectionGroup != group)
                LoadSavedSelection();

            AddSelectedPrerequisites();
            group.labyrinthEnhancedNodeIds = selectedNodeIds.OrderBy(id => id).ToList();
            UpdateNodeSelection();
        }

        private void AddSelectedPrerequisites()
        {
            var nodes = MainManager.Instance?.LabyrinthEnhanceNodeDic;
            if (nodes == null)
                return;

            var pending = new Stack<int>(selectedNodeIds);
            var visited = new HashSet<int>();
            while (pending.Count > 0)
            {
                int nodeId = pending.Pop();
                if (!visited.Add(nodeId) || !nodes.TryGetValue(nodeId, out var node) || node == null)
                    continue;

                foreach (int prerequisiteId in new[] { node.pre_node_1, node.pre_node_2, node.pre_node_3 })
                {
                    if (prerequisiteId > 0 && nodes.TryGetValue(prerequisiteId, out var prerequisite) && prerequisite != null)
                    {
                        selectedNodeIds.Add(prerequisiteId);
                        pending.Push(prerequisiteId);
                    }
                }
            }
        }

        private void OnNodeSelectionChanged(int nodeId, bool isSelected)
        {
            var nodes = MainManager.Instance?.LabyrinthEnhanceNodeDic;
            if (nodeId <= 0 || nodes == null || !nodes.TryGetValue(nodeId, out var node) || node == null)
                return;

            if (selectionGroup != PlayerGroup)
                LoadSavedSelection();

            if (isSelected)
            {
                selectedNodeIds.Add(nodeId);
                AddSelectedPrerequisites();
            }
            else
            {
                // 移除依赖此节点的整条后续分支，保留其他独立分支。
                var removedIds = new HashSet<int> { nodeId };
                bool changed;
                do
                {
                    changed = false;
                    foreach (int selectedId in selectedNodeIds)
                    {
                        if (removedIds.Contains(selectedId) || !nodes.TryGetValue(selectedId, out var selected) || selected == null)
                            continue;

                        if (removedIds.Contains(selected.pre_node_1) || removedIds.Contains(selected.pre_node_2) ||
                            removedIds.Contains(selected.pre_node_3))
                            changed |= removedIds.Add(selectedId);
                    }
                } while (changed);

                selectedNodeIds.ExceptWith(removedIds);
            }

            UpdateNodeSelection();
        }

        private void UpdateNodeSelection()
        {
            foreach (var pair in nodeViews)
            {
                if (pair.Value != null)
                    pair.Value.SetSelected(selectedNodeIds.Contains(pair.Key));
            }
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
                loadCoroutine = StartCoroutine(LoadNodes());
        }

        private IEnumerator LoadNodes()
        {
            while (MainManager.Instance == null || !MainManager.Instance.LoadFinished)
                yield return null;

            Refresh();
            loadCoroutine = null;
        }

        public void Refresh()
        {
            if (labyrinthEnhancePrefab == null || Content == null)
            {
                Debug.LogWarning("请绑定科技树的 labyrinthEnhancePrefab 和 Content。", this);
                return;
            }

            var template = labyrinthEnhancePrefab.GetComponent<labyrinthEnhancePrefab>();
            if (template == null || template.GetComponent<RectTransform>() == null)
            {
                Debug.LogWarning("节点预制件需要挂载 labyrinthEnhancePrefab 和 RectTransform。", this);
                return;
            }

            var nodes = MainManager.Instance?.LabyrinthEnhanceNodeDic;
            if (nodes == null)
                return;

            if (selectionGroup != PlayerGroup)
                LoadSavedSelection();
            else
                AddSelectedPrerequisites();

            // 从上到下为 8、5、2，三行共用 pos_x 列号，缺失节点保留空位。
            var orderedNodes = nodes.Values
                .Where(node => node != null && (node.pos_y == 8 || node.pos_y == 5 || node.pos_y == 2))
                .OrderByDescending(node => node.pos_y)
                .ThenBy(node => node.pos_x)
                .ThenBy(node => node.node_id)
                .ToList();
            var renderedIds = new HashSet<int>();
            var templateRect = template.GetComponent<RectTransform>();
            float nodeWidth = Mathf.Max(1f, templateRect.rect.width);
            float nodeHeight = Mathf.Max(1f, templateRect.rect.height);

            int index = 0;
            foreach (var node in orderedNodes)
            {
                if (!nodeViews.TryGetValue(node.node_id, out var view) || view == null)
                {
                    view = Instantiate(template, Content, false);
                    view.SelectionChanged += OnNodeSelectionChanged;
                    nodeViews[node.node_id] = view;
                }

                var rect = view.GetComponent<RectTransform>();
                float width = rect.rect.width;
                float height = rect.rect.height;
                rect.anchorMin = rect.anchorMax = new Vector2(0f, 1f);
                rect.pivot = new Vector2(0f, 1f);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
                nodeWidth = Mathf.Max(nodeWidth, width);
                nodeHeight = Mathf.Max(nodeHeight, height);
                rect.SetSiblingIndex(index++);
                view.SetData(node);
                view.SetSelected(selectedNodeIds.Contains(node.node_id));
                view.gameObject.SetActive(true);
                renderedIds.Add(node.node_id);
            }

            float horizontalGap = Mathf.Max(0f, nodeSpacing.x);
            float verticalGap = Mathf.Max(0f, nodeSpacing.y);
            float paddingX = Mathf.Max(0f, contentPadding.x);
            float paddingY = Mathf.Max(0f, contentPadding.y);
            int maxColumns = 0;
            foreach (var node in orderedNodes)
            {
                int rowIndex = (8 - node.pos_y) / 3;
                int columnIndex = Mathf.Max(0, node.pos_x - 1);
                nodeViews[node.node_id].GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    paddingX + columnIndex * (nodeWidth + horizontalGap),
                    -paddingY - rowIndex * (nodeHeight + verticalGap));
                maxColumns = Mathf.Max(maxColumns, columnIndex + 1);
            }

            Content.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,
                paddingX * 2f + maxColumns * nodeWidth + Mathf.Max(0, maxColumns - 1) * horizontalGap);
            Content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
                paddingY * 2f + 3f * nodeHeight + 2f * verticalGap);

            foreach (var pair in nodeViews)
            {
                if (pair.Value != null && !renderedIds.Contains(pair.Key))
                    pair.Value.gameObject.SetActive(false);
            }
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
            foreach (var view in nodeViews.Values)
            {
                if (view != null)
                    view.SelectionChanged -= OnNodeSelectionChanged;
            }

            if (Instance == this)
                Instance = null;
        }
    }
}

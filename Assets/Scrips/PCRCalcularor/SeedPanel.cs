using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PCRCaculator.Guild;
using System.Linq;
using Elements;
using PCRCaculator;
public class SeedPanel : MonoBehaviour
{
    public GameObject SeedCell;
    public ScrollRect scrollRect;
    private List<GameObject> cells = new List<GameObject>();

    private void OnEnable()
    {
        var dmglist = GuildCalculator.Instance.dmglist.Where(p => p.enabled).ToArray();
        var dmgcnt = new Dictionary<ProbEvent, int>();
        var seedDict = new Dictionary<ProbEvent, int>();
        var rnd = new System.Random();

        for (int i = 0; i < GuildManager.StaticsettingData.n2; ++i)
        {
            var seed = rnd.Next();
            DoubleWithEx.SetState(seed);
            LongWithEx.SetState(seed);
            var hash = rnd.Next();

            foreach (var evt in dmglist)
            {
                if (evt.predict(hash))
                {
                    dmgcnt[evt] = dmgcnt.TryGetValue(evt, out int count) ? count + 1 : 1;
                    seedDict[evt] = seed;
                    break;
                }
            }
        }

        foreach (var pair in dmgcnt.OrderBy(p => p.Key.frame))
        {
            var evt = pair.Key;
            var count = pair.Value;

            string[] content = new[]
            {
                $"{GuildManager.Instance.SettingData.limitTime - evt.frame / 60}",
                evt.frame.ToString(),
                $"<color=#f75291>{count / (float)GuildManager.StaticsettingData.n2:P2}</color>",
                $"{seedDict[evt]}",
                evt.unit,
                evt.description
            };

            GameObject cell = Instantiate(SeedCell, transform);
            SetCellText(cell, content);
            AddCopyButton(cell, seedDict[evt]);
            cells.Add(cell);
        }

        if (scrollRect != null)
        {
            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }

    private void AddCopyButton(GameObject cell, int seed)
    {
        Button button = cell.AddComponent<Button>();
        button.onClick.AddListener(() =>
        {
            TextEditor editor = new TextEditor { text = seed.ToString() };
            editor.SelectAll();
            editor.Copy();
            MainManager.Instance.WindowMessage($"已复制种子: {seed}");
        });
    }

    private void SetCellText(GameObject cell, string[] content)
    {
        Text[] texts = cell.GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.Length && i < content.Length; i++)
        {
            texts[i].text = content[i];
        }
    }

    public void OnDisable()
    {
        cells.ToList().ForEach(Destroy);
        cells.Clear();
    }
}
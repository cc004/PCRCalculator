using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using PCRCaculator;
using PCRCaculator.SQL;

public class TalentLevel : MonoBehaviour
{
    public Slider talentLevelSlider;
    public Text talentLevelText;
    public Text talentValueText;

    void Start()
    {
        // 初始化 Slider 的监听事件
        if (talentLevelSlider != null)
        {
            // 设置初始值
            int initialValue = (int)talentLevelSlider.value;
            UpdateDisplay(initialValue);

            // 绑定滑块事件
            talentLevelSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        int level = (int)value;
        UpdateDisplay(level);
    }

    private void UpdateDisplay(int level)
    {
        if (talentLevelText != null)
            talentLevelText.text = level.ToString();

        if (talentValueText != null)
            talentValueText.text = (level * 20).ToString();
    }
}
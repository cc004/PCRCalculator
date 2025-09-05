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
    public InputField talentLevelInput;

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

        // 添加输入框事件监听
        if (talentLevelInput != null)
        {
            talentLevelInput.onEndEdit.AddListener(OnInputFieldEndEdit);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        int level = (int)value;
        UpdateDisplay(level);

        if (talentLevelInput != null)
        {
            talentLevelInput.text = level.ToString();
        }
    }

    private void OnInputFieldEndEdit(string value)
    {
        // 用户完成输入时更新滑块值
        if (int.TryParse(value, out int level))
        {
            talentLevelSlider.value = level;
            UpdateDisplay(level);
            talentLevelInput.text = level.ToString();
        }
        else
        {
            // 输入无效时恢复为滑块当前值
            talentLevelInput.text = ((int)talentLevelSlider.value).ToString();
        }
    }

    private void UpdateDisplay(int level)
    {
        if (talentLevelText != null)
            talentLevelInput.text = level.ToString();

        if (talentValueText != null)
            talentValueText.text = (level * 20).ToString();
    }

    public void AddButton()
    {
        talentLevelSlider.value++;
        UpdateDisplay((int)talentLevelSlider.value);
    }

    public void MinusButton()
    {
        talentLevelSlider.value--;
        UpdateDisplay((int)talentLevelSlider.value);
    }
}
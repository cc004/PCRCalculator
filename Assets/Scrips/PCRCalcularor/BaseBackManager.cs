﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PCRCaculator
{


    public class BaseBackManager : MonoBehaviour
    {
        public static BaseBackManager Instance;

        public GameObject latestUIback;//最上方ui底板，用于动态添加UIperferb
        public Text debugtext;
        public TextMeshProUGUI playerLevelText;
        public List<Slider> sliders;
        public List<TextMeshProUGUI> texts;
        //public Toggle allowRarity6Toggle;
        public Toggle tpCalculationChanged, exDivisionFixed;
        //public Toggle showAllUnitToggle;
        public GameObject SettingBack;

        public InputField TPMaxText;
        public InputField RBLevelText;
        public InputField RBValueText;
        public InputField RBTPValueText;
        public GameObject ShowTextPrefab;
        public PlayerSetting PlayerSetting { get => MainManager.Instance.PlayerSetting; }

        private bool isLoading;
        private void Awake()
        {
            Instance = this;
        }
        /// <summary>
        /// 主界面设置按钮
        /// </summary>
        public void SettingButton()
        {
            isLoading = true;
            SettingBack.SetActive(true);
            sliders[0].value = PlayerSetting.playerLevel;
            sliders[1].value = PlayerSetting.playerProcess;
            sliders[2].value = PlayerSetting.maxUniqueEqLv;

            texts[0].text = PlayerSetting.playerLevel + "";
            texts[1].text = PlayerSetting.ProcessStr();
            texts[2].text = PlayerSetting.maxUniqueEqLv + "";
            //allowRarity6Toggle.isOn = PlayerSetting.allowRarity6;
            tpCalculationChanged.isOn = PlayerSetting.tpCalculationChanged;
            exDivisionFixed.isOn = PlayerSetting.exDivisionFixed;
            //if (showAllUnitToggle!=null)
            //     showAllUnitToggle.isOn = PlayerSetting.showAllUnits;

            TPMaxText.text = PlayerSetting.maxTPUpValue.ToString();

            isLoading = false;
        }
        /// <summary>
        /// 滑动条函数
        /// </summary>
        public void OnSliderDraged()
        {
            if (isLoading) { return; }
            PlayerSetting.playerLevel = (int)sliders[0].value;
            PlayerSetting.playerProcess = (int)sliders[1].value;
            PlayerSetting.maxUniqueEqLv = (int)sliders[2].value;

            texts[0].text = PlayerSetting.playerLevel + "";
            texts[1].text = PlayerSetting.ProcessStr();
            texts[2].text = PlayerSetting.maxUniqueEqLv + "";

            if (playerLevelText != null)
            {
                playerLevelText.text = PlayerSetting.playerLevel + "";
            }

        }
        /// <summary>
        /// 设置界面保存按钮
        /// </summary>
        public void SaveButton()
        {
            //PlayerSetting.allowRarity6 = allowRarity6Toggle.isOn;
            PlayerSetting.tpCalculationChanged = tpCalculationChanged.isOn;
            PlayerSetting.exDivisionFixed = exDivisionFixed.isOn;
            //if(showAllUnitToggle!=null)
            //PlayerSetting.showAllUnits = showAllUnitToggle.isOn;

            PlayerSetting.maxTPUpValue = int.Parse(TPMaxText.text, System.Globalization.NumberStyles.Any);

            MainManager.Instance.SavePlayerSetting();
            MainManager.Instance.WindowMessage("保存成功！");
        }
        public void DeleteButton()
        {
            MainManager.Instance.DeletePlayerData();
        }
        public void AddButton(int id)
        {
            if (sliders[id].value < sliders[id].maxValue)
            {
                sliders[id].value++;
            }
        }
        public void MinusButton(int id)
        {
            if (sliders[id].value > sliders[id].minValue)
            {
                sliders[id].value--;
            }
        }
        public void MainUnderButton(int id)
        {
            switch (id)
            {
                case 1:
                    MainManager.Instance.HomeButton();
                    break;
                case 2:
                    MainManager.Instance.CharacterButton();
                    break;
                case 3:
                    MainManager.Instance.BattleButton();
                    break;
                case 4:
                    MainManager.Instance.GambleButton();
                    break;
                case 5:
                    MainManager.Instance.CalculatorButton();
                    break;
            }
        }
        public void ShowText(string str)
        {
            GameObject a = Instantiate(ShowTextPrefab, latestUIback.transform, false);
            a.GetComponent<ShowTextUI>().Set(str);
        }
    }
}
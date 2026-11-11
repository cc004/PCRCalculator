using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Elements;
using Elements.Battle;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PCRCaculator.Guild;
namespace PCRCaculator.Battle
{
    public enum eLogMessageType
    {
        ERROR = 0,
        BATTLE_READY = 1,
        READY_ACTION = 2,
        EXEC_ACTION = 3,
        CANCEL_ACTION = 4,

        GET_DAMAGE = 10,
        MISS_ACTION = 11,
        DIE = 12,
        HP_RECOVERY = 15,
        CHANGE_TP = 20,
        BUFF_DEBUFF = 30,
        MISS_DAMAGE_BY_NO_DAMAGE = 40,
        WARNING = 50,
        OTHER = 999,

    }
    public class BattleUIManager : MonoBehaviour
    {
        public static BattleUIManager Instance;
        public GameObject OldManager;
        public GameObject NewManager;
        public SpriteRenderer backGroundImage;
        public Text timeText;
        public Text frameText;
        public Text debugText1;
        public GameObject debugBack;
        public ScrollRect scrollRect1;
        //public Text debugText2;
        //public ScrollRect scrollRect2;
        public int maxTextLength;
        public GameObject skillNamePerfab;
        public Transform parent;
        public GameObject buffUIPrefab;
        public List<Sprite> rularSprites;
        public Slider timeScaleSlider;
        public Text timeScaleText;
        public Text FPSText;
        public List<CharacterPageButton> PlayerUI;
        public List<CharacterPageButton> EnemyUI;
        public List<Sprite> buffDebuffIcons;
        public Toggle AutoToggle;
        public Toggle SpeedToggle;
        public Toggle SetToggle;
        public GameObject numberPrefab;
        public GameObject missPrefab;
        public Vector3 numberPosFix;
        public List<Sprite> number_physical_large;
        public List<Sprite> number_magical_large;
        public List<Sprite> number_physical_small;
        public List<Sprite> number_heal_large;
        public List<Sprite> number_energy_large;
        public List<Sprite> talentBonusBackground;
        public List<Sprite> talentBonusBackground2;
        public List<Sprite> talentBonusIcon;
        public List<Image> talentBonus;
        public Sprite sprite_total_physical;
        public Sprite sprite_total_magical;
        public Sprite sprite_critical_physical;
        public Sprite sprite_critical_magical;
        public Sprite jjcBackGround;
        public DamageNumbers guildTotalDamageNumber;
        public GameObject DebugWindowPrefab;
        public BattleBuffUI fieldActionUI;
        //public RectTransform UIRect;
        public GameObject buffUISettingBack;
        public ScrollEx buffUIScrollEX;

        public RenderTexture screenShootTx;
        public CharacterPageButton screenshootBt;
        public EquipmentPageIcon screenshootEx;
        public float DamageRandomScale;
        public float DamageScale;

        private float timeCount;
        private MyGameCtrl myGameCtrl=> MyGameCtrl.Instance;
        private float guildTotalDamage;
        private Action _Update;

        private float m_LastUpdateShowTime;    //上一次更新帧率的时间;
        private float m_UpdateShowDeltaTime = 0.05f;//更新帧率的时间间隔;
        private int m_FrameUpdate;//帧数;
        private int m_FPS;

        static string[] SPLIT = { "\n" };
        private List<string> debugStrList = new List<string>();
        public float TimeCount { get => timeCount; set => timeCount = value; }
        public List<string> DebugStrList { get => debugStrList;}

        private int[] UBButtonState = new int[5] { 0, 0, 0, 0, 0 };

        private List<byte[]> imageSaved = new List<byte[]>();
        public List<byte[]> imageSaved2 = new List<byte[]>(); //ex装备图标

        public Dictionary<Elements.eStateIconType, bool> ShowBuffDic => MainManager.Instance.PlayerSetting.ShowBuffDic;
        public GameObject battleStatus;
        public Toggle translucent;
        public InputField pauseTime;
        public InputField pauseTime2;
        public Button changeBattlePanel;
        private Dictionary<KeyCode, Action> inputActions;
        private Dictionary<KeyCode, int> keyToValue;
        public GameObject PlayerEnvironmentUI;
        public GameObject EnemyEnvironmentUI;
        public void Awake()
        {
            Instance = this;
            /*if (MainManager.Instance.UseNewBattleSystem)
            {
                NewManager.SetActive(true);
            }
            else
            {
                OldManager.SetActive(true);
            }*/

        }
        public void Start()
        {
            m_LastUpdateShowTime = Time.realtimeSinceStartup;
            if(myGameCtrl != null)
            {
                _Update += _Update_1;
            }
            _Update += _UpdateFPSCount; 
            timeScaleSlider.value = (int)(Math.Log(GuildManager.Instance.SettingData.calSpeed, 2) + 3);
            inputActions = new Dictionary<KeyCode, Action>
            {
                { KeyCode.RightArrow, StepButton }, // 方向右键
                { KeyCode.Mouse1, StepButton }, // 鼠标右键
                { KeyCode.Tab, changeBattlePanelButton }, // Tab键切换面板
                { KeyCode.Z, StatusShowButton }, // Z键显示状态
                { KeyCode.F5, RetryButton }, // F5键重打
                { KeyCode.C, RetryButton }, // C键重打
                { KeyCode.Escape, ExitButton2 }, // Escape键退出
                { KeyCode.B, SkipUBButton }, // Escape键退出
            };

            keyToValue = new Dictionary<KeyCode, int>
            {
                { KeyCode.Q, 0 },
                { KeyCode.W, 1 },
                { KeyCode.E, 2 },
                { KeyCode.R, 3 },
                { KeyCode.T, 4 },
                { KeyCode.Y, 5 },
                { KeyCode.U, 6 },
                { KeyCode.I, 7 },
                { KeyCode.O, 8 },
                { KeyCode.P, 9 },
                { KeyCode.LeftBracket, 10 }, // [键
                { KeyCode.RightBracket, 11 }, // ]键
                { KeyCode.Backslash, 12 } // \键
            };
        }
        public void SetPauseFrame()
        {
            OnPauseFrameEndEdit(pauseTime != null ? pauseTime.text : string.Empty);
        }
        public void SetPauseFrame2()
        {
            OnPauseFrameEndEdit(pauseTime2 != null ? pauseTime2.text : string.Empty);
        }
        public void OnPauseFrameEndEdit(string value)
        {
#if UNITY_5_3_OR_NEWER
            pauseTime.SetTextWithoutNotify(value);
            pauseTime2.SetTextWithoutNotify(value);
#endif
            myGameCtrl.stopFrame = int.TryParse(value, out int frame) && frame >= 0
                ? frame
                : -1;
        }
        public void ReStart()
        {
            guildTotalDamage = 0;
            foreach(Transform skill in parent)
            {
                Destroy(skill.gameObject);
            }
            debugStrList.Clear();
            imageSaved.Clear();
            imageSaved2.Clear();
            timeScaleSlider.value = GuildManager.Instance.calSlider.value;
        }
        private HashSet<KeyCode> pressedKeys = new HashSet<KeyCode>();
        private void Update()
        {
            if (BattleManager.Instance.skipping)
                return;
            _Update?.Invoke();
            // 遍历所有可能的按键
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                // 检测按键按下
                if (Input.GetKeyDown(key))
                {
                    // 如果集合为空，说明这是第一个按键
                    if (pressedKeys.Count == 0)
                    {
                        foreach (var kvp in inputActions)
                        {
                            if (Input.GetKeyDown(kvp.Key))
                            {
                                kvp.Value();
                            }
                        }
                    }

                    // 将按键添加到集合中
                    pressedKeys.Add(key);
                }

                // 检测按键松开
                if (Input.GetKeyUp(key))
                {
                    // 从集合中移除按键
                    pressedKeys.Remove(key);
                }
            }

            SetTimeScale();
            HandleTimeScale();
            HandleToggleKeys();
        }
        private void SetTimeScale() 
        {
            foreach (var kvp in keyToValue)
            {
                if (Input.GetKeyDown(kvp.Key) && timeScaleSlider.value != kvp.Value)
                {
                    timeScaleSlider.value = kvp.Value;
                    break;
                }
            }
        }
        private void HandleTimeScale()
        {
            if (Input.GetKey(KeyCode.UpArrow) && timeScaleSlider.value < timeScaleSlider.maxValue)
            {
                timeScaleSlider.value = Mathf.Clamp(timeScaleSlider.value + 1, timeScaleSlider.minValue, timeScaleSlider.maxValue);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && timeScaleSlider.value > timeScaleSlider.minValue)
            {
                timeScaleSlider.value = Mathf.Clamp(timeScaleSlider.value - 1, timeScaleSlider.minValue, timeScaleSlider.maxValue);
            }
        }

        private void HandleToggleKeys()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                AutoToggle.isOn = !AutoToggle.isOn;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetToggle.isOn = !SetToggle.isOn;
            }
        }
        private void _Update_1()
        {
            frameText.text = BattleHeaderController.CurrentFrameCount + "";
        }
        private void _UpdateFPSCount()
        {
            m_FrameUpdate++;
            if (Time.realtimeSinceStartup - m_LastUpdateShowTime >= m_UpdateShowDeltaTime)
            {
                m_FPS = Mathf.RoundToInt(m_FrameUpdate / (Time.realtimeSinceStartup - m_LastUpdateShowTime));
                m_FrameUpdate = 0;
                m_LastUpdateShowTime = Time.realtimeSinceStartup;
            }
            FPSText.text = "" + m_FPS;
        }
        private void UpdateTimeCount()
        {
            int lastTime = Mathf.RoundToInt(90 - TimeCount);
            if (lastTime >= 60)
            {
                timeText.text = "01:" + ((lastTime-60)>=10?  ((lastTime - 60) + ""):("0" + (lastTime - 60)));
            }
            else if(lastTime>=0)
            {
                timeText.text = "00:" + (lastTime>=10?("" + lastTime):("0" + lastTime));
            }
            else
            {
                timeText.text = "<color=#FF0000>00:00</color>";
            }
        }
        public void UpdateTimeCount(float lastTimefloat)
        {
            int lastTime = Mathf.CeilToInt(lastTimefloat);
            if (lastTime >= 60)
            {
                timeText.text = "01:" + ((lastTime - 60) >= 10 ? ((lastTime - 60) + "") : ("0" + (lastTime - 60)));
            }
            else if (lastTime >= 0)
            {
                timeText.text = "00:" + (lastTime >= 10 ? ("" + lastTime) : ("0" + lastTime));
            }
            else
            {
                timeText.text = "<color=#FF0000>00:00</color>";
            }
        }

        // private static StreamWriter sw = new StreamWriter(File.OpenWrite("logmessage.txt")){AutoFlush = true};
        public void LogMessage(string word, eLogMessageType logMessageType, bool isOther)
        {
            long fpsCount = 0;
            if (myGameCtrl != null)
            {
                fpsCount = BattleHeaderController.CurrentFrameCount;
            }
            // string logtext = $"({fpsCount}/{BattleManager.Instance.FrameCount}){word}\n";
            string logtext = $"({fpsCount}){word}\n";
            debugStrList.Add(logtext);
            // sw.Write(logtext);
#if UNITY_5_3_OR_NEWER
            Text debugText = debugText1;//= isOther ? debugText2 : debugText1;
            ScrollRect scrollRect = scrollRect1;//isOther ? scrollRect2 : scrollRect1;
            int overText = debugText.text.Length - maxTextLength;
            if (overText > 0)
            {
                string[] messages = debugText.text.Split(SPLIT, StringSplitOptions.None);
                Stack<string> messageStack = new Stack<string>();
                int lengthCount = 0;
                for (int i = messages.Length - 1; i >= 0; i--)
                {
                    lengthCount += messages[i].Length;
                    if (lengthCount < maxTextLength)
                    {
                        messageStack.Push(messages[i]);
                    }
                    else
                    {
                        break;
                    }
                }
                debugText.text = "";
                foreach (string str in messageStack)
                {
                    debugText.text += str;
                }
                //debugText2.text = debugText2.text.Substring(overText+5);
                //debugText2.text = "";
            }
            debugText.text += logtext;
            scrollRect.verticalNormalizedPosition = 0;
#else
#if ENABLE_THREADING
            Console.Write($"[Thread #{Thread.CurrentThread.ManagedThreadId}] " + logtext);
#else
            Console.Write(logtext);
#endif
#endif
            if (logMessageType == eLogMessageType.ERROR)
            {
#if UNITY_EDITOR
                Debug.Log(fpsCount + "-" + word);
#endif
                /*if (BattleManager.Instance.ShowErrorMessage)
                {
                    MainManager.Instance.WindowConfigMessage("<color=#FF0000>" + word + "</color>", null, null);
                }*/
            }
            /*else
            {
#if UNITY_EDITOR
                Debug.Log(fpsCount + "-" + word);
#endif
            }*/
            //Canvas.ForceUpdateCanvases();
        }
        public void LogMessage(string word,eLogMessageType messageType,UnitCtrl unitCtrl)
        {
            if (BattleManager.Instance.skipping) return;
            if (unitCtrl == null)
            {
                LogMessage(word, messageType, false);
                return;
            }
            bool other = unitCtrl.IsOther;
            LogMessage(unitCtrl.UnitName + word, messageType, other);
        }
        public void ShowFullDebugWord()
        {
            if (BattleHeaderController.Instance.GetIsPause())
            {
                GameObject a = Instantiate(DebugWindowPrefab);
                a.transform.SetParent(BaseBackManager.Instance.latestUIback.transform, false);
            }
            else
            {
                MainManager.Instance.WindowMessage("请在暂停时查看详情！");
            }
        }
        public void ShowSkillName(string skillName, Transform transform)
        {
            if (BattleManager.Instance.skipping) return;
            if(skillName == "") { return; }
            GameObject a = Instantiate(skillNamePerfab);
            a.transform.SetParent(parent, false);
            a.GetComponent<SkillNameImage>().SetName(skillName, transform);

        }
        public void SetUI_2(MyGameCtrl gameCtrl)
        {
            guildTotalDamageNumber.gameObject.SetActive(false);
            for (int i = 0; i < PlayerUI.Count; i++)
            {
                if (gameCtrl.playerUnitCtrl.Count > i)
                {
                    GameObject a = Instantiate(buffUIPrefab);
                    CharacterBuffUIController b = a.GetComponent<CharacterBuffUIController>();
                    //BattleManager.Instance.PlayersList[i].SetUI(PlayerUI[i], b);
                    gameCtrl.playerUnitCtrl[i].SetUI(PlayerUI[i], b);
                    b.SetBuffUI(rularSprites[i], gameCtrl.playerUnitCtrl[i]);
                    PlayerUI[i].SetAlive();
                    ChangeUbSet(i, false);
                    PlayerUI[i].gameObject.SetActive(true);
                }
                else
                {
                    PlayerUI[i].gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < EnemyUI.Count; i++)
            {
                if (gameCtrl.enemyUnitCtrl.Count > i)
                {
                    GameObject a = Instantiate(buffUIPrefab);
                    CharacterBuffUIController b = a.GetComponent<CharacterBuffUIController>();
                    //BattleManager.Instance.EnemiesList[i].SetUI(EnemyUI[i], b);
                    gameCtrl.enemyUnitCtrl[i].SetUI(EnemyUI[i], b);
                    b.SetBuffUI(rularSprites[i + 5], gameCtrl.enemyUnitCtrl[i]);
                    if (gameCtrl.tempData.isGuildBattle)
                    {
                        gameCtrl.enemyUnitCtrl[i].OnDamage += RefreshGuildEnemyTotalDamage2;
                        guildTotalDamageNumber.gameObject.SetActive(true);
                    }
                    else
                    {
                        EnemyUI[i].gameObject.SetActive(true);
                    }
                    EnemyUI[i].SetAlive();
                }
                else
                {
                    EnemyUI[i].gameObject.SetActive(false);
                }
            }

            AutoToggle.isOn = gameCtrl.IsAutoMode;
            SetToggle.isOn = false;
            SetToggle.isOn = gameCtrl.IsSetMode;
            if (!myGameCtrl.tempData.isGuildBattle)
            {
#if !UNITY_EDITOR
                debugBack.SetActive(false);
#endif
                // backGroundImage.sprite = jjcBackGround;
            }
            StartCoroutine(AutoSaveImage2());
            SetTalentBonus();
        }
        public void SetSummonUI(UnitCtrl summon)
        {
            GameObject a = Instantiate(buffUIPrefab);
            CharacterBuffUIController b = a.GetComponent<CharacterBuffUIController>();
            //BattleManager.Instance.EnemiesList[i].SetUI(EnemyUI[i], b);
            summon.SetUIForSuommon();
            b.SetBuffUI(rularSprites[0], summon,true);
        }

        public void StepButton()
        {
            if (!BattleHeaderController.Instance.IsPaused)
            {
                PauseButton();
            }
            BattleManager.Instance.stepping = true;
            PauseButton();
        }

        public void SkipUBButton()
        {
            BattleHeaderController.Instance.OnClickSkipUBButton();
        }

        public void SkipButton()
        {
            BattleManager.Instance.skipping = true;
        }

        public void ExitButton()
        {
            if (MyGameCtrl.Instance.IsGAUbMode)
            {
                MainManager.Instance.WindowMessage("请先停止优化！");
            }
            else if (BattleManager.Instance?.EnemyList?.All(enemy => enemy.Hp <= 0) ?? true)
            {
                ExitButton2();
            }
            else
            {
                for (int i = 0; i < BattleManager.Instance.EnemyList.Count; i++)
                {
                    BattleManager.Instance.EnemyList[i].Hp = 0;
                    BattleManager.Instance.EnemyList[i].SetState(UnitCtrl.ActionState.DIE);
                }
            }
        }

        public void ExitButton2()
        {
            bool isGuildBossBattle = false;
            if (MyGameCtrl.Instance != null)
            {
                isGuildBossBattle = MyGameCtrl.Instance.tempData.isGuildBattle;
            }

            Time.timeScale = 1;
            if (isGuildBossBattle)
            {
                SceneManager.LoadScene("GuildScene");
            }
            else
            {
                SceneManager.LoadScene("BeginScene");
            }
        }

        public void RetryButton()
        {
            GuildManager.Instance.SettingInputs[8].text = pauseTime.text;
            GuildCalculator.Instance.guildPageUI.SetActive(!GuildCalculator.Instance.guildPageUI.activeSelf);
            StartCoroutine(RetryButton2());
        }

        public IEnumerator RetryButton2()
        {
            BattleManager.Instance.CancelBattle();
            while(!GuildCalculator.Instance.isFinishCalc)
            {
                yield return null;
            }
            GuildManager.Instance.ReStartCalButton();
        }

        public void PauseButton()
        {
            {
                MyGameCtrl.Instance.PauseButton();
            }
        }
        public void CharacterUBTryingButton(int charidx)
        {
            if (MyGameCtrl.Instance != null)
            {
                if (MyGameCtrl.Instance.ForceAutoMode)
                {
                    MainManager.Instance.WindowMessage("强制自动战斗模式下无法手动释放UB！");
                    return;
                }
                switch (UBButtonState[charidx])
                {
                    case 0:
                        UBButtonState[charidx] = 1;
                        if (!BattleHeaderController.Instance.IsPaused)
                            MyGameCtrl.Instance.TryingExecUB(charidx);
                        StartCoroutine(UBCool(charidx));
                        break;
                    case 1:
                        ChangeUbSet(charidx, true);
                        break;
                    default:
                        ChangeUbSet(charidx, false);
                        break;
                }
                MyGameCtrl.Instance.TryingExecUB(charidx);
            }
        }
        private IEnumerator UBCool(int idx)
        {
            yield return new WaitForSeconds(0.6f);
            if (UBButtonState[idx] == 1)
                UBButtonState[idx] = 0;
        }

        private void ChangeUbSet(int idx, bool isSet)
        {
            PlayerUI[idx].ShowContinousPress(isSet);
            UBButtonState[idx] = isSet ? 2 : 0;
            MyGameCtrl.Instance.playerUnitCtrl[idx].pressing = isSet;
            if (AllStatesAreTwo(UBButtonState))
            {
                SetToggle.isOn = true;
            }
            else
            {
                SetToggle.isOn = false;
            }
        }

        private void HideContinoueText()
        {
            foreach(var a in PlayerUI)
            {
                a.ShowContinousPress(false);
            }
        }
        public void OnAutoToggleSwitched()
        {
            if (MyGameCtrl.Instance.ForceAutoMode)
            {
                MainManager.Instance.WindowMessage("强制自动战斗模式下无法手动释放UB！");
                return;
            }
            myGameCtrl.IsAutoMode = AutoToggle.isOn;
        }
        public void OnSpeedToggleSwitched()
        {
            myGameCtrl.SetBattleSpeed(SpeedToggle.isOn ? 2 : 1);
        }
        public void OnSetToggleSwitched()
        {
            if (MyGameCtrl.Instance.ForceAutoMode)
            {
                MainManager.Instance.WindowMessage("强制自动战斗模式下无法手动释放UB！");
                return;
            }
            if (SetToggle.isOn)
            {
                for (int i = 0; i < MyGameCtrl.Instance.playerUnitCtrlDic.Count; i++){
                    ChangeUbSet(i, true);
                }
            }
            else if (!SetToggle.isOn && AllStatesAreTwo(UBButtonState))
            {
                for (int i = 0; i < MyGameCtrl.Instance.playerUnitCtrlDic.Count; i++)
                {
                    ChangeUbSet(i, false);
                };
            }

        }
        private bool AllStatesAreTwo(int[] states)
        {
            int count = MyGameCtrl.Instance.playerUnitCtrlDic.Count;
            for (int i = 0; i < count; i++)
            {
                if (states[i] != 2)
                {
                    return false;
                }
            }
            return true;
        }
        public void OnTimeScaleSliderDraged()
        {
            int scaleRate = Mathf.RoundToInt(timeScaleSlider.value);
            float timeScale = Mathf.Pow(2, (scaleRate - 3));
            string timeText = "x";
            if (timeScale < 1)
            {
                timeText += "1/" + Mathf.RoundToInt(1.0f / timeScale);
            }
            else{
                timeText = "x" + timeScale; 
                GuildManager.Instance.SettingData.calSpeed = (int)timeScale;
            }
            timeScaleText.text = timeText;
            //Time.timeScale = timeScale;
            SetTimeScale(timeScale);
            GuildManager.Instance.calSlider?.SetValueWithoutNotify(timeScaleSlider.value);
    }
        public void SetTimeScale(float scale)
        {
            {
                MyGameCtrl.Instance.SetBattleSpeed(scale);
            }
        }
        public void SetEnvironmentUI(int enyType, bool isActive, bool isEnemy)
        {
            GameObject targetUI = isEnemy ? PlayerEnvironmentUI : EnemyEnvironmentUI;

            if (isActive)
            {
                if (enyType >= buffDebuffIcons.Count)
                {
                    LogMessage("未设置" + ((eStateIconType)enyType).GetDescription() + "的环境Buff图标！", eLogMessageType.ERROR, false);
                    return;
                }
                Sprite iconSprite = buffDebuffIcons[enyType];
                Transform imageTransform = targetUI.transform.Find("Image");
                Image imageComponent = imageTransform.GetComponent<Image>();
                imageComponent.sprite = iconSprite;
            }

            targetUI.SetActive(isActive);
        }
        public void SetTalentBonus()
        {
            // 隐藏所有属性图标
            for (int i = 0; i < talentBonus.Count; i++)
            {
                talentBonus[i].gameObject.SetActive(false);
            }

            bool anyBonusGreaterThanZero = BattleManager.Instance.UnitTalentFormationBonusDictionary.Values.Any(value => value > 0);
            talentBonus[5].SetActive(anyBonusGreaterThanZero);

            if (!anyBonusGreaterThanZero) return;

            // 设置对应的图标
            int bonusIndex = 0;
            for (int talentId = 1; talentId <= 5 && bonusIndex < 2; talentId++)
            {
                if (BattleManager.Instance.UnitTalentFormationBonusDictionary.TryGetValue(talentId, out long bonusValue) && bonusValue > 0)
                {
                    if (bonusIndex < talentBonus.Count)
                    {
                        if (bonusIndex == 0)
                        {
                            talentBonus[0].sprite = talentBonusBackground[talentId - 1];
                            talentBonus[0].gameObject.SetActive(true);
                            talentBonus[2].sprite = talentBonusIcon[talentId - 1];
                            talentBonus[4].sprite = talentBonusIcon[talentId - 1];
                            talentBonus[4].gameObject.SetActive(true);
                        }
                        else if (bonusIndex == 1)
                        {
                            talentBonus[4].gameObject.SetActive(false);
                            talentBonus[1].sprite = talentBonusBackground2[talentId - 2];
                            talentBonus[1].gameObject.SetActive(true);
                            talentBonus[2].gameObject.SetActive(true);
                            talentBonus[3].sprite = talentBonusIcon[talentId - 1];
                            talentBonus[3].gameObject.SetActive(true);
                        }
                        bonusIndex++;
                    }
                }
            }
        }
        public Sprite GetAbnormalIconSprite(eStateIconType stateIconType)
        {
            int index;
            try
            {
                index = int.Parse(stateIconType.GetDescription());
                return buffDebuffIcons[index];
            }
            catch
            {
                LogMessage("未设置" + stateIconType.GetDescription() + "的技能图标！", eLogMessageType.ERROR, false);
            }
            return buffDebuffIcons[0];
        }
        public void SetDamageNumber(Vector3 pos, long value, eDamageType damageType, eDamageEffectType effectType, bool isCritical = false, bool isTotal = false)
        {
            string valuestr = value.ToString();
            List<Sprite> numbers = new List<Sprite>();
            List<Sprite> sprites = number_physical_large;
            if (damageType == eDamageType.MGC)
            {
                sprites = number_magical_large;
            }
            for (int i = 0; i < valuestr.Length; i++)
            {
                //int num = (int)valuestr[i];
                int num = valuestr[i] - 48;
                numbers.Add(sprites[num]);
            }
            Sprite head = null;
            if (isCritical)
            {
                if (damageType == eDamageType.MGC)
                {
                    head = sprite_critical_magical;
                }
                else
                {
                    head = sprite_critical_physical;
                }
            }
            if (isTotal)
            {
                head = damageType == eDamageType.MGC ? sprite_total_magical : sprite_total_physical;
            }
            float scale = 1;
            if (effectType == eDamageEffectType.LARGE)
            {
                scale = 2;
            }
            SetPrefabNumber(pos + numberPosFix, numbers, head, scale);
        }
        public void SetHealNumber(Vector3 pos, int value, float scale = 1)
        {
            string valuestr = value.ToString();
            List<Sprite> numbers = new List<Sprite>();
            List<Sprite> sprites = number_heal_large;
            for (int i = 0; i < valuestr.Length; i++)
            {
                //int num = (int)valuestr[i];
                int num = valuestr[i] - 48;
                numbers.Add(sprites[num]);
            }
            SetPrefabNumber(pos + numberPosFix, numbers, null, scale);
        }
        public void SetEnergyNumber(Vector3 pos, int value, float scale = 1)
        {
            List<Sprite> numbers = new List<Sprite>();
            List<Sprite> sprites = number_energy_large;
            if (value < 0)
            {
                numbers.Add(sprites[10]);
                value = Mathf.Abs(value);
            }
            string valuestr = value.ToString();
            for (int i = 0; i < valuestr.Length; i++)
            {
                //int num = (int)valuestr[i];
                int num = valuestr[i] - 48;
                if (num >= 0 && num <= 9)
                {
                    numbers.Add(sprites[num]);
                }
            }
            SetPrefabNumber(pos + numberPosFix, numbers, null, scale);
        }
        private float HeldRandom(float scale=0.1f)
        {
            return BattleManager.HeldRandom(-100, 100) / 100.0f*scale;
        }
        public void SetMissEffect(Vector3 pos0, float scale = 1)
        {
            Vector3 randomPos = new Vector3(HeldRandom(), HeldRandom(), 0);
            Vector3 pos = pos0 + numberPosFix + randomPos;
            GameObject a = Instantiate(missPrefab);
            a.transform.position = pos;
            a.GetComponent<DamageNumbers>().SetMiss(scale*DamageScale);
        }
        public void RefreshGuildEnemyTotalDamage(long value)
        {
            guildTotalDamage += value;
            int valueint = (int)guildTotalDamage;
            string valuestr = valueint.ToString();
            List<Sprite> numbers = new List<Sprite>();
            List<Sprite> sprites = number_physical_large;
            for (int i = 0; i < valuestr.Length; i++)
            {
                //int num = (int)valuestr[i];
                int num = valuestr[i] - 48;
                numbers.Add(sprites[num]);
            }

            guildTotalDamageNumber.SetDamageNumber(null, numbers, sprite_total_physical, 2);
        }
        public void RefreshGuildEnemyTotalDamage2(bool byAttack, LongWithEx value,bool critical)
        {
            RefreshGuildEnemyTotalDamage(value);
        }
        private void SetPrefabNumber(Vector3 pos, List<Sprite> numbers, Sprite head = null, float scale = 1)
        {
            GameObject a = Instantiate(numberPrefab);
            Vector3 randomPos = new Vector3(HeldRandom(0.65f), HeldRandom(0.35f), 0);
            a.transform.position = pos + randomPos * DamageRandomScale;
            a.GetComponent<DamageNumbers>().SetDamageNumber(numbers, head, scale * DamageScale);

        }
        public void StartFieldEffect(ChangeParameterFieldData dataBase)
        {
            fieldActionUI.gameObject.SetActive(true);
            Elements.eStateIconType type = UnitCtrl.BUFF_DEBUFF_ICON_DIC[dataBase.BuffParamKind].DebuffIcon;
            string des = "" + dataBase.Value;
            fieldActionUI.Init(GetAbnormalIconSprite((eStateIconType)(int)type), type, dataBase.StayTime,des, a => fieldActionUI.gameObject.SetActive(false));
        }

        public IEnumerator CaptureByUI(RectTransform UIRect,int extendValue=0)
        {
#if UNITY_5_3_OR_NEWER
            //等待帧画面渲染结束
            yield return new WaitForEndOfFrame();

            int width = (int)(UIRect.rect.width)+2*extendValue;
            int height = (int)(UIRect.rect.height)+2*extendValue;

            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

            //左下角为原点（0, 0）
            float leftBtmX = UIRect.transform.position.x + UIRect.rect.xMin-extendValue;
            float leftBtmY = UIRect.transform.position.y + UIRect.rect.yMin-extendValue;

            //从屏幕读取像素, leftBtmX/leftBtnY 是读取的初始位置,width、height是读取像素的宽度和高度
            tex.ReadPixels(new Rect(leftBtmX, leftBtmY, width, height), 0, 0);
            //执行读取操作
            tex.Apply();
            byte[] bytes = tex.EncodeToPNG();
            //保存
            imageSaved.Add(bytes);
#else
            yield return null;
#endif
        }
        public IEnumerator AutoSaveImage()
        {
            yield return null;
            foreach (var a in PlayerUI)
            {
                StartCoroutine(CaptureByUI(a.ScreenShotTrans,MainManager.Instance.PlayerSetting.pixFix));
                yield return null;
                yield return null;
            }
            EnemyUI[0].HideBackImageTemp();
            yield return null;
            StartCoroutine(CaptureByUI(EnemyUI[0].ScreenShotTrans, MainManager.Instance.PlayerSetting.pixFix));
            //imageSaved.Add(EnemyUI[0].characterImage.sprite.texture.EncodeToPNG());
        }
        public IEnumerator AutoSaveImage2()
        {
            yield return null;
            yield return null;
            foreach (var unitctrl in myGameCtrl.playerUnitCtrl.ToArray())
            {
                if (unitctrl.IsSummonOrPhantom) continue;
                screenshootBt.SetButton(unitctrl);
                yield return null;
                yield return new WaitForEndOfFrame();
                byte[] bytes = SaveRenderTexture(screenShootTx);
                imageSaved.Add(bytes);
                yield return null;
            }
            screenshootBt.SetButton(myGameCtrl.enemyUnitCtrl[0]);
            yield return null;
            yield return new WaitForEndOfFrame();
            byte[] bytes2 = SaveRenderTexture(screenShootTx);
            imageSaved.Add(bytes2);
            yield return null;
            screenshootEx.SetActive(true);
            for (int i = 0; i < myGameCtrl.tempData.playerList.Count; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int[] exEquipSubsData = null;
                    if (myGameCtrl.tempData.playerList[i].exEquipSubs != null &&
                        myGameCtrl.tempData.playerList[i].exEquipSubs.ContainsKey(j))
                    {
                        exEquipSubsData = myGameCtrl.tempData.playerList[i].exEquipSubs[j];
                    }
                    screenshootEx.SetExEquipmentIcon(myGameCtrl.tempData.playerList[i].unitId, myGameCtrl.tempData.playerList[i].exEquip[j], myGameCtrl.tempData.playerList[i].exEquipLevel[j], exEquipSubsData, j);
                    yield return null;
                    yield return new WaitForEndOfFrame();
                    byte[] equipBytes = SaveRenderTexture(screenShootTx);
                    imageSaved2.Add(equipBytes); // 保存到imageSaved2
                    yield return null;
                }
            }
            screenshootEx.SetActive(false);
        }
        //将RenderTexture保存成一张png图片  
        public byte[] SaveRenderTexture(RenderTexture rt)
        {
#if UNITY_5_3_OR_NEWER
            RenderTexture prev = RenderTexture.active;
            RenderTexture.active = rt;
            Texture2D png = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
            png.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
            png.Apply();
            byte[] bytes = png.EncodeToPNG();
            DestroyImmediate(png);
            png = null;
            RenderTexture.active = prev;
            return bytes;
#else
            return null;
#endif

        }
        public List<byte[]> GetCharactersImage()
        {
            return imageSaved;
        }
        // public List<byte[]> GetExequipImage()
        // {
        //     return imageSaved2;
        // }
        public void SetBUFFUIShowButton()
        {
            if (!BattleHeaderController.Instance.GetIsPause())
            {
              PauseButton();
              MainManager.Instance.WindowMessage("已暂停");
            }
                battleStatus.SetActive(false);
                buffUISettingBack.SetActive(true);
                buffUIScrollEX.ClearAll();
                foreach (Elements.eStateIconType stateIconType in Enum.GetValues(typeof(Elements.eStateIconType))
                             .OfType<Elements.eStateIconType>().OrderByDescending(type => 
                                 type == Elements.eStateIconType.NONE ? int.MaxValue : 
                                 CharacterPageButton.SetAbnormalIconsCounter.TryGetValue(type, out var val) ? val : 0))
                {
                    buffUIScrollEX.CreatePrefab(
                        a =>
                        {
                            bool isshow = ShowBuffDic.TryGetValue(stateIconType, out bool value) && value;
                            a.GetComponent<BUFFShowPrefab>().Init(GetAbnormalIconSprite((eStateIconType)(int)stateIconType), stateIconType, isshow);
                        });
                }
                buffUIScrollEX.AutoFit();
        }
        public void SaveBUFFUISetting()
        {
            foreach(GameObject a in buffUIScrollEX.Prefabs)
            {
                var buff = a.GetComponent<BUFFShowPrefab>();
                if (buff != null)
                {
                    if (ShowBuffDic.ContainsKey(buff.stateIconType))
                        ShowBuffDic[buff.stateIconType] = buff.toggle.isOn;
                    else
                        ShowBuffDic.Add(buff.stateIconType,buff.toggle.isOn);
                }
            }
            MainManager.Instance.SavePlayerSetting();
        }
        public void StatusShowButton()
        {
            buffUISettingBack.SetActive(false);
            battleStatus.SetActive(!battleStatus.activeSelf);
        }
    
        public void changeBattlePanelButton()
        {
            battleStatus.SetActive(false);
            buffUISettingBack.SetActive(false);
            GuildCalculator.Instance.guildPageUI.SetActive(!GuildCalculator.Instance.guildPageUI.activeSelf);
        }
        public void ChangeTranslucent()
        {
            if (translucent.isOn)
            {
                Color currentColor = battleStatus.GetComponent<Image>().color;
                currentColor.a = 255f / 255f;
                battleStatus.GetComponent<Image>().color = currentColor;
            }
            else
            {
                Color currentColor = battleStatus.GetComponent<Image>().color;
                currentColor.a = 100f / 255f;
                battleStatus.GetComponent<Image>().color = currentColor;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public float DamageRandomScale;
        public float DamageScale;

        private long fpsCount;
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

        public Dictionary<Elements.eStateIconType, bool> ShowBuffDic => MainManager.Instance.PlayerSetting.ShowBuffDic;
        public GameObject battleStatus;
        public Toggle translucent;
        public InputField pauseTime;
        public Button changeBattlePanel;

        private void Awake()
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
        private void Start()
        {
            m_LastUpdateShowTime = Time.realtimeSinceStartup;
            if(myGameCtrl != null)
            {
                _Update += _Update_1;
            }
            _Update += _UpdateFPSCount;
            timeScaleSlider.value = GuildManager.Instance.calSlider.value;
        }
        private void Update()
        {
            _Update?.Invoke();
            if (Input.GetMouseButtonDown(1)|| Input.GetKeyDown(KeyCode.RightArrow))
            { 
                StepButton(); //鼠标右键或者右方向键 触发step
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

        //private static StreamWriter sw = new StreamWriter(File.OpenWrite("logmessage.txt")){AutoFlush = true};
        public void LogMessage(string word, eLogMessageType logMessageType, bool isOther)
        {
            if (myGameCtrl != null)
            {
                fpsCount = BattleHeaderController.CurrentFrameCount;
            }
            string logtext = $"({fpsCount}/{BattleManager.Instance.FrameCount}){word}\n";
            debugStrList.Add(logtext);
            //sw.WriteLine(logtext);
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
                }
                else
                {
                    EnemyUI[i].gameObject.SetActive(false);
                }
            }
            var data = GuildManager.Instance.SettingData.GetCurrentPlayerGroup();
            // SetToggle.isOn = GuildManager.Instance.SetModeToggle.isOn;
            AutoToggle.isOn = data.useAutoMode;
            SetToggle.isOn = data.useSetMode;
            if (!myGameCtrl.tempData.isGuildBattle)
            {
                debugBack.SetActive(false);
                backGroundImage.sprite = jjcBackGround;
            }
            StartCoroutine(AutoSaveImage2());
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
            if (!BattleHeaderController.Instance.IsPaused) return;
            BattleManager.Instance.stepping = true;
            PauseButton();
        }

        public void SkipButton()
        {
            BattleManager.Instance.skipping = true;
        }

        public void ExitButton()
        {
            if ((BattleManager.Instance?.BossUnit?.Hp ?? 0) == 0)
            {
                ExitButton2();
            }
            else
            {
                BattleManager.Instance.BossUnit.Hp = 0;
                BattleManager.Instance.BossUnit.SetState(UnitCtrl.ActionState.DIE);
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
            GuildManager.Instance.StartCalButton();
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
                MyGameCtrl.Instance.playerUnitCtrl[charidx].pressing = false;
                switch (UBButtonState[charidx])
                {
                    case 0:
                        UBButtonState[charidx] = 1;
                        MyGameCtrl.Instance.TryingExecUB(charidx);
                        StartCoroutine(UBCool(charidx));
                        SetToggle.isOn = false;
                        break;
                    case 1:
                        UBButtonState[charidx] = 2;
                        MyGameCtrl.Instance.playerUnitCtrl[charidx].pressing = true;
                        PlayerUI[charidx].ShowContinousPress(true);
                        break;
                    default:
                        PlayerUI[charidx].ShowContinousPress(false);
                        UBButtonState[charidx] = 0;
                        SetToggle.isOn = false;
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
                for (int i = 0; i < MyGameCtrl.Instance.playerUnitCtrl.Count; i++){
                    MyGameCtrl.Instance.playerUnitCtrl[i].pressing = true;
                    PlayerUI[i].ShowContinousPress(true);
                    UBButtonState[i] = 2;
                }
            }
            else if (!SetToggle.isOn && AllStatesAreTwo(UBButtonState))
            {
                for (int i = 0; i < MyGameCtrl.Instance.playerUnitCtrl.Count; i++)
                {
                    MyGameCtrl.Instance.playerUnitCtrl[i].pressing = false;
                    PlayerUI[i].ShowContinousPress(false);
                    UBButtonState[i] = 0;
                };
            }

        }
        private bool AllStatesAreTwo(int[] states)
        {
            foreach (int state in states)
            {
                if (state != 2)
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
            GuildManager.Instance.calSlider.value = timeScaleSlider.value;
    }
        public void SetTimeScale(float scale)
        {
            {
                MyGameCtrl.Instance.SetBattleSpeed(scale);
            }
        }
        public Sprite GetAbnormalIconSprite(eStateIconType stateIconType)
        {
            int index = 40;
            try
            {
                index = int.Parse(stateIconType.GetDescription());
                return buffDebuffIcons[index];
            }
            catch
            {
                LogMessage("未设置" + stateIconType.GetDescription() + "的技能图标！", eLogMessageType.ERROR, false);
            }
            return buffDebuffIcons[40];
        }
        public void SetDamageNumber(Vector3 pos,int value, eDamageType damageType, eDamageEffectType effectType, bool isCritical = false, bool isTotal = false)
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
        public void RefreshGuildEnemyTotalDamage(float value)
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
        public void RefreshGuildEnemyTotalDamage2(bool byAttack, FloatWithEx value,bool critical)
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

        }
        //将RenderTexture保存成一张png图片  
        public byte[] SaveRenderTexture(RenderTexture rt)
        {
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

        }
        public List<byte[]> GetCharactersImage()
        {
            return imageSaved;
        }
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
                            a.GetComponent<BUFFShowPrefab>().Init(stateIconType, isshow);
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

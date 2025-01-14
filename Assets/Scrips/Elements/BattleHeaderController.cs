// Decompiled with JetBrains decompiler
// Type: Elements.BattleHeaderController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using Elements.Battle;
using UnityEngine;
//using Elements.Data;

namespace Elements
{
    public class BattleHeaderController : MonoBehaviour // : SingletonMonoBehaviour<BattleHeaderController>, ISingletonField
    {
        public static BattleHeaderController Instance;
        public static int CurrentFrameCount;
        public void ReStart()
        {
            CurrentFrameCount = 0;
            IsPaused = false;
        }
        private void Awake()
        {
            Instance = this;
            CurrentFrameCount = 0;
        }
        private void OnDestroy()
        {
            Instance = null;
            CurrentFrameCount = 0;
        }
        /*internal void PauseNoMoreTimeUp(bool v)
        {
            //throw new NotImplementedException();
        }*/
        public void PauseNoMoreTimeUp(bool _pause)
        {
            if (!isNoMoreTime || _pause == noMoreTimePause)
                return;
            noMoreTimePause = _pause;
            //this.timeUpFrameSprite.SetActive(!_pause);
            //this.timeLabel2.SetActive(!_pause);
            //for (int index = 0; index < this.noMoreTimeTweeners.Count; ++index)
           //     this.noMoreTimeTweeners[index].enabled = !_pause;
        }
        internal void SetRestTime(float battleLeftTime)
        {
            // throw new NotImplementedException();
            MyGameCtrl.Instance.SetTimeCount(battleLeftTime);
        }
        /*
        private const int ABSENT_USER_UNIT_ID = 1;
        private const float TIME_UP_FRAME_SCALE = 1.6f;
        [SerializeField]
        private CustomUIButton pauseButton;
        [SerializeField]
        private CustomUILabel timeLimit;
        [SerializeField]
        private GameObject waveObject;
        [SerializeField]
        private CustomUILabel wave;
        [SerializeField]
        private CustomUILabel waveIndicaterLabel;
        [SerializeField]
        private CustomUILabel rewardsLabel;
        [SerializeField]
        private GameObject rewardsObject;
        [SerializeField]
        private CustomUILabel goldLabel;
        [SerializeField]
        private GameObject goldObject;
        [SerializeField]
        private List<TweenScale> goldTweenScaleList;
        [SerializeField]
        private List<TweenAlpha> goldTweenAlphaList;
        [SerializeField]
        private List<TweenScale> treasureTweenScaleList;
        [SerializeField]
        private List<TweenAlpha> treasureTweenAlphaList;
        [SerializeField]
        private PartsBossGauge partsBossGauge;
        [SerializeField]
        private CustomUILabel timeLabel2;
        [SerializeField]
        private PartsBossTotalDamage partsBossTotalDamage;
        [SerializeField]
        private PartsSkillWindow partsSkillWindow;
        [SerializeField]
        private PartsCrownIcon crownIcon;
        [SerializeField]
        private List<UITweener> noMoreTimeTweeners;
        [SerializeField]
        private UISprite timeUpFrameSprite;
        [SerializeField]
        private Transform pointCounterParent;
        [SerializeField]
        private GameObject rehearsalIcon;
        private ViewManager viewManager = ManagerSingleton<ViewManager>.Instance;*/
        private BattleManager battleManager;
        private int waveNum;
        private int waveMax;
        private int preSetMin = -1;
        private int preSetSec = -1;
        public bool IsPaused;/*
        private TowerTempData towerTempData;
        private SpaceBattleTempData spaceBattleTemp;
        private ReplayTempData replayTempData;*/
        private bool isNoMoreTime;
        /*private float nextSeTiming = 10f;
        private const int NO_MORE_NOTIFY_TIME = 10;
        private const int NO_MORE_NOTIFY_TIME_2 = 5;
        private const int NO_MORE_NOTIFY_TIME_3 = 3;
        [SerializeField]
        private GameObject iconTimeCount;
        [SerializeField]
        private GameObject iconTimeCount2;
        [SerializeField]
        private GameObject iconTimeCount3;
        private static readonly Dictionary<eBattleCategory, Dictionary<BattleHeaderController.eHeaderContents, bool>> headerContentsFlagDictionary = new Dictionary<eBattleCategory, Dictionary<BattleHeaderController.eHeaderContents, bool>>((IEqualityComparer<eBattleCategory>)new eBattleCategory_DictComparer())
{
{
eBattleCategory.QUEST,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
true
},
{
BattleHeaderController.eHeaderContents.REWARD,
true
},
{
BattleHeaderController.eHeaderContents.GOLD,
true
}
}
},
{
eBattleCategory.QUEST_REPLAY,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
true
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.TRAINING,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
true
},
{
BattleHeaderController.eHeaderContents.REWARD,
true
},
{
BattleHeaderController.eHeaderContents.GOLD,
true
}
}
},
{
eBattleCategory.DUNGEON,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.ARENA,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.FRIEND,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.ARENA_REPLAY,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.CLAN_BATTLE,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.GLOBAL_RAID,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.STORY,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
true
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.TUTORIAL,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
true
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.GRAND_ARENA,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.GRAND_ARENA_REPLAY,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.HATSUNE_BATTLE,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
true
},
{
BattleHeaderController.eHeaderContents.REWARD,
true
},
{
BattleHeaderController.eHeaderContents.GOLD,
true
}
}
},
{
eBattleCategory.HATSUNE_BOSS_BATTLE,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
true
},
{
BattleHeaderController.eHeaderContents.GOLD,
true
}
}
},
{
eBattleCategory.HATSUNE_SPECIAL_BATTLE,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
true
},
{
BattleHeaderController.eHeaderContents.GOLD,
true
}
}
},
{
eBattleCategory.UEK_BOSS_BATTLE,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
true
},
{
BattleHeaderController.eHeaderContents.GOLD,
true
}
}
},
{
eBattleCategory.TOWER,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
true
},
{
BattleHeaderController.eHeaderContents.GOLD,
true
}
}
},
{
eBattleCategory.TOWER_REHEARSAL,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.TOWER_EX,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
true
},
{
BattleHeaderController.eHeaderContents.REWARD,
true
},
{
BattleHeaderController.eHeaderContents.GOLD,
true
}
}
},
{
eBattleCategory.TOWER_REPLAY,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.TOWER_EX_REPLAY,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
true
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.TOWER_CLOISTER,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
true
},
{
BattleHeaderController.eHeaderContents.REWARD,
true
},
{
BattleHeaderController.eHeaderContents.GOLD,
true
}
}
},
{
eBattleCategory.TOWER_CLOISTER_REPLAY,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
true
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.HIGH_RARITY,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.KAISER_BATTLE_MAIN,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.KAISER_BATTLE_SUB,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
},
{
eBattleCategory.SPACE_BATTLE,
new Dictionary<BattleHeaderController.eHeaderContents, bool>((IEqualityComparer<BattleHeaderController.eHeaderContents>) new BattleHeaderController.eHeaderContents_DictComparer())
{
{
BattleHeaderController.eHeaderContents.PAUSE_BUTTON,
true
},
{
BattleHeaderController.eHeaderContents.WAVE_NUM,
false
},
{
BattleHeaderController.eHeaderContents.REWARD,
false
},
{
BattleHeaderController.eHeaderContents.GOLD,
false
}
}
}
};

        public PartsBossGauge BossGauge => this.partsBossGauge;

        public PartsBossTotalDamage BossTotalDamage => this.partsBossTotalDamage;

        public PartsSkillWindow SkillWindow => this.partsSkillWindow;

        public int Gold { get; private set; }

        public int Reward { get; private set; }
        */
        private bool noMoreTimePause { get; set; }
        /*
        private bool isTowerFloor { get; set; }

        private PartsHastunePointCounter pointCounter { get; set; }

        private void OnDestroy()
        {
            this.pauseButton = (CustomUIButton)null;
            this.timeLimit = (CustomUILabel)null;
            this.timeLabel2 = (CustomUILabel)null;
            this.waveObject = (GameObject)null;
            this.wave = (CustomUILabel)null;
            this.rewardsLabel = (CustomUILabel)null;
            this.rewardsObject = (GameObject)null;
            this.goldLabel = (CustomUILabel)null;
            this.goldObject = (GameObject)null;
            this.goldTweenScaleList = (List<TweenScale>)null;
            this.goldTweenAlphaList = (List<TweenAlpha>)null;
            this.treasureTweenScaleList = (List<TweenScale>)null;
            this.treasureTweenAlphaList = (List<TweenAlpha>)null;
            this.partsBossGauge = (PartsBossGauge)null;
            this.partsBossTotalDamage = (PartsBossTotalDamage)null;
            this.partsSkillWindow = (PartsSkillWindow)null;
            this.crownIcon = (PartsCrownIcon)null;
            this.towerTempData = (TowerTempData)null;
            this.spaceBattleTemp = (SpaceBattleTempData)null;
            this.replayTempData = (ReplayTempData)null;
            this.viewManager = (ViewManager)null;
            this.noMoreTimeTweeners = (List<UITweener>)null;
            this.timeUpFrameSprite = (UISprite)null;
            this.iconTimeCount = (GameObject)null;
            this.iconTimeCount2 = (GameObject)null;
            this.iconTimeCount3 = (GameObject)null;
            this.battleManager = (FKOEBGHGCNP)null;
            this.pointCounter = (PartsHastunePointCounter)null;
        }

        public void PauseNoMoreTimeUp(bool _pause)
        {
            if (!this.isNoMoreTime || _pause == this.noMoreTimePause)
                return;
            this.noMoreTimePause = _pause;
            this.timeUpFrameSprite.SetActive(!_pause);
            this.timeLabel2.SetActive(!_pause);
            for (int index = 0; index < this.noMoreTimeTweeners.Count; ++index)
                this.noMoreTimeTweeners[index].enabled = !_pause;
        }
        */
        /*public void SetRestTime(float _restTime)
        {
            int num1 = (int)((double)_restTime / 60.0);
            int num2 = (int)((double)_restTime - (double)(num1 * 60));
            if ((double)_restTime < 0.0)
                this.timeUpFrameSprite.SetActive(false);
            if ((double)_restTime - (double)Mathf.Floor(_restTime) > 0.0)
            {
                ++num2;
                if (num2 >= 60)
                {
                    num2 = 0;
                    ++num1;
                }
            }
            if (this.isTowerFloor)
            {
                if ((double)_restTime > 0.0 && (double)_restTime < (double)this.nextSeTiming)
                {
                    --this.nextSeTiming;
                    ManagerSingleton<SoundManager>.Instance.PlaySe(eSE.BTL_TOWER_TIMECOUNT);
                }
                if ((double)_restTime < 10.0)
                {
                    if (!this.isNoMoreTime)
                    {
                        this.isNoMoreTime = true;
                        for (int index = 0; index < this.noMoreTimeTweeners.Count; ++index)
                            this.noMoreTimeTweeners[index].enabled = true;
                    }
                    for (int index = 0; index < this.noMoreTimeTweeners.Count; ++index)
                    {
                        if (this.noMoreTimeTweeners[index] is ExtendedTweenAlpha || this.noMoreTimeTweeners[index] is ExtendedTweenScale)
                        {
                            float num3 = 10f - _restTime;
                            float factor = num3 - (float)(int)num3;
                            this.noMoreTimeTweeners[index].Sample(factor, false);
                        }
                    }
                    if (this.iconTimeCount.activeSelf && !this.iconTimeCount2.activeSelf && (double)_restTime < 5.0)
                    {
                        this.iconTimeCount.SetActive(false);
                        this.iconTimeCount2.SetActive(true);
                    }
                    if (!this.iconTimeCount3.activeSelf && (double)_restTime < 3.0)
                    {
                        this.iconTimeCount2.SetActive(false);
                        this.iconTimeCount3.SetActive(true);
                    }
                }
                else if (this.isNoMoreTime)
                {
                    this.isNoMoreTime = false;
                    for (int index = 0; index < this.noMoreTimeTweeners.Count; ++index)
                    {
                        UITweener noMoreTimeTweener = this.noMoreTimeTweeners[index];
                        noMoreTimeTweener.ResetToBeginning();
                        if (noMoreTimeTweener is ExtendedTweenAlpha)
                            (noMoreTimeTweener as ExtendedTweenAlpha).value = 0.0f;
                        noMoreTimeTweener.enabled = false;
                    }
                    this.iconTimeCount.SetActive(true);
                    this.iconTimeCount2.SetActive(false);
                    this.iconTimeCount3.SetActive(false);
                    this.timeLabel2.SetText("", (object[])Array.Empty<object>());
                    this.timeLimit.SetTextColor(eColor.GRAY.Color());
                }
            }
            if (this.preSetMin == num1 && this.preSetSec == num2)
                return;
            this.preSetMin = num1;
            this.preSetSec = num2;
            string _str = string.Format(eTextId.PROGRESS_TIME.Name(), (object)num1, (object)num2);
            if ((double)_restTime < 10.0 && this.isTowerFloor)
            {
                _str = eColorGradation.Alert.EnchantCode((object)_str);
                this.timeLabel2.SetText(_str, (object[])Array.Empty<object>());
            }
            this.timeLimit.SetText(_str, (object[])Array.Empty<object>());
        }*/
        /*
        public void SetRewardNum(int _num)
        {
            this.Reward = _num;
            this.setActiveTweenAlpha(this.treasureTweenAlphaList, true);
            this.setNumLabel(this.rewardsLabel, _num, this.treasureTweenAlphaList, this.treasureTweenScaleList);
        }

        public void SetGoldNum(int _num)
        {
            if (this.Gold == _num)
                return;
            this.Gold = _num;
            this.setActiveTweenAlpha(this.goldTweenAlphaList, true);
            this.setNumLabel(this.goldLabel, _num, this.goldTweenAlphaList, this.goldTweenScaleList);
        }

        private void setNumLabel(
        CustomUILabel _label,
        int _num,
        List<TweenAlpha> _tweenAlphaList,
        List<TweenScale> _tweenScaleList)
        {
            _label.SetText(eTextId.NUM, (object)_num);
            ManagerSingleton<SoundManager>.Instance.PlaySe(eSE.BTL_GET_BOX_MANA);
            for (int index = 0; index < _tweenAlphaList.Count; ++index)
            {
                _tweenAlphaList[index].ResetToBeginning();
                _tweenAlphaList[index].PlayForward();
            }
            for (int index = 0; index < _tweenScaleList.Count; ++index)
            {
                _tweenScaleList[index].ResetToBeginning();
                _tweenScaleList[index].PlayForward();
            }
        }

        public void Init(FKOEBGHGCNP _battleManager)
        {
            this.battleManager = _battleManager;
            EHPLBCOOOPK.APKIAPODBFI phdacaoaoma = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
            eBattleCategory battleCategory = phdacaoaoma.CLCOKFOINCL;
            if (_battleManager.DJAFHOCGJPJ)
            {
                switch (_battleManager.GetPurpose())
                {
                    case eHatsuneSpecialPurpose.GET_POINT:
                    case eHatsuneSpecialPurpose.SHIELD:
                    case eHatsuneSpecialPurpose.ABSORBER:
                        PartsHastunePointCounter BBEPEGPNNAP = (PartsHastunePointCounter)null;
                        this.battleManager.InstantiatePointCounter(this.pointCounterParent, ref BBEPEGPNNAP);
                        this.pointCounter = BBEPEGPNNAP;
                        break;
                }
            }
            Dictionary<BattleHeaderController.eHeaderContents, bool> activeFlagDictionary = BattleHeaderController.headerContentsFlagDictionary[battleCategory];
            this.timeUpFrameSprite.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
            this.timeUpFrameSprite.width = Mathf.CeilToInt(this.viewManager.ActiveScreenWidthInUIRoot / 1.6f);
            this.timeUpFrameSprite.height = Mathf.CeilToInt(this.viewManager.ActiveScreenHeightInUIRoot / 1.6f);
            this.timeUpFrameSprite.SetLocalPosY(WrapperUnityEngineScreen.GetRestoreNGUIOffsetY());
            this.isTowerFloor = battleCategory == eBattleCategory.TOWER || battleCategory == eBattleCategory.TOWER_REPLAY || battleCategory == eBattleCategory.TOWER_REHEARSAL || battleCategory == eBattleCategory.TOWER_CLOISTER;
            this.rewardsLabel.SetTextNum(0);
            this.goldLabel.SetTextNum(0);
            this.waveMax = phdacaoaoma.AOPCDPIJOIE.Count;
            if (this.battleManager.OPCIJEAJPAG)
                this.partsBossTotalDamage.SetActive(true);
            Yggdrasil<BattleHeaderController> singletonTree = this.CreateSingletonTree<BattleHeaderController>();
            if (battleCategory == eBattleCategory.TOWER_EX_REPLAY || battleCategory == eBattleCategory.TOWER_CLOISTER_REPLAY)
                this.replayTempData = singletonTree.Get<ReplayTempData>();
            if (this.battleManager.EMKDFABDFAH)
            {
                this.towerTempData = singletonTree.Get<TowerTempData>();
                this.waveMax = 3;
            }
            if (this.battleManager.JILIICMHLCH == eBattleCategory.SPACE_BATTLE)
                this.spaceBattleTemp = singletonTree.Get<SpaceBattleTempData>();
            this.pauseButton.SetOnClickDelegate((MonoBehaviour)this, new System.Action(this.OnClickPauseButton));
            System.Action<GameObject, BattleHeaderController.eHeaderContents> action1 = (System.Action<GameObject, BattleHeaderController.eHeaderContents>)((targetObj, headerContents) =>
           {
               bool flag = activeFlagDictionary[headerContents];
               targetObj.SetActive(flag);
           });
            ((System.Action<GameObject, BattleHeaderController.eHeaderContents>)((targetObj, headerContents) =>
           {
               bool flag = activeFlagDictionary[headerContents] && (this.waveMax > 1 || battleCategory == eBattleCategory.TOWER_EX);
               targetObj.SetActive(flag);
           }))(this.waveObject, BattleHeaderController.eHeaderContents.WAVE_NUM);
            action1(this.goldObject, BattleHeaderController.eHeaderContents.GOLD);
            action1(this.rewardsObject, BattleHeaderController.eHeaderContents.REWARD);
            if (!activeFlagDictionary[BattleHeaderController.eHeaderContents.PAUSE_BUTTON])
            {
                this.pauseButton.isEnabled = false;
                this.pauseButton.SetOnDisabledClickDelegate((MonoBehaviour)this, new System.Action(this.onClickPauseButtonDisable));
            }
            this.setActiveTweenAlpha(this.goldTweenAlphaList, false);
            this.setActiveTweenAlpha(this.treasureTweenAlphaList, false);
            this.waveIndicaterLabel.SetText(eTextId.WAVE, (object[])Array.Empty<object>());
            if (this.waveMax > 1)
            {
                int _num = 1;
                switch (battleCategory)
                {
                    case eBattleCategory.TOWER_EX:
                        this.waveIndicaterLabel.SetText(eTextId.PARTY, (object[])Array.Empty<object>());
                        _num = this.towerTempData.CurrentExPartyIndex + 1;
                        break;
                    case eBattleCategory.TOWER_EX_REPLAY:
                        this.waveIndicaterLabel.SetText(eTextId.PARTY, (object[])Array.Empty<object>());
                        _num = this.replayTempData.CurrentPartyIndex + 1;
                        break;
                    case eBattleCategory.TOWER_CLOISTER:
                        _num = this.towerTempData.CloisterWaveIndex + 1;
                        break;
                    case eBattleCategory.TOWER_CLOISTER_REPLAY:
                        _num = this.replayTempData.CloisterWaveIndex + 1;
                        break;
                }
                this.SetWaveNum(_num);
            }
            this.rehearsalIcon.SetActive(battleCategory == eBattleCategory.TOWER_REHEARSAL);
            System.Action<List<UITweener>> action2 = (System.Action<List<UITweener>>)(_target =>
           {
               int index = 0;
               for (int count = _target.Count; index < count; ++index)
               {
                   _target[index].Toggle();
                   _target[index].ResetToBeginning();
                   _target[index].Toggle();
                   _target[index].enabled = false;
               }
           });
            action2(this.goldTweenAlphaList.Cast<UITweener>().ToList<UITweener>());
            action2(this.goldTweenScaleList.Cast<UITweener>().ToList<UITweener>());
            action2(this.treasureTweenAlphaList.Cast<UITweener>().ToList<UITweener>());
            action2(this.treasureTweenScaleList.Cast<UITweener>().ToList<UITweener>());
            this.crownIcon.SetActiveWithCheck(false);
            if (battleCategory != eBattleCategory.GRAND_ARENA && battleCategory != eBattleCategory.GRAND_ARENA_REPLAY)
                return;
            this.crownIcon.SetActiveWithCheck(true);
            this.crownIcon.UpdateStatus();
        }

        public void SetWaveNum(int _num)
        {
            this.waveNum = _num;
            this.wave.SetText(eTextId.PROGRESS_PARAM, (object)this.waveNum, (object)this.waveMax);
        }

        private void onClickPauseButtonDisable() => this.viewManager.OpenNoticeWindow(eTextId.DISABLE_PAUSE_BUTTON, (object[])Array.Empty<object>());

        public bool IsPauseButtonEnabled() => this.isActiveAndEnabled && this.pauseButton.isEnabled;
        */
        public BattleManager BattleManager { get { 
            if(battleManager == null)
                {
                    battleManager = BattleManager.Instance;
                }
                return battleManager;
            } set => battleManager = value; }

        public bool GetIsPause()
        {
            if (BattleManager.GetIsPlayCutin())// || this.IsPaused)
                return false;
            return IsPaused;

        }
        public void OnClickPauseButton()
        {
            //XX: experimental
            BattleManager.deltaTimeAccumulated = 0;
            if (BattleManager.GetIsPlayCutin())// || this.IsPaused)
                return;
            if (!IsPaused)
            {
                IsPaused = true;
                battleManager.GamePause(true);
            }
            else
            {
                battleManager.GamePause(false);
                //this.battleManager.ResumeAbnormalEffect();
                IsPaused = false;
            }
            /*EHPLBCOOOPK tempData = Singleton<EHPLBCOOOPK>.Instance;
            DialogManager dialogManager = ManagerSingleton<DialogManager>.Instance;
            eBattleCategory jiliicmhlch = this.battleManager.JILIICMHLCH;
            for (int index = 0; index < this.noMoreTimeTweeners.Count; ++index)
                this.noMoreTimeTweeners[index].enabled = false;
            System.Action action = (System.Action)(() =>
           {
               if (this.isNoMoreTime)
               {
                   for (int index = 0; index < this.noMoreTimeTweeners.Count; ++index)
                       this.noMoreTimeTweeners[index].enabled = true;
               }
               if (Singleton<EHPLBCOOOPK>.Instance.OIPDBCEJAPK == TutorialDefine.PROLOGUE_2_PART_D_QUEST_ID_1 && this.battleManager.EBEBMGJEHOE)
               {
                   this.IsPaused = false;
                   this.battleManager.TutorialOpenFocusDialog();
               }
               else
               {
                   this.battleManager.GamePause(false, false);
                   this.battleManager.ResumeAbnormalEffect();
                   Time.timeScale = tmpTimeScale;
                   this.IsPaused = false;
               }
           });
            System.Action callbackRetireButton = (System.Action)(() =>
           {
               dialogManager.CloseOne();
               dialogManager.CloseOne();
               this.battleManager.GamePause(true, false);
               this.battleManager.RetireBattle();
           });
            if (dialogManager.IsUse(eDialogId.FLATOUT_PLAYER))
                dialogManager.ForceCloseOne(eDialogId.FLATOUT_PLAYER);
            this.battleManager.GamePause(true, false);
            this.battleManager.PauseAbnormalEffect();
            switch (jiliicmhlch)
            {
                case eBattleCategory.QUEST:
                case eBattleCategory.TRAINING:
                    DialogConfirm.eType retireDialogType1 = DialogConfirm.eType.QUEST_RETIRE_NOMAL;
                    switch (Singleton<EHPLBCOOOPK>.Instance.PPPJHIFBEPF)
                    {
                        case SystemIdDefine.AreaType.NORMAL:
                            retireDialogType1 = DialogConfirm.eType.QUEST_RETIRE_NOMAL;
                            if (UserUtil.IsReleaseFriendSupportGoldUseTimingChange() && SupportUnitUtility.IsUseSupport())
                            {
                                retireDialogType1 = DialogConfirm.eType.QUEST_RETIRE_NOMAL_USE_SUPPORT;
                                break;
                            }
                            break;
                        case SystemIdDefine.AreaType.HARD:
                        case SystemIdDefine.AreaType.VERY_HARD:
                        case SystemIdDefine.AreaType.UNIQUE_EQUIP:
                        case SystemIdDefine.AreaType.HIGH_RARITY_EQUIP:
                            retireDialogType1 = DialogConfirm.eType.QUEST_RETIRE_HARD;
                            if (UserUtil.IsReleaseFriendSupportGoldUseTimingChange() && SupportUnitUtility.IsUseSupport())
                            {
                                retireDialogType1 = DialogConfirm.eType.QUEST_RETIRE_HARD_USE_SUPPORT;
                                break;
                            }
                            break;
                        case SystemIdDefine.AreaType.RUPEE:
                        case SystemIdDefine.AreaType.EXP:
                            retireDialogType1 = DialogConfirm.eType.BATTLE_RETIRE;
                            if (UserUtil.IsReleaseFriendSupportGoldUseTimingChange() && SupportUnitUtility.IsUseSupport())
                            {
                                retireDialogType1 = DialogConfirm.eType.BATTLE_RETIRE_USE_SUPPORT;
                                break;
                            }
                            break;
                    }
                    EHPLBCOOOPK.APKIAPODBFI phdacaoaoma1 = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
                    dialogManager.OpenBattleMenu(phdacaoaoma1.PPOHCJIBJHM, phdacaoaoma1.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(retireDialogType1, callbackRetireButton, false)), action, !TutorialManager.IsStartTutorial, !TutorialManager.IsStartTutorial, tempData.KOKHNPBHMGF != 0 ? tempData.AKECCLGMDNG : 0);
                    break;
                case eBattleCategory.DUNGEON:
                    this.openBattleMenuDungeon(callbackRetireButton, action);
                    break;
                case eBattleCategory.ARENA:
                    dialogManager.OpenBattleMenu(eTextId.BATTLE_ARENA.Name(), "", this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.QUEST_RETIRE_NOMAL, callbackRetireButton, false)), action, true, false);
                    break;
                case eBattleCategory.ARENA_REPLAY:
                case eBattleCategory.GRAND_ARENA_REPLAY:
                    dialogManager.OpenConfirm(DialogConfirm.eType.REPLAY_QUIT, callbackRetireButton, action, false);
                    break;
                case eBattleCategory.STORY:
                    dialogManager.OpenConfirm(DialogConfirm.eType.SKIP_STORY_BATTLE, callbackRetireButton, action, false);
                    break;
                case eBattleCategory.TUTORIAL:
                    dialogManager.OpenConfirm(DialogConfirm.eType.TUTORIAL_BATTLE_SKIP, callbackRetireButton, action, false);
                    break;
                case eBattleCategory.GRAND_ARENA:
                    dialogManager.OpenBattleMenu(eTextId.GRAND_ARENA.Name(), "", this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.QUEST_RETIRE_NOMAL, callbackRetireButton, false)), action, true, false);
                    break;
                case eBattleCategory.CLAN_BATTLE:
                    this.openBattleMenuClanBattle(callbackRetireButton, action);
                    break;
                case eBattleCategory.HATSUNE_BATTLE:
                    DialogConfirm.eType retireDialogType2 = DialogConfirm.eType.QUEST_RETIRE_NOMAL;
                    switch (Singleton<EHPLBCOOOPK>.Instance.DEBFKNDECIN.PPPJHIFBEPF)
                    {
                        case SystemIdDefine.AreaType.HATSUNE_NORMAL:
                            retireDialogType2 = DialogConfirm.eType.QUEST_RETIRE_NOMAL;
                            if (UserUtil.IsReleaseFriendSupportGoldUseTimingChange() && SupportUnitUtility.IsUseSupport())
                            {
                                retireDialogType2 = DialogConfirm.eType.QUEST_RETIRE_NOMAL_USE_SUPPORT;
                                break;
                            }
                            break;
                        case SystemIdDefine.AreaType.HATSUNE_HARD:
                            retireDialogType2 = DialogConfirm.eType.QUEST_RETIRE_HARD;
                            if (UserUtil.IsReleaseFriendSupportGoldUseTimingChange() && SupportUnitUtility.IsUseSupport())
                            {
                                retireDialogType2 = DialogConfirm.eType.QUEST_RETIRE_HARD_USE_SUPPORT;
                                break;
                            }
                            break;
                    }
                    EHPLBCOOOPK.APKIAPODBFI phdacaoaoma2 = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
                    dialogManager.OpenBattleMenu(phdacaoaoma2.PPOHCJIBJHM, phdacaoaoma2.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(retireDialogType2, callbackRetireButton, false)), action, !TutorialManager.IsStartTutorial, !TutorialManager.IsStartTutorial, tempData.KOKHNPBHMGF != 0 ? tempData.AKECCLGMDNG : 0);
                    break;
                case eBattleCategory.HATSUNE_BOSS_BATTLE:
                case eBattleCategory.HATSUNE_SPECIAL_BATTLE:
                    int gfaddfnbdaj = Singleton<EHPLBCOOOPK>.Instance.DEBFKNDECIN.GFADDFNBDAJ;
                    bool _enableRetire = (int)HatsuneUtility.GetHatsuneBossInfo(gfaddfnbdaj).retire_flag > 0;
                    EHPLBCOOOPK.APKIAPODBFI phdacaoaoma3 = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
                    eHatsuneBossDifficulity difficulity = HatsuneUtility.GetCurrentEventBossDifficulty(gfaddfnbdaj);
                    eTextId _textId = eTextId.NONE;
                    switch (difficulity)
                    {
                        case eHatsuneBossDifficulity.NORMAL:
                            _textId = eTextId.QUEST_DIFFICULTY_NORMAL;
                            break;
                        case eHatsuneBossDifficulity.HARD:
                            _textId = eTextId.QUEST_DIFFICULTY_HARD;
                            break;
                        case eHatsuneBossDifficulity.VERY_HARD:
                            _textId = eTextId.QUEST_DIFFICULTY_VERY_HARD;
                            break;
                    }
                    dialogManager.OpenBattleMenu(phdacaoaoma3.PPOHCJIBJHM + _textId.Name(), phdacaoaoma3.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() =>
                   {
                       if (difficulity == eHatsuneBossDifficulity.SPECIAL_EX)
                           dialogManager.OpenConfirm(DialogConfirm.eType.EX_BOSS_BATTLE_RETIRE, callbackRetireButton, false);
                       else if (tempData.DEBFKNDECIN.OEDFPPGHHCG == HatsuneDefine.eHatsuneEventType.Shiori)
                           dialogManager.OpenConfirm(DialogConfirm.eType.SHIORI_BOSS_BATTLE_RETIRE, callbackRetireButton, false);
                       else
                           dialogManager.OpenConfirm(DialogConfirm.eType.BOSS_BATTLE_RETIRE, callbackRetireButton, false);
                   }), action, !TutorialManager.IsStartTutorial, _enableRetire);
                    break;
                case eBattleCategory.UEK_BOSS_BATTLE:
                    EHPLBCOOOPK.APKIAPODBFI phdacaoaoma4 = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
                    dialogManager.OpenBattleMenu(phdacaoaoma4.PPOHCJIBJHM, phdacaoaoma4.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.UEK_BOSS_BATTLE_RETIRE, callbackRetireButton, false)), action, !TutorialManager.IsStartTutorial, true);
                    break;
                case eBattleCategory.QUEST_REPLAY:
                case eBattleCategory.TOWER_REPLAY:
                case eBattleCategory.TOWER_EX_REPLAY:
                    dialogManager.OpenConfirm(DialogConfirm.eType.QUEST_REPLAY_QUIT, callbackRetireButton, action, false);
                    break;
                case eBattleCategory.TOWER:
                case eBattleCategory.TOWER_CLOISTER:
                    this.openBattleMenuTower(callbackRetireButton, action);
                    break;
                case eBattleCategory.TOWER_EX:
                    this.openBattleMenuTowerEx(callbackRetireButton, action);
                    break;
                case eBattleCategory.TOWER_REHEARSAL:
                    this.openBattleMenuTowerRehearsal(callbackRetireButton, action);
                    break;
                case eBattleCategory.GLOBAL_RAID:
                    this.openSekaiBattleMenu(callbackRetireButton, action);
                    break;
                case eBattleCategory.HIGH_RARITY:
                    this.openBattleMenuHighRarityQuest(callbackRetireButton, action);
                    break;
                case eBattleCategory.FRIEND:
                    dialogManager.OpenBattleMenu(eTextId.FRIEND_BATTLE.Name(), "", this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.BATTLE_RETIRE_LOSE_NOTHING, (System.Action)(() => callbackRetireButton.Call()), false)), action, true, true);
                    break;
                case eBattleCategory.KAISER_BATTLE_MAIN:
                    EHPLBCOOOPK.APKIAPODBFI phdacaoaoma5 = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
                    dialogManager.OpenBattleMenu(phdacaoaoma5.PPOHCJIBJHM, phdacaoaoma5.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.KAISER_MAIN_RETIRE, callbackRetireButton, false)), action, !TutorialManager.IsStartTutorial, true);
                    break;
                case eBattleCategory.KAISER_BATTLE_SUB:
                    EHPLBCOOOPK.APKIAPODBFI phdacaoaoma6 = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
                    dialogManager.OpenBattleMenu(phdacaoaoma6.PPOHCJIBJHM, phdacaoaoma6.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.KAISER_SUB_RETIRE, callbackRetireButton, false)), action, !TutorialManager.IsStartTutorial, true);
                    break;
                case eBattleCategory.SPACE_BATTLE:
                    this.openSpaceBattleMenu(callbackRetireButton, action);
                    break;
                default:
                    EHPLBCOOOPK.APKIAPODBFI phdacaoaoma7 = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
                    dialogManager.OpenBattleMenu(phdacaoaoma7.PPOHCJIBJHM, phdacaoaoma7.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.BATTLE_RETIRE, callbackRetireButton, false)), action, !TutorialManager.IsStartTutorial, !TutorialManager.IsStartTutorial);
                    break;
            }*/
        }
        /*
        private void openBattleMenuClanBattle(
        System.Action _callbackRetireButton,
        System.Action _callbackCancelButton)
        {
            DialogManager dialogManager = ManagerSingleton<DialogManager>.Instance;
            ClanBattleTempData instance = Singleton<ClanBattleTempData>.Instance;
            EHPLBCOOOPK.APKIAPODBFI phdacaoaoma = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
            dialogManager.OpenBattleMenu(phdacaoaoma.PPOHCJIBJHM, phdacaoaoma.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.CLAN_BATTLE_REHEARSAL_RETIRE, _callbackRetireButton, false)), _callbackCancelButton, !TutorialManager.IsStartTutorial, instance.IsRehearsal && !TutorialManager.IsStartTutorial, instance.SupportOwnerId != 0 ? instance.SupportUnitId : 0);
        }

        private void openSekaiBattleMenu(System.Action _callbackRetireButton, System.Action _callbackCancelButton)
        {
            DialogManager dialogManager = ManagerSingleton<DialogManager>.Instance;
            EHPLBCOOOPK.APKIAPODBFI phdacaoaoma = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
            LBFILPINNPJ instance = Singleton<LBFILPINNPJ>.Instance;
            dialogManager.OpenBattleMenu(phdacaoaoma.PPOHCJIBJHM, phdacaoaoma.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.BATTLE_RETIRE_LOSE_NOTHING, _callbackRetireButton, false)), _callbackCancelButton, !TutorialManager.IsStartTutorial, true, instance.KOKHNPBHMGF != 0 ? instance.AKECCLGMDNG : 0);
        }

        private void openSpaceBattleMenu(System.Action _callbackRetireButton, System.Action _callbackCancelButton)
        {
            DialogManager dialogManager = ManagerSingleton<DialogManager>.Instance;
            EHPLBCOOOPK.APKIAPODBFI phdacaoaoma = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
            dialogManager.OpenBattleMenu(phdacaoaoma.PPOHCJIBJHM, phdacaoaoma.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.BATTLE_RETIRE_LOSE_NOTHING, _callbackRetireButton, false)), _callbackCancelButton, !TutorialManager.IsStartTutorial, true, this.spaceBattleTemp.SupportOwnerId != 0 ? this.spaceBattleTemp.SupportUnitId : 0);
        }

        private void openBattleMenuDungeon(System.Action _callbackRetireButton, System.Action _callbackCancelButton)
        {
            DialogManager dialogManager = ManagerSingleton<DialogManager>.Instance;
            DungeonTempData instance = Singleton<DungeonTempData>.Instance;
            EHPLBCOOOPK.APKIAPODBFI phdacaoaoma = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
            dialogManager.OpenBattleMenu(phdacaoaoma.PPOHCJIBJHM, phdacaoaoma.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.BATTLE_RETIRE, _callbackRetireButton, false)), _callbackCancelButton, !TutorialManager.IsStartTutorial, !TutorialManager.IsStartTutorial, instance.DispatchOwnerId != 0 ? instance.DispatchUnitId : 0);
        }

        private void openBattleMenuTower(System.Action _callbackRetireButton, System.Action _callbackCancelButton)
        {
            DialogManager dialogManager = ManagerSingleton<DialogManager>.Instance;
            EHPLBCOOOPK.APKIAPODBFI phdacaoaoma = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
            dialogManager.OpenBattleMenu(phdacaoaoma.PPOHCJIBJHM, phdacaoaoma.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() =>
           {
               int staminaStart = (int)ManagerSingleton<MasterDataManager>.Instance.masterTowerQuestData.Get(this.towerTempData.CurrentQuestId).stamina_start;
               dialogManager.OpenConfirm(ParamDialogCommonText.eDialogSize.Small, eTextId.RETIRE.Name(), eTextId.DIALOG_QUEST_RETIRE_TYPE_CONTINUIOS.Format((object)staminaStart), eTextId.CANCEL.Name(), eTextId.RETIRE.Name(), (System.Action)null, _callbackRetireButton);
           }), _callbackCancelButton, !TutorialManager.IsStartTutorial, !TutorialManager.IsStartTutorial, this.towerTempData.DispatchOwnerId != 0 ? this.towerTempData.DispatchUnitId : 0);
        }

        private void openBattleMenuTowerRehearsal(
        System.Action _callbackRetireButton,
        System.Action _callbackCancelButton)
        {
            DialogManager dialogManager = ManagerSingleton<DialogManager>.Instance;
            EHPLBCOOOPK.APKIAPODBFI phdacaoaoma = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
            dialogManager.OpenBattleMenu(phdacaoaoma.PPOHCJIBJHM, phdacaoaoma.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.CLAN_BATTLE_REHEARSAL_RETIRE, _callbackRetireButton, false)), _callbackCancelButton, !TutorialManager.IsStartTutorial, !TutorialManager.IsStartTutorial, this.towerTempData.DispatchOwnerId != 0 ? this.towerTempData.DispatchUnitId : 0);
        }

        private void openBattleMenuTowerEx(System.Action _callbackRetireButton, System.Action _callbackCancelButton)
        {
            TowerTempData towerTempData = this.CreateSingletonTree<BattleHeaderController>().Get<TowerTempData>();
            DialogManager dialogManager = ManagerSingleton<DialogManager>.Instance;
            EHPLBCOOOPK.APKIAPODBFI phdacaoaoma = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
            dialogManager.OpenBattleMenu(phdacaoaoma.PPOHCJIBJHM, phdacaoaoma.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(towerTempData.ClearedExQuestIdList.Contains(towerTempData.CurrentExQuestId) ? DialogConfirm.eType.BATTLE_RETIRE_LOSE_NOTHING : DialogConfirm.eType.BATTLE_RETIRE, (System.Action)(() =>
          {
              towerTempData.LastChallengedExQuestId = towerTempData.CurrentExQuestId;
              _callbackRetireButton.Call();
          }), false)), _callbackCancelButton, !TutorialManager.IsStartTutorial, !TutorialManager.IsStartTutorial, towerTempData.ExDispatchOwnerId != 0 ? towerTempData.ExDispatchUnitId : 0);
        }

        private void openBattleMenuHighRarityQuest(
        System.Action _callbackRetireButton,
        System.Action _callbackCancelButton)
        {
            DialogManager dialogManager = ManagerSingleton<DialogManager>.Instance;
            EHPLBCOOOPK.APKIAPODBFI phdacaoaoma = Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA;
            dialogManager.OpenBattleMenu(phdacaoaoma.PPOHCJIBJHM, phdacaoaoma.EJBHPKDKLAE, this.battleManager.UnitUiCtrl.UnitCtrls, (System.Action)(() => dialogManager.OpenConfirm(DialogConfirm.eType.QUEST_RETIRE_HIGH_RARITY_EQUIP, _callbackRetireButton, false)), _callbackCancelButton, !TutorialManager.IsStartTutorial, !TutorialManager.IsStartTutorial);
        }

        private void setActiveTweenAlpha(List<TweenAlpha> _tweenAlphaList, bool _isActive)
        {
            for (int index = 0; index < _tweenAlphaList.Count; ++index)
                _tweenAlphaList[index].SetActive(_isActive);
        }
        
        public void SetEnemyPoint(int _point) => this.pointCounter.SetNum(_point, this.battleManager.GetPurpose(), (float)this.battleManager.GetPurposeCount(), true);
        */
        private enum eHeaderContents
        {
            PAUSE_BUTTON,
            WAVE_NUM,
            GOLD,
            REWARD,
        }

        private class eHeaderContents_DictComparer : IEqualityComparer<eHeaderContents>
        {
            public bool Equals(
            eHeaderContents _x,
            eHeaderContents _y) => _x == _y;

            public int GetHashCode(eHeaderContents _obj) => (int)_obj;
        }
    }
}

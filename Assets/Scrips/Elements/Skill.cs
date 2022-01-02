// Decompiled with JetBrains decompiler
// Type: Elements.Skill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft0.Json;

namespace Elements
{
    [Serializable]
    public class Skill
    {
        public bool IsPrincessForm;
        public List<PrincessSkillMovieData> PrincessSkillMovieDataList;
        [JsonIgnore]
        private List<ActionParameter> actionParameters = new List<ActionParameter>();
        public List<ActionParameterOnPrefab> ActionParametersOnPrefab = new List<ActionParameterOnPrefab>();
        public List<NormalSkillEffect> SkillEffects = new List<NormalSkillEffect>();
        [JsonIgnore]
        public List<ShakeEffect> ShakeEffects = new List<ShakeEffect>();
        //public List<BlurEffect.BlurEffectData> BlurEffects = new List<BlurEffect.BlurEffectData>();
        //public BattleDefine.ZoomEffect ZoomEffect = new BattleDefine.ZoomEffect();
        //public BattleDefine.SlowEffect SlowEffect = new BattleDefine.SlowEffect();
        public bool ForcePlayNoTarget;
        public int ParameterTarget;
        public List<ChangeSortOrderData> ChangeSortDatas = new List<ChangeSortOrderData>();
        [JsonIgnore]
        public List<ScaleChanger> ScaleChangers = new List<ScaleChanger>();
        [NonSerialized]
        [JsonIgnore]
        public int SkillId;
        [NonSerialized]
        [JsonIgnore]
        public float skillAreaWidth;
        [NonSerialized]
        [JsonIgnore]
        public eSpineCharacterAnimeId AnimId;
        [NonSerialized]
        [JsonIgnore]
        public bool Cancel;
        public bool DisableOtherCharaOnStart;
        [JsonIgnore]
        private List<SkillEffectCtrl> effectObjs = new List<SkillEffectCtrl>();
        [JsonIgnore]
        private List<SkillEffectCtrl> loopEffectObjs = new List<SkillEffectCtrl>();
        public float BlackOutTime;
        public bool BlackoutEndWithMotion;
        public bool ForceComboDamage;
        [JsonIgnore]
        public Color BlackoutColor = (Color)new Color32((byte)0, (byte)0, (byte)0, (byte)245);
        public float CutInMovieFadeStartTime = 1.9f;
        public float CutInMovieFadeDurationTime = 0.1f;
        public float CutInSkipTime;
        [NonSerialized]
        [JsonIgnore]
        public int Level;
        [NonSerialized]
        [JsonIgnore]
        public string SkillName;
        public SkillMotionType SkillMotionType;
        public bool TrackDamageNum = true;
        [NonSerialized]
        [JsonIgnore]
        public SystemIdDefine.eWeaponSeType WeaponType;
        public bool PauseStopState;
        public List<int> BranchIds = new List<int>();
        [JsonIgnore]
        public List<ActionParameter> ActionParameters
        {
            get => this.actionParameters;
            set => this.actionParameters = value;
        }
        [JsonIgnore]
        public float CastTime { get; set; }
        [JsonIgnore]
        public int SkillNum { get; set; }
        [JsonIgnore]
        public List<SkillEffectCtrl> EffectObjs => this.effectObjs;
        [JsonIgnore]
        public List<SkillEffectCtrl> LoopEffectObjs => this.loopEffectObjs;
        [JsonIgnore]
        public List<int> HasParentIndexes { get; set; }
        [JsonIgnore]
        public Vector3 OwnerReturnPosition { get; set; }
        [JsonIgnore]
        public bool IsModeChange { set; get; }
        [JsonIgnore]
        public int DefeatEnemyCount { get; set; }
        [JsonIgnore]
        public bool DefeatByThisSkill { get; set; }
        [JsonIgnore]
        public bool AlreadyAddAttackSelfSeal { get; set; }
        [JsonIgnore]
        public bool AlreadyExexActionByHit { get; set; }
        [JsonIgnore]
        public int LifeSteal { get; set; }
        [JsonIgnore]
        public bool CountBlind { get; set; }
        [JsonIgnore]
        public AttackActionBase.eAttackType CountBlindType { get; set; }
        [JsonIgnore]
        public int EffectBranchId { get; set; }
        [JsonIgnore]
        public bool HasAttack { get; set; }
        [JsonIgnore]
        public bool LoopEffectAlreadyDone { get; set; }
        [JsonIgnore]
        public List<BasePartsData> DamagedPartsList { get; set; }
        [JsonIgnore]
        public List<BasePartsData> CriticalPartsList { get; set; }
        [JsonIgnore]
        public FloatWithEx TotalDamage { get; set; }
        [JsonIgnore]
        public float AweValue { get; set; }
        [JsonIgnore]
        public bool IsLifeStealEnabled { get; set; } = true;
        [JsonIgnore]
        public int AbsorberValue { get; set; }
        public void SetLevel(int _level)
        {
            this.Level = _level;
            for (int index = 0; index < this.actionParameters.Count; ++index)
                this.actionParameters[index].SetLevel((float)_level);
        }

        public void ReadySkill()
        {
            this.TotalDamage = 0f;
            this.DamagedPartsList.Clear();
            for (int index1 = 0; index1 < this.SkillEffects.Count; ++index1)
            {
                NormalSkillEffect skillEffect = this.SkillEffects[index1];
                for (int index2 = 0; index2 < skillEffect.AlreadyFireArmExecedKeys.Count; ++index2)
                    skillEffect.AlreadyFireArmExecedData[skillEffect.AlreadyFireArmExecedKeys[index2]] = false;
            }
        }
    }
}

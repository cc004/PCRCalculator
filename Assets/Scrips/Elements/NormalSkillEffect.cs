// Decompiled with JetBrains decompiler
// Type: Elements.NormalSkillEffect
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
    public class NormalSkillEffect
    {
        [JsonIgnore]
        public GameObject Prefab;
        [JsonIgnore]
        public GameObject PrefabLeft;
        [JsonIgnore]
        public FirearmCtrlData PrefabData;//额外附加
        public float[] ExecTime = new float[1];
        public bool IsReaction;
        //[NonSerialized]
        [JsonIgnore]
        public List<NormalSkillEffect> FireArmEndEffects = new List<NormalSkillEffect>();
        public bool TargetActionIsReflexive;
        public int TargetActionIndex;
        public int TargetActionId;
        [JsonIgnore]
        public ActionParameter TargetAction;
        [JsonIgnore]
        public ActionParameter FireAction;
        public int FireActionId = -1;
        public int TargetMotionIndex;
        public eEffectBehavior EffectBehavior;
        public eEffectTarget EffectTarget;
        public eTargetBone TargetBone;
        public eEffectTarget FireArmEndTarget = eEffectTarget.ALL_TARGET;
        public eTargetBone FireArmEndTargetBone;
        public eTrackType TrackType;
        public eTrackDimension TrackDimension;
        public string TargetBoneName = "";
        public bool TrackRotation;
        public bool DestroyOnEnemyDead;
        public float CenterY;
        public float DeltaY;
        public bool TrackTarget;
        public float Height;
        public bool IsAbsoluteFireArm;
        public float AbsoluteFireDistance;
        [JsonIgnore]
        public List<ShakeEffect> ShakeEffects = new List<ShakeEffect>();
        public int TargetBranchId;
        public bool PlayWithCutIn;
        [JsonIgnore]
        public Dictionary<UnitCtrl, bool> AlreadyFireArmExecedData { get; set; }
        [JsonIgnore]
        public List<UnitCtrl> AlreadyFireArmExecedKeys { get; set; }

        public bool AppendAndJudgeAlreadyExeced(UnitCtrl _target)
        {
            if (!this.AlreadyFireArmExecedData.ContainsKey(_target))
            {
                this.AlreadyFireArmExecedData.Add(_target, true);
                this.AlreadyFireArmExecedKeys.Add(_target);
                return false;
            }
            if (this.AlreadyFireArmExecedData[_target])
                return true;
            this.AlreadyFireArmExecedData[_target] = true;
            return false;
        }
    }
}

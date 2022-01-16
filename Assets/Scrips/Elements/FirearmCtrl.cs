// Decompiled with JetBrains decompiler
// Type: Elements.FirearmCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using Cute;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [ExecuteAlways]
    public class FirearmCtrl : SkillEffectCtrl
    {
        private CustomEasing easingX;
        private CustomEasing easingUpY;
        private CustomEasing easingDownY;
        private CustomEasing easingUpRotate;
        private CustomEasing easingDownRotate;
        [SerializeField]
        private float HitDelay = 0.1f;
        [SerializeField]
        protected float MoveRate = 4f;
        [SerializeField]
        private float duration = 0.8f;
        public eMoveTypes MoveType;
        [SerializeField]
        private float startRotate = 60f;
        [SerializeField]
        private float endRotate = -60f;
        //[NonSerialized]
        [SerializeField]
        public Bounds ColliderBox;
        private bool activeSelf = true;
        private const float HIT_DELAY_DISTANCE = 0.2f;
        private float SPEED_FIX => MyGameCtrl.Instance.tempData.SettingData.skillEffeckFix;

        public bool IsAbsolute { get; set; }

        public bool InFlag { get; set; }

        public BasePartsData FireTarget { get; protected set; }

        public List<NormalSkillEffect> SkillHitEffects { set; get; }

        public List<ActionParameter> EndActions { set; get; }

        public Skill Skill { set; get; }

        public Action<FirearmCtrl> OnHitAction { get; set; }

        public Vector3 TargetPos { get; set; }

        public List<ShakeEffect> ShakeEffects { get; set; }

        private bool stopFlag { get; set; }

        protected Vector3 initialPosistion { get; set; }

        protected Vector3 speed { get; set; }

        protected UnitCtrl owner { get; set; }

        protected Action onCowHit { get; set; }


        protected override void onDestroy()
        {
            this.FireTarget = (BasePartsData)null;
            this.SkillHitEffects = (List<NormalSkillEffect>)null;
            this.EndActions = (List<ActionParameter>)null;
            this.Skill = (Skill)null;
            this.OnHitAction = (Action<FirearmCtrl>)null;
            this.ShakeEffects = (List<ShakeEffect>)null;
            this.easingX = (CustomEasing)null;
            this.easingUpY = (CustomEasing)null;
            this.easingDownY = (CustomEasing)null;
            this.easingUpRotate = (CustomEasing)null;
            this.easingDownRotate = (CustomEasing)null;
            this.owner = (UnitCtrl)null;
            this.onCowHit = (Action)null;
        }

        protected virtual Vector3 getHeadBonePos(BasePartsData _target) => _target.GetBottomTransformPosition() + _target.GetFixedCenterPos();

        public void OnDisable()
        {

        }

        public virtual void Initialize(
          BasePartsData _target,
          List<ActionParameter> _actions,
          Skill _skill,
          List<NormalSkillEffect> _skillEffect,
          UnitCtrl _owner,
          float _height,
          bool _hasBlackOutTime,
          bool _isAbsolute,
          Vector3 _targetPosition,
          List<ShakeEffect> _shakes,
          eTargetBone _targetBone)
        {
            this.ShakeEffects = _shakes;
            this.IsAbsolute = _isAbsolute;
            this.Skill = _skill;
            if (_isAbsolute)
            {
                this.TargetPos = _targetPosition;
            }
            else
            {
                switch (_targetBone)
                {
                    case eTargetBone.BOTTOM:
                        this.TargetPos = _target.GetBottomTransformPosition();
                        break;
                    case eTargetBone.HEAD:
                        this.TargetPos = this.getHeadBonePos(_target);
                        break;
                    case eTargetBone.CENTER:
                    case eTargetBone.FIXED_CENETER:
                        this.TargetPos = _target.GetBottomTransformPosition() + _target.GetFixedCenterPos();
                        break;
                }
            }
            this.FireTarget = _target;
            _target.Owner.FirearmCtrlsOnMe.Add(this);
            this.EndActions = _actions;
            this.SkillHitEffects = _skillEffect;
            this.owner = _owner;
            this.setInitialPosition();
            this.initMoveType(_height, _owner);
            this.battleManager.AppendCoroutine(this.updatePosition(Vector3.Distance(this.TargetPos, this.initialPosistion) + 1f), ePauseType.SYSTEM, _hasBlackOutTime ? _owner : (UnitCtrl)null);
            this.battleManager.AppendEffect((SkillEffectCtrl)this, _hasBlackOutTime ? _owner : (UnitCtrl)null, false);
        }

        protected virtual void setInitialPosition() => this.initialPosistion = this.transform.position;

        protected virtual void Awake() => this.isRepeat = true;

        private void initMoveType(float _height, UnitCtrl _owner)
        {
            switch (this.MoveType)
            {
                case eMoveTypes.LINEAR:
                    Vector3 toDirection = this.TargetPos - this.transform.position;
                    toDirection.Normalize();
                    //this.speed = this.MoveRate * toDirection;
                    this.speed = this.MoveRate * toDirection*SPEED_FIX;
                    //this.transform.rotation = Quaternion.FromToRotation((UnityEngine.Object)_owner == (UnityEngine.Object)null || !_owner.IsLeftDir ? Vector3.right : Vector3.left, toDirection);
                    break;
                case eMoveTypes.NONE:
                case eMoveTypes.HORIZONTAL:
                    this.speed = new Vector3(this.MoveRate, 0.0f, 0.0f)*SPEED_FIX;
                    break;
                case eMoveTypes.PARABORIC:
                case eMoveTypes.PARABORIC_ROTATE:
                    float durationTime = this.duration / 2f;
                    this.easingUpY = new CustomEasing(CustomEasing.eType.outCubic, this.transform.position.y, this.transform.position.y + _height, durationTime);
                    this.easingDownY = new CustomEasing(CustomEasing.eType.inQuad, this.transform.position.y + _height, this.TargetPos.y, durationTime);
                    this.easingX = new CustomEasing(CustomEasing.eType.linear, this.transform.position.x, this.TargetPos.x, this.duration);
                    break;
            }
            if (this.MoveType != eMoveTypes.PARABORIC_ROTATE)
                return;
            this.easingUpRotate = new CustomEasing(CustomEasing.eType.inQuad, this.startRotate, 0.0f, this.duration / 2f);
            this.easingDownRotate = new CustomEasing(CustomEasing.eType.linear, 0.0f, this.endRotate, this.duration / 2f);
        }

        private Vector3 GetParaboricPosition(float _currentTime, float _deltaTime)
        {
            Vector3 vector3 = this.transform.position;
            if (this.easingX.IsMoving)
                vector3 = new Vector3(this.easingX.GetCurVal(_deltaTime, true), (double)_currentTime >= (double)this.duration / 2.0 ? this.easingDownY.GetCurVal(_deltaTime, true) : this.easingUpY.GetCurVal(_deltaTime, true), this.transform.position.z);
            return vector3;
        }

        private IEnumerator updatePosition(float _lifeDistance)
        {
            Debug.Log(BattleHeaderController.CurrentFrameCount + "-弹道开始");
            FirearmCtrl firearmCtrl = this;
            float currentTime = 0.0f;
            bool hitFlag = false;
            float hitTimer = 0.0f;
            while (firearmCtrl.IsPlaying)
            {
                if (!firearmCtrl.activeSelf)
                {
                    yield return (object)null;
                }
                else
                {
                    float fnhfjlaenpf = firearmCtrl.battleManager.DeltaTime_60fps;
                    if (hitFlag)
                    {
                        hitTimer += fnhfjlaenpf;
                        float _b = firearmCtrl.MoveType == eMoveTypes.LINEAR ? 0.2f / firearmCtrl.MoveRate : firearmCtrl.HitDelay;
                        if ((double)hitTimer > (double)_b || BattleUtil.Approximately(hitTimer, _b))
                        {
                            hitTimer = 0.0f;
                            hitFlag = false;
                            firearmCtrl.stopFlag = true;
                            if (firearmCtrl.OnHitAction != null)
                            {
                                firearmCtrl.FireTarget.Owner.FirearmCtrlsOnMe.Remove(firearmCtrl);
                                firearmCtrl.OnHitAction(firearmCtrl);
                            }
                            firearmCtrl.onCowHit.Call();
                            firearmCtrl.onCowHit = (Action)null;
                        }
                    }
                    if (firearmCtrl.getStopFlag())
                    {
                        firearmCtrl.activeSelf = false;
                        firearmCtrl.FireTarget.Owner.FirearmCtrlsOnMe.Remove(firearmCtrl);
                        firearmCtrl.timeToDie = true;
                    }
                    else
                    {
                        if ((UnityEngine.Object)firearmCtrl == (UnityEngine.Object)null)
                            break;
                        Vector3 b = firearmCtrl.transform.position;
                        switch (firearmCtrl.MoveType)
                        {
                            case eMoveTypes.LINEAR:
                            case eMoveTypes.HORIZONTAL:
                                b += firearmCtrl.speed * fnhfjlaenpf;
                                firearmCtrl.transform.position = b;
                                break;
                            case eMoveTypes.PARABORIC:
                            case eMoveTypes.PARABORIC_ROTATE:
                                currentTime += fnhfjlaenpf;
                                b = firearmCtrl.GetParaboricPosition(currentTime, fnhfjlaenpf);
                                if ((double)currentTime > (double)firearmCtrl.duration)
                                {
                                    hitFlag = true;
                                    break;
                                }
                                break;
                        }
                        if (firearmCtrl.MoveType == eMoveTypes.PARABORIC_ROTATE)
                            firearmCtrl.particle.transform.eulerAngles = new Vector3(0.0f, 0.0f, (double)currentTime >= (double)firearmCtrl.duration / 2.0 ? firearmCtrl.easingDownRotate.GetCurVal(fnhfjlaenpf, true) : firearmCtrl.easingUpRotate.GetCurVal(fnhfjlaenpf, true));
                        firearmCtrl.transform.position = b;
                        switch (firearmCtrl.MoveType)
                        {
                            case eMoveTypes.LINEAR:
                            case eMoveTypes.HORIZONTAL:
                                if ((double)Vector3.Distance(firearmCtrl.initialPosistion, b) > (double)_lifeDistance)
                                //if ((double)Mathf.Abs(firearmCtrl.initialPosistion.x- b.x) > (double)3*_lifeDistance)

                                {
                                    firearmCtrl.FireTarget.Owner.FirearmCtrlsOnMe.Remove(firearmCtrl);
                                    firearmCtrl.timeToDie = true;
                                    break;
                                }
                                break;
                        }
                    }
                    if (!firearmCtrl.IsAbsolute)
                        hitFlag = firearmCtrl.collisionDetection(hitFlag, currentTime);
                    yield return (object)null;
                }
            }
        }

        public override bool _Update()
        {
            if (!this.activeSelf)
                this.gameObject.SetActive(false);
            if (!this.timeToDie)
                return true;
            //this.skillSeSource.Stop();
            this.IsPlaying = false;
            this.gameObject.SetActive(false);
            return false;
        }

        private bool collisionDetection(bool _hitFlag, float _currentTime)
        {
            if ((this.MoveType == eMoveTypes.PARABORIC || this.MoveType == eMoveTypes.PARABORIC_ROTATE) && (double)_currentTime < (double)this.duration * 0.5 || (_hitFlag || this.InFlag))
                return _hitFlag;
            float num1 = this.transform.position.x + this.ColliderBox.center.x;
            float num2 = this.ColliderBox.extents.x * 0.5f;
            double num3 = (double)this.FireTarget.GetColliderCenter().x + (double)this.FireTarget.GetPosition().x;
            float num4 = this.FireTarget.GetColliderSize().x * 0.5f;
            float _b1 = num1 - num2;
            float _b2 = num1 + num2;
            float _a1 = (float)num3 - num4;
            float _a2 = (float)num3 + num4;
            //Debug.Log(BattleHeaderController.CurrentFrameCount + "-弹道检测：" + num1 + "--" + num2 + "--" + num3+"--"+num4);
            if ((double)_a1 >= (double)_b2 && !BattleUtil.Approximately(_a1, _b2) || (double)_a2 <= (double)_b1 && !BattleUtil.Approximately(_a2, _b1))
                return _hitFlag;
            if (this.MoveType != eMoveTypes.PARABORIC && this.MoveType != eMoveTypes.PARABORIC_ROTATE)
                return true;
            this.InFlag = true;
            return _hitFlag;
        }

        protected virtual bool getStopFlag() => this.stopFlag;

        public override void ResetParameter(GameObject _prefab, int _skinId, bool _isShadow)
        {
            base.ResetParameter(_prefab, _skinId);
            this.activeSelf = true;
            this.stopFlag = false;
        }
        public FirearmCtrlData GetPrefabData()
        {
            return new FirearmCtrlData(HitDelay, MoveRate, duration,(PCRCaculator.Battle.eMoveTypes)(int)MoveType, startRotate, endRotate, ColliderBox);
        }

        public void SetDatas(FirearmCtrlData data)
        {
            HitDelay = data.HitDelay;
            MoveRate = data.MoveRate;
            duration = data.duration;
            MoveType = (eMoveTypes)(int)data.MoveType;
            startRotate = data.startRotate;
            endRotate = data.endRotate;
            ColliderBox = new Bounds(
                new Vector3(data.ColliderBoxCentre[0], data.ColliderBoxCentre[1], data.ColliderBoxCentre[2]),
                new Vector3(data.ColliderBoxSize[0], data.ColliderBoxSize[1], data.ColliderBoxSize[2]));
        }
    }
}

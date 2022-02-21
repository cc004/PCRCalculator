// Decompiled with JetBrains decompiler
// Type: Elements.FirearmCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using Cute;
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
        //private float SPEED_FIX => MyGameCtrl.Instance.tempData.SettingData.skillEffeckFix;

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
            FireTarget = null;
            SkillHitEffects = null;
            EndActions = null;
            Skill = null;
            OnHitAction = null;
            ShakeEffects = null;
            easingX = null;
            easingUpY = null;
            easingDownY = null;
            easingUpRotate = null;
            easingDownRotate = null;
            owner = null;
            onCowHit = null;
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
            ShakeEffects = _shakes;
            IsAbsolute = _isAbsolute;
            Skill = _skill;
            if (_isAbsolute)
            {
                TargetPos = _targetPosition;
            }
            else
            {
                switch (_targetBone)
                {
                    case eTargetBone.BOTTOM:
                        TargetPos = _target.GetBottomTransformPosition();
                        break;
                    case eTargetBone.HEAD:
                        TargetPos = getHeadBonePos(_target);
                        break;
                    case eTargetBone.CENTER:
                    case eTargetBone.FIXED_CENETER:
                        TargetPos = _target.GetBottomTransformPosition() + _target.GetFixedCenterPos();
                        break;
                }
            }
            FireTarget = _target;
            _target.Owner.FirearmCtrlsOnMe.Add(this);
            EndActions = _actions;
            SkillHitEffects = _skillEffect;
            owner = _owner;
            setInitialPosition();
            initMoveType(_height, _owner);
            battleManager.AppendCoroutine(updatePosition(Vector3.Distance(TargetPos, initialPosistion) + 1f), ePauseType.SYSTEM, _hasBlackOutTime ? _owner : null);
            battleManager.AppendEffect(this, _hasBlackOutTime ? _owner : null);
        }

        protected virtual void setInitialPosition() => initialPosistion = transform.position;

        protected virtual void Awake() => isRepeat = true;

        private void initMoveType(float _height, UnitCtrl _owner)
        {
            switch (MoveType)
            {
                case eMoveTypes.LINEAR:
                    Vector3 toDirection = TargetPos - transform.position;
                    toDirection.Normalize();
                    //this.speed = this.MoveRate * toDirection;
                    //this.speed = this.MoveRate * toDirection*SPEED_FIX;
                    //this.transform.rotation = Quaternion.FromToRotation((UnityEngine.Object)_owner == (UnityEngine.Object)null || !_owner.IsLeftDir ? Vector3.right : Vector3.left, toDirection);
                    break;
                case eMoveTypes.NONE:
                case eMoveTypes.HORIZONTAL:
                    //this.speed = new Vector3(this.MoveRate, 0.0f, 0.0f)*SPEED_FIX;
                    break;
                case eMoveTypes.PARABORIC:
                case eMoveTypes.PARABORIC_ROTATE:
                    float durationTime = duration / 2f;
                    easingUpY = new CustomEasing(CustomEasing.eType.outCubic, transform.position.y, transform.position.y + _height, durationTime);
                    easingDownY = new CustomEasing(CustomEasing.eType.inQuad, transform.position.y + _height, TargetPos.y, durationTime);
                    easingX = new CustomEasing(CustomEasing.eType.linear, transform.position.x, TargetPos.x, duration);
                    break;
            }
            if (MoveType != eMoveTypes.PARABORIC_ROTATE)
                return;
            easingUpRotate = new CustomEasing(CustomEasing.eType.inQuad, startRotate, 0.0f, duration / 2f);
            easingDownRotate = new CustomEasing(CustomEasing.eType.linear, 0.0f, endRotate, duration / 2f);
        }

        private Vector3 GetParaboricPosition(float _currentTime, float _deltaTime)
        {
            Vector3 vector3 = transform.position;
            if (easingX.IsMoving)
                vector3 = new Vector3(easingX.GetCurVal(_deltaTime, true), _currentTime >= duration / 2.0 ? easingDownY.GetCurVal(_deltaTime, true) : easingUpY.GetCurVal(_deltaTime, true), transform.position.z);
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
                    yield return null;
                }
                else
                {
                    float fnhfjlaenpf = firearmCtrl.battleManager.DeltaTime_60fps;
                    if (hitFlag)
                    {
                        hitTimer += fnhfjlaenpf;
                        float _b = firearmCtrl.MoveType == eMoveTypes.LINEAR ? 0.2f / firearmCtrl.MoveRate : firearmCtrl.HitDelay;
                        if (hitTimer > (double)_b || BattleUtil.Approximately(hitTimer, _b))
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
                            firearmCtrl.onCowHit = null;
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
                        if (firearmCtrl == null)
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
                                if (currentTime > (double)firearmCtrl.duration)
                                {
                                    hitFlag = true;
                                }
                                break;
                        }
                        if (firearmCtrl.MoveType == eMoveTypes.PARABORIC_ROTATE)
                            firearmCtrl.particle.transform.eulerAngles = new Vector3(0.0f, 0.0f, currentTime >= firearmCtrl.duration / 2.0 ? firearmCtrl.easingDownRotate.GetCurVal(fnhfjlaenpf, true) : firearmCtrl.easingUpRotate.GetCurVal(fnhfjlaenpf, true));
                        firearmCtrl.transform.position = b;
                        switch (firearmCtrl.MoveType)
                        {
                            case eMoveTypes.LINEAR:
                            case eMoveTypes.HORIZONTAL:
                                if (Vector3.Distance(firearmCtrl.initialPosistion, b) > (double)_lifeDistance)
                                //if ((double)Mathf.Abs(firearmCtrl.initialPosistion.x- b.x) > (double)3*_lifeDistance)

                                {
                                    firearmCtrl.FireTarget.Owner.FirearmCtrlsOnMe.Remove(firearmCtrl);
                                    firearmCtrl.timeToDie = true;
                                }
                                break;
                        }
                    }
                    if (!firearmCtrl.IsAbsolute)
                        hitFlag = firearmCtrl.collisionDetection(hitFlag, currentTime);
                    yield return null;
                }
            }
        }

        public override bool _Update()
        {
            if (!activeSelf)
                gameObject.SetActive(false);
            if (!timeToDie)
                return true;
            //this.skillSeSource.Stop();
            IsPlaying = false;
            gameObject.SetActive(false);
            return false;
        }

        private bool collisionDetection(bool _hitFlag, float _currentTime)
        {
            if ((MoveType == eMoveTypes.PARABORIC || MoveType == eMoveTypes.PARABORIC_ROTATE) && _currentTime < duration * 0.5 || (_hitFlag || InFlag))
                return _hitFlag;
            float num1 = transform.position.x + ColliderBox.center.x;
            float num2 = ColliderBox.extents.x * 0.5f;
            double num3 = FireTarget.GetColliderCenter().x + (double)FireTarget.GetPosition().x;
            float num4 = FireTarget.GetColliderSize().x * 0.5f;
            float _b1 = num1 - num2;
            float _b2 = num1 + num2;
            float _a1 = (float)num3 - num4;
            float _a2 = (float)num3 + num4;
            //Debug.Log(BattleHeaderController.CurrentFrameCount + "-弹道检测：" + num1 + "--" + num2 + "--" + num3+"--"+num4);
            if (_a1 >= (double)_b2 && !BattleUtil.Approximately(_a1, _b2) || _a2 <= (double)_b1 && !BattleUtil.Approximately(_a2, _b1))
                return _hitFlag;
            if (MoveType != eMoveTypes.PARABORIC && MoveType != eMoveTypes.PARABORIC_ROTATE)
                return true;
            InFlag = true;
            return _hitFlag;
        }

        protected virtual bool getStopFlag() => stopFlag;

        public override void ResetParameter(GameObject _prefab, int _skinId, bool _isShadow)
        {
            base.ResetParameter(_prefab, _skinId);
            activeSelf = true;
            stopFlag = false;
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

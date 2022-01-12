// Decompiled with JetBrains decompiler
// Type: Elements.BasePartsData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
using Elements.Battle;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [Serializable]
    public class BasePartsData
    {
        public float PositionX;
        public float PositionY;
        public List<BasePartsData.AttachmentNamePair> AttachmentNamePairList = new List<BasePartsData.AttachmentNamePair>();
        public float ChangeAttachmentStartTime;
        public float ChangeAttachmentEndTime;
        public float BodyWidthValue;
        public int Index;
        public int EnemyId = 100000001;
        private float lastHealEffectTime;
        private const int ALL_DETAIL_VALUE = -1;

        public List<NormalSkillEffect> BreakEffectList { get; set; }

        public float InitialPositionX { get; set; }

        public UnitCtrl Owner { get; set; }

        public float TotalDamage { get; set; }

        protected BattleManager battleManager { get; set; }

        public int UbAttackHitCount { get; set; }

        public bool PassiveUbIsMagic { get; set; }

        public bool IsBlackoutTarget { get; set; }

        public void SetBattleManager(BattleManager _battleManager) => this.battleManager = _battleManager;

        public virtual Vector3 GetPosition() => this.Owner.transform.position;

        public virtual Vector3 GetLocalPosition() => this.Owner.transform.localPosition;

        public virtual float GetBodyWidth() => this.Owner.BodyWidth;

        //public virtual Vector3 GetBottomTransformPosition() => (UnityEngine.Object)this.Owner == (UnityEngine.Object)null ? Vector3.zero : this.Owner.BottomTransform.position;
        public virtual Vector3 GetBottomTransformPosition() => (UnityEngine.Object)this.Owner == (UnityEngine.Object)null ? Vector3.zero : this.Owner.BottomTransform.position*60f;

        //public virtual Vector3 GetOwnerBottomTransformPosition() => this.Owner.BottomTransform.position;
        public virtual Vector3 GetOwnerBottomTransformPosition() => this.Owner.BottomTransform.position * 60f;

        //public Vector3 GetBottomTransformLocalPosition() => this.Owner.BottomTransform.localPosition;
        public Vector3 GetBottomTransformLocalPosition() => this.Owner.BottomTransform.localPosition * 60f;

        public Vector3 GetBottomLossyScale() => this.Owner.BottomTransform.lossyScale;

        public virtual Bone GetStateBone() => this.Owner.StateBone;

        public virtual Bone GetCenterBone() => this.Owner.CenterBone;

        public virtual Vector3 GetFixedCenterPos() => this.Owner.FixedCenterPos;

        public virtual Vector3 GetColliderCenter() => this.Owner.ColliderCenter;

        public virtual Vector3 GetColliderSize() => this.Owner.ColliderSize;

        public virtual int GetLevel() => (int)this.Owner.Level;

        public void IncrementUbAttackHitCount() => ++this.UbAttackHitCount;

        public virtual int GetAtkZero() => (int)this.Owner.AtkZero;

        public virtual int GetMagicStrZero() => (int)this.Owner.MagicStrZero;

        public virtual int GetAccuracyZero() => (int)this.Owner.AccuracyZero;

        public virtual int GetPhysicalCriticalZero() => (int)this.Owner.PhysicalCriticalZero;

        public virtual int GetMagicCriticalZero() => (int)this.Owner.MagicCriticalZero;

        public virtual int GetLifeStealZero() => (int)this.Owner.LifeStealZero;

        public virtual float GetHpRecoverRateZero() => (float)(int)this.Owner.HpRecoveryRateZero;

        public virtual float GetDodgeRate(int _accuracy) => this.Owner.GetDodgeRate(_accuracy);

        public virtual int GetDefZero() => (int)this.Owner.DefZero;

        public virtual int GetMagicDefZero() => (int)this.Owner.MagicDefZero;

        public virtual void SetMissAtk(
          UnitCtrl _source,
          eMissLogType _missLogType,
          eDamageEffectType _damageEffectType,
          float _scale)
        {
            if (this.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.NO_DAMAGE_MOTION))
                return;
            this.Owner.SetMissAtk(_source, _missLogType, _damageEffectType, _scale: _scale);
        }

        /*public void ShowHitEffect(
          SystemIdDefine.eWeaponSeType _weaponSeType,
          Skill _skill,
          bool _isLeft) => this.Owner.ShowHitEffect(_weaponSeType, _skill, _isLeft, this);*/

        public virtual int GetStartAtk() => (int)this.Owner.StartAtk;

        public virtual int GetStartDef() => (int)this.Owner.StartDef;

        public virtual int GetStartMagicStr() => (int)this.Owner.StartMagicStr;

        public virtual int GetStartMagicDef() => (int)this.Owner.StartMagicDef;

        public virtual int GetStartDodge() => (int)this.Owner.StartDodge;

        public virtual int GetStartPhysicalCritical() => (int)this.Owner.StartPhysicalCritical;

        public virtual int GetStartMagicCritical() => (int)this.Owner.StartMagicCritical;

        public virtual int GetStartLifeSteal() => (int)this.Owner.StartLifeSteal;

        /*public IEnumerator StartTotalDamage(bool _isLarge, float _delay)
        {
          BasePartsData _targetParts = this;
          float tmpTotalDamage = _targetParts.TotalDamage;
          yield return (object) new WaitForSeconds(_delay);
          DamageEffectCtrl damageNumEffect = _targetParts.Owner.CreateDamageNumEffect((int) tmpTotalDamage, _isLarge ? _targetParts.battleManager.TotalDamageEffectPrefabLarge : _targetParts.battleManager.TotalDamageEffectPrefabSmall, false, false, (Skill) null, _targetParts.PassiveUbIsMagic, (UnitCtrl) null, DamageData.eDamageSoundType.HIT, _targetParts, 1f);
                if (!((UnityEngine.Object)damageNumEffect == (UnityEngine.Object)null))
                    damageNumEffect.SetSortOrder((int)((_targetParts.Owner.IsFront ? 11500 : 0) + 11250 + _targetParts.Owner.RespawnPos));
        }*/

        public bool JudgeShowTotalDamage() => this.UbAttackHitCount >= 2 && (double)this.TotalDamage != 0.0;

        public void ResetTotalDamage()
        {
            this.TotalDamage = 0.0f;
            this.UbAttackHitCount = 0;
        }

        public virtual void SetBuffDebuff(
          bool _enable,
          FloatWithEx _value,
          UnitCtrl.BuffParamKind _kind,
          UnitCtrl _source,
          BattleLogIntreface _battleLog,
          bool _additional, int hash)
        {
            if (!_enable)
                _value = _value * -1f;
            BattleLogIntreface mlegmhaocon = _battleLog;
            UnitCtrl unitCtrl = _source;
            UnitCtrl owner1 = this.Owner;
            int HLIKLPNIOKJ = (int)((_enable ? 1 : 2) * 10 + _kind);
            long KGNFLOPBOMB = (long)_value;
            UnitCtrl JELADBAMFKH = unitCtrl;
            UnitCtrl LIMEKPEENOB = owner1;
            mlegmhaocon.AppendBattleLog(eBattleLogType.SET_BUFF_DEBUFF, HLIKLPNIOKJ, KGNFLOPBOMB, 0L, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
            if (_additional)
            {
                if (this.Owner.AdditionalBuffDictionary.ContainsKey(_kind))
                    this.Owner.AdditionalBuffDictionary[_kind] = this.Owner.AdditionalBuffDictionary[_kind] + _value;
                else
                    this.Owner.AdditionalBuffDictionary.Add(_kind, _value);
            }
            else
            {
                switch (_kind)
                {
                    case UnitCtrl.BuffParamKind.ATK:
                        UnitCtrl owner2 = this.Owner;
                        owner2.Atk = owner2.Atk.Sum(hash, _value);
                        break;
                    case UnitCtrl.BuffParamKind.DEF:
                        UnitCtrl owner3 = this.Owner;
                        owner3.Def = (int)((int)owner3.Def + _value);
                        string des = (_source==null?"???":_source.UnitName) + "的技能" + (_enable?"开始":"结束") + "变更" + _value;
                        Owner.MyOnBaseValueChanged?.Invoke(Owner.UnitId, 1, owner3.Def, BattleHeaderController.CurrentFrameCount,des);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_STR:
                        UnitCtrl owner4 = this.Owner;
                        owner4.MagicStr = owner4.MagicStr.Sum(hash,  _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_DEF:
                        UnitCtrl owner5 = this.Owner;
                        owner5.MagicDef = (int)((int)owner5.MagicDef + _value);
                        string des2 = (_source == null ? "???" : _source.UnitName) + "的技能" + (_enable ? "开始" : "结束") + "变更" + _value;
                        Owner.MyOnBaseValueChanged?.Invoke(Owner.UnitId, 2, owner5.MagicDef, BattleHeaderController.CurrentFrameCount, des2);
                        break;
                    case UnitCtrl.BuffParamKind.DODGE:
                        UnitCtrl owner6 = this.Owner;
                        owner6.Dodge = (int)((int)owner6.Dodge + _value);
                        break;
                    case UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL:
                        UnitCtrl owner7 = this.Owner;
                        owner7.PhysicalCritical = (int)((int)owner7.PhysicalCritical + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_CRITICAL:
                        UnitCtrl owner8 = this.Owner;
                        owner8.MagicCritical = (int)((int)owner8.MagicCritical + _value);
                        break;
                    case UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE:
                        UnitCtrl owner9 = this.Owner;
                        owner9.EnergyRecoveryRate = (int)((int)owner9.EnergyRecoveryRate + _value);
                        break;
                    case UnitCtrl.BuffParamKind.LIFE_STEAL:
                        UnitCtrl owner10 = this.Owner;
                        owner10.LifeSteal = (int)((int)owner10.LifeSteal + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MOVE_SPEED:
                        this.Owner.MoveSpeed += (float)_value;
                        break;
                    case UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE:
                        UnitCtrl owner11 = this.Owner;
                        owner11.PhysicalCriticalDamageRate = (int)((int)owner11.PhysicalCriticalDamageRate + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE:
                        UnitCtrl owner12 = this.Owner;
                        owner12.MagicCriticalDamageRate = (int)((int)owner12.MagicCriticalDamageRate + _value);
                        break;
                    case UnitCtrl.BuffParamKind.ACCURACY:
                        UnitCtrl owner13 = this.Owner;
                        owner13.Accuracy = (int)(int)((int)owner13.Accuracy + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAX_HP:
                        if ((long)this.Owner.MaxHp + (long)_value > 0L)
                        {
                            UnitCtrl owner14 = this.Owner;
                            owner14.MaxHp = (ObscuredLong)((long)owner14.MaxHp + (long)_value);
                            if ((long)this.Owner.MaxHp >= (long)this.Owner.Hp)
                                break;
                            this.Owner.SetCurrentHp((long)this.Owner.MaxHp);
                            break;
                        }
                        if ((long)this.Owner.Hp <= 0L)
                            break;
                        this.Owner.SetCurrentHpZero();
                        this.Owner.ClearKnightGuard();
                        if (this.Owner.IsDead || this.Owner.CurrentState >= UnitCtrl.ActionState.DIE)
                            break;
                        this.Owner.SetState(UnitCtrl.ActionState.DIE);
                        break;

                }
            }
        }

        /*public void RecoveryEffect(
          UnitCtrl _source,
          bool _isEnergy,
          BattleEffectPoolInterface _battleEffectPool,
          bool _isRegenerate = false)
        {
          float time = Time.time;
          if ((double) time < (double) this.lastHealEffectTime + 0.449999988079071)
            return;
          LCEGKJFKOPD instance = Singleton<LCEGKJFKOPD>.Instance;
          GameObject MDOJNMEMHLN = !_isEnergy ? (!_isRegenerate || !((UnityEngine.Object) instance.JKKJAAJNLCN != (UnityEngine.Object) null) ? instance.CGHCKMNBOGF : instance.JKKJAAJNLCN) : (!_isRegenerate || !((UnityEngine.Object) instance.AAEJOJPJHOM != (UnityEngine.Object) null) ? instance.KPMBMLBNKLC : instance.AAEJOJPJHOM);
          GameObject gameObject = _battleEffectPool.GetEffect(MDOJNMEMHLN).gameObject;
          gameObject.transform.localScale = MDOJNMEMHLN.transform.localScale;
          gameObject.transform.position = MDOJNMEMHLN.transform.position + this.GetPosition();
          gameObject.transform.parent = ExceptNGUIRoot.Transform;
          SkillEffectCtrl component = gameObject.GetComponent<SkillEffectCtrl>();
          component.InitializeSort();
          component.SortTarget = this.Owner;
          component.SetSortOrderBack();
          component.ExecAppendCoroutine(_source);
          if (this.Owner.gameObject.activeSelf)
            this.Owner.StartCoroutine(component.TrackTarget(this, Vector3.zero, bone: this.GetCenterBone()));
          component.PlaySe(this.Owner.SoundUnitId, this.Owner.IsLeftDir);
          this.lastHealEffectTime = time;
        }*/

        public virtual bool GetTargetable() => true;

        public Dictionary<eActionType, Dictionary<int, int>> ResistStatusDictionary { get; set; }

        public void InitializeResistStatus(int resistId)
        {
            /* this.ResistStatusDictionary = new Dictionary<eActionType, Dictionary<int, int>>((IEqualityComparer<eActionType>)new eActionType_DictComparer());

             MasterDataManager instance = ManagerSingleton<MasterDataManager>.Instance;
             this.ResistStatusDictionary = new Dictionary<eActionType, Dictionary<int, int>>((IEqualityComparer<eActionType>) new eActionType_DictComparer());
             if (resistId == 0)
               return;
             MasterResistData.ResistData resistData = instance.masterResistData.Get(resistId);
             for (int index = 0; index < resistData.Ailments.Length; ++index)
             {
               MasterAilmentData.AilmentData ailmentData = instance.masterAilmentData.Get(index + 1);
               if (ailmentData == null)
                 break;
               eActionType ailmentAction = (eActionType) (int) ailmentData.ailment_action;
               if (!this.ResistStatusDictionary.ContainsKey(ailmentAction))
                 this.ResistStatusDictionary.Add(ailmentAction, new Dictionary<int, int>());
               this.ResistStatusDictionary[ailmentAction][(int) ailmentData.ailment_detail_1] = resistData.Ailments[index];
             }*/
            Dictionary<eActionType, Dictionary<int, int>> resistDic = new Dictionary<eActionType, Dictionary<int, int>>();
            PCRCaculator.Battle.ResistData unitResistdata = new PCRCaculator.Battle.ResistData(resistId);
            for (int i = 0; i < PCRCaculator.Battle.StaticAilmentData.ailmentDatas.Length; i++)
            {
                PCRCaculator.Battle.AilmentData ailment = PCRCaculator.Battle.StaticAilmentData.ailmentDatas[i];
                if (!resistDic.ContainsKey((eActionType)ailment.ailment_action))
                {
                    resistDic.Add((eActionType)ailment.ailment_action, new Dictionary<int, int>());
                }
                resistDic[(eActionType)ailment.ailment_action].Add(ailment.ailment_detail1, unitResistdata.ailments[i]);
            }
            ResistStatusDictionary = resistDic;

        }

        public bool ResistStatus(
      eActionType _actionType,
      int _detail1,
      UnitCtrl _source,
      bool _last,
      bool _targetOneParts,
      BasePartsData _target)
        {
            if (!this.ResistStatusDictionary.ContainsKey(_actionType))
                return false;
            Dictionary<int, int> resistStatus = this.ResistStatusDictionary[_actionType];
            if (resistStatus.ContainsKey(-1))
                _detail1 = -1;
            if (!resistStatus.ContainsKey(_detail1) || (double)BattleManager.Random(0.0f, 1f, 
                new PCRCaculator.Guild.RandomData(_source,_target.Owner, (int)_actionType, 9, resistStatus[_detail1] / 100.0f)) >= (double)resistStatus[_detail1] / 100.0)
                return false;
            if (_last)
            {
                if (_targetOneParts)
                    this.Owner.SetMissAtk(_source, eMissLogType.DODGE_ATTACK, _parts: _target);
                else
                    this.Owner.SetMissAtk(_source, eMissLogType.DODGE_ATTACK);
            }
            return true;
        }

        [Serializable]
        public class AttachmentNamePair
        {
            public string TargetSlotName = "";
            public string TargetAttachmentName = "";
            public string AppliedSlotName = "";
            public string AppliedAttachmentName = "";

            public int TargetIndex { get; set; }

            public Attachment TargetAttachment { get; set; }

            public Attachment AppliedAttachment { get; set; }
        }
    }
}

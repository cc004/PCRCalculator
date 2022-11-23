// Decompiled with JetBrains decompiler
// Type: Elements.BasePartsData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using Elements.Battle;
using PCRCaculator.Battle;
using PCRCaculator.Guild;
using Spine;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Attachment = Spine.Attachment;

namespace Elements
{
    [Serializable]
    public class BasePartsData
    {
        public float PositionX;
        public float PositionY;
        public List<AttachmentNamePair> AttachmentNamePairList = new List<AttachmentNamePair>();
        public bool DisableStartBreakEffect;
        public float ChangeAttachmentStartTime;
        public float ChangeAttachmentEndTime;
        public float BodyWidthValue;
        public int Index;
        public int EnemyId = 100000001;
        public float BreakEffectTime;
        public float BreakEffectOffsetY;
        public float BreakEffectSize = 1f;
        private float lastHealEffectTime;
        private const int ALL_DETAIL_VALUE = -1;
        private int currentAbnormalResistId;

        private int currentDebuffResistId;

        private Dictionary<int, Dictionary<eActionType, Dictionary<int, int>>> abnormalResistIdDictionary = new Dictionary<int, Dictionary<eActionType, Dictionary<int, int>>>();

        private Dictionary<int, Dictionary<UnitCtrl.BuffParamKind, int>> debuffResistIdDictionary = new Dictionary<int, Dictionary<UnitCtrl.BuffParamKind, int>>();

        public List<NormalSkillEffect> BreakEffectList { get; set; }

        public float InitialPositionX { get; set; }

        public UnitCtrl Owner { get; set; }

        public float TotalDamage { get; set; }

        protected BattleManager battleManager { get; set; }

        public int UbAttackHitCount { get; set; }

        public bool PassiveUbIsMagic { get; set; }

        public bool IsBlackoutTarget { get; set; }
        public int StartAbnormalResistId
        {
            get;
            private set;
        }

        public int StartDebuffResistId
        {
            get;
            private set;
        }

        public int AbnormalResistIdSetCount
        {
            get;
            private set;
        }

        public int DebuffResistIdSetCount
        {
            get;
            private set;
        }

        private Dictionary<eActionType, Dictionary<int, int>> abnormalResistStatusDictionary => abnormalResistIdDictionary[currentAbnormalResistId];

        private Dictionary<UnitCtrl.BuffParamKind, int> debuffResistStatusDictionary => debuffResistIdDictionary[currentDebuffResistId];

        public void SetBattleManager(BattleManager _battleManager) => battleManager = _battleManager;

        public virtual Vector3 GetPosition() => Owner.transform.position;

        public virtual Vector3 GetLocalPosition() => Owner.transform.localPosition;

        public virtual float GetLocalPositionX() => Owner.transform.positionX / FixedTransformMonoBehavior.FixedTransform.DIGID;

        public virtual float GetBodyWidth() => Owner.BodyWidth;

        //public virtual Vector3 GetBottomTransformPosition() => (UnityEngine.Object)this.Owner == (UnityEngine.Object)null ? Vector3.zero : this.Owner.BottomTransform.position;
        public virtual Vector3 GetBottomTransformPosition() => Owner == null ? Vector3.zero : Owner.BottomTransform.position;

        //public virtual Vector3 GetOwnerBottomTransformPosition() => this.Owner.BottomTransform.position;
        public virtual Vector3 GetOwnerBottomTransformPosition() => Owner.BottomTransform.position * 60f;

        //public Vector3 GetBottomTransformLocalPosition() => this.Owner.BottomTransform.localPosition;
        public Vector3 GetBottomTransformLocalPosition() => Owner.BottomTransform.localPosition;

        public Vector3 GetBottomLossyScale() => Owner.BottomTransform.lossyScale;

        public virtual Bone GetStateBone() => Owner.StateBone;

        public virtual Bone GetCenterBone() => Owner.CenterBone;

        public virtual Vector3 GetFixedCenterPos() => Owner.FixedCenterPos;

        public virtual Vector3 GetColliderCenter() => Owner.ColliderCenter;

        public virtual Vector3 GetColliderSize() => Owner.ColliderSize;

        public virtual int GetLevel() => Owner.Level;

        public void IncrementUbAttackHitCount() => ++UbAttackHitCount;

        public virtual int GetAtkZero() => (int)Owner.AtkZero;

        public virtual int GetMagicStrZero() => (int)Owner.MagicStrZero;

        public virtual FloatWithEx GetAtkZeroEx() => Owner.AtkZero;

        public virtual FloatWithEx GetMagicStrZeroEx() => Owner.MagicStrZero;

        public virtual int GetAccuracyZero() => Owner.AccuracyZero;

        public virtual int GetPhysicalCriticalZero() => Owner.PhysicalCriticalZero;

        public virtual int GetMagicCriticalZero() => Owner.MagicCriticalZero;

        public virtual int GetLifeStealZero() => Owner.LifeStealZero;

        public virtual float GetHpRecoverRateZero() => Owner.HpRecoveryRateZero;

        public virtual float GetDodgeRate(int _accuracy) => Owner.GetDodgeRate(_accuracy);

        public virtual int GetDefZero() => Owner.DefZero;

        public virtual int GetMagicDefZero() => Owner.MagicDefZero;

        public virtual void SetMissAtk(
          UnitCtrl _source,
          eMissLogType _missLogType,
          eDamageEffectType _damageEffectType,
          float _scale)
        {
            if (!Owner.IsNoDamageMotion())
            {
                Owner.SetMissAtk(_source, _missLogType, _damageEffectType, null, _scale);
            }
        }

        /*public void ShowHitEffect(
          SystemIdDefine.eWeaponSeType _weaponSeType,
          Skill _skill,
          bool _isLeft) => this.Owner.ShowHitEffect(_weaponSeType, _skill, _isLeft, this);*/

        public virtual int GetStartAtk() => Owner.StartAtk;

        public virtual int GetStartDef() => Owner.StartDef;

        public virtual int GetStartMagicStr() => Owner.StartMagicStr;

        public virtual int GetStartMagicDef() => Owner.StartMagicDef;

        public virtual int GetStartDodge() => Owner.StartDodge;

        public virtual int GetStartPhysicalCritical() => Owner.StartPhysicalCritical;

        public virtual int GetStartMagicCritical() => Owner.StartMagicCritical;

        public virtual int GetStartLifeSteal() => Owner.StartLifeSteal;

        /*public IEnumerator StartTotalDamage(bool _isLarge, float _delay)
        {
          BasePartsData _targetParts = this;
          float tmpTotalDamage = _targetParts.TotalDamage;
          yield return (object) new WaitForSeconds(_delay);
          DamageEffectCtrl damageNumEffect = _targetParts.Owner.CreateDamageNumEffect((int) tmpTotalDamage, _isLarge ? _targetParts.battleManager.TotalDamageEffectPrefabLarge : _targetParts.battleManager.TotalDamageEffectPrefabSmall, false, false, (Skill) null, _targetParts.PassiveUbIsMagic, (UnitCtrl) null, DamageData.eDamageSoundType.HIT, _targetParts, 1f);
                if (!((UnityEngine.Object)damageNumEffect == (UnityEngine.Object)null))
                    damageNumEffect.SetSortOrder((int)((_targetParts.Owner.IsFront ? 11500 : 0) + 11250 + _targetParts.Owner.RespawnPos));
        }*/

        public bool JudgeShowTotalDamage() => UbAttackHitCount >= 2 && TotalDamage != 0.0;

        public void ResetTotalDamage()
        {
            TotalDamage = 0.0f;
            UbAttackHitCount = 0;
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
            UnitCtrl owner1 = Owner;
            int HLIKLPNIOKJ = (int)((_enable ? 1 : 2) * 10 + _kind);
            long KGNFLOPBOMB = (long)_value;
            UnitCtrl JELADBAMFKH = unitCtrl;
            UnitCtrl LIMEKPEENOB = owner1;
            mlegmhaocon.AppendBattleLog(eBattleLogType.SET_BUFF_DEBUFF, HLIKLPNIOKJ, KGNFLOPBOMB, 0L, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
            if (_additional)
            {
                if (Owner.AdditionalBuffDictionary.ContainsKey(_kind))
                    Owner.AdditionalBuffDictionary[_kind] = Owner.AdditionalBuffDictionary[_kind] + _value;
                else
                    Owner.AdditionalBuffDictionary.Add(_kind, _value);
            }
            else
            {
                UnitCtrl owner = Owner;
                switch (_kind)
                {
                    case UnitCtrl.BuffParamKind.ATK:
                        UnitCtrl owner2 = Owner;
                        owner2.Atk = owner2.Atk + _value;
                        break;
                    case UnitCtrl.BuffParamKind.DEF:
                        UnitCtrl owner3 = Owner;
                        owner3.Def = (int)((int)owner3.Def + _value);
                        string des = (_source==null?"???":_source.UnitName) + "的技能" + (_enable?"开始":"结束") + "变更" + _value;
                        Owner.MyOnBaseValueChanged?.Invoke(Owner.UnitId, 1, owner3.Def, BattleHeaderController.CurrentFrameCount,des);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_STR:
                        UnitCtrl owner4 = Owner;
                        owner4.MagicStr = owner4.MagicStr +  _value;
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_DEF:
                        UnitCtrl owner5 = Owner;
                        owner5.MagicDef = (int)((int)owner5.MagicDef + _value);
                        string des2 = (_source == null ? "???" : _source.UnitName) + "的技能" + (_enable ? "开始" : "结束") + "变更" + _value;
                        Owner.MyOnBaseValueChanged?.Invoke(Owner.UnitId, 2, owner5.MagicDef, BattleHeaderController.CurrentFrameCount, des2);
                        break;
                    case UnitCtrl.BuffParamKind.DODGE:
                        UnitCtrl owner6 = Owner;
                        owner6.Dodge = (int)((int)owner6.Dodge + _value);
                        break;
                    case UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL:
                        UnitCtrl owner7 = Owner;
                        owner7.PhysicalCritical = (int)((int)owner7.PhysicalCritical + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_CRITICAL:
                        UnitCtrl owner8 = Owner;
                        owner8.MagicCritical = (int)((int)owner8.MagicCritical + _value);
                        break;
                    case UnitCtrl.BuffParamKind.ENERGY_RECOVER_RATE:
                        UnitCtrl owner9 = Owner;
                        owner9.EnergyRecoveryRate = (int)((int)owner9.EnergyRecoveryRate + _value);
                        break;
                    case UnitCtrl.BuffParamKind.LIFE_STEAL:
                        UnitCtrl owner10 = Owner;
                        owner10.LifeSteal = (int)((int)owner10.LifeSteal + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MOVE_SPEED:
                        Owner.MoveSpeed += (float)_value;
                        break;
                    case UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL_DAMAGE_RATE:
                        UnitCtrl owner11 = Owner;
                        owner11.PhysicalCriticalDamageRate = (int)((int)owner11.PhysicalCriticalDamageRate + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_CRITICAL_DAMAGE_RATE:
                        UnitCtrl owner12 = Owner;
                        owner12.MagicCriticalDamageRate = (int)((int)owner12.MagicCriticalDamageRate + _value);
                        break;
                    case UnitCtrl.BuffParamKind.ACCURACY:
                        UnitCtrl owner13 = Owner;
                        owner13.Accuracy = (int)((int)owner13.Accuracy + _value);
                        break;
                    case UnitCtrl.BuffParamKind.RECEIVE_CRITICAL_DAMAGE_RATE:
                        {
                            owner.ReceiveCriticalDamageRate = (int)(owner.ReceiveCriticalDamageRate - _value);
                            break;
                        }
                    case UnitCtrl.BuffParamKind.RECEIVE_PHYSICAL_AND_MAGIC_DAMAGE_PERCENT:
                        {
                            owner.AdditionalPhysicalAndMagicReceiveDamagePercent = (int)(owner.AdditionalPhysicalAndMagicReceiveDamagePercent - _value);
                            break;
                        }
                    case UnitCtrl.BuffParamKind.RECEIVE_PHYSICAL_DAMAGE_PERCENT:
                        {
                            owner.AdditionalPhysicalReceiveDamagePercent = (int)(owner.AdditionalPhysicalReceiveDamagePercent - _value);
                            break;
                        }
                    case UnitCtrl.BuffParamKind.RECEIVE_MAGIC_DAMAGE_PERCENT:
                        {
                            owner.AdditionalMagicReceiveDamagePercent = (int)(owner.AdditionalMagicReceiveDamagePercent - _value);
                            break;
                        }
                    case UnitCtrl.BuffParamKind.MAX_HP:
                        if (Owner.MaxHp + (long)_value > 0L)
                        {
                            UnitCtrl owner14 = Owner;
                            owner14.MaxHp = owner14.MaxHp + (long)_value;
                            if (Owner.MaxHp >= (long)Owner.Hp)
                                break;
                            Owner.SetCurrentHp(Owner.MaxHp);
                            if (!_enable)
                            {
                                break;
                            }
                            float jEOCPILJNAD = (float)(long)Owner.Hp / (float)(long)Owner.MaxHp;
                            foreach (KeyValuePair<int, Action<float>> item in Owner.OnRecoverListForChangeSpeedDisableByMaxHp)
                            {
                                item.Value?.Invoke(jEOCPILJNAD);
                            }
                        }
                        if ((long)Owner.Hp <= 0L)
                            break;
                        Owner.SetCurrentHpZero();
                        Owner.ClearKnightGuard();
                        if (Owner.IsDead || Owner.CurrentState >= UnitCtrl.ActionState.DIE)
                            break;
                        Owner.SetState(UnitCtrl.ActionState.DIE);
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

        public void SetAbnormalResistId(int _resistId, bool _isInit)
        {
            if (!abnormalResistIdDictionary.ContainsKey(_resistId))
            {
                createResistStatus(_resistId);
            }
            currentAbnormalResistId = _resistId;
            if (_isInit)
            {
                StartAbnormalResistId = _resistId;
            }
            AbnormalResistIdSetCount++;
        }

        public void SetDebuffResistId(int _resistId, bool _isInit)
        {
            if (!debuffResistIdDictionary.ContainsKey(_resistId))
            {
                createDebuffStatus(_resistId);
            }
            currentDebuffResistId = _resistId;
            if (_isInit)
            {
                StartDebuffResistId = _resistId;
            }
            DebuffResistIdSetCount++;
        }
        private void createResistStatus(int _resistId)
        {
            abnormalResistIdDictionary.AddIfNoContains(_resistId, new Dictionary<eActionType, Dictionary<int, int>>());
            if (_resistId == 0)
            {
                return;
            }
            Dictionary<eActionType, Dictionary<int, int>> resistDic = new Dictionary<eActionType, Dictionary<int, int>>();
            ResistData unitResistdata = new ResistData(_resistId);
            for (int i = 0; i < StaticAilmentData.ailmentDatas.Length; i++)
            {
                AilmentData ailment = StaticAilmentData.ailmentDatas[i];
                if (ailment == null)
                    break;
                if (!resistDic.ContainsKey((eActionType)ailment.ailment_action))
                {
                    resistDic.Add((eActionType)ailment.ailment_action, new Dictionary<int, int>());
                }
                resistDic[(eActionType)ailment.ailment_action].Add(ailment.ailment_detail1, unitResistdata.ailments[i]);
                eActionType key = (eActionType)(int)ailment.ailment_action;
                if (!abnormalResistIdDictionary[_resistId].ContainsKey(key))
                {
                    abnormalResistIdDictionary[_resistId].Add(key, new Dictionary<int, int>());
                }
                abnormalResistIdDictionary[_resistId][key][ailment.ailment_detail1] = unitResistdata.ailments[i];
            }
            
        }
        private void createDebuffStatus(int _resistId)
        {
            debuffResistIdDictionary.AddIfNoContains(_resistId, new Dictionary<UnitCtrl.BuffParamKind, int>());
            if (_resistId != 0)
            {
                ResistVariationData resistVariationData = StaticAilmentData.resistVariationDataDic[_resistId];
                for (int i = 0; i < 4; i++)
                {
                    debuffResistIdDictionary[_resistId][(UnitCtrl.BuffParamKind)(i + 1)] = resistVariationData.DebuffDecrement[i];
                }
            }
        }
        //public Dictionary<eActionType, Dictionary<int, int>> ResistStatusDictionary { get; set; }

        /*public void InitializeResistStatus(int resistId)
        {
            
            Dictionary<eActionType, Dictionary<int, int>> resistDic = new Dictionary<eActionType, Dictionary<int, int>>();
            ResistData unitResistdata = new ResistData(resistId);
            for (int i = 0; i < StaticAilmentData.ailmentDatas.Length; i++)
            {
                AilmentData ailment = StaticAilmentData.ailmentDatas[i];
                if (!resistDic.ContainsKey((eActionType)ailment.ailment_action))
                {
                    resistDic.Add((eActionType)ailment.ailment_action, new Dictionary<int, int>());
                }
                resistDic[(eActionType)ailment.ailment_action].Add(ailment.ailment_detail1, unitResistdata.ailments[i]);
            }
            ResistStatusDictionary = resistDic;

        }*/

        public float GetResistProb(eActionType _actionType, int _detail1)
        {

            if (!abnormalResistStatusDictionary.ContainsKey(_actionType))
            {
                return 0;
            }
            Dictionary<int, int> dictionary = abnormalResistStatusDictionary[_actionType];
            if (dictionary.ContainsKey(-1))
                _detail1 = -1;
            if (dictionary.ContainsKey(_detail1))
                return dictionary[_detail1] / 100f;
            return 0;
        }
        public bool ResistStatus(
      eActionType _actionType,
      int _detail1,
      UnitCtrl _source,
      bool _last,
      bool _targetOneParts,
      BasePartsData _target)
        {
            if (!abnormalResistStatusDictionary.ContainsKey(_actionType))
            {
                return false;
            }
            Dictionary<int, int> dictionary = abnormalResistStatusDictionary[_actionType];
            if (dictionary.ContainsKey(-1))
                _detail1 = -1;
            if (!dictionary.ContainsKey(_detail1) || BattleManager.Random(0.0f, 1f, 
                    new RandomData(_source,_target.Owner, (int)_actionType, 9, dictionary[_detail1] / 100.0f)) >= dictionary[_detail1] / 100.0)
                return false;
            if (_last)
            {
                if (_targetOneParts)
                    Owner.SetMissAtk(_source, eMissLogType.DODGE_ATTACK, _parts: _target);
                else
                    Owner.SetMissAtk(_source, eMissLogType.DODGE_ATTACK);
            }
            return true;
        }
        public int GetDebuffResistPercent(UnitCtrl.BuffParamKind _buffParamKind)
        {
            debuffResistStatusDictionary.TryGetValue(_buffParamKind, out var value);
            return value;
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

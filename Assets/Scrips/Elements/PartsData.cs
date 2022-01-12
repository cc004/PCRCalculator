// Decompiled with JetBrains decompiler
// Type: Elements.PartsData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using CodeStage.AntiCheat.ObscuredTypes;
using Cute;
using Elements.Battle;
//using Elements.Data;
using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [Serializable]
    public class PartsData : BasePartsData, ISingletonField
    {
        private const string CENER_BONE = "Center_{0:D2}";
        private const string STATE_BONE = "State_{0:D2}";
        //private BattleEffectPoolInterface battleEffectPool;

        public Action OnBreak { get; set; }

        public Action OnBreakEnd { get; set; }

        public bool IsBreak { get; set; }

        public UnitCtrl BreakSource { get; set; }

        //public MultiTargetCursor MultiTargetCursor { private get; set; }

        private Bone centerBone { get; set; }

        private Bone stateBone { get; set; }

        private Vector3 fixedCenterPos { get; set; }

        public int Level { get; protected set; }

        public SumFloatWithEx Atk { get; protected set; }

        public SumFloatWithEx MagicStr { get; protected set; }

        public ObscuredInt Def { get; protected set; }

        public ObscuredInt MagicDef { get; protected set; }

        public ObscuredInt Dodge { get; protected set; }

        public ObscuredInt Accuracy { get; protected set; }

        public ObscuredInt PhysicalCritical { get; protected set; }

        public ObscuredInt MagicCritical { get; protected set; }

        public ObscuredInt LifeSteal { get; private set; }

        public ObscuredInt HpRecoveryRate { get; private set; }

        public ObscuredInt StartAtk { get; protected set; }

        public ObscuredInt StartMagicStr { get; protected set; }

        public ObscuredInt StartDef { get; protected set; }

        public ObscuredInt StartMagicDef { get; protected set; }

        public ObscuredInt StartDodge { get; protected set; }

        public ObscuredInt StartPhysicalCritical { get; protected set; }

        public ObscuredInt StartMagicCritical { get; protected set; }

        public ObscuredInt StartLifeSteal { get; private set; }

        public ObscuredInt BreakPoint { get; set; }

        public float BreakTime { get; set; }

        public float RecoverTime = 1.12f;//添加
        public ObscuredInt MaxBreakPoint { get; private set; }

        private Dictionary<UnitCtrl.BuffParamKind, FloatWithEx> additionalBuffDictionary { get; set; } = new Dictionary<UnitCtrl.BuffParamKind, FloatWithEx>();

        private int getAdditionalBuff(UnitCtrl.BuffParamKind _kind)
        {
            FloatWithEx num;
            return (int)(!this.additionalBuffDictionary.TryGetValue(_kind, out num) ? 0 : num);
        }

        public override bool GetTargetable() => !this.IsBreak || this.AttachmentNamePairList.Count <= 0;

        public PartsData() { }// => this.battleEffectPool = (BattleEffectPoolInterface)SingletonTreeTreeFunc.CreateSingletonTree<PartsData>().Get<BattleEffectPool>();

        public void Initialize(MasterEnemyMParts.EnemyMParts _enemyMParts)
        {
            BattleSpineController currentSpineCtrl = this.Owner.GetCurrentSpineCtrl();
            foreach (BasePartsData.AttachmentNamePair attachmentNamePair in this.AttachmentNamePairList)
            {
                BasePartsData.AttachmentNamePair pair = attachmentNamePair;
                pair.TargetIndex = currentSpineCtrl.skeleton.slots.FindIndex((Predicate<Spine.Slot>)(e => e.data.Name == pair.TargetSlotName));
                pair.TargetAttachment = currentSpineCtrl.skeleton.GetAttachment(pair.TargetIndex, pair.TargetAttachmentName);
                int index = currentSpineCtrl.skeleton.slots.FindIndex((Predicate<Spine.Slot>)(e => e.data.Name == pair.AppliedSlotName));
                pair.AppliedAttachment = currentSpineCtrl.skeleton.GetAttachment(index, pair.AppliedAttachmentName);
            }
            string boneName = string.Format("Center_{0:D2}", (object)this.Index);
            this.centerBone = currentSpineCtrl.skeleton.FindBone(boneName);
            this.stateBone = currentSpineCtrl.skeleton.FindBone(string.Format("State_{0:D2}", (object)this.Index));
            this.fixedCenterPos = Vector3.Scale(new Vector3(this.centerBone.worldX, this.centerBone.worldY, 0.0f), this.Owner.UnitSpineCtrl.transform.lossyScale);
            int unit_id = this.EnemyId;
            if (_enemyMParts != null)
            {
                switch (this.Index)
                {
                    case 1:
                        unit_id = (int)_enemyMParts.child_enemy_parameter_1;
                        break;
                    case 2:
                        unit_id = (int)_enemyMParts.child_enemy_parameter_2;
                        break;
                    case 3:
                        unit_id = (int)_enemyMParts.child_enemy_parameter_3;
                        break;
                    case 4:
                        unit_id = (int)_enemyMParts.child_enemy_parameter_4;
                        break;
                    case 5:
                        unit_id = (int)_enemyMParts.child_enemy_parameter_5;
                        break;
                }
            }
            // MasterEnemyParameter.EnemyParameter fromAllKind = ManagerSingleton<MasterDataManager>.Instance.masterEnemyParameter.GetFromAllKind(unit_id);
            var data = MyGameCtrl.Instance.tempData.MPartsDataDic[unit_id]; 
            this.Dodge = this.StartDodge = (ObscuredInt)data.baseData.Dodge;
            this.LifeSteal = this.StartLifeSteal = (ObscuredInt)data.baseData.Life_steal;
            this.HpRecoveryRate = (ObscuredInt)data.baseData.Hp_recovery_rate;
            this.InitializeResistStatus((int)data.resist_status_id);
            /*if (Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA.CLCOKFOINCL == eBattleCategory.CLAN_BATTLE)
            {
              ClanBattleTopInfo clanBattleTopInfo = Singleton<ClanBattleTempData>.Instance.ClanBattleTopInfo;
              int clanBattleId = clanBattleTopInfo.ClanBattleId;
              int lapNum = clanBattleTopInfo.LapNum;
              this.Atk = this.StartAtk = (ObscuredInt) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.atk, eEnemyAdjustParamType.ATK);
              this.Def = this.StartDef = (ObscuredInt) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.def, eEnemyAdjustParamType.DEF);
              this.MagicStr = this.StartMagicStr = (ObscuredInt) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.magic_str, eEnemyAdjustParamType.MAGIC_ATK);
              this.MagicDef = this.StartMagicDef = (ObscuredInt) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.magic_def, eEnemyAdjustParamType.MAGIC_DEF);
              this.PhysicalCritical = this.StartPhysicalCritical = (ObscuredInt) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.physical_critical, eEnemyAdjustParamType.PHYSICAL_CRITICAL);
              this.MagicCritical = this.StartMagicCritical = (ObscuredInt) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.magic_critical, eEnemyAdjustParamType.MAGIC_CRITICAL);
              this.Accuracy = (ObscuredInt) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.accuracy, eEnemyAdjustParamType.ACCURACY);
              this.Level = (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.level, eEnemyAdjustParamType.LEVEL);
              this.BreakPoint = this.MaxBreakPoint = (ObscuredInt) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.hp, eEnemyAdjustParamType.HP);
            }
            else
            {*/
            this.Atk = (FloatWithEx)(int)(this.StartAtk = (ObscuredInt)data.baseData.Atk);
            this.Def = this.StartDef = (ObscuredInt)data.baseData.Def;
            this.MagicStr = (FloatWithEx)(int)(this.StartMagicStr = (ObscuredInt)data.baseData.Magic_str);
            this.MagicDef = this.StartMagicDef = (ObscuredInt)data.baseData.Magic_def;
            this.PhysicalCritical = this.StartPhysicalCritical = (ObscuredInt)data.baseData.Physical_critical;
            this.MagicCritical = this.StartMagicCritical = (ObscuredInt)data.baseData.Magic_critical;
            this.Accuracy = (ObscuredInt)data.baseData.Accuracy;
            this.Level = (int)data.level;
            this.BreakPoint = this.MaxBreakPoint = (ObscuredInt)data.baseData.Hp;
            //}
        }

        public override Bone GetStateBone() => this.stateBone;

        public override Bone GetCenterBone() => this.centerBone;

        public override Vector3 GetBottomTransformPosition() => this.Owner.BottomTransform.position + new Vector3(this.PositionX / 540f, 0.0f);

        public override Vector3 GetFixedCenterPos()
        {
            bool flag = this.Owner.IsLeftDir || this.Owner.IsForceLeftDirOrPartsBoss;
            float num = this.InitialPositionX / 540f;
            return new Vector3(flag ? -this.fixedCenterPos.x : this.fixedCenterPos.x, this.fixedCenterPos.y, this.fixedCenterPos.z) + new Vector3(flag ? -num : num, 0.0f);
        }

        public override Vector3 GetPosition() => this.Owner.transform.position + new Vector3(this.PositionX / 540f, 0.0f);

        public override Vector3 GetLocalPosition() => this.Owner.transform.localPosition + new Vector3(this.PositionX, 0.0f, 0.0f);

        public override float GetBodyWidth() => this.BodyWidthValue;

        public override Vector3 GetColliderCenter() => new Vector3((this.Owner.IsLeftDir ? 1 : (this.Owner.IsForceLeftDirOrPartsBoss ? 1 : 0)) != 0 ? -this.fixedCenterPos.x : this.fixedCenterPos.x, this.fixedCenterPos.y, this.fixedCenterPos.z);

        public override Vector3 GetColliderSize() => new Vector3(this.BodyWidthValue / 540f, 0.0f, 0.0f);

        public override int GetDefZero() => Mathf.Max(0, (int)this.Def) + this.getAdditionalBuff(UnitCtrl.BuffParamKind.DEF);

        public override int GetMagicDefZero() => Mathf.Max(0, (int)this.MagicDef) + this.getAdditionalBuff(UnitCtrl.BuffParamKind.MAGIC_DEF);

        private int getDodgeZero() => Mathf.Max(0, (int)this.Dodge) + this.getAdditionalBuff(UnitCtrl.BuffParamKind.DODGE);

        public override int GetAtkZero() => Mathf.Max(0, (int)(FloatWithEx)this.Atk) + this.getAdditionalBuff(UnitCtrl.BuffParamKind.ATK);

        public override int GetMagicStrZero() => Mathf.Max(0, (int)(FloatWithEx)this.MagicStr) + this.getAdditionalBuff(UnitCtrl.BuffParamKind.MAGIC_STR);

        public override int GetAccuracyZero() => Mathf.Max(0, (int)this.Accuracy) + this.getAdditionalBuff(UnitCtrl.BuffParamKind.ACCURACY);

        public override int GetPhysicalCriticalZero() => Mathf.Max(0, (int)this.PhysicalCritical) + this.getAdditionalBuff(UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL);

        public override int GetMagicCriticalZero() => Mathf.Max(0, (int)this.MagicCritical) + this.getAdditionalBuff(UnitCtrl.BuffParamKind.MAGIC_CRITICAL);

        public override int GetLifeStealZero() => Mathf.Max(0, (int)this.LifeSteal) + this.getAdditionalBuff(UnitCtrl.BuffParamKind.LIFE_STEAL);

        public override float GetHpRecoverRateZero() => (float)Mathf.Max(0, (int)this.HpRecoveryRate);

        public override int GetLevel() => this.Level;

        public override float GetDodgeRate(int _accuracy)
        {
            int num = Mathf.Max(this.getDodgeZero() - _accuracy, 0);
            return (float)num / ((float)num + 100f);
        }

        public override int GetStartAtk() => (int)this.StartAtk;

        public override int GetStartDef() => (int)this.StartDef;

        public override int GetStartMagicStr() => (int)this.StartMagicStr;

        public override int GetStartMagicDef() => (int)this.StartMagicDef;

        public override int GetStartDodge() => (int)this.StartDodge;

        public override int GetStartPhysicalCritical() => (int)this.StartPhysicalCritical;

        public override int GetStartMagicCritical() => (int)this.StartMagicCritical;

        public override int GetStartLifeSteal() => (int)this.StartLifeSteal;

        public override void SetMissAtk(
          UnitCtrl _source,
          eMissLogType _missLogType,
          eDamageEffectType _damageEffectType,
          float _scale) => this.Owner.SetMissAtk(_source, _missLogType, _damageEffectType, (BasePartsData)this, _scale);

        public override void SetBuffDebuff(
          bool _enable,
          FloatWithEx _value,
          UnitCtrl.BuffParamKind _kind,
          UnitCtrl _source,
          BattleLogIntreface _battleLog,
          bool _additional, int hash)
        {
            if (!_enable)
                _value = _value * -1f;
            if (_additional)
            {
                if (this.additionalBuffDictionary.ContainsKey(_kind))
                    this.additionalBuffDictionary[_kind] += _value;
                else
                    this.additionalBuffDictionary.Add(_kind, _value);
            }
            else
            {
                BattleLogIntreface mlegmhaocon = _battleLog;
                UnitCtrl unitCtrl = _source;
                UnitCtrl owner = this.Owner;
                int HLIKLPNIOKJ = (int)((_enable ? 1 : 2) * 10 + _kind);
                long KGNFLOPBOMB = (long)_value;
                UnitCtrl JELADBAMFKH = unitCtrl;
                UnitCtrl LIMEKPEENOB = owner;
                mlegmhaocon.AppendBattleLog(eBattleLogType.SET_BUFF_DEBUFF, HLIKLPNIOKJ, KGNFLOPBOMB, 0L, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
                switch (_kind)
                {
                    case UnitCtrl.BuffParamKind.ATK:
                        this.Atk = this.Atk.Sum(hash, _value);
                        break;
                    case UnitCtrl.BuffParamKind.DEF:
                        this.Def = (int)((int)this.Def + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_STR:
                        this.MagicStr = this.MagicStr.Sum(hash, _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_DEF:
                        this.MagicDef = (int)((int)this.MagicDef + _value);
                        break;
                    case UnitCtrl.BuffParamKind.DODGE:
                        this.Dodge = (int)((int)this.Dodge + _value);
                        break;
                    case UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL:
                        this.PhysicalCritical = (int)((int)this.PhysicalCritical + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_CRITICAL:
                        this.MagicCritical = (int)((int)this.MagicCritical + _value);
                        break;
                    case UnitCtrl.BuffParamKind.LIFE_STEAL:
                        this.LifeSteal = (int)((int)this.LifeSteal + _value);
                        break;
                    case UnitCtrl.BuffParamKind.ACCURACY:
                        this.Accuracy = (int)((int)this.Accuracy + _value);
                        break;
                }
            }
        }

        /*public IEnumerator TrackBottom()
        {
            PartsData partsData = this;
            while (true)
            {
                if ((UnityEngine.Object)partsData.MultiTargetCursor != (UnityEngine.Object)null && (UnityEngine.Object)partsData.MultiTargetCursor.Panel != (UnityEngine.Object)null)
                {
                    if ((int)partsData.BreakPoint == 0 && partsData.IsBreak)
                        partsData.MultiTargetCursor.RedUi.alpha = 0.0f;
                    else
                        partsData.MultiTargetCursor.RedUi.alpha = Mathf.Lerp(1f, 0.0f, (float)(int)partsData.BreakPoint / (float)(int)partsData.MaxBreakPoint);
                }
                if (!partsData.Owner.IsFront)
                    partsData.MultiTargetCursor.Panel.sortingOrder = partsData.Owner.GetCurrentSpineCtrl().Depth + (partsData.Owner.UseTargetCursorOver ? 100 : -100);
                partsData.MultiTargetCursor.Panel.transform.position = partsData.GetBottomTransformPosition() + new Vector3(0.0f, partsData.PositionY, 0.0f);
                yield return (object)null;
            }
        }*/

        public void SetDamage(int _damage, UnitCtrl _source)
        {
          if (this.Owner.IsAbnormalState(UnitCtrl.eAbnormalState.PARTS_NO_DAMAGE))
            return;
          this.BreakPoint = (ObscuredInt) ((int) this.BreakPoint - _damage);
          if ((int) this.BreakPoint < 0)
          {
            this.BreakPoint = (ObscuredInt) 0;
            this.BreakSource = _source;
          }
          /*if (!((UnityEngine.Object) this.MultiTargetCursor != (UnityEngine.Object) null))
            return;
          if ((int) this.BreakPoint == 0 && this.IsBreak)
            this.MultiTargetCursor.RedUi.alpha = 0.0f;
          else
            this.MultiTargetCursor.RedUi.alpha = Mathf.Lerp(1f, 0.0f, (float) (int) this.BreakPoint / (float) (int) this.MaxBreakPoint);*/
        }

        public void SetBreak(bool _enable, Transform _unitUiCtrl)
        {
            //if (!((UnityEngine.Object) this.MultiTargetCursor != (UnityEngine.Object) null))
            //  return;
            if (_enable)
            {
              if (!this.Owner.BossPartsListForBattle.Exists((Predicate<PartsData>) (e => !e.IsBreak)))
                this.Owner.OnBreakAll.Call();
              //ManagerSingleton<ResourceManager>.Instance.InstantiateAndGetComponent<Animator>(eResourceId.ANIM_MULTI_TARGET_BREAK, _unitUiCtrl).transform.position = this.GetBottomTransformPosition() + this.GetFixedCenterPos();
              //this.MultiTargetCursor.RedUi.alpha = 0.0f;
              //this.MultiTargetCursor.Animator.Play("BattleMultiTargetsCursor_breaked");
              //ManagerSingleton<SoundManager>.Instance.PlaySeByOuterSource(this.MultiTargetCursor.SeSource, eSE.MULTI_TARGETS_BREAK);
              //this.createBreakEffect(0);
              this.Owner.AppendCoroutine(this.updateChangeAttachment(this.ChangeAttachmentStartTime, true), ePauseType.SYSTEM, this.Owner);
              if (this.AttachmentNamePairList.Count <= 0)
                return;
              //this.MultiTargetCursor.gameObject.SetActive(false);
            }
            else
            {
              //ManagerSingleton<SoundManager>.Instance.PlaySeByOuterSource(this.MultiTargetCursor.SeSource, eSE.MULTI_TARGETS_REVIVAL);
              //this.MultiTargetCursor.Animator.Play("BattleMultiTargetsCursor_breakEnd");
              this.Owner.AppendCoroutine(this.waitAndBreakPointReset(), ePauseType.SYSTEM);
              //this.createBreakEffect(1);
              if (this.AttachmentNamePairList.Count <= 0)
                return;
              //this.MultiTargetCursor.gameObject.SetActive(true);
            }
        }

        /*private void createBreakEffect(int _targetMotion)
        {
          for (int index = 0; index < this.BreakEffectList.Count; ++index)
          {
            if (_targetMotion == this.BreakEffectList[index].TargetMotionIndex)
            {
              SkillEffectCtrl effect = this.battleEffectPool.GetEffect(this.Owner.IsLeftDir ? this.BreakEffectList[index].PrefabLeft : this.BreakEffectList[index].Prefab);
              effect.transform.parent = ExceptNGUIRoot.Transform;
              effect.InitializeSort();
              effect.PlaySe(this.Owner.SoundUnitId, this.Owner.IsLeftDir);
              effect.SetPossitionAppearanceType(this.BreakEffectList[index], this.Owner.GetFirstParts(true), this.Owner, (Skill) null);
              effect.ExecAppendCoroutine(this.Owner);
            }
          }
        }*/

        private IEnumerator updateChangeAttachment(float _timer, bool _enable)
        {
            PartsData partsData = this;
            while (true)
            {
                _timer -= partsData.Owner.DeltaTimeForPause;
                if ((double)_timer >= 0.0)
                {
                    if (partsData.IsBreak == _enable)
                        yield return (object)null;
                    else
                        break;
                }
                else
                    goto label_5;
            }
            yield break;
        label_5:
            BattleSpineController currentSpineCtrl = partsData.Owner.GetCurrentSpineCtrl();
            if (_enable)
            {
                foreach (BasePartsData.AttachmentNamePair attachmentNamePair in partsData.AttachmentNamePairList)
                {
                    (currentSpineCtrl.skeleton.skin == null ? currentSpineCtrl.skeleton.data.defaultSkin : currentSpineCtrl.skeleton.skin).AddAttachment(attachmentNamePair.TargetIndex, attachmentNamePair.TargetAttachmentName, attachmentNamePair.AppliedAttachment);
                    currentSpineCtrl.skeleton.slots.Items[attachmentNamePair.TargetIndex].attachment = attachmentNamePair.AppliedAttachment;
                }
            }
            else
            {
                foreach (BasePartsData.AttachmentNamePair attachmentNamePair in partsData.AttachmentNamePairList)
                {
                    (currentSpineCtrl.skeleton.skin == null ? currentSpineCtrl.skeleton.data.defaultSkin : currentSpineCtrl.skeleton.skin).AddAttachment(attachmentNamePair.TargetIndex, attachmentNamePair.TargetAttachmentName, attachmentNamePair.TargetAttachment);
                    currentSpineCtrl.skeleton.slots.Items[attachmentNamePair.TargetIndex].attachment = attachmentNamePair.TargetAttachment;
                }
            }
        }

        public void FixAttachment(UnitCtrl _owner)
        {
            BattleSpineController currentSpineCtrl = _owner.GetCurrentSpineCtrl();
            foreach (BasePartsData.AttachmentNamePair attachmentNamePair in this.AttachmentNamePairList)
            {
                (currentSpineCtrl.skeleton.skin == null ? currentSpineCtrl.skeleton.data.defaultSkin : currentSpineCtrl.skeleton.skin).AddAttachment(attachmentNamePair.TargetIndex, attachmentNamePair.TargetAttachmentName, attachmentNamePair.TargetAttachment);
                currentSpineCtrl.skeleton.slots.Items[attachmentNamePair.TargetIndex].attachment = attachmentNamePair.TargetAttachment;
            }
        }

        public void DisableCursor() { }// => this.MultiTargetCursor.SetActive(false);

        private IEnumerator waitAndBreakPointReset()
        {
            PartsData partsData = this;
            //float time = 1.12f;
            /*while ((double)time > 0.0)
            {
                time -= partsData.battleManager.DeltaTime_60fps;
                yield return (object)null;
            }*/
            RecoverTime = 1.12f;
            while ((double)RecoverTime > 0.0)
            {
                RecoverTime -= partsData.battleManager.DeltaTime_60fps;
                yield return (object)null;
            }
            RecoverTime = 1.12f;
            partsData.IsBreak = false;
            partsData.Owner.AppendCoroutine(partsData.updateChangeAttachment(partsData.ChangeAttachmentEndTime, false), ePauseType.SYSTEM, partsData.Owner);
            partsData.BreakPoint = partsData.MaxBreakPoint;
            partsData.OnBreakEnd.Call();
        }
    }
}

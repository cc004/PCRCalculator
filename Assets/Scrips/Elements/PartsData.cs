// Decompiled with JetBrains decompiler
// Type: Elements.PartsData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;

using Cute;
using Elements.Battle;
using Spine;
using UnityEngine;
//using Elements.Data;

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

        public FloatWithEx Atk { get; protected set; }

        public FloatWithEx MagicStr { get; protected set; }

        public int Def { get; protected set; }

        public int MagicDef { get; protected set; }

        public int Dodge { get; protected set; }

        public int Accuracy { get; protected set; }

        public int PhysicalCritical { get; protected set; }

        public int MagicCritical { get; protected set; }

        public int LifeSteal { get; private set; }

        public int HpRecoveryRate { get; private set; }

        public int StartAtk { get; protected set; }

        public int StartMagicStr { get; protected set; }

        public int StartDef { get; protected set; }

        public int StartMagicDef { get; protected set; }

        public int StartDodge { get; protected set; }

        public int StartPhysicalCritical { get; protected set; }

        public int StartMagicCritical { get; protected set; }

        public int StartLifeSteal { get; private set; }

        public int BreakPoint { get; set; }

        public float BreakTime { get; set; }

        public float RecoverTime = 1.12f;//添加
        public int MaxBreakPoint { get; private set; }

        private Dictionary<UnitCtrl.BuffParamKind, FloatWithEx> additionalBuffDictionary { get; set; } = new Dictionary<UnitCtrl.BuffParamKind, FloatWithEx>();

        private int getAdditionalBuff(UnitCtrl.BuffParamKind _kind)
        {
            FloatWithEx num;
            return (int)(!additionalBuffDictionary.TryGetValue(_kind, out num) ? 0 : num);
        }

        private FloatWithEx getAdditionalBuffEx(UnitCtrl.BuffParamKind _kind)
        {
            FloatWithEx num;
            return (!additionalBuffDictionary.TryGetValue(_kind, out num) ? 0 : num);
        }

        public override bool GetTargetable() => !IsBreak || AttachmentNamePairList.Count <= 0;

        // => this.battleEffectPool = (BattleEffectPoolInterface)SingletonTreeTreeFunc.CreateSingletonTree<PartsData>().Get<BattleEffectPool>();

        public void Initialize(MasterEnemyMParts.EnemyMParts _enemyMParts)
        {
            BattleSpineController currentSpineCtrl = Owner.GetCurrentSpineCtrl();
            foreach (AttachmentNamePair attachmentNamePair in AttachmentNamePairList)
            {
                AttachmentNamePair pair = attachmentNamePair;
                pair.TargetIndex = currentSpineCtrl.skeleton.slots.FindIndex(e => e.data.Name == pair.TargetSlotName);
                pair.TargetAttachment = currentSpineCtrl.skeleton.GetAttachment(pair.TargetIndex, pair.TargetAttachmentName);
                int index = currentSpineCtrl.skeleton.slots.FindIndex(e => e.data.Name == pair.AppliedSlotName);
                pair.AppliedAttachment = currentSpineCtrl.skeleton.GetAttachment(index, pair.AppliedAttachmentName);
            }
            string boneName = string.Format("Center_{0:D2}", Index);
            centerBone = currentSpineCtrl.skeleton.FindBone(boneName);
            stateBone = currentSpineCtrl.skeleton.FindBone(string.Format("State_{0:D2}", Index));
            fixedCenterPos = Vector3.Scale(new Vector3(centerBone.worldX, centerBone.worldY, 0.0f), Owner.UnitSpineCtrl.transform.lossyScale);
            int unit_id = EnemyId;
            if (_enemyMParts != null)
            {
                switch (Index)
                {
                    case 1:
                        unit_id = _enemyMParts.child_enemy_parameter_1;
                        break;
                    case 2:
                        unit_id = _enemyMParts.child_enemy_parameter_2;
                        break;
                    case 3:
                        unit_id = _enemyMParts.child_enemy_parameter_3;
                        break;
                    case 4:
                        unit_id = _enemyMParts.child_enemy_parameter_4;
                        break;
                    case 5:
                        unit_id = _enemyMParts.child_enemy_parameter_5;
                        break;
                }
            }
            // MasterEnemyParameter.EnemyParameter fromAllKind = ManagerSingleton<MasterDataManager>.Instance.masterEnemyParameter.GetFromAllKind(unit_id);
            var data = MyGameCtrl.Instance.tempData.MPartsDataDic[unit_id]; 
            Dodge = StartDodge = (int)data.baseData.Dodge;
            LifeSteal = StartLifeSteal = (int)data.baseData.Life_steal;
            HpRecoveryRate = (int)data.baseData.Hp_recovery_rate;
            InitializeResistStatus(data.resist_status_id);
            /*if (Singleton<EHPLBCOOOPK>.Instance.PHDACAOAOMA.CLCOKFOINCL == eBattleCategory.CLAN_BATTLE)
            {
              ClanBattleTopInfo clanBattleTopInfo = Singleton<ClanBattleTempData>.Instance.ClanBattleTopInfo;
              int clanBattleId = clanBattleTopInfo.ClanBattleId;
              int lapNum = clanBattleTopInfo.LapNum;
              this.Atk = this.StartAtk = (int) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.atk, eEnemyAdjustParamType.ATK);
              this.Def = this.StartDef = (int) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.def, eEnemyAdjustParamType.DEF);
              this.MagicStr = this.StartMagicStr = (int) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.magic_str, eEnemyAdjustParamType.MAGIC_ATK);
              this.MagicDef = this.StartMagicDef = (int) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.magic_def, eEnemyAdjustParamType.MAGIC_DEF);
              this.PhysicalCritical = this.StartPhysicalCritical = (int) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.physical_critical, eEnemyAdjustParamType.PHYSICAL_CRITICAL);
              this.MagicCritical = this.StartMagicCritical = (int) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.magic_critical, eEnemyAdjustParamType.MAGIC_CRITICAL);
              this.Accuracy = (int) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.accuracy, eEnemyAdjustParamType.ACCURACY);
              this.Level = (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.level, eEnemyAdjustParamType.LEVEL);
              this.BreakPoint = this.MaxBreakPoint = (int) (int) ClanBattleUtil.CalcParamAdjustStatus(clanBattleId, lapNum, (long) (int) fromAllKind.hp, eEnemyAdjustParamType.HP);
            }
            else
            {*/
            Atk = (int)(StartAtk = (int)data.baseData.Atk);
            Def = StartDef = (int)data.baseData.Def;
            MagicStr = (int)(StartMagicStr = (int)data.baseData.Magic_str);
            MagicDef = StartMagicDef = (int)data.baseData.Magic_def;
            PhysicalCritical = StartPhysicalCritical = (int)data.baseData.Physical_critical;
            MagicCritical = StartMagicCritical = (int)data.baseData.Magic_critical;
            Accuracy = (int)data.baseData.Accuracy;
            Level = data.level;
            BreakPoint = MaxBreakPoint = (int)data.baseData.Hp;
            //}
        }

        public override Bone GetStateBone() => stateBone;

        public override Bone GetCenterBone() => centerBone;

        public override Vector3 GetBottomTransformPosition() => Owner.BottomTransform.position + new Vector3(PositionX / 540f, 0.0f);

        public override Vector3 GetFixedCenterPos()
        {
            bool flag = Owner.IsLeftDir || Owner.IsForceLeftDirOrPartsBoss;
            float num = InitialPositionX / 540f;
            return new Vector3(flag ? -fixedCenterPos.x : fixedCenterPos.x, fixedCenterPos.y, fixedCenterPos.z) + new Vector3(flag ? -num : num, 0.0f);
        }

        public override Vector3 GetPosition() => Owner.transform.position + new Vector3(PositionX / 540f, 0.0f);

        public override Vector3 GetLocalPosition() => Owner.transform.localPosition + new Vector3(PositionX, 0.0f, 0.0f);

        public override float GetBodyWidth() => BodyWidthValue;

        public override Vector3 GetColliderCenter() => new Vector3((Owner.IsLeftDir ? 1 : (Owner.IsForceLeftDirOrPartsBoss ? 1 : 0)) != 0 ? -fixedCenterPos.x : fixedCenterPos.x, fixedCenterPos.y, fixedCenterPos.z);

        public override Vector3 GetColliderSize() => new Vector3(BodyWidthValue / 540f, 0.0f, 0.0f);

        public override int GetDefZero() => Mathf.Max(0, Def) + getAdditionalBuff(UnitCtrl.BuffParamKind.DEF);

        public override int GetMagicDefZero() => Mathf.Max(0, MagicDef) + getAdditionalBuff(UnitCtrl.BuffParamKind.MAGIC_DEF);

        private int getDodgeZero() => Mathf.Max(0, Dodge) + getAdditionalBuff(UnitCtrl.BuffParamKind.DODGE);

        public override int GetAtkZero() => Mathf.Max(0, (int)Atk) + getAdditionalBuff(UnitCtrl.BuffParamKind.ATK);

        public override int GetMagicStrZero() => Mathf.Max(0, (int)MagicStr) + getAdditionalBuff(UnitCtrl.BuffParamKind.MAGIC_STR);

        public override FloatWithEx GetAtkZeroEx() => Atk.Max(0) + getAdditionalBuffEx(UnitCtrl.BuffParamKind.ATK);

        public override FloatWithEx GetMagicStrZeroEx() => MagicStr.Max(0) + getAdditionalBuffEx(UnitCtrl.BuffParamKind.MAGIC_STR);

        public override int GetAccuracyZero() => Mathf.Max(0, Accuracy) + getAdditionalBuff(UnitCtrl.BuffParamKind.ACCURACY);

        public override int GetPhysicalCriticalZero() => Mathf.Max(0, PhysicalCritical) + getAdditionalBuff(UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL);

        public override int GetMagicCriticalZero() => Mathf.Max(0, MagicCritical) + getAdditionalBuff(UnitCtrl.BuffParamKind.MAGIC_CRITICAL);

        public override int GetLifeStealZero() => Mathf.Max(0, LifeSteal) + getAdditionalBuff(UnitCtrl.BuffParamKind.LIFE_STEAL);

        public override float GetHpRecoverRateZero() => Mathf.Max(0, HpRecoveryRate);

        public override int GetLevel() => Level;

        public override float GetDodgeRate(int _accuracy)
        {
            int num = Mathf.Max(getDodgeZero() - _accuracy, 0);
            return num / (num + 100f);
        }

        public override int GetStartAtk() => StartAtk;

        public override int GetStartDef() => StartDef;

        public override int GetStartMagicStr() => StartMagicStr;

        public override int GetStartMagicDef() => StartMagicDef;

        public override int GetStartDodge() => StartDodge;

        public override int GetStartPhysicalCritical() => StartPhysicalCritical;

        public override int GetStartMagicCritical() => StartMagicCritical;

        public override int GetStartLifeSteal() => StartLifeSteal;

        public override void SetMissAtk(
          UnitCtrl _source,
          eMissLogType _missLogType,
          eDamageEffectType _damageEffectType,
          float _scale) => Owner.SetMissAtk(_source, _missLogType, _damageEffectType, this, _scale);

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
                if (additionalBuffDictionary.ContainsKey(_kind))
                    additionalBuffDictionary[_kind] += _value;
                else
                    additionalBuffDictionary.Add(_kind, _value);
            }
            else
            {
                BattleLogIntreface mlegmhaocon = _battleLog;
                UnitCtrl unitCtrl = _source;
                UnitCtrl owner = Owner;
                int HLIKLPNIOKJ = (int)((_enable ? 1 : 2) * 10 + _kind);
                long KGNFLOPBOMB = (long)_value;
                UnitCtrl JELADBAMFKH = unitCtrl;
                UnitCtrl LIMEKPEENOB = owner;
                mlegmhaocon.AppendBattleLog(eBattleLogType.SET_BUFF_DEBUFF, HLIKLPNIOKJ, KGNFLOPBOMB, 0L, 0, 0, JELADBAMFKH: JELADBAMFKH, LIMEKPEENOB: LIMEKPEENOB);
                switch (_kind)
                {
                    case UnitCtrl.BuffParamKind.ATK:
                        Atk = (int)(Atk + _value);
                        break;
                    case UnitCtrl.BuffParamKind.DEF:
                        Def = (int)((int)Def + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_STR:
                        MagicStr = (int)(MagicStr + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_DEF:
                        MagicDef = (int)((int)MagicDef + _value);
                        break;
                    case UnitCtrl.BuffParamKind.DODGE:
                        Dodge = (int)((int)Dodge + _value);
                        break;
                    case UnitCtrl.BuffParamKind.PHYSICAL_CRITICAL:
                        PhysicalCritical = (int)((int)PhysicalCritical + _value);
                        break;
                    case UnitCtrl.BuffParamKind.MAGIC_CRITICAL:
                        MagicCritical = (int)((int)MagicCritical + _value);
                        break;
                    case UnitCtrl.BuffParamKind.LIFE_STEAL:
                        LifeSteal = (int)((int)LifeSteal + _value);
                        break;
                    case UnitCtrl.BuffParamKind.ACCURACY:
                        Accuracy = (int)((int)Accuracy + _value);
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
          if (Owner.IsAbnormalState(UnitCtrl.eAbnormalState.PARTS_NO_DAMAGE))
            return;
          BreakPoint = BreakPoint - _damage;
          if (BreakPoint < 0)
          {
            BreakPoint = 0;
            BreakSource = _source;
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
              if (!Owner.BossPartsListForBattle.Exists(e => !e.IsBreak))
                Owner.OnBreakAll.Call();
              //ManagerSingleton<ResourceManager>.Instance.InstantiateAndGetComponent<Animator>(eResourceId.ANIM_MULTI_TARGET_BREAK, _unitUiCtrl).transform.position = this.GetBottomTransformPosition() + this.GetFixedCenterPos();
              //this.MultiTargetCursor.RedUi.alpha = 0.0f;
              //this.MultiTargetCursor.Animator.Play("BattleMultiTargetsCursor_breaked");
              //ManagerSingleton<SoundManager>.Instance.PlaySeByOuterSource(this.MultiTargetCursor.SeSource, eSE.MULTI_TARGETS_BREAK);
              //this.createBreakEffect(0);
              Owner.AppendCoroutine(updateChangeAttachment(ChangeAttachmentStartTime, true), ePauseType.SYSTEM, Owner);
              if (AttachmentNamePairList.Count <= 0)
                return;
              //this.MultiTargetCursor.gameObject.SetActive(false);
            }
            else
            {
              //ManagerSingleton<SoundManager>.Instance.PlaySeByOuterSource(this.MultiTargetCursor.SeSource, eSE.MULTI_TARGETS_REVIVAL);
              //this.MultiTargetCursor.Animator.Play("BattleMultiTargetsCursor_breakEnd");
              Owner.AppendCoroutine(waitAndBreakPointReset(), ePauseType.SYSTEM);
              //this.createBreakEffect(1);
              if (AttachmentNamePairList.Count <= 0)
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
                if (_timer >= 0.0)
                {
                    if (partsData.IsBreak == _enable)
                        yield return null;
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
                foreach (AttachmentNamePair attachmentNamePair in partsData.AttachmentNamePairList)
                {
                    (currentSpineCtrl.skeleton.skin == null ? currentSpineCtrl.skeleton.data.defaultSkin : currentSpineCtrl.skeleton.skin).AddAttachment(attachmentNamePair.TargetIndex, attachmentNamePair.TargetAttachmentName, attachmentNamePair.AppliedAttachment);
                    currentSpineCtrl.skeleton.slots.Items[attachmentNamePair.TargetIndex].attachment = attachmentNamePair.AppliedAttachment;
                }
            }
            else
            {
                foreach (AttachmentNamePair attachmentNamePair in partsData.AttachmentNamePairList)
                {
                    (currentSpineCtrl.skeleton.skin == null ? currentSpineCtrl.skeleton.data.defaultSkin : currentSpineCtrl.skeleton.skin).AddAttachment(attachmentNamePair.TargetIndex, attachmentNamePair.TargetAttachmentName, attachmentNamePair.TargetAttachment);
                    currentSpineCtrl.skeleton.slots.Items[attachmentNamePair.TargetIndex].attachment = attachmentNamePair.TargetAttachment;
                }
            }
        }

        public void FixAttachment(UnitCtrl _owner)
        {
            BattleSpineController currentSpineCtrl = _owner.GetCurrentSpineCtrl();
            foreach (AttachmentNamePair attachmentNamePair in AttachmentNamePairList)
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
            while (RecoverTime > 0.0)
            {
                RecoverTime -= partsData.battleManager.DeltaTime_60fps;
                yield return null;
            }
            RecoverTime = 1.12f;
            partsData.IsBreak = false;
            partsData.Owner.AppendCoroutine(partsData.updateChangeAttachment(partsData.ChangeAttachmentEndTime, false), ePauseType.SYSTEM, partsData.Owner);
            partsData.BreakPoint = partsData.MaxBreakPoint;
            partsData.OnBreakEnd.Call();
        }
    }
}

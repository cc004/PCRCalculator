// Decompiled with JetBrains decompiler
// Type: Elements.Battle.PLFCLLHLDOO
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Elements.Battle
{
    public class AbnormalStateDataBase : ISingletonField
    {
#if ENABLE_THREADING
        private BattleManager staticBattleManager;
#else
        private static BattleManager staticBattleManager;
#endif
        private const float FIELD_INTERVAL = 1f;
        //protected SkillEffectCtrl skillEffect;
        //private static Yggdrasil<AbnormalStateDataBase> staticSingletonTree;
        //private static BattleEffectPoolInterface staticBattleEffectPool;
        private const float BASE_EFFECT_SIZE = 350f;

        public eFieldType FildType { get; set; }

        public bool Enable { get; private set; }

        public eFieldExecType ExecType { get; set; }

        public float StayTime { get; set; }

        public float CenterX { get; set; }

        public float Size { get; set; }


        public float Left, Right;

        public void Cache()
        {
            Left = CenterX - Size;
            Right = CenterX + Size;
        }

        public eFieldTargetType FieldTargetType { get; set; }

        public List<BasePartsData> TargetList { get; set; }
        public HashSet<BasePartsData> TargetSet { get; set; } = new HashSet<BasePartsData>();

        public List<UnitCtrl> AllTargetList { get; set; }

        protected BattleManager BattleManager => staticBattleManager;

        public UnitCtrl PauseTarget { get; set; }
        public Skill Skill { get; set; }

        public UnitCtrl Source { get; set; }

        protected int fieldIndex { get; set; }

        //protected BattleEffectPoolInterface IPEGKPHMBBN => AbnormalStateDataBase.staticBattleEffectPool;

        public GameObject UniqueEffectPrefab { get; set; }

        public GameObject UniqueEffectPrefabLeft { get; set; }

        public Action<string> onExec;

        public int TargetTalentId { get; set; } 

        private bool stopFlag { get; set; }
        public bool IsTargetAbnormalStateBuff { get; set; } // TODO find where set

        /*public static void StaticRelease()
        {
          AbnormalStateDataBase.staticSingletonTree = (Yggdrasil<AbnormalStateDataBase>) null;
          AbnormalStateDataBase.staticBattleManager = (DGDNPNNDBDL) null;
          AbnormalStateDataBase.staticBattleEffectPool = (BattleEffectPoolInterface) null;
        }*/

        public AbnormalStateDataBase()
        {
            //if (AbnormalStateDataBase.staticSingletonTree != null)
            //  return;
            //AbnormalStateDataBase.staticSingletonTree = this.CreateSingletonTree<AbnormalStateDataBase>();
            staticBattleManager = BattleManager.Instance;
            //AbnormalStateDataBase.staticBattleEffectPool = (BattleEffectPoolInterface) AbnormalStateDataBase.staticSingletonTree.Get<BattleEffectPool>();
        }

        public virtual void OnEnter(BasePartsData GEDLBPMPOKB, UnitCtrl _source)
        {
            TargetList.Add(GEDLBPMPOKB);
            TargetSet.Add(GEDLBPMPOKB);
        }

        public virtual void OnExit(BasePartsData GEDLBPMPOKB, UnitCtrl _source)
        {
            TargetList.Remove(GEDLBPMPOKB);
            TargetSet.Remove(GEDLBPMPOKB);
        }

        public virtual void OnRepeat()
        {
        }

        public virtual void StartField()
        {
            //if ((UnityEngine.Object) this.skillEffect != (UnityEngine.Object) null)
            //  this.BGAGEJBMAMH.AppendEffect(this.skillEffect, this.EGEPDDJBILL, false);
            //this.BattleManager.AppendCoroutine(this.Update(), ePauseType.SYSTEM, this.EGEPDDJBILL);
            // fix ub field bug
            BattleManager.AppendCoroutine(Update(), ePauseType.SYSTEM, PauseTarget);
            fieldIndex = ++BattleManager.MCLFFJEFMIF;
        }

        /*protected void initializeSkillEffect()
        {
          this.skillEffect.transform.parent = ExceptNGUIRoot.Transform;
          this.skillEffect.transform.localPosition = new Vector3((this.skillEffect as FieldEffectController).DisplayCenter ? 0.0f : this.FNFIJFAIPNE / 540f, (float) ((5000.0 + (double) this.BGAGEJBMAMH.GetRespawnPos(eUnitRespawnPos.MAIN_POS_3)) / 540.0), 0.0f);
          this.skillEffect.transform.localScale = Vector3.one * this.HIKKPKEKLDA / 350f;
          this.skillEffect.InitializeSort();
          if (this.BGAGEJBMAMH.GetBlackOutUnitLength() > 0)
            this.skillEffect.SetSortOrderFront();
          else
            this.skillEffect.SetSortOrderBack();
        }*/

        public virtual void ResetTarget(UnitCtrl FNHGFDNICFG, UnitCtrl.eAbnormalState GMDFAELOLFL, bool _strict)
        {
        }

        protected virtual int getClearedIndex(UnitCtrl FNHGFDNICFG) => 0;

        protected float remaining;

        private int skillTargetCacheKey = -1;

        public IEnumerator Update()
        {
            float time = 0.0f;
            float intervalCount = 0.0f;
            Enable = true;
            remaining = StayTime;
            //Debug.Log(BattleHeaderController.CurrentFrameCount + "领域开始");

            while (true)
            {
                //if (plfcllhldoo.BattleManager.GetBlackOutUnitLength() == 0 && (UnityEngine.Object) plfcllhldoo.skillEffect != (UnityEngine.Object) null)
                //  plfcllhldoo.skillEffect.SetSortOrderBack();
                if (BattleManager.Instance.PositionChanged(ref skillTargetCacheKey))
                {
                    updateTarget();
                }

                if (ExecType == eFieldExecType.REPEAT)
                {
                    if (BattleManager.GetBlackOutUnitLength() == 0)
                        intervalCount += BattleManager.DeltaTime_60fps;
                    if (intervalCount > 1.0)
                    {
                        intervalCount = 0.0f;
                        OnRepeat();
                    }
                }

                if (BattleManager.GetBlackOutUnitLength() == 0)
                {
                    time += BattleManager.DeltaTime_60fps;
                    remaining = StayTime - time;
                }

                //Debug.Log(BattleHeaderController.CurrentFrameCount + "领域加时"+);
                eBattleGameState mmbmbjnnacg = BattleManager.GameState;
                if (time <= StayTime && mmbmbjnnacg != eBattleGameState.NEXT_WAVE_PROCESS &&
                    (mmbmbjnnacg != eBattleGameState.WAIT_WAVE_END && !stopFlag))
                    yield return null;
                else
                    break;

            }

            for (int index = TargetList.Count - 1; index >= 0; --index)
                OnExit(TargetList[index], Source);
            Enable = false;
            //Debug.Log(BattleHeaderController.CurrentFrameCount + "领域结束!");
            /*if ((UnityEngine.Object) plfcllhldoo.skillEffect != (UnityEngine.Object) null)
            {
              FieldEffectController fieldEffect = plfcllhldoo.skillEffect as FieldEffectController;
              fieldEffect.StopEmitter();
              if ((double) fieldEffect.EndDuration == 0.0)
              {
                plfcllhldoo.skillEffect.SetTimeToDie(true);
              }
              else
              {
                float endDuration = fieldEffect.EndDuration;
                while ((double) endDuration > 0.0)
                {
                  endDuration -= BattleManager.DeltaTime_60fps;
                  yield return (object) null;
                }
                plfcllhldoo.skillEffect.SetTimeToDie(true);
              }
              plfcllhldoo.BattleManager.AppendCoroutine(plfcllhldoo.createPrefabWithTime(fieldEffect.EndEffectPrefab, plfcllhldoo.skillEffect.transform.position), ePauseType.SYSTEM, plfcllhldoo.EGEPDDJBILL);
              fieldEffect = (FieldEffectController) null;
            }*/
        }
        private void Updateb__83_0(BasePartsData target)
        {
            var owner = target.Owner;
            if (TargetSet.Contains(target))
            {
                if (owner.IsSummonOrPhantom && owner.IdleOnly || owner.IsStealth ||
                    getClearedIndex(owner) >= fieldIndex || owner.IsDead)
                {
                    OnExit(target, Source);
                }
                else
                {
                    var posx = target.GetLocalPositionX();
                    if (!(posx <= Right &&
                          posx >= Left || BattleUtil.Approximately(posx, Right) ||
                          BattleUtil.Approximately(posx, Left)))
                    {
                        OnExit(target, Source);
                    }
                }
            }
            else
            {
                if (owner.IsSummonOrPhantom && owner.IdleOnly || owner.IsStealth ||
                    getClearedIndex(owner) >= fieldIndex || owner.IsDead) return;
                var posx = target.GetLocalPositionX();
                if (posx <= Right &&
                    posx >= Left || BattleUtil.Approximately(posx, Right) ||
                    BattleUtil.Approximately(posx, Left))
                {
                    if (TargetTalentId != 0 && !owner.TalentIds.Contains(TargetTalentId))
                        return;
                    if (!judgeWorking(owner))
                        return;
                    OnEnter(target, Source);
                }
            }
            /*
            var contains = TargetSet.Contains(target);
            if (owner.IsStealth)
            {
                if (contains)
                {
                    OnExit(target);
                }
            }
            else if (getClearedIndex(owner) >= fieldIndex)
            {
                if (contains)
                {
                    OnExit(target);
                }
            }
            else if (owner.IsDead)
            {
                if (contains)
                {
                    OnExit(target);
                }
            }
            else
            {
                var posx = target.GetLocalPosition().x;
                bool flag = (((posx <= (CenterX + Size)) &&
                    (posx >= (CenterX - Size))) || BattleUtil.Approximately(posx, CenterX + Size)) || BattleUtil.Approximately(posx, CenterX - Size);
                if (owner.IsSummonOrPhantom && owner.IdleOnly)
                {
                    flag = false;
                }
                if (flag && !TargetList.Contains(target))
                {
                    OnEnter(target);
                }
                else if (!flag && TargetList.Contains(target))
                {
                    OnExit(target);
                }
            }*/
        }

        /*private IEnumerator createPrefabWithTime(
      List<PrefabWithTime> HJLIJBDJHKF,
      Vector3 LDLDLKBNMEF)
    {
      float time = 0.0f;
      bool[] isEffectCreated = new bool[HJLIJBDJHKF.Count];
      int numCreatedEffect = 0;
      while (true)
      {
        for (int index = 0; index < HJLIJBDJHKF.Count; ++index)
        {
          if (!isEffectCreated[index] && (double) time > (double) HJLIJBDJHKF[index].Time)
          {
            this.IPEGKPHMBBN.GetEffect(HJLIJBDJHKF[index].Prefab);
            SkillEffectCtrl effect = this.IPEGKPHMBBN.GetEffect(Singleton<LCEGKJFKOPD>.Instance.MFOOOGCBKGJ);
            effect.transform.parent = ExceptNGUIRoot.Transform;
            effect.transform.position = LDLDLKBNMEF;
            effect.InitializeSort();
            effect.SetSortOrderBack();
            isEffectCreated[index] = true;
            ++numCreatedEffect;
            if (numCreatedEffect >= HJLIJBDJHKF.Count)
              yield break;
          }
        }
        yield return (object) null;
        time += Time.deltaTime;
      }
    }*/

        public void StopField(eTargetAssignment OAHLOGOLMHD, bool MOBKHPNMEDM)
        {
            if (!judgeStopTarget(OAHLOGOLMHD, MOBKHPNMEDM))
                return;
            stopFlag = true;
        }

        protected virtual bool judgeWorking(UnitCtrl _unit)
        {
            return true;
        }

        private bool judgeStopTarget(eTargetAssignment OAHLOGOLMHD, bool MOBKHPNMEDM)
        {
            switch (OAHLOGOLMHD)
            {
                case eTargetAssignment.OTHER_SIDE:
                    return FieldTargetType == eFieldTargetType.PLAYER == MOBKHPNMEDM;
                case eTargetAssignment.OWNER_SITE:
                    return FieldTargetType == eFieldTargetType.PLAYER == !MOBKHPNMEDM;
                case eTargetAssignment.ALL:
                    return true;
                default:
                    return false;
            }
        }

        // RVA: 0x2925DC8 Offset: 0x2925DC8 VA: 0x2925DC8
        protected void updateTarget()
        {
            foreach (var target in AllTargetList)
            {
                if (target.IsPartsBoss)
                {
                    foreach (var part in target.BossPartsListForBattle)
                    {
                        Updateb__83_0(part);
                    }
                }
                else
                {
                    Updateb__83_0(target.GetFirstParts());
                }
            }
        }

    }
}

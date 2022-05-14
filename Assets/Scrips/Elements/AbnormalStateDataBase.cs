// Decompiled with JetBrains decompiler
// Type: Elements.Battle.PLFCLLHLDOO
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements.Battle
{
    public class AbnormalStateDataBase : ISingletonField
    {
        private static BattleManager staticBattleManager;
        private const float FIELD_INTERVAL = 1f;
        //protected SkillEffectCtrl skillEffect;
        //private static Yggdrasil<AbnormalStateDataBase> staticSingletonTree;
        //private static BattleEffectPoolInterface staticBattleEffectPool;
        private const float BASE_EFFECT_SIZE = 350f;

        public eFieldType KNLCAOOKHPP { get; set; }

        public bool isPlaying { get; private set; }

        public eFieldExecType HKDBJHAIOMB { get; set; }

        public float StayTime { get; set; }

        public float CenterX { get; set; }

        public float Size { get; set; }

        public eFieldTargetType LCHLGLAFJED { get; set; }

        public List<BasePartsData> TargetList { get; set; }

        public List<UnitCtrl> ALFDJACNNCL { get; set; }

        protected BattleManager BattleManager => staticBattleManager;

        public UnitCtrl EGEPDDJBILL { get; set; }

        public Skill PMHDBOJMEAD { get; set; }

        public UnitCtrl PPOJKIDHGNJ { get; set; }

        private int fieldIndex { get; set; }

        //protected BattleEffectPoolInterface IPEGKPHMBBN => AbnormalStateDataBase.staticBattleEffectPool;

        public GameObject HGMNJJBLJIO { get; set; }

        public GameObject LALMMFAOJDP { get; set; }

        public Action<string> onExec;
        private bool AHABEPKMKJJ { get; set; }

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

        public virtual void OnEnter(BasePartsData GEDLBPMPOKB) => TargetList.Add(GEDLBPMPOKB);

        public virtual void OnExit(BasePartsData GEDLBPMPOKB) => TargetList.Remove(GEDLBPMPOKB);

        public virtual void OnRepeat()
        {
        }

        public virtual void StartField()
        {
            //if ((UnityEngine.Object) this.skillEffect != (UnityEngine.Object) null)
            //  this.BGAGEJBMAMH.AppendEffect(this.skillEffect, this.EGEPDDJBILL, false);
            //this.BattleManager.AppendCoroutine(this.Update(), ePauseType.SYSTEM, this.EGEPDDJBILL);
            // fix ub field bug
            BattleManager.AppendCoroutine(Update(), ePauseType.SYSTEM, EGEPDDJBILL);
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

        public virtual void ResetTarget(UnitCtrl FNHGFDNICFG, UnitCtrl.eAbnormalState GMDFAELOLFL)
        {
        }

        protected virtual int getClearedIndex(UnitCtrl FNHGFDNICFG) => 0;

        public IEnumerator Update()
        {
            float time = 0.0f;
            float intervalCount = 0.0f;
            isPlaying = true;
            //Debug.Log(BattleHeaderController.CurrentFrameCount + "领域开始");
            while (true)
            {
                //if (plfcllhldoo.BattleManager.GetBlackOutUnitLength() == 0 && (UnityEngine.Object) plfcllhldoo.skillEffect != (UnityEngine.Object) null)
                //  plfcllhldoo.skillEffect.SetSortOrderBack();
                int index1 = 0;
                for (int count = ALFDJACNNCL.Count; index1 < count; ++index1)
                {
                    // ISSUE: reference to a compiler-generated method
                    Action<BasePartsData> action = Updateb__83_0;
                    UnitCtrl unitCtrl = ALFDJACNNCL[index1];
                    if (!unitCtrl.IsPartsBoss)
                    {
                        action(unitCtrl.GetFirstParts());
                    }
                    else
                    {
                        for (int index2 = 0; index2 < unitCtrl.BossPartsListForBattle.Count; ++index2)
                            action(unitCtrl.BossPartsListForBattle[index2]);
                    }
                }
                if (HKDBJHAIOMB == eFieldExecType.REPEAT)
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
                }
                //Debug.Log(BattleHeaderController.CurrentFrameCount + "领域加时"+);
                eBattleGameState mmbmbjnnacg = BattleManager.GameState;
                if (time <= StayTime && mmbmbjnnacg != eBattleGameState.NEXT_WAVE_PROCESS && (mmbmbjnnacg != eBattleGameState.WAIT_WAVE_END && !AHABEPKMKJJ))
                    yield return null;
                else
                    break;
            }
            for (int index = TargetList.Count - 1; index >= 0; --index)
                OnExit(TargetList[index]);
            isPlaying = false;
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
                  endDuration -= plfcllhldoo.BattleManager.DeltaTime_60fps;
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
            if (target.Owner.IsStealth)
            {
                if (TargetList.Contains(target))
                {
                    OnExit(target);
                }
            }
            else if (getClearedIndex(target.Owner) >= fieldIndex)
            {
                if (TargetList.Contains(target))
                {
                    OnExit(target);
                }
            }
            else if (target.Owner.IsDead)
            {
                if (TargetList.Contains(target))
                {
                    OnExit(target);
                }
            }
            else
            {
                bool flag = (((target.GetLocalPosition().x <= (CenterX + Size)) && (target.GetLocalPosition().x >= (CenterX - Size))) || BattleUtil.Approximately(target.GetLocalPosition().x, CenterX + Size)) || BattleUtil.Approximately(target.GetLocalPosition().x, CenterX - Size);
                if (target.Owner.IsSummonOrPhantom && target.Owner.IdleOnly)
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
            }
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
            AHABEPKMKJJ = true;
        }

        private bool judgeStopTarget(eTargetAssignment OAHLOGOLMHD, bool MOBKHPNMEDM)
        {
            switch (OAHLOGOLMHD)
            {
                case eTargetAssignment.OTHER_SIDE:
                    return LCHLGLAFJED == eFieldTargetType.PLAYER == MOBKHPNMEDM;
                case eTargetAssignment.OWNER_SITE:
                    return LCHLGLAFJED == eFieldTargetType.PLAYER == !MOBKHPNMEDM;
                case eTargetAssignment.ALL:
                    return true;
                default:
                    return false;
            }
        }
    }
}

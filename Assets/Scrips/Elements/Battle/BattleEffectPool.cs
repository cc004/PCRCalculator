// Decompiled with JetBrains decompiler
// Type: Elements.Battle.BattleEffectPool
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace Elements.Battle
{
    public class BattleEffectPool : BattleEffectPoolInterface
    {
        private Dictionary<string, List<SkillEffectCtrl>> effectDictionary;
        //private Dictionary<string, List<DamageEffectCtrlBase>> numberEffectDictionary;

        public bool BACHGMADMKC { get; set; }

        public BattleEffectPool()
        {
            this.effectDictionary = new Dictionary<string, List<SkillEffectCtrl>>();
            //this.numberEffectDictionary = new Dictionary<string, List<DamageEffectCtrlBase>>();
        }

        public SkillEffectCtrl GetEffect(GameObject effectPrefab, UnitCtrl source = null)
        {
            bool _isShadow = !((Object)source == (Object)null) && !this.BACHGMADMKC && source.IsShadow;
            SkillEffectCtrl component;
            if (!this.effectDictionary.ContainsKey(effectPrefab.name))
            {
                component = Object.Instantiate<GameObject>(effectPrefab).GetComponent<SkillEffectCtrl>();
                List<SkillEffectCtrl> skillEffectCtrlList = new List<SkillEffectCtrl>()
        {
          component
        };
                this.effectDictionary.Add(effectPrefab.name, skillEffectCtrlList);
            }
            else
            {
                List<SkillEffectCtrl> effect = this.effectDictionary[effectPrefab.name];
                for (int index = 0; index < effect.Count; ++index)
                {
                    SkillEffectCtrl skillEffectCtrl = effect[index];
                    if (!skillEffectCtrl.IsPlaying)
                    {
                        skillEffectCtrl.gameObject.SetActive(true);
                        skillEffectCtrl.ResetParameter(effectPrefab, (Object)source != (Object)null ? UnitUtility.GetSkinId(source.SoundUnitId, source.SDSkin) : 0, _isShadow);
                        return skillEffectCtrl;
                    }
                }
                component = Object.Instantiate<GameObject>(effectPrefab).GetComponent<SkillEffectCtrl>();
                effect.Add(component);
            }
            component.ResetParameter(effectPrefab, (Object)source != (Object)null ? UnitUtility.GetSkinId(source.SoundUnitId, source.SDSkin) : 0, _isShadow);
            return component;
        }

        /*public DamageEffectCtrlBase LoadNumberEffect(GameObject MDOJNMEMHLN)
        {
          List<DamageEffectCtrlBase> damageEffectCtrlBaseList1 = (List<DamageEffectCtrlBase>) null;
          if ((Object) MDOJNMEMHLN == (Object) null)
            return (DamageEffectCtrlBase) null;
          DamageEffectCtrlBase component;
          if (!this.numberEffectDictionary.TryGetValue(MDOJNMEMHLN.name, out damageEffectCtrlBaseList1))
          {
            component = Object.Instantiate<GameObject>(MDOJNMEMHLN).GetComponent<DamageEffectCtrlBase>();
            component.transform.parent = ExceptNGUIRoot.Transform;
            List<DamageEffectCtrlBase> damageEffectCtrlBaseList2 = new List<DamageEffectCtrlBase>()
            {
              component
            };
            this.numberEffectDictionary.Add(MDOJNMEMHLN.name, damageEffectCtrlBaseList2);
          }
          else
          {
            for (int index = 0; index < damageEffectCtrlBaseList1.Count; ++index)
            {
              DamageEffectCtrlBase damageEffectCtrlBase = damageEffectCtrlBaseList1[index];
              if ((Object) damageEffectCtrlBase != (Object) null && !damageEffectCtrlBase.IsPlaying)
              {
                damageEffectCtrlBase.gameObject.SetActive(true);
                damageEffectCtrlBase.ResetParameter(MDOJNMEMHLN);
                return damageEffectCtrlBase;
              }
            }
            component = Object.Instantiate<GameObject>(MDOJNMEMHLN).GetComponent<DamageEffectCtrlBase>();
            component.transform.parent = ExceptNGUIRoot.Transform;
            damageEffectCtrlBaseList1.Add(component);
          }
          component.ResetParameter(MDOJNMEMHLN);
          return component;
        }*/

        public void DisableAllEffect(bool MMEHPMCNCGF)
        {
            /*foreach (KeyValuePair<string, List<DamageEffectCtrlBase>> numberEffect in this.numberEffectDictionary)
            {
              for (int index = 0; index < numberEffect.Value.Count; ++index)
                numberEffect.Value[index].SetActive(false);
            }*/
            foreach (KeyValuePair<string, List<SkillEffectCtrl>> effect in this.effectDictionary)
            {
                for (int index = 0; index < effect.Value.Count; ++index)
                {
                    if ((Object)effect.Value[index] != (Object)null && !(effect.Value[index].IsAura & MMEHPMCNCGF))
                        effect.Value[index].SetActive(false);
                }
            }
        }

        public void Release()
        {
            this.effectDictionary = (Dictionary<string, List<SkillEffectCtrl>>)null;
            //this.numberEffectDictionary = (Dictionary<string, List<DamageEffectCtrlBase>>) null;
        }

        public SkillEffectCtrl GetEffect(GameObject MDOJNMEMHLN, FirearmCtrlData prefabData, UnitCtrl DCMBKLBBCHD = null)
        {
            SkillEffectCtrl skillEffectCtrl = GetEffect(MDOJNMEMHLN, DCMBKLBBCHD);
            if (prefabData != null)
            {
                FirearmCtrl firearmCtrl = skillEffectCtrl.gameObject.GetComponent<FirearmCtrl>();
                if (firearmCtrl != null)
                {
                    firearmCtrl.SetDatas(prefabData);
                    return firearmCtrl;
                }
            }
            return skillEffectCtrl;
        }
    }
}

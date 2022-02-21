// Decompiled with JetBrains decompiler
// Type: Elements.ChangePatternAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;

namespace Elements
{
  public class ChangePatternAction : ActionParameter
  {
    public override void ExecActionOnStart(
      Skill _skill,
      UnitCtrl _source,
      UnitActionController _sourceActionController)
    {
      base.ExecActionOnStart(_skill, _source, _sourceActionController);
      if (ActionDetail1 != 1)
        return;
      _source.CreateAttackPattern(
          //ManagerSingleton<MasterDataManager>.Instance.masterUnitSkillData[_source.CharacterUnitId]
          _source.unitParameter.SkillData
          , ActionDetail2);
    }

    public override void ExecAction(
      UnitCtrl _source,
      BasePartsData _target,
      int _num,
      UnitActionController _sourceActionController,
      Skill _skill,
      float _starttime,
      Dictionary<int, bool> _enabledChildAction,
      Dictionary<eValueNumber, FloatWithEx> _valueDictionary)
    {
      base.ExecAction(_source, _target, _num, _sourceActionController, _skill, _starttime, _enabledChildAction, _valueDictionary);
      AppendTargetNum(_target.Owner, _num);
      //this.endAllBeforeEffect();
      //for (int index = 0; index < this.ActionEffectList.Count; ++index)
      //  this.playActionEffect(_source, _skill, this.ActionEffectList[index]);
      switch ((eChangePatternType) ActionDetail1)
      {
        case eChangePatternType.ATTACK_PATTERN:
          _target.Owner.ChangeAttackPattern(ActionDetail2, _skill.Level, _valueDictionary[eValueNumber.VALUE_1]);
          break;
        case eChangePatternType.UNION_BURST:
          _target.Owner.ChangeChargeSkill(ActionDetail2, _valueDictionary[eValueNumber.VALUE_1]);
          break;
      }
            switch ((eUbActive)ActionDetail3)
            {
                case eUbActive.ENABLE:
                    _target.Owner.UbIsDisableByChangePattern = false;
                    break;
                case eUbActive.DISABLE:
                    _target.Owner.UbIsDisableByChangePattern = true;
                    break;
            }

        }

        /*private void endAllBeforeEffect()
        {
          for (int index = 0; index < this.changePatternCurrentSkillEffect.Count; ++index)
          {
            SkillEffectCtrl JEOCPILJNAD = this.changePatternCurrentSkillEffect[index];
            JEOCPILJNAD.SetTimeToDie(true);
            JEOCPILJNAD.OnEffectEnd.Call<SkillEffectCtrl>(JEOCPILJNAD);
          }
          this.changePatternCurrentSkillEffect.Clear();
        }*/

        /*private void playActionEffect(UnitCtrl _source, Skill _skill, NormalSkillEffect _actionEffect)
        {
          SkillEffectCtrl effect = this.battleEffectPool.GetEffect(_source.IsLeftDir ? _actionEffect.PrefabLeft : _actionEffect.Prefab);
          effect.IsRepeat = true;
          effect.transform.parent = ExceptNGUIRoot.Transform;
          effect.InitializeSort();
          effect.PlaySe(_source.SoundUnitId, _source.IsLeftDir);
          effect.SetPossitionAppearanceType(_actionEffect, _source.GetFirstParts(true), _source, _skill);
          effect.ExecAppendCoroutine(_source);
          this.changePatternCurrentSkillEffect.Add(effect);
        }*/

        private enum eChangePatternType
    {
      ATTACK_PATTERN = 1,
      UNION_BURST = 2,
    }
        private enum eUbActive
        {
            ENABLE,
            DISABLE,
        }

    }
}

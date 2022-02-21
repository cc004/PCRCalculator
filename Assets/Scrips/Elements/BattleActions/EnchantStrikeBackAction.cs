// Decompiled with JetBrains decompiler
// Type: Elements.EnchantStrikeBackAction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 81CDCA9F-D99D-4BB7-B092-3FE4B4616CF6
// Assembly location: D:\PCRCalculator\解包数据\逆向dll\Assembly-CSharp.dll

using System.Collections.Generic;
using Cute;
using UnityEngine;

namespace Elements
{
    public class EnchantStrikeBackAction : ActionParameter
    {
        public override void ExecActionOnStart(
          Skill _skill,
          UnitCtrl _source,
          UnitActionController _sourceActionController)
        {
            base.ExecActionOnStart(_skill, _source, _sourceActionController);
            /*switch (this.ActionDetail2)
            {
              case 1:
                Singleton<LCEGKJFKOPD>.Instance.LoadToyStrikeBackEffect();
                break;
              case 2:
                Singleton<LCEGKJFKOPD>.Instance.LoadOmemeStrikeBackEffect();
                break;
              case 3:
                Singleton<LCEGKJFKOPD>.Instance.LoadOmemeStrikeBackPlusEffect();
                break;
            }*/
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
            AppendIsAlreadyExeced(_target.Owner, _num);
            SkillEffectCtrl skillEffectCtrl1 = null;
            bool flag = true;
            //if (_target.Owner.StrikeBackDictionary.ContainsKey((EnchantStrikeBackAction.eStrikeBackEffectType) this.ActionDetail2))
            //  flag = !_target.Owner.StrikeBackDictionary[(EnchantStrikeBackAction.eStrikeBackEffectType) this.ActionDetail2].SkillEffect.IsPlaying;
            //CircleEffectController effectController;
            /*if (flag)
            {
              switch (this.ActionDetail2)
              {
                case 1:
                  skillEffectCtrl1 = this.battleEffectPool.GetEffect(Singleton<LCEGKJFKOPD>.Instance.JPEFHMCPLJA);
                  break;
                case 2:
                  skillEffectCtrl1 = this.battleEffectPool.GetEffect(_target.Owner.IsLeftDir ? Singleton<LCEGKJFKOPD>.Instance.PMKMBEKBDCI : Singleton<LCEGKJFKOPD>.Instance.CAPHKPABAHM);
                  break;
                case 3:
                  skillEffectCtrl1 = this.battleEffectPool.GetEffect(_target.Owner.IsLeftDir ? Singleton<LCEGKJFKOPD>.Instance.PAFGPCHIIPJ : Singleton<LCEGKJFKOPD>.Instance.DEGHDMKCDBM);
                  break;
              }
              skillEffectCtrl1.transform.parent = ExceptNGUIRoot.Transform;
              skillEffectCtrl1.InitializeSort();
              skillEffectCtrl1.PlaySe(_source.SoundUnitId, _source.IsLeftDir);
              effectController = skillEffectCtrl1 as CircleEffectController;
              SkillEffectCtrl skillEffectCtrl2 = skillEffectCtrl1;
              NormalSkillEffect skillEffect = new NormalSkillEffect();
              skillEffect.EffectTarget = eEffectTarget.ALL_TARGET;
              skillEffect.TargetBone = eTargetBone.CENTER;
              skillEffect.TrackType = effectController.FollowXY ? eTrackType.BONE : eTrackType.BOTTOM;
              skillEffect.TrackDimension = effectController.FollowXY ? eTrackDimension.XY : eTrackDimension.NONE;
              BasePartsData firstParts = _target.Owner.GetFirstParts(true);
              UnitCtrl _owner = _source;
              Skill skill = _skill;
              skillEffectCtrl2.SetPossitionAppearanceType(skillEffect, firstParts, _owner, skill);
              skillEffectCtrl1.ExecAppendCoroutine((double) _skill.BlackOutTime > 0.0 ? _source : (UnitCtrl) null);
              effectController.SetChildrenInactive();
            }
            else
            {
              skillEffectCtrl1 = _target.Owner.StrikeBackDictionary[(EnchantStrikeBackAction.eStrikeBackEffectType) this.ActionDetail2].SkillEffect;
              effectController = skillEffectCtrl1 as CircleEffectController;
            }*/
            StrikeBackDataSet strikeBackDataSet = null;
            if (!_target.Owner.StrikeBackDictionary.TryGetValue((eStrikeBackEffectType)ActionDetail2, out strikeBackDataSet))
            {
                strikeBackDataSet = new StrikeBackDataSet
                {
                    DataList = new List<StrikeBackData>()
                };
                _target.Owner.StrikeBackDictionary.Add((eStrikeBackEffectType)ActionDetail2, strikeBackDataSet);
            }
            strikeBackDataSet.SkillEffect = skillEffectCtrl1;
            int count = 5;// effectController.Children.Count;
            int number = Mathf.Min((int)_valueDictionary[eValueNumber.VALUE_3], count - strikeBackDataSet.DataList.Count);
            //effectController.AddEffect(number);
            for (int index = 0; index < number; ++index)
            {
                StrikeBackData strikeBackData = new StrikeBackData
                {
                    StrikeBackType = (StrikeBackData.eStrikeBackType)ActionDetail1,
                    Damage = (int)_valueDictionary[eValueNumber.VALUE_1],
                    EffectType = (eStrikeBackEffectType)ActionDetail2,
                    ActionEffectList = ActionEffectList,
                    Skill = _skill
                };
                strikeBackData.DataSet = strikeBackDataSet;
                strikeBackDataSet.DataList.Add(strikeBackData);
                _target.Owner.OnChangeState.Call(_target.Owner, eStateIconType.STRIKE_BACK, true);
                _target.Owner.MyOnChangeAbnormalState?.Invoke(_target.Owner, eStateIconType.STRIKE_BACK, true, 90, "反击中");

            }
        }

        public override void SetLevel(float _level)
        {
            base.SetLevel(_level);
            Value[eValueNumber.VALUE_1] = (float)(MasterData.action_value_1 + MasterData.action_value_2 * _level);
        }

        public enum eStrikeBackEffectType
        {
            TOY = 1,
            OMEME = 2,
            OMEME_PLUS = 3,
        }

        public class eStrikeBackEffectType_DictComparer : IEqualityComparer<eStrikeBackEffectType>
        {
            public bool Equals(
              eStrikeBackEffectType _x,
              eStrikeBackEffectType _y) => _x == _y;

            public int GetHashCode(eStrikeBackEffectType _obj) => (int)_obj;
        }
    }
}

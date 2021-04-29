using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
namespace PCRCaculator.Battle
{

    public enum eAnimationType 
    { 
        run_start=0,idle=1,attack=2,attack_skipQuest=3,damage=4,die=5,run=6,walk=7,standBy = 8,multi_standBy = 9,
        joy_long = 10,joy_long_return = 11,joy_short = 12,joy_short_return = 13,
        
        skill0 = 20, skill0_1 = 21, skill0_2 = 22, skill0_3 = 23, skill0_4 = 24,
        skill1 = 30, skill1_1 = 31, skill1_2 = 32, skill1_3 = 33, skill1_4 = 34,
        skill2 = 40, skill2_1 = 41, skill2_2 = 42, skill2_3 = 43, skill2_4 = 44

    }
    public class BattleUnitBaseSpineController : MonoBehaviour
    {
        public SkeletonAnimation sa;
        public int unitid;
        public List<Spine.Animation> spineBasicAnimations = new List<Spine.Animation>();
        public List<Spine.Animation> spineSkillAnimations = new List<Spine.Animation>();

        private float timeScale = 1;
        private float playSpeed = 1;
        private bool isPause = false;
        private eAnimationType currentAnimation;
        private UnitCtrl owner;
        private float animePlayTime = 0;
        private float currentAnimationDuration;

        private readonly Dictionary<string, eAnimationType> animationNameDic = new Dictionary<string, eAnimationType>();
        private Dictionary<eAnimationType, float> animationTimeDic = new Dictionary<eAnimationType, float>();

        private static string[] basicAnimationName = new string[14]
        {
        "_run_gamestart","_idle","_attack","_attack_skipQuest",
        "_damage","_die","_run","_walk","_standBy","_multi_standBy",
        "_joy_long","_joy_long_return","_joy_short","_joy_short_return"
        };
        private static string[] skillAnimationName = new string[15]
        {
        "_skill0","_skill0_1","_skill0_2","_skill0_3","_skill0_4",
        "_skill1","_skill1_1","_skill1_2","_skill1_3","_skill1_4",
        "_skill2","_skill2_1","_skill2_2","_skill2_3","_skill2_4"
        };

        private const float stand_by_time = 1.67f;
        /*static BattleUnitBaseSpineController()
        {
            animationNameDic = new Dictionary<string, eAnimationType>
            {
                {"_skill0",eAnimationType.skill0 },    
                {"_skill0_1",eAnimationType.skill0_1 },
                {"_skill0_2",eAnimationType.skill0_2 },
                {"_skill0_3",eAnimationType.skill0_3 },
                {"_skill0_4",eAnimationType.skill0_4 },
                {"_skill1",eAnimationType.skill1 },
                {"_skill1_1",eAnimationType.skill1_1 },
                {"_skill1_2",eAnimationType.skill1_2 },
                {"_skill1_3",eAnimationType.skill1_3 },
                {"_skill1_4",eAnimationType.skill1_4 },
                {"_skill2",eAnimationType.skill2 },
                {"_skill2_1",eAnimationType.skill2_1 },
                {"_skill2_2",eAnimationType.skill2_2 },
                {"_skill2_3",eAnimationType.skill2_3 },
                {"_skill2_4",eAnimationType.skill2_4 },

            };
            for(int i = 0; i < 14; i++)
            {
                animationNameDic.Add(basicAnimationName[i], (eAnimationType)i);
            }
        }*/
        public bool IsPlayingAnimeBattleComplete { get => CheckIsPlayingAnimeBattle();}

        /// <summary>
        /// 初始化动画控制器
        /// </summary>
        /// <param name="sa"></param>
        /// <param name="dataAsset"></param>
        /// <param name="unitid"></param>
        public void SetOnAwake(SkeletonDataAsset dataAsset, int unitid,UnitCtrl unitCtrl)
        {
            this.sa = gameObject.GetComponent<SkeletonAnimation>();
            this.unitid = unitid;
            owner = unitCtrl;

            int motiontype = unitid < 300000 ? MainManager.Instance.UnitRarityDic[unitid].detailData.motionType : 0;
            string motiontype_str = motiontype < 10 ? "0" + motiontype : "" + motiontype;
            for (int i = 0; i < basicAnimationName.Length; i++)
            {
                string animationName = motiontype_str + basicAnimationName[i];
                if (unitid >= 300000)
                {
                    animationName = unitid + basicAnimationName[i];
                    if (i == 0)
                    {
                        animationName = unitid + "_gamestart";
                    }
                }
                var spineAnimation = dataAsset.GetSkeletonData(false).FindAnimation(animationName);
                spineBasicAnimations.Add(spineAnimation);
                animationNameDic.Add(animationName, (eAnimationType)i);
                animationTimeDic.Add(animationNameDic[animationName], spineAnimation == null ? -1 : spineAnimation.Duration);
            }
            for (int i = 0; i < skillAnimationName.Length; i++)
            {
                string animationName = unitid + skillAnimationName[i];
                var spineAnimation = dataAsset.GetSkeletonData(false).FindAnimation(animationName);
                spineSkillAnimations.Add(spineAnimation);
                eAnimationType type = (eAnimationType)(i + 20);
                if (i >= 10) { type = (eAnimationType)(i + 30); }
                else if (i >= 5) { type = (eAnimationType)(i + 25); }
                animationNameDic.Add(animationName, type);
                animationTimeDic.Add(animationNameDic[animationName], spineAnimation == null ? -1 : spineAnimation.Duration);
            }

        }
        /// <summary>
        /// 设置动画状态，由UnitCtrl调用
        /// </summary>
        /// <param name="type"></param>
        /// <param name="isloop"></param>
        public void SetAnimaton(eAnimationType type,bool isloop,float speed = 1)
        {
            //Resume();
            Spine.Animation animation = null;
            if ((int)type <= 19)
            {
                animation = spineBasicAnimations[(int)type];
            }
            else if((int)type<=29)
            {
                animation =  spineSkillAnimations[(int)type-20];
            }
            else if ((int)type <= 39)
            {
                animation =  spineSkillAnimations[(int)type-25];
            }
            else if ((int)type <= 49)
            {
                animation = spineSkillAnimations[(int)type - 30];
            }
            if(animation != null)
            {
                sa.AnimationState.SetAnimation(0, animation, isloop);
                playSpeed = speed;
                timeScale = owner.TimeScale;
                sa.timeScale = timeScale * playSpeed;
                currentAnimation = type;
                currentAnimationDuration = animation.Duration;
            }
            else
            {
                string errorword = owner.UnitName + "的" + type.GetDescription() + "动画不存在！";
                BattleUIManager.Instance.LogMessage(errorword,eLogMessageType.ERROR,owner.IsOther);
                SetAnimaton(eAnimationType.idle, true, 1);
            }
            animePlayTime = 0;
            //Debug.Log(owner.UnitName  +"-"+ type.GetDescription() + "-" +animation.duration);
        }
        private void Update()
        {
            animePlayTime += owner.DeltaTimeForPause;
        }
        public void SetTimeScale(float scale)
        {
            timeScale = scale;
            sa.timeScale = isPause ? 0 : timeScale * playSpeed;
        }
        public void Pause(bool isPause = true)
        {
            this.isPause = isPause;
            sa.timeScale = isPause ? 0 : timeScale * playSpeed;
        }
        public void SetCurColor(Color color)
        {
            //mpb.SetColor("_OverlayColor", color);
            //meshRenderer.SetPropertyBlock(mpb);
            sa.skeleton.SetColor(color);
        }
        public void Resume()
        {
            sa.ClearState();
            sa.timeScale = owner.TimeScale;
        }
        public eAnimationType GetCurrentAnimationName()
        {
            string name =  sa.AnimationState.GetCurrent(0).animation.name;
            return animationNameDic[name];
        }
        public bool HasAnimation(eAnimationType eAnimation)
        {
            if ((int)eAnimation <= 30)
            {
                return true;
            }
            return spineSkillAnimations[(int)eAnimation - 25] != null;
        }
        public bool HasNextAnimation()
        {
            eAnimationType eAnimation = currentAnimation;
            if((int)eAnimation >= 31 && (int)eAnimation <=33)
            {
                return spineSkillAnimations[(int)eAnimation - 25] == null;
            }
            if ((int)eAnimation >= 41 && (int)eAnimation <= 43)
            {
                return spineSkillAnimations[(int)eAnimation - 30] == null;
            }
            return false;
        }
        private bool CheckIsPlayingAnimeBattle()
        {
            float duration = currentAnimationDuration;
            if(currentAnimation == eAnimationType.standBy)
            {
                duration = stand_by_time;
            }
            if (animePlayTime >= duration)
            {
                return true;
            }
            return false;
            /*if(sa.AnimationState.GetCurrent(0)!= null)
            {
                return sa.AnimationState.GetCurrent(0).IsComplete;
            }
            return true;*/
        }

        public UnitSkillTimeData CreateAnimeTimeData()
        {
            UnitSkillTimeData skillTimeData = new UnitSkillTimeData();
            skillTimeData.spineTime_Attack = animationTimeDic[eAnimationType.attack];
            skillTimeData.spineTime_MainSkill1 = animationTimeDic[eAnimationType.skill1];
            skillTimeData.spineTime_MainSkill2 = animationTimeDic[eAnimationType.skill2];
            skillTimeData.spineTime_UB = animationTimeDic[eAnimationType.skill0];
            skillTimeData.all_spineTime_Dic = new Dictionary<string, float>();
            foreach(eAnimationType eAnimation in animationTimeDic.Keys)
            {
                skillTimeData.all_spineTime_Dic.Add(eAnimation.GetDescription(), animationTimeDic[eAnimation]);
            }
            return skillTimeData;
        }
    }
}
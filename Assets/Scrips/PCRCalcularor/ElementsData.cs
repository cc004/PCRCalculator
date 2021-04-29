using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elements.Battle;
using PCRCaculator.Battle;
namespace Elements
{

    [System.Serializable]
    public class FirearmCtrlData
    {
        public float HitDelay = 0.5f;
        public float MoveRate = 1;
        public float duration;
        public PCRCaculator.Battle.eMoveTypes MoveType = PCRCaculator.Battle.eMoveTypes.LINEAR;
        public float startRotate;
        public float endRotate;
        //public Bounds ColliderBox;
        public float[] ColliderBoxCentre = new float[3] { 0, 0, 0 };
        public float[] ColliderBoxSize = new float[3] { 0.2f, 0.2f, 0 };

        public bool ignoreFirearm = false;
        public float fixedExecTime = 0;

        public FirearmCtrlData() { }

        public FirearmCtrlData(float hitDelay, float moveRate, float duration, PCRCaculator.Battle.eMoveTypes moveType, float startRotate, float endRotate, Bounds colliderBox)
        {
            HitDelay = hitDelay;
            MoveRate = moveRate;
            this.duration = duration;
            MoveType = moveType;
            this.startRotate = startRotate;
            this.endRotate = endRotate;
            //ColliderBox = colliderBox;
            ColliderBoxCentre = new float[] { colliderBox.center.x, colliderBox.center.y, colliderBox.center.z };
            ColliderBoxSize = new float[] { colliderBox.extents.x, colliderBox.extents.y, colliderBox.extents.z };
        }

    }
    public class UnitActionControllerData
    {
        public PCRCaculator.Battle.ActionParameterOnPrefabDetail AttackDetail;
        public bool UseDefaultDelay;
        public PCRCaculator.Battle.Skill Attack;
        public List<PCRCaculator.Battle.Skill> UnionBurstList;
        public List<PCRCaculator.Battle.Skill> MainSkillList;
        public List<PCRCaculator.Battle.Skill> SpecialSkillList;
        public List<PCRCaculator.Battle.Skill> UnionBurstEvolutionList;
        public List<PCRCaculator.Battle.Skill> MainSkillEvolutionList;
        public PCRCaculator.Battle.Skill Annihilation;

        public UnitActionControllerData(PCRCaculator.Battle.ActionParameterOnPrefabDetail attackDetail, bool useDefaultDelay, PCRCaculator.Battle.Skill attack, List<PCRCaculator.Battle.Skill> unionBurstList, List<PCRCaculator.Battle.Skill> mainSkillList, List<PCRCaculator.Battle.Skill> specialSkillList, List<PCRCaculator.Battle.Skill> unionBurstEvolutionList, List<PCRCaculator.Battle.Skill> mainSkillEvolutionList, PCRCaculator.Battle.Skill annihilation)
        {
            AttackDetail = attackDetail;
            UseDefaultDelay = useDefaultDelay;
            Attack = attack;
            UnionBurstList = unionBurstList;
            MainSkillList = mainSkillList;
            SpecialSkillList = specialSkillList;
            UnionBurstEvolutionList = unionBurstEvolutionList;
            MainSkillEvolutionList = mainSkillEvolutionList;
            Annihilation = annihilation;
        }
    }
    public class UnitActionControllerData2
    {
        public ActionParameterOnPrefabDetail AttackDetail;
        public bool UseDefaultDelay;
        public Skill Attack;
        public List<Skill> UnionBurstList;
        public List<Skill> MainSkillList;
        public List<Skill> SpecialSkillList;
        public List<Skill> SpecialSkillEvolutionList;
        public List<Skill> UnionBurstEvolutionList;
        public List<Skill> MainSkillEvolutionList;
        public Skill Annihilation;

        public UnitActionControllerData2(ActionParameterOnPrefabDetail attackDetail,
            bool useDefaultDelay, Skill attack, List<Skill> unionBurstList, List<Skill> mainSkillList,
            List<Skill> specialSkillList, List<Skill> specialSkillEvList,
            List<Skill> unionBurstEvolutionList, List<Skill> mainSkillEvolutionList, Skill annihilation)
        {
            AttackDetail = attackDetail;
            UseDefaultDelay = useDefaultDelay;
            Attack = attack;
            UnionBurstList = unionBurstList;
            MainSkillList = mainSkillList;
            SpecialSkillList = specialSkillList;
            SpecialSkillEvolutionList = specialSkillEvList;
            UnionBurstEvolutionList = unionBurstEvolutionList;
            MainSkillEvolutionList = mainSkillEvolutionList;
            Annihilation = annihilation;
        }
    }
    [System.Serializable]
    public class UnitPrefabData
    {
        public UnitActionControllerData2 UnitActionControllerData;
        public Dictionary<string, List<Elements.FirearmCtrlData>> unitFirearmDatas;
    }

    public interface ICopyAble<T>
    {
        T CopyThis();
    }
    public class My<T>
    {
        public T data;
    }
    public static class EXTools
    {
        public static List<T> ChangeType<T>(this List<My<T>> list) where T : ICopyAble<T>
        {
            List<T> a = new List<T>();
            if (list.Count > 0)
            {
                foreach (My<T> b in list)
                {
                    a.Add(b.data.CopyThis());
                }
            }
            return a;
        }
        public static List<T2> ChangeType<T, T2>(this List<My<T>> list) where T : ICopyAble<T2>
        {
            List<T2> a = new List<T2>();
            if (list.Count > 0)
            {
                foreach (My<T> b in list)
                {
                    a.Add(b.data.CopyThis());
                }
            }
            return a;
        }
    }

    [System.Serializable]
    public class UnitActionController4Json
    {
        public FilePathPair m_GameObject;
        public int m_Enabled;
        public FilePathPair m_Script;
        public string m_Name;
        public MyActionParameterOnPerferbDetail AttackDetail;
        public bool UseDefaultDelay;
        public MySkill Attack;
        public List<My<MySkill>> UnionBurstList;
        public List<My<MySkill>> MainSkillList;
        public List<My<MySkill>> SpecialSkillList;
        public List<My<MySkill>> SpecialSkillEvolutionList;
        public List<My<MySkill>> UnionBurstEvolutionList;
        public List<My<MySkill>> MainSkillEvolutionList;
        public MySkill Annihilation;

        public List<Skill> GetUBList()
        {
            List<Skill> a = new List<Skill>();
            foreach (My<MySkill> b in UnionBurstList)
            {
                a.Add(b.data.CopyThis());
            }
            return a;
        }
        public List<Skill> GetUBEvList()
        {
            List<Skill> a = new List<Skill>();
            foreach (My<MySkill> b in UnionBurstEvolutionList)
            {
                a.Add(b.data.CopyThis());
            }
            return a;
        }
        public List<Skill> GetMainSkillList()
        {
            List<Skill> a = new List<Skill>();
            foreach (My<MySkill> b in MainSkillList)
            {
                a.Add(b.data.CopyThis());
            }
            return a;
        }
        public List<Skill> GetMainSkillEvList()
        {
            List<Skill> a = new List<Skill>();
            foreach (My<MySkill> b in MainSkillEvolutionList)
            {
                a.Add(b.data.CopyThis());
            }
            return a;
        }


        public sealed class FilePathPair
        {
            public long m_FileID;
            public long m_PathID;
        }
        [System.Serializable]
        public class MySkill:ICopyAble<Skill>
        {
            public bool IsPrincessForm;
            public List<PrincessSkillMovieData> PrincessSkillMovieDataList = new List<PrincessSkillMovieData>();
            public List<My<MyActionParameterOnPrefab>> ActionParametersOnPrefab = new List<My<MyActionParameterOnPrefab>>();
            public List<My<MyNormalSkillEffect>> SkillEffects;

            public bool ForcePlayNoTarget;
            public int ParameterTarget;
            public eSpineCharacterAnimeId AnimId;
            public float BlackOutTime;
            public bool BlackoutEndWithMotion;
            public bool ForceComboDamage;
            public float CutInMovieFadeStartTime;
            public float CutInMovieFadeDurationTime;
            public float CutInSkipTime;
            public SkillMotionType SkillMotionType;
            public bool TrackDamageNum;
            public bool PauseStopState;

            public List<ActionParameterOnPrefab> GetActionParameterOnPrefab()
            {
                return ActionParametersOnPrefab.ChangeType<MyActionParameterOnPrefab,ActionParameterOnPrefab>();
            }
            public List<NormalSkillEffect> GetNormalSkillEffects()
            {
                return SkillEffects.ChangeType<MyNormalSkillEffect, NormalSkillEffect>();
            }
            public Skill CopyThis()
            {
                Skill s = new Skill();
                s.IsPrincessForm = IsPrincessForm;
                s.PrincessSkillMovieDataList = PrincessSkillMovieDataList;
                s.ActionParametersOnPrefab = GetActionParameterOnPrefab();
                s.SkillEffects = SkillEffects.ChangeType<MyNormalSkillEffect,NormalSkillEffect>();
                s.ForcePlayNoTarget = ForcePlayNoTarget;
                s.ParameterTarget = ParameterTarget;
                //s.AnimId = AnimId;
                s.BlackOutTime = BlackOutTime;
                s.BlackoutEndWithMotion = BlackoutEndWithMotion;
                s.ForceComboDamage = ForceComboDamage;
                s.CutInMovieFadeStartTime = CutInMovieFadeStartTime;
                s.CutInMovieFadeDurationTime = CutInMovieFadeDurationTime;
                s.CutInSkipTime = CutInSkipTime;
                s.SkillMotionType = SkillMotionType;
                s.TrackDamageNum = TrackDamageNum;
                s.PauseStopState = PauseStopState;
                return s;
            }
        }
        public class MyNormalSkillEffect:ICopyAble<NormalSkillEffect>
        {
            public FilePathPair Prefab;
            public FilePathPair PrefabLeft;
            public My<float>[] ExecTime;
            public bool IsReaction;
            public List<My<MyNormalSkillEffect>> FireArmEndEffects;
            public bool TargetActionIsReflexive;
            public int TargetActionIndex;
            public int TargetActionId;
            public int FireActionId;
            public int TargetMotionIndex;
            public eEffectBehavior EffectBehavior;
            public eEffectTarget EffectTarget;
            public eTargetBone TargetBone;
            public eEffectTarget FireArmEndTarget;
            public eTargetBone FireArmEndTargetBone;
            public eTrackType TrackType;
            public eTrackDimension TrackDimension;
            public string TargetBoneName;
            public bool TrackRotation;
            public bool DestroyOnEnemyDead;
            public float CenterY;
            public float DeltaY;
            public bool TrackTarget;
            public float Height;
            public bool IsAbsoluteFireArm;
            public float AbsoluteFireDistance;
            public List<ShakeEffect> ShakeEffects;
            public int TargetBranchId;
            public bool PlayWithCutIn;

            public NormalSkillEffect CopyThis()
            {
                NormalSkillEffect a = new NormalSkillEffect();
                if (ExecTime.Length > 0)
                {
                    a.ExecTime = new float[ExecTime.Length];
                    for (int i = 0; i < ExecTime.Length; i++)
                        a.ExecTime[i] = ExecTime[i].data;
                }
                a.IsReaction = IsReaction;
                a.FireArmEndEffects = FireArmEndEffects.ChangeType<MyNormalSkillEffect,NormalSkillEffect>();
                a.TargetActionIsReflexive = TargetActionIsReflexive;
                a.TargetActionIndex = TargetActionIndex;
                a.TargetActionId = TargetActionId;
                a.FireActionId = FireActionId;
                a.TargetMotionIndex = TargetMotionIndex;
                a.EffectBehavior = EffectBehavior;
                a.EffectTarget = EffectTarget;
                a.TargetBone = TargetBone;
                a.FireArmEndTarget = FireArmEndTarget;
                a.FireArmEndTargetBone = FireArmEndTargetBone;
                a.TrackType = TrackType;
                a.TrackDimension = TrackDimension;
                a.TargetBoneName = TargetBoneName;
                a.TrackRotation = TrackRotation;
                a.DestroyOnEnemyDead = DestroyOnEnemyDead;
                a.CenterY = CenterY;
                a.DeltaY = DeltaY;
                a.TrackTarget = TrackTarget;
                a.Height = Height;
                a.IsAbsoluteFireArm = IsAbsoluteFireArm;
                a.AbsoluteFireDistance = AbsoluteFireDistance;

                a.TargetBranchId = TargetBranchId;
                a.PlayWithCutIn = PlayWithCutIn;
                return a;
            }

        }

        [System.Serializable]
        public class MyActionParameterOnPrefab:ICopyAble<ActionParameterOnPrefab>
        {
            public bool Visible;
            public eActionType ActionType;
            public List<My<MyActionParameterOnPerferbDetail>> Details;
            public eEffectType EffectType;

            public ActionParameterOnPrefab CopyThis()
            {
                ActionParameterOnPrefab ac = new ActionParameterOnPrefab();
                ac.Visible = Visible;
                ac.ActionType = ActionType;
                ac.EffectType = EffectType;
                ac.Details = Details.ChangeType<MyActionParameterOnPerferbDetail, ActionParameterOnPrefabDetail>();
                return ac;
            }
        }
        [System.Serializable]
        public class MyActionParameterOnPerferbDetail : ICopyAble<ActionParameterOnPrefabDetail>
        {
            public bool Visible;
            public List<My<ActionExecTime>> ExecTimeForPrefab;//ExecTimeForPrefab
            public List<My<ActionExecTimeCombo>> ExecTimeCombo;
            public int ActionId;
            public ActionParameterOnPrefabDetail CopyThis()
            {
                ActionParameterOnPrefabDetail ac = new ActionParameterOnPrefabDetail();
                ac.Visible = Visible;
                ac.ExecTimeForPrefab = ExecTimeForPrefab.ChangeType();
                ac.ExecTimeCombo = ExecTimeCombo.ChangeType();
                ac.ActionId = ActionId;
                return ac;
            }
        }
    }
}
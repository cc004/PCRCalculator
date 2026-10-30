using System.Text.Json;
class Program {
static UnitCtrl Owner=new();static bool StopModeChangeEndEffectCalled;
static void Fixed(NormalSkillEffect _skillEffect,Skill _skill,BasePartsData _target,bool _modeChangeEndEffect){BasePartsData _firearmEndTarget=_target;
if (_skillEffect.EffectBehavior == eEffectBehavior.SERIES_FIREARM && !(_skillEffect.FireAction is AttackAction) && (!(_skillEffect.FireAction is RatioDamageAction) && !(_skillEffect.FireAction is UpperLimitAttackAction)) && (!(_skillEffect.FireAction is HealAction) && _skillEffect.AppendAndJudgeAlreadyExeced(_firearmEndTarget.Owner)) || (_skillEffect.TargetBranchId != 0 && _skillEffect.TargetBranchId != _skill.EffectBranchId || _skillEffect.TargetMotionIndex == 1 && _skill.LoopEffectAlreadyDone) || (_modeChangeEndEffect && StopModeChangeEndEffectCalled))
                return;
switch(_skillEffect.EffectBehavior){case eEffectBehavior.WORLD_POS_CENTER:break;case eEffectBehavior.TARGET_CENTER:break;
case eEffectBehavior.SKILL_AREA_RANDOM:
                    var xRandomData = new RandomData(Owner, _target.Owner, _skill.SkillId, 8, 0);
                    BattleManager.Random(0.0f, 1.0f, ref xRandomData);
                    var yRandomData = new RandomData(Owner, _target.Owner, _skill.SkillId, 8, 0);
                    BattleManager.Random(-1.0f, 1.0f, ref yRandomData);
                    break;
}}
static void Main(){var rows=new List<object>();int mismatches=0;for(int behavior=0;behavior<7;behavior++)for(int mode=0;mode<2;mode++)for(int stop=0;stop<2;stop++)for(int branch=0;branch<2;branch++)for(int loop=0;loop<2;loop++){
var effect=new NormalSkillEffect{EffectBehavior=(eEffectBehavior)behavior,TargetBranchId=branch,TargetMotionIndex=loop};var skill=new Skill{EffectBranchId=0,LoopEffectAlreadyDone=true};StopModeChangeEndEffectCalled=stop!=0;BattleManager.Calls.Clear();Fixed(effect,skill,new(),mode!=0);
int native=behavior==4&&branch==0&&loop==0&&!(mode!=0&&stop!=0)?2:0;
if(native!=BattleManager.Calls.Count){mismatches++;rows.Add(new{behavior,mode,stop,branch,loop,native_draws=native,patched_draws=BattleManager.Calls.Count});}}
BattleManager.Calls.Clear();Fixed(new NormalSkillEffect{EffectBehavior=eEffectBehavior.SKILL_AREA_RANDOM},new Skill(),new(),false);var normal=BattleManager.Calls.ToArray();
BattleManager.Calls.Clear();for(int i=0;i<5;i++)Fixed(new NormalSkillEffect{EffectBehavior=eEffectBehavior.SKILL_AREA_RANDOM},new Skill(),new(),false);
int fiveValidDispatches=BattleManager.Calls.Count;BattleManager.Rng.InitState(666);var stateBefore=BattleManager.Rng.state;StopModeChangeEndEffectCalled=true;BattleManager.Calls.Clear();Fixed(new NormalSkillEffect{EffectBehavior=eEffectBehavior.SKILL_AREA_RANDOM},new Skill(),new(),true);bool skippedStateUnchanged=stateBefore.Equals(BattleManager.Rng.state);int stoppedCalls=BattleManager.Calls.Count;
Console.WriteLine(JsonSerializer.Serialize(new{skippedStateUnchanged,stoppedCalls,cases=112,mismatches,counterexamples=rows,normal_call_order=normal,five_valid_dispatches=fiveValidDispatches},new JsonSerializerOptions{WriteIndented=true}));}
}
public enum eEffectBehavior{NORMAL=0,FIREARM=1,WORLD_POS_CENTER=2,TARGET_CENTER=3,SKILL_AREA_RANDOM=4,SERIES=5,SERIES_FIREARM=6}
public class UnitCtrl{}public class BasePartsData{public UnitCtrl Owner=new();}public class Skill{public int SkillId,EffectBranchId;public bool LoopEffectAlreadyDone;}
public class AttackAction{}public class RatioDamageAction{}public class UpperLimitAttackAction{}public class HealAction{}
public class NormalSkillEffect{public eEffectBehavior EffectBehavior;public object FireAction=new AttackAction();public int TargetBranchId,TargetMotionIndex;public bool AppendAndJudgeAlreadyExeced(UnitCtrl u)=>false;}
public class RandomData{public RandomData(UnitCtrl a,UnitCtrl b,int skill,int type,int value){}}
public static class BattleManager{public static UnityRandom Rng=new();public static List<float[]>Calls=new();public static float Random(float min,float max,ref RandomData d){Calls.Add(new[]{min,max});return Rng.Range((int)(min*1000),(int)(max*1000))/1000f;}}

using System.Text.Json;
var cases=new[]{(0.499999999999999,0.49999997,0.5f,0.5f,true),(0.1,1.0,0.5f,0.5f,false),(1.0,0.1,0.5f,0.5f,false),(1.0,1.0,0.5f,0.5f,true)};var results=new List<object>();
foreach(var (first,second,draw1,draw2,expected) in cases){BattleManager.rng.InitState(666);BattleManager.Calls=0;BattleManager.Draws=new Queue<float>(new[]{draw1,draw2});BattleUtil.Dodge=second;var before=BattleManager.rng.state;var actual=Decide(first);if(actual!=expected||BattleManager.Calls!=2||before.Equals(BattleManager.rng.state))throw new Exception("Fear contract mismatch");results.Add(new{first,second,actual,draws=BattleManager.Calls});}Console.WriteLine(JsonSerializer.Serialize(new{test="FearAction exact source decision block",results}));
bool Decide(double threshold){object _source=null;var _target=new Target();var _skill=new Skill();int ActionId=1;bool IsExEquipSkillAction=false;var _valueDictionary=new Dictionary<eValueNumber,double>{{eValueNumber.VALUE_3,threshold}};
// SOURCE_BLOCK
}
enum eValueNumber{VALUE_3} class Target{public object Owner;}class Skill{public int Level;}
class RandomData{public RandomData(object s,object t,int id,int type,float prob){}}
static class BattleManager{public static UnityRandom rng=new();public static int Calls;public static Queue<float> Draws=new();public static float Random(float lo,float hi,ref RandomData data){Calls++;rng.Range(0,1000);return Draws.Dequeue();}}
static class Mathf{public const float Epsilon=float.Epsilon;public static float Abs(float x)=>Math.Abs(x);public static float Max(float a,float b)=>Math.Max(a,b);}
static class BattleUtil{public static double Dodge;public static double CalcDodgeByLevelDiffUseParam(int l,object s,Target t,bool e,float v)=>Dodge;
// SOURCE_HELPERS
}

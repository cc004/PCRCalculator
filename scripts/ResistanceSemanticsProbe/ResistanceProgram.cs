using System.Text.Json;
var results=new List<object>();var source=new UnitCtrl();
void Setup(int resistance,int len=54,int?explicitDetail=null){MainManager.Instance.AilmentDataDic.Clear();for(int i=1;i<=53;i++)MainManager.Instance.AilmentDataDic[i]=new Row{ailment_id=i,ailment_action=8,ailment_detail_1=i};if(explicitDetail.HasValue)MainManager.Instance.AilmentDataDic[54]=new Row{ailment_id=54,ailment_action=46,ailment_detail_1=explicitDetail.Value};ResistData.Values=new int[len];if(len>=54)ResistData.Values[53]=resistance;BattleManager.rng.InitState(666);BattleManager.Count=0;BattleManager.Fixed=null;MyGameCtrl.Instance.tempData.randomData.Override=false;}
bool R(dynamic p,int detail=1)=>p.ResistStatus(eActionType.RATIO_DAMAGE,detail,source,false,true,p,null);
int gameGridDifferences=0;for(int percent=0;percent<=100;percent++){
 Setup(percent,54,-1);var p=new GameParts();p.Build(1);
 for(int raw=0;raw<1000;raw++){float v=raw/1000f;BattleManager.Fixed=v;if(R(p)!=((double)v<percent/100.0))gameGridDifferences++;}}
results.Add(new{test="game_candidate_101000_grid",gameGridDifferences});
var scenarioRows=new List<object>();
foreach(string kind in new[]{"complete0","complete100","missing54","gap20_before54","null20_before54","other54","short53","resist0","forcehit","wildcard","explicit_detail_miss","prior46_before_gap"}){
 Setup(kind=="complete100"?100:0,kind=="short53"?53:54,-1);int detail=1;
 if(kind=="missing54")MainManager.Instance.AilmentDataDic.Remove(54);
 if(kind=="gap20_before54"||kind=="prior46_before_gap")MainManager.Instance.AilmentDataDic.Remove(20);
 if(kind=="null20_before54")MainManager.Instance.AilmentDataDic[20]=null;
 if(kind=="other54")MainManager.Instance.AilmentDataDic[54].ailment_action=8;
 if(kind=="explicit_detail_miss")MainManager.Instance.AilmentDataDic[54].ailment_detail_1=2;
 if(kind=="prior46_before_gap"){MainManager.Instance.AilmentDataDic[1].ailment_action=46;MainManager.Instance.AilmentDataDic[1].ailment_detail_1=2;detail=2;}
 if(kind=="wildcard")detail=999;
 var p=new GameParts();p.Build(kind=="resist0"?0:1);p.Owner.Force=kind=="forcehit";var start=BattleManager.rng.state;
 bool answer=R(p,detail);int expected=kind is "complete0" or "complete100" or "wildcard" or "prior46_before_gap"?1:0;
 if(BattleManager.Count!=expected||answer!=(kind=="complete100"))throw new Exception("game condition mismatch "+kind);
 scenarioRows.Add(new{kind,draws=BattleManager.Count,resisted=answer,stateUnchanged=start.Equals(BattleManager.rng.state),ratioMapping=p.Ratio});
}
results.Add(new{test="game_condition_table",scenarioRows});
Setup(30,54,-1);var untouched=new GameParts();untouched.Build(1);BattleManager.Fixed=.8f;MyGameCtrl.Instance.tempData.randomData.Override=true;MyGameCtrl.Instance.tempData.randomData.Value=0;
results.Add(new{test="explicit_experiment_override_preserved",resisted=R(untouched),draws=BattleManager.Count});

if(!R(untouched))throw new Exception("explicit override lost");
if(gameGridDifferences!=0)throw new Exception("float boundary mismatch");
Console.WriteLine(JsonSerializer.Serialize(results));

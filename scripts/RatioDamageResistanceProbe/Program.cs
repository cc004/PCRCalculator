using Elements;
using Elements.Battle;
using PCRCaculator;
using PCRCaculator.Guild;
using PCRCaculator.SQL;

void Require(bool value, string message) { if (!value) throw new Exception(message); }
MainManager.Instance = new MainManager();
MyGameCtrl.Instance = new MyGameCtrl { tempData = new TempData { randomData = new GuildRandomData() } };
UnityEngine.Random.InitState(666);
var draws = new List<RandomData>();
BattleManager.OnCallRandom = draws.Add;
var source = new UnitCtrl();

BasePartsData Parts(int resistance, int length = 100, int resistId = 300000012, params ailment_data[] metadata)
{
    MainManager.Instance.AilmentDataDic.Clear();
    foreach (var row in metadata) MainManager.Instance.AilmentDataDic.Add(row.ailment_id, row);
    var values = new int[length];
    if (length >= 54) values[53] = resistance;
    MainManager.Instance.ResistDataDic[resistId] = values;
    var parts = new BasePartsData { Owner = new UnitCtrl() };
    parts.SetAbnormalResistId(resistId, true);
    return parts;
}
bool Resist(BasePartsData parts, int detail = 1) =>
    parts.ResistStatus(eActionType.RATIO_DAMAGE, detail, source, false, true, parts, null);

// Nephi-Nera action 129700305: five independently targeted guardians must
// consume five resistance draws even when ailment_54 is zero.
var targets = Enumerable.Range(0, 5).Select(_ => Parts(0)).ToArray();
foreach (var target in targets) Require(!Resist(target), "0% resistance blocked the attack");
Require(draws.Count == 5 && draws.All(d => d.type == 9 && d.actionID == 46 && d.targetResult == 0), "five targets must consume exactly five ratio resistance draws");
var immune = Parts(100);
Require(Resist(immune) && draws.Count == 6, "100% resistance must still draw and resist");
var partial = Parts(30);
Require(Math.Abs(partial.GetResistProb(eActionType.RATIO_DAMAGE, 1) - 0.3f) < 0.00001, "fallback must use slot 54, not a hardcoded probability");

var explicitRow = Parts(100, 100, 300000012, new ailment_data { ailment_id = 54, ailment_action = 46, ailment_detail_1 = 2 });
Require(!Resist(explicitRow, 1) && draws.Count == 6, "explicit detail mapping must not gain wildcard fallback");
Require(Resist(explicitRow, 2) && draws.Count == 7, "explicit mapping must remain effective");
var alternate = Parts(100, 100, 300000012, new ailment_data { ailment_id = 1, ailment_action = 46, ailment_detail_1 = 2 });
Require(!Resist(alternate, 1) && draws.Count == 7, "another explicit ratio mapping must win");
var reusedSlot = Parts(100, 100, 300000012, new ailment_data { ailment_id = 54, ailment_action = 8, ailment_detail_1 = 1 });
Require(!Resist(reusedSlot) && draws.Count == 7, "existing slot 54 must not be reinterpreted");
Require(!Resist(Parts(0, 53)) && draws.Count == 7, "short legacy arrays must not gain a draw");
Require(!Resist(Parts(0, 100, 0)) && draws.Count == 7, "resist id zero must not gain a draw");
var forced = Parts(100);
forced.Owner.AbnormalStateForceHitData[eActionType.RATIO_DAMAGE] = new List<int> { 1 };
Require(!Resist(forced) && draws.Count == 7, "force-hit must retain its no-draw short circuit");
BattleManager.OnCallRandom = null;
Console.WriteLine("PASS RatioDamageResistanceProbe five_targets=5 zero_percent=true full_resist=true data_priority=true legacy=true force_hit=true");

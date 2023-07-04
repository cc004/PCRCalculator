using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Elements.Battle;
using PCRCaculator;
using PCRCaculator.Battle;
using PCRCaculator.Guild;
using Spine.Unity;
using UnityEngine;

namespace Elements
{
    public class MyGameCtrl : MonoBehaviour
    {
        public static MyGameCtrl Instance;
        public Transform unitParent;
        public Transform enemyParent;
        public Transform effectParent;
        public GameObject prefab1;
        public GameObject prefab2;
        public GameObject battleUnitPrefab;
        public GameObject firearmPrefab;
        //public GameObject emptyObject;


        public bool IsUnitCreated;
        public TempData tempData;
        public List<UnitCtrl> playerUnitCtrl;
        public List<UnitCtrl> enemyUnitCtrl;
        public Dictionary<int, UnitCtrlData> allUnitCtrlDataDic = new Dictionary<int, UnitCtrlData>();

        private int finishCount;
        private MainManager mainManager;
        private BattleManager staticBattleManager;
        private Dictionary<int, UnitCtrl> playerUnitCtrlDic = new Dictionary<int, UnitCtrl>();
        private Dictionary<int, UnitCtrl> enemyUnitCtrlDic = new Dictionary<int, UnitCtrl>();

        public bool IsAutoMode;
        public bool ForceAutoMode;

        public int CurrentSeedForSave { get; set; }
        //public bool ignoreEffects => false;//!tempData.SettingData.useSkillEffects;


        private const float SPINE_SCALE = 0.01f;

        private void Awake()
        {
            Instance = this;
            foreach (var pair in ABExTool.mgrCharacter.registries)
            {
                if (!pair.Key.StartsWith("a/all_fxsk_")) continue;
                ABExTool.TryGetAssetBundleByName(pair.Key.Split("/")[1], false);
            }
            //System.IO.File.WriteAllText("fieldnames.txt",
            //    string.Join("\n", typeof(UnitCtrl).GetProperties().Select(p => p.Name)));
        }
        private void Start()
        {
            staticBattleManager = BattleManager.Instance;
            if(MainManager.Instance == null)
            {
                return;
            }
            mainManager = MainManager.Instance;
            IsAutoMode = mainManager.IsAutoMode;
            ForceAutoMode = mainManager.ForceAutoMode;
            tempData = new TempData();
            tempData.playerList = mainManager.PlayerDataForBattle;
            if (mainManager.IsGuildBattle)
            {
                tempData.guildEnemy = mainManager.GuildBattleData.enemyData;
                tempData.MPartsDataDic = mainManager.GuildBattleData.MPartsDataDic;
                tempData.mParts = mainManager.GuildBattleData.mParts;
                tempData.enemyList = tempData.guildEnemy.Select(x => x.CreateUnitData()).ToList();
                tempData.UBExecTimeList = mainManager.GuildBattleData.UBExecTimeList;
                tempData.isGuildBattle = true;
                tempData.isGuildEnemyViolent = mainManager.GuildBattleData.SettingData.GetCurrentPlayerGroup().isViolent;
                tempData.tryCount = mainManager.GuildBattleData.SettingData.UBTryingCount;
                tempData.SettingData = mainManager.GuildBattleData.SettingData;
                tempData.randomData = tempData.SettingData.GetCurrentRandomData();
                tempData.skipping = mainManager.GuildBattleData.skipping;
                LoadAllUnitCtrlData();
            }
            else
            {
                tempData.enemyList = mainManager.EnemyDataForBattle;
                tempData.SettingData = GuildManager.StaticsettingData;
            }
            tempData.CreateAllUnitParameters(tempData.SettingData.GetCurrentPlayerGroup().useLogBarrierNew);
            unitCount.Clear();
            BattleManager.Instance.Init(this);
            if (!IsAutoMode)
            {
                BattleManager.Instance.ubmanager.SetUbExec(tempData.UBExecTimeList, tempData.tryCount);
            }
            else
            {
                BattleManager.Instance.ubmanager.SetUbExec(null, 0);
            }
    }
    private void LoadAllUnitCtrlData()
        {
        }
        public void Initialize()//由GameManager的init调用，生成己方战斗小人
        {
            finishCount = 0;
            int idx = 0;
            foreach (PCRCaculator.UnitData unit in tempData.playerList)
            {
                LoadCharacter(unit, idx, true, () =>
                {
                    finishCount++;
                    if (finishCount >= tempData.playerList.Count)
                    {
                        IsUnitCreated = true;
                    }
                });
                idx++;
            }
        }
        public void LoadEnemy(Action action)
        {
            finishCount = 0;
            int idx = 0;
            if (tempData.isGuildBattle)
            {
                foreach (var enemy in tempData.guildEnemy)
                {
                    var unitData = enemy.CreateUnitData();
                    LoadCharacter(unitData, idx, false, action);
                }
            }
            else
            {
                foreach (PCRCaculator.UnitData unit in tempData.enemyList)
                {
                    LoadCharacter(unit, idx, false, action);
                    idx++;
                }
            }
        }
        public void LoadCharacter(PCRCaculator.UnitData unitData,int idx,bool isPlayer, Action action,bool immediate = true)
        {
            if (immediate)
            {
                LoadCharacterPrefabImmediately(unitData, idx, isPlayer, action);
            }
            else
            {
                StartCoroutine(LoadCharacterPrefab(unitData, idx, isPlayer, action));
            }
        }
        public void TryingExecUB(int playerIdx)
        {
            if (playerIdx < playerUnitCtrl.Count)
            {
                playerUnitCtrl[playerIdx].IsUbExecTrying = true;
            }
        }
        public void SetUI()
        {
            BattleUIManager.Instance.SetUI_2(this);
            if (tempData.isGuildBattle && GuildCalculator.Instance != null)
            {
                GuildCalculator.Instance.Initialize(playerUnitCtrl.ToList(), enemyUnitCtrl[0]);
            }

            if (tempData.isGuildBattle)
                SetBattleSpeed(mainManager.GuildBattleData.SettingData.calSpeed);
            else
                SetBattleSpeed(1);
        }
        public static void SetSummonUI(UnitCtrl summon)
        {
            BattleUIManager.Instance.SetSummonUI(summon);
        }
        public void PauseButton()
        {
            BattleHeaderController.Instance.OnClickPauseButton();
        }
        public void SetTimeCount(float LastTimeFloat)
        {
            BattleUIManager.Instance.UpdateTimeCount(LastTimeFloat);
        }
        public void SetBattleSpeed(float speed)
        {
            BattleSpeedInterface battleSpeed = BattleManager.Instance.battleTimeScale;
            if (speed == 1)
            {
                battleSpeed.SpeedUpFlag = false;
            }
            else
            {
                battleSpeed.SpeedUpRate = speed;
                battleSpeed.SpeedUpFlag = true;
            }
        }
        private void CreateUnitSpine(PCRCaculator.UnitData data,int idx, bool isplayer,eUnitRespawnPos respawnPos, Action callBack)
        {
            int prefab = mainManager.UnitRarityDic.TryGetValue(data.unitId, out var val) ? val.detailData.prefabIdBattle : 0;
            var unitid = data.unitId;
            //SkeletonDataAsset dataAsset = ScriptableObject.CreateInstance<SkeletonDataAsset>();
            //dataAsset = Resources.Load<SkeletonDataAsset>("Unit/" + unitid + "/" + unitid + "_SkeletonData");
            //if(dataAsset == null)
            //{
            int skinID = UnitUtility.GetSkinId(unitid, 3 /* force use 3x skin */);
            int unitid2 = unitid, prefabOverride = -1;
            string skin = null, p0 = null;
            
            if (unitid == 105701 && data.rarity == 6)
            {
                skinID = 170161;
                unitid2 = 170101;
            }

            if (prefab != 0)
            {
                prefabOverride = UnitUtility.GetSkinId(prefab, 3);
            }
            
            var dataAsset = SpineCreator.Instance.Createskeletondata(skinID, SPINE_SCALE, true, prefabOverride: prefabOverride);
            //}
            if(dataAsset == null)
            {
                MainManager.Instance.WindowConfigMessage("角色" + unitid + "的骨骼动画数据丢失！",null);
                return;
            }
            //var sa = SkeletonAnimation.NewSkeletonAnimationGameObject(dataAsset); // Spawn a new SkeletonAnimation GameObject.
            BattleSpineController battleSpineController = BattleSpineController.LoadNewSkeletonAnimationGameObject(dataAsset);
            //Elements.BattleUnitBaseSpineController sa = SkeletonAnimation.NewSpineGameObject<Elements.BattleUnitBaseSpineController>(dataAsset);
            //sa.gameObject.name = unitid.ToString();
            battleSpineController.Initialize(false);
            UnitCtrl unitCtrl = isplayer ? playerUnitCtrlDic[unitid] : enemyUnitCtrlDic[unitid];
            //unitCtrl.IsOther = !isplayer;
            unitCtrl.unitData = data;
            battleSpineController.gameObject.transform.SetParent(unitCtrl.transform.TargetTransform, false);
            //battleSpineController.gameObject.name = "rotate_center";
            battleSpineController.gameObject.name = "" + data.unitId+"-"+data.GetNicName();
            SpineResourceInfo info = ResourceDefineSpine.GetResource(eSpineType.SD_NORMAL_BOUND);
            SpineResourceSet resourceSet = new SpineResourceSet(info, unitid2, 0, 0);
            resourceSet.Skelton = dataAsset;
            
            battleSpineController.Create(resourceSet);

            unitCtrl.UnitSpineCtrl = battleSpineController;
            battleSpineController.Owner = unitCtrl;
            
            unitCtrl.CenterBone = battleSpineController.skeleton.FindBone("Center");
            unitCtrl.StateBone = battleSpineController.skeleton.FindBone("State");
            battleSpineController.SetAnimeEventDelegateForBattle(() => { battleSpineController.IsStopState = true; });
            //unitCtrl.transform.parent = unitParentTransform;
            unitCtrl.transform.ResetLocal();
            unitCtrl.transform.localPosition = new Vector3(-1400f, staticBattleManager.GetRespawnPos(respawnPos), 0);
            //battleProcessor.AddUnitSkillUseCount(ref unitCtrl.SkillUseCount, pPararm.MasterData.PrefabId);
            PCRCaculator.UnitData unitData = isplayer ? tempData.playerList[idx] : tempData.enemyList[idx];
            UnitParameter unitParameter = isplayer ? tempData.PlayerParameters[idx] : tempData.EnemyParameters[idx];
            unitCtrl.unitParameter = unitParameter;
            unitCtrl.Initialize(unitParameter,unitData ,false, true);
            unitCtrl.BattleStartProcess(respawnPos);
            //callBack.Call<UnitCtrl>(unitCtrl);
            //if (!IsAutoMode && isplayer)
            //{
            //unitCtrl.SetUBExecTime(tempData.UBExecTimeList[idx], tempData.tryCount);
            //}

        }
        private IEnumerator LoadCharacterPrefab(PCRCaculator.UnitData data,int idx,bool isplayer,Action finishAction )
        {
            //yield return new WaitForSeconds(5f);
            int unitid = data.unitId;
            WWW www = new WWW("file:///" + Application.dataPath + "/AB/all_battleunitprefab_" + unitid + ".unity3d");
            yield return www;
            var ab = www.assetBundle;
            foreach (string path_0 in ab.GetAllAssetNames())
            {
                //try
                //{
                    if (path_0.Contains(".prefab"))
                    {
                        GameObject b = Instantiate(ab.LoadAsset<GameObject>(path_0));
                        //GameObject b = www.assetBundle.LoadAsset<GameObject>(path_0);

                        //b.SetActive(false);
                        b.transform.SetParent(unitParent);
                        UnitCtrl unitCtrl = b.GetComponent<UnitCtrl>();
                        if (isplayer)
                        {
                            playerUnitCtrl.Add(unitCtrl);
                            playerUnitCtrlDic.Add(unitid, unitCtrl);
                        }
                        else
                        {
                            enemyUnitCtrl.Add(unitCtrl);
                            enemyUnitCtrlDic.Add(unitid, unitCtrl);
                        }
                        CreateUnitSpine(data,idx, isplayer, BattleDefine.GetUnitRespawnPos(idx, !isplayer), null);
                    }
               // }
                //catch (System.Exception e)
                //{
                //    Debug.LogError("load " + path_0 + " failed beacuse " + e.Message);
                //}
            }
            ab.Unload(false);
            finishAction?.Invoke();

        }
        public void LoadCharacterPrefabImmediately(PCRCaculator.UnitData data, int idx, bool isplayer, Action finishAction)
        {
            int unitid = data.unitId;
            bool useSkillEffects = false;
            //if (!ignoreEffects)
            //{
            //    useSkillEffects = LoadSkillEffectPrefabs(unitid);
            //}
            //GameObject b = Instantiate(battleUnitPrefab);
            GameObject prefab = ABExTool.LoadUnitPrefab(unitid);
            GameObject b;
            if (prefab != null)
                b = Instantiate(ABExTool.LoadUnitPrefab(unitid));
            else
            {
                b = Instantiate(battleUnitPrefab);
            }
            b.transform.SetParent(isplayer?unitParent:enemyParent);
            b.name = "rotate_center";

            UnitCtrl unitCtrl = b.GetComponent<UnitCtrl>();
            //gameObject.name = "rotate_center";
            // ISSUE: reference to a compiler-generated field
            unitCtrl.RotateCenter = b.transform;
            unitCtrl.enemyId = data.enemyid;
            if (isplayer)
            {
                playerUnitCtrl.Add(unitCtrl);
                playerUnitCtrlDic.Add(unitid, unitCtrl);
            }
            else
            {
                enemyUnitCtrl.Add(unitCtrl);
                enemyUnitCtrlDic.Add(unitid, unitCtrl);
            }
            if (tempData.isGuildBattle && !isplayer)
            {
                unitCtrl.UnitName = tempData.guildEnemy.FirstOrDefault(e => e.unit_id == unitCtrl.UnitId)?.name ??
                                    "BOSS";
            }
            else
            {
                /*if (PCRCaculator.MainManager.Instance.UnitName_cn.TryGetValue(unitid, out string name_cn))
                {
                    unitCtrl.UnitName = name_cn;
                }
                else if (PCRCaculator.MainManager.Instance.UnitRarityDic.TryGetValue(unitid, out var rarityData))
                {
                    unitCtrl.UnitName = rarityData.unitName;
                }*/
                unitCtrl.UnitName = MainManager.Instance.GetUnitNickName(data.unitId);
            }
            unitCtrl.UnitNameEx = unitCtrl.UnitName;
            unitCtrl.UnitName = (isplayer ? "<color=#FF0000>" : "<color=#0024FF>") + unitCtrl.UnitName + "</color>";
            unitCtrl.posIdx = idx;
            var actionController = b.GetComponent<UnitActionController>();
            actionController.LoadActionControllerData(unitid,prefab!=null);
           //  actionController.UseSkillEffect = useSkillEffects;
            CreateUnitSpine(data, idx, isplayer, BattleDefine.GetUnitRespawnPos(idx, !isplayer), null);
            finishAction?.Invoke();

        }

        private Dictionary<int, int> unitCount = new Dictionary<int, int>();
        public UnitCtrl LoadSummonPrefabImmediately(SummonData summonData)
        {
            int unitid = summonData.SummonId;
            if (unitid >= 999999)
                unitid = GuildManager.EnemyDataDic[unitid].unit_id;
            var a = ABExTool.LoadUnitPrefab(unitid);
            if (a == null)
            {
                unitid = unitid / 100 * 100;
                GuildManager.EnemyDataDic[summonData.SummonId].unit_id = unitid;
                a = ABExTool.LoadUnitPrefab(unitid);
            }
            GameObject b = Instantiate(a);
            b.transform.SetParent(unitParent);
            b.name = unitid + "(clone)";
            UnitCtrl unitCtrl = b.GetComponent<UnitCtrl>();
            unitCount.TryGetValue(unitid, out var val);
            unitCount[unitid] = val + 1;
            unitCtrl.UnitName = mainManager.GetUnitNickName(unitid) + $"[召唤物{val+1}]";
            unitCtrl.UnitName = ("<color=#C080FF>") + unitCtrl.UnitName + "</color>";
            unitCtrl.UnitNameEx = unitCtrl.UnitName;
            var actionController = b.GetComponent<UnitActionController>();
            actionController.LoadActionControllerData(unitid);
            bool useSkillEffects = false;
            //if (!ignoreEffects)
            //{
           //     useSkillEffects = LoadSkillEffectPrefabs(unitid);
           // }
            //actionController.UseSkillEffect = useSkillEffects;
            return unitCtrl;
        }
        public static void CreateSummonSpine(SummonData summonData,UnitCtrl summonUnitCtrl, GameObject exitObj,
          Action<BattleSpineController> _callback = null)
        {
            int unitid = summonData.SummonId;
            if (unitid >= 999999)
                unitid = GuildManager.EnemyDataDic[unitid].unit_id;
            int prefabID = ABExTool.GetPrefabId(unitid);
            //SkeletonDataAsset dataAsset = ScriptableObject.CreateInstance<SkeletonDataAsset>();
            //dataAsset = Resources.Load<SkeletonDataAsset>("Unit/" + prefabID + "/" + prefabID + "_SkeletonData");
            //if (dataAsset == null)
            //{
            var dataAsset = SpineCreator.Instance.Createskeletondata(UnitUtility.GetSkinId(prefabID, summonData.ConsiderEquipmentAndBonus ? 3 : 0), SPINE_SCALE, true);
            //}
            //var sa = SkeletonAnimation.NewSkeletonAnimationGameObject(dataAsset); // Spawn a new SkeletonAnimation GameObject.
            BattleSpineController battleSpineController = BattleSpineController.LoadAddedSkeletonAnimation(dataAsset,exitObj);
            battleSpineController.Initialize(false);
            SpineResourceInfo info = ResourceDefineSpine.GetResource(eSpineType.SD_NORMAL_BOUND);
            SpineResourceSet resourceSet = new SpineResourceSet(info, prefabID, 0, 0);
            resourceSet.Skelton = dataAsset;
            battleSpineController.Create(resourceSet);
            if (!BattleManager.Instance.skipping)
                SetSummonUI(summonUnitCtrl);
            _callback?.Invoke(battleSpineController);
            if (!BattleManager.Instance.skipping)
                GuildCalculator.Instance.AddSummonUnit(summonUnitCtrl);

        }
        public static void CreateModeChangeSpine(eSpineType _spineType,
          int _unitId,
          int _rarity,
          Transform _transform,
          Action<BattleSpineController> _callback = null)
        {
            int skinId = UnitUtility.GetSkinId(_spineType, _unitId, _rarity);
            int num = 0;

            //GameObject a = Instantiate(Instance.emptyObject);
            //a.transform.SetParent(_transform, false);
            //SkeletonDataAsset dataAsset = ScriptableObject.CreateInstance<SkeletonDataAsset>();
            int unitid = _unitId;
            if (unitid >= 999999)
                unitid = GuildManager.EnemyDataDic[unitid].unit_id;
            int prefabID = ABExTool.GetPrefabId(unitid);
            //dataAsset = Resources.Load<SkeletonDataAsset>("Unit/" + _unitId + "_1/" + (_unitId+30) + "_1_SkeletonData");
            //if (dataAsset == null)
            //{
            var dataAsset = SpineCreator.Instance.Createskeletondata(prefabID, SPINE_SCALE, true, $"spine_sdmodechange_{skinId}_1.unity3d", $"{skinId}_1");
            //}
            //var sa = SkeletonAnimation.NewSkeletonAnimationGameObject(dataAsset); // Spawn a new SkeletonAnimation GameObject.
            BattleSpineController battleSpineController = BattleSpineController.LoadNewSkeletonAnimationGameObject(dataAsset);
            battleSpineController.Initialize(false);
            SpineResourceInfo info = ResourceDefineSpine.GetResource(eSpineType.SD_NORMAL_BOUND);
            SpineResourceSet resourceSet = new SpineResourceSet(info, _unitId, 0, 0);
            resourceSet.Skelton = dataAsset;
            battleSpineController.Create(resourceSet);
            battleSpineController.gameObject.transform.SetParent(_transform, false);
            _callback?.Invoke(battleSpineController);
            /*int skinId = UnitUtility.GetSkinId(_spineType, _unitId, _rarity);
            SpineResourceLoader.Load(_spineType, skinId, skinId, _enemyColor, _transform, (Action<SpineResourceSet>) (_resourceSpineSet =>
            {
              BattleSpineController component = _resourceSpineSet.Controller.GetComponent<BattleSpineController>();
              component.Create(_resourceSpineSet);
              _callback.Call<BattleSpineController>(component);
            }));*/
        }
        private bool LoadSkillEffectPrefabs(int unitid)
        {
            List<string> neededPrefab = new List<string>();
            bool flag = mainManager.FirearmData.GetAddLoadPrefabNames(unitid, neededPrefab);
            if (flag)
            {
                foreach (string name in neededPrefab)
                {
                    List<GameObject> ins = ABExTool.GetAllAssetBundleByName<GameObject>("all_" + name + ".unity3d", "prefab");
                    if (ins != null || ins.Count == 0)
                    {
                        //GameObject a = Instantiate(ins);
                        //a.SetActive(false);
                    }
                    else
                    {
                        flag = false;
                        Debug.LogError(unitid + "的通用弹道数据丢失！");
                    }
                }
                GameObject ins2 = ABExTool.GetAssetBundleByName<GameObject>("all_battleunit_" + unitid + ".unity3d", "prefab", unitid > 200000);
                if (ins2 == null && unitid > 200000)
                {
                    ins2 = ABExTool.GetAssetBundleByName<GameObject>("all_battleunit_" + (unitid / 100 * 100 + 99) + ".unity3d", "prefab", true);
                }
                if (ins2 != null)
                {
                    //GameObject b = Instantiate(ins2);
                    //b.SetActive(false);
                }
                else
                {
                    flag = false;
                    Debug.LogError(unitid + "的特效数据丢失！");
                }
            }
            return flag;
        }
        /*
        public static void ResetSkillEffects(UnitActionController actionController)
        {
            int unitid = actionController.Owner.unitData.unitId;
            TextAsset text_0 = Resources.Load<TextAsset>("unitPrefabDatas/UNIT_" + unitid);
            if (text_0 != null && text_0.text != "")
            {
                string json_0 = text_0.text;
                PCRCaculator.UnitPrefabData prefabData = JsonConvert.DeserializeObject<PCRCaculator.UnitPrefabData>(json_0);
                //UseDefaultDelay = prefabData.UnitActionControllerData.UseDefaultDelay;
                //AttackDetail = prefabData.UnitActionControllerData.AttackDetail;
                Action<List<NormalSkillEffect>, string> action = (a, b) => {
                    if (prefabData.unitFirearmDatas.ContainsKey(b))
                    {
                        List<FirearmCtrlData> firearmCtrlDatas = prefabData.unitFirearmDatas[b];
                        for (int i = 0; i < a.Count; i++)
                        {
                            if (i < firearmCtrlDatas.Count)
                            {
                                a[i].Prefab = Instance.prefab2;
                                a[i].PrefabLeft = Instance.prefab2;
                                a[i].PrefabData = firearmCtrlDatas[i];
                            }
                            else
                            {
                                a[i].Prefab = Instance.prefab1;
                                a[i].PrefabLeft = Instance.prefab1;
                            }
                        }
                    }
                    else
                    {
                        foreach(var p in a)
                        {
                            p.Prefab = Instance.prefab1;
                            p.PrefabLeft = Instance.prefab1;
                        }
                    }

                };
                action(actionController.Attack.SkillEffects, "attack");
                action(actionController.UnionBurstList[0].SkillEffects, "skill0");
                if (actionController.MainSkillList.Count >= 1)
                {
                    action(actionController.MainSkillList[0].SkillEffects, "skill1");
                }
                if (actionController.MainSkillList.Count >= 2)
                {
                    action(actionController.MainSkillList[1].SkillEffects, "skill2");
                }

            }

        }*/
        public void OnBattleFinished(int result)//1-lose,2-timeover
        {
            if (!mainManager.AutoCalculatorData.isFinish)
            {
                mainManager.AutoCalculatorData.resultDatas.Add(
                  GuildCalculator.Instance.GetOnceResultData( mainManager.AutoCalculatorData.execedTime + 1));
                StartCoroutine(BackToTitle());
            }
            else if (tempData.isGuildBattle)
            {
                GuildCalculator.Instance.OnBattleFinished(result, BattleHeaderController.CurrentFrameCount);
            }
        }
        private IEnumerator BackToTitle()
        {
            yield return new WaitForSecondsRealtime(1.5f);
            BattleUIManager.Instance.ExitButton2();
        }

    }
    public class ExceptNGUIRoot
    {
        public static Transform Transform => MyGameCtrl.Instance.effectParent;

    }
    public class TempData
    {
        public List<List<UnitParameter>> AllUnitParameters;
        public List<UnitParameter> PlayerParameters;
        public List<UnitParameter> EnemyParameters;
        public List<List<float>> UBExecTimeList;
        public int tryCount = 30;
        public bool isGuildBattle;
        public bool isGuildEnemyViolent;
        public List<PCRCaculator.UnitData> playerList;
        public List<PCRCaculator.UnitData> enemyList;
        public List<EnemyData> guildEnemy;
        public Dictionary<int, EnemyData> MPartsDataDic;
        public MasterEnemyMParts.EnemyMParts mParts;
        //public bool UseFixedRandomSeed;
        //public int RandomSeed;
        public GuildSettingData SettingData;
        public Dictionary<int, BaseData> playerUnitBaseDataDic;
        public GuildRandomData randomData;
        public bool skipping;

        public void CreateAllUnitParameters(GuildPlayerGroupData.LogBarrierType useLogBarrier = GuildPlayerGroupData.LogBarrierType.NoBarrier)
        {
            AllUnitParameters = new List<List<UnitParameter>>();
            PlayerParameters = new List<UnitParameter>();
            EnemyParameters = new List<UnitParameter>();
            foreach(PCRCaculator.UnitData unitData in playerList)
            {
                UnitParameter parameter = CreateUnitParameter(unitData);
                PlayerParameters.Add(parameter);
            }
            if (!isGuildBattle)
            {
                foreach (PCRCaculator.UnitData unitData in enemyList)
                {
                    UnitParameter parameter = CreateUnitParameter(unitData);
                    EnemyParameters.Add(parameter);
                }
            }
            else
            {
                foreach (var enemy in guildEnemy)
                {
                    UnitParameter parameter = CreateUnitParameter(enemy,useLogBarrier);
                    EnemyParameters.Add(parameter);
                }
            }
            AllUnitParameters.Add(PlayerParameters);
            AllUnitParameters.Add(EnemyParameters);
        }
        public static UnitParameter CreateUnitParameter(PCRCaculator.UnitData unitData)
        {
            UnitRarityData rarityData = MainManager.Instance.UnitRarityDic[unitData.unitId];
            //int[] skillList = rarityData.GetSkillList(unitData);
            //int[] skillList = rarityData.GetFullSkillList();
            UnitSkillData skillData = rarityData.skillData;
            int[] skillLvEv = unitData.GetEvlotionSkillLv();
            UnitDetailData detailData = rarityData.detailData;
            return new UnitParameter(
                new UnitData(unitData.unitId, new DateTime(), 1, unitData.rarity, unitData.rarity, unitData.level, 0, ePromotionLevel.Bronze,
                    new List<SkillLevelInfo> { new SkillLevelInfo(skillData.UB, unitData.skillLevel[0], 0), new SkillLevelInfo(skillData.UB_ev, skillLvEv[0], 0) },
                    new List<SkillLevelInfo> { 
                        new SkillLevelInfo(skillData.skill_1, unitData.rank < 2 ? 0 : unitData.skillLevel[1], 0), 
                        new SkillLevelInfo(skillData.skill_2, unitData.rank < 4 ? 0 : unitData.skillLevel[2], 0) ,
                        new SkillLevelInfo(skillData.skill_1_ev, skillLvEv[1], 0), 
                        new SkillLevelInfo(skillData.skill_2_ev, skillLvEv[2], 0)
                    },
                    new List<SkillLevelInfo> { new SkillLevelInfo(skillData.EXskill, unitData.rank < 7 ? 0 : unitData.skillLevel[3], 0) },
                    new List<SkillLevelInfo>(), 0),
                new UnitDataForView(),
                new MasterUnitData.UnitData(unitData.unitId, detailData.name, "?", unitData.unitId, 0, detailData.minrarity, detailData.motionType, detailData.seType,
                detailData.moveSpeed, detailData.searchAreaWidth, detailData.atkType, detailData.normalAtkCastTime, 0, 0, 0, 0, detailData.guildId),
                new MasterUnitSkillData.UnitSkillData(
                    unit_id: unitData.unitId,
                    union_burst: skillData.UB,
                    main_skill_1: skillData.skill_1,
                    main_skill_2: skillData.skill_2,
                    main_skill_3: skillData.skill_3,
                    main_skill_4: skillData.skill_4,
                    main_skill_5: skillData.skill_5,
                    main_skill_6: skillData.skill_6,
                    main_skill_7: skillData.skill_7,
                    main_skill_8: skillData.skill_8,
                    main_skill_9: skillData.skill_9,
                    main_skill_10: skillData.skill_10,
                    ex_skill_1: skillData.EXskill,
                    ex_skill_2: skillData.EXskill_2,
                    ex_skill_3: skillData.EXskill_3,
                    ex_skill_4: skillData.EXskill_4,
                    ex_skill_5: skillData.EXskill_5,
                    sp_skill_1: skillData.SPskill_1,
                    sp_skill_2: skillData.SPskill_2,
                    sp_skill_3: skillData.SPskill_3,
                    sp_skill_4: skillData.SPskill_4,
                    sp_skill_5: skillData.SPskill_5,
                    sp_skill_evolution_1:skillData.SPskill_1_ev,
                    sp_skill_evolution_2:skillData.SPskill_2_ev,
                union_burst_evolution: skillData.UB_ev,
                main_skill_evolution_1: skillData.skill_1_ev,
                main_skill_evolution_2: skillData.skill_2_ev,
                ex_skill_evolution_1: skillData.EXskill_ev,
                sp_union_burst:skillData.SPUB)

                ) ;
        }
        public static UnitParameter CreateUnitParameter(EnemyData enemyData, GuildPlayerGroupData.LogBarrierType useLogBarrier)
        {
            List<SkillLevelInfo> mainSkillLevelInfo = new List<SkillLevelInfo>();
            for(int i = 0; i < enemyData.main_skill_lvs.Count; i++)
            {
                mainSkillLevelInfo.Add(new SkillLevelInfo(enemyData.skillData.MainSkills[i], enemyData.main_skill_lvs[i], 0));
            }
            return new UnitParameter(
                new UnitData(enemyData.unit_id, new DateTime(), 1, enemyData.rarity, enemyData.rarity, enemyData.level, 0, ePromotionLevel.Bronze,
                    new List<SkillLevelInfo> { new SkillLevelInfo(enemyData.skillData.UB, enemyData.union_burst_level, 0) },
                     mainSkillLevelInfo,       
                     new List<SkillLevelInfo>(),
                    new List<SkillLevelInfo>(),
                    enemyData.resist_status_id),
                new UnitDataForView(),
                new MasterUnitData.UnitData(enemyData.unit_id, enemyData.detailData.unit_name, "?", enemyData.detailData.unit_id, 0, 1, enemyData.detailData.motion_type, enemyData.detailData.se_type,
                enemyData.detailData.move_speed, enemyData.detailData.search_area_width, enemyData.detailData.atk_type, enemyData.detailData.normal_atk_cast_time),
                new MasterUnitSkillData.UnitSkillData(unit_id: enemyData.unit_id, union_burst: enemyData.skillData.UB,
                main_skill_1: JudgeAndSetSkillByID(enemyData.skillData.MainSkills[0],useLogBarrier),
                main_skill_2: JudgeAndSetSkillByID(enemyData.skillData.MainSkills[1], useLogBarrier),
                main_skill_3: JudgeAndSetSkillByID(enemyData.skillData.MainSkills[2], useLogBarrier),
                main_skill_4: JudgeAndSetSkillByID(enemyData.skillData.MainSkills[3], useLogBarrier),
                main_skill_5: JudgeAndSetSkillByID(enemyData.skillData.MainSkills[4], useLogBarrier),
                main_skill_6: JudgeAndSetSkillByID(enemyData.skillData.MainSkills[5], useLogBarrier),
                main_skill_7: JudgeAndSetSkillByID(enemyData.skillData.MainSkills[6], useLogBarrier),
                main_skill_8: JudgeAndSetSkillByID(enemyData.skillData.MainSkills[7], useLogBarrier),
                main_skill_9: JudgeAndSetSkillByID(enemyData.skillData.MainSkills[8], useLogBarrier),
                main_skill_10: JudgeAndSetSkillByID(enemyData.skillData.MainSkills[9], useLogBarrier)
                ),enemyData);
        }
        private static int JudgeAndSetSkillByID(int skillid, GuildPlayerGroupData.LogBarrierType enableLogBarrier)
        {
            if (skillid == 0 || enableLogBarrier != GuildPlayerGroupData.LogBarrierType.NoBarrier)
                return skillid;
            var skillData = MainManager.Instance.SkillDataDic[skillid];
            foreach(int actionid in skillData.skillactions)
            {
                if (actionid > 0)
                {
                    var actionData = MainManager.Instance.SkillActionDic[actionid];
                    if (actionData.type == 73)
                        return 0;
                }
            }
            return skillid;
        }

    }
    [Serializable]
    public class UnitCtrlData
    {
        public int unitid;
        public float ShowTitleDelay = 0.5f;
        public float UnitAppearDelay = 3f;
        public float BossAppearDelay = 0.8f;
        public float BattleCameraSize = 1f;
        public float Scale = 1.35f;
        public float BossDeltaX;
        public float BossDeltaY;
        public float AllUnitCenter;
        public float BossBodyWidthOffset;
        public string SummonTargetAttachmentName = "";
        public string SummonAppliedAttachmentName = "";
        public bool IsGameStartDepthBack;
        public bool BossSortIsBack;
        public bool DisableFlash;
        public bool IsForceLeftDir;
        public List<PartsData> BossPartsList = new List<PartsData>();
        public bool UseTargetCursorOver;


        public UnitCtrlData() { }
        public UnitCtrlData(int unitid, float showTitleDelay, float unitAppearDelay, float bossAppearDelay, float battleCameraSize, float scale, float bossDeltaX, float bossDeltaY, float allUnitCenter, float bossBodyWidthOffset, string summonTargetAttachmentName, string summonAppliedAttachmentName, bool isGameStartDepthBack, bool bossSortIsBack, bool disableFlash, bool isForceLeftDir, List<PartsData> bossPartsList, bool useTargetCursorOver)
        {
            this.unitid = unitid;
            ShowTitleDelay = showTitleDelay;
            UnitAppearDelay = unitAppearDelay;
            BossAppearDelay = bossAppearDelay;
            BattleCameraSize = battleCameraSize;
            Scale = scale;
            BossDeltaX = bossDeltaX;
            BossDeltaY = bossDeltaY;
            AllUnitCenter = allUnitCenter;
            BossBodyWidthOffset = bossBodyWidthOffset;
            SummonTargetAttachmentName = summonTargetAttachmentName;
            SummonAppliedAttachmentName = summonAppliedAttachmentName;
            IsGameStartDepthBack = isGameStartDepthBack;
            BossSortIsBack = bossSortIsBack;
            DisableFlash = disableFlash;
            IsForceLeftDir = isForceLeftDir;
            BossPartsList = bossPartsList;
            UseTargetCursorOver = useTargetCursorOver;
        }
    }


}
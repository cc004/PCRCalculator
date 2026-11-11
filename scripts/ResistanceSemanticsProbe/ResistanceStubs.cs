using System;using System.Collections.Generic;
public enum eActionType { RATIO_DAMAGE=46, OTHER=8 }
public enum eMissLogType { DODGE_ATTACK }
public class Skill {}
public class UnitCtrl {public bool Force; public bool IsForceHit(eActionType a,int d)=>Force;public void SetMissAtk(UnitCtrl source,eMissLogType type,BasePartsData _parts=null){} }
public class BasePartsData {public UnitCtrl Owner=new();protected int current;protected Dictionary<int,Dictionary<eActionType,Dictionary<int,int>>> abnormalResistIdDictionary=new();protected Dictionary<eActionType,Dictionary<int,int>> abnormalResistStatusDictionary=>abnormalResistIdDictionary[current];public Dictionary<int,int> Ratio=>abnormalResistStatusDictionary.GetValueOrDefault(eActionType.RATIO_DAMAGE);}
public static class DictExt {public static void AddIfNoContains<K,V>(this Dictionary<K,V> d,K k,V v){if(!d.ContainsKey(k))d.Add(k,v);}}
public class Row {public int ailment_id,ailment_action,ailment_detail_1;}
public class MainManager {public static MainManager Instance=new();public Dictionary<int,Row>AilmentDataDic=new();}
public class ResistData {public static int[] Values;public int[] ailments;public ResistData(int id){ailments=Values;}}
public class RandomData {public RandomData(UnitCtrl s,UnitCtrl t,int action,int type,double p){} }
public static class BattleManager {public static UnityRandom rng=new();public static int Count;public static float? Fixed;public static float Random(float min,float max,ref RandomData data){Count++;int x=rng.Range((int)(min*1000.0),(int)(max*1000.0));return Fixed??x/1000f;}}
public class MyGameCtrl {public static MyGameCtrl Instance=new();public Temp tempData=new();}
public class Temp {public RandomSettings randomData=new();}
public class RandomSettings {public bool Override; public float Value;public bool TryJudgeRandomSpecialSetting(UnitCtrl s,UnitCtrl t,Skill skill,eActionType type,int frame,out float value){value=Value;return Override;}}
public static class BattleHeaderController{public static int CurrentFrameCount=0;}

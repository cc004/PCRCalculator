using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using Spine.Unity;
using Elements;
using System;
namespace PCRCaculator.Battle
{


    public class LoadUnity3d : MonoBehaviour
    {      // Use this for initialization 
        public GameObject a;
        public GameObject b;
        public int numAdd;
        public List<int> charNum;
        public List<string> path;
        public List<string> dependpath;
        public bool loadAllDepend;
        public bool loadAllUnit;
        public bool destroyImmediate;
        public bool CreateJson;
        public bool LoadEnemy;
        public bool LoadSpecial;
        public bool LoadSpineIm;
        List<int> showUnitIDs = new List<int>();
        [TextArea]
        public string path1;
        public bool createperferb;

        string dependPath = "all_fxsk_";
        string unit_1 = "all_battleunit_";
        string unit_2 = "all_battleunitprefab_";
        string battleManagerStr = "bdl_battlemanager";
        string battleSpineController = "bdl_battlespinecontroller";
        string spinePath = "spine_sdnormal_";//spine_sdnormal_100111

        public List<GameObject> unitEffects = new List<GameObject>();
        public GameObject unitprefab;

        AssetBundleManifest manifest;
        private Dictionary<eResourceId, ResourceDefineRecord> resourceDefineAllDictionary;
        private Dictionary<eBundleId, BundleDefineRecord> bundleDefineAllDictionary;

        void Start()
        {
            //LoadManifest();
            if (LoadSpecial)
            {
                StartCoroutine(LoadSpecial_0(battleManagerStr));
                StartCoroutine(LoadSpecial_0(battleSpineController));
                return;
            }
            for(int i=0;i<charNum.Count;i++)
            {
                charNum[i] += numAdd;
            }
            for (int i = 100101; i <= 106301; i++)
            {
                showUnitIDs.Add(i);
            }
            showUnitIDs.Remove(101901);
            showUnitIDs.Remove(102401);
            showUnitIDs.Remove(103501);
            showUnitIDs.Remove(103901);
            showUnitIDs.Remove(104101);
            showUnitIDs.Remove(106101);
            showUnitIDs.Remove(106201);
            showUnitIDs.Add(107101);
            for (int i = 107501; i <= 109301; i++)
            {
                showUnitIDs.Add(i);
            }

            //LoadManiFest();
            if (loadAllDepend)
            {
                List<string> vs = new List<string>();
                for (int i = 1; i < 121; i++)
                {
                    string num = "0";
                    if (i < 10) { num = "000" + i; }
                    else if (i < 100) { num = "00" + i; }
                    else
                    {
                        num += i;
                    }
                    vs.Add(dependPath + num);
                    //StartCoroutine(LoadScene(vs, 0));
                }
                vs.Add("all_fxsk_0500");
                vs.Add("all_fxsk_0501");
                vs.Add("all_fxsk_0600");
                for (int i = 0; i < vs.Count; i++)
                    StartCoroutine(LoadScene(vs, i));

            }
            else
            {
                for (int i = 0; i < dependpath.Count; i++)
                {
                    StartCoroutine(LoadScene(dependpath, i));

                }
            }
            if (loadAllUnit)
            {
                foreach(int unitid in showUnitIDs)
                {
                    StartCoroutine(LoadCharacterPrefab(unitid));
                }
            }
            else if(LoadEnemy)
            {
                for (int i = 0; i < charNum.Count; i++)
                {
                    //StartCoroutine(LoadScene(path,i));      
                    StartCoroutine(LoadCharacterPrefab(charNum[i]));
                }
            }
            else
            {
                for (int i = 0; i < charNum.Count; i++)
                {
                    //StartCoroutine(LoadScene(path,i));      
                    StartCoroutine(LoadCharacterPrefab(charNum[i] * 100 + 100001));
                    if(LoadSpineIm)
                    LoadSpine(charNum[i] * 100 + 100011);
                }
            }
        }

        IEnumerator LoadScene(List<string> path, int i)
        {         //文件路径，也就是我们打包的那个         
            WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/AB/" + path[i] + ".unity3d");
            yield return www;
            //Instantiate(www.assetBundle.mainAsset);
            /*for(int j = 0; j < www.assetBundle.GetAllAssetNames().Length; j++)
            {
                Debug.Log(www.assetBundle.GetAllAssetNames()[j]);
            }*/
            //a.transform.SetParent(b.transform);
            foreach (string path_0 in www.assetBundle.GetAllAssetNames())
            {
                //if (www.assetBundle.Contains(path1))
                //{
                try
                {
                    if (path_0.Contains(".mat"))
                    {
                        //www.assetBundle.LoadAsset<Material>(path);
                    }
                    else if (path_0.Contains(".prefab"))
                    {
                        //Instantiate(www.assetBundle.LoadAsset<GameObject>(path));
                        //www.assetBundle.LoadAssetWithSubAssets<GameObject>(path);
                        GameObject g = Instantiate(www.assetBundle.LoadAsset<GameObject>(path_0));
                        g.transform.SetParent(a.transform);
                        g.SetActive(false);

                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("load " + path_0 + " failed beacuse " + e.Message);
                }
                //}
                //else
                //{
                //    Debug.Log("fail!");
                //}
            }
            yield break;
        }
        IEnumerator LoadSpecial_0(string path)
        {         //文件路径，也就是我们打包的那个         
            WWW www = new WWW("file:///" + Application.dataPath + "/AB/" + path + ".unity3d");
            yield return www;
            //Instantiate(www.assetBundle.mainAsset);
            /*for(int j = 0; j < www.assetBundle.GetAllAssetNames().Length; j++)
            {
                Debug.Log(www.assetBundle.GetAllAssetNames()[j]);
            }*/
            //a.transform.SetParent(b.transform);
            foreach (string path_0 in www.assetBundle.GetAllAssetNames())
            {
                //if (www.assetBundle.Contains(path1))
                //{
                try
                {
                    if (path_0.Contains(".mat"))
                    {
                        //www.assetBundle.LoadAsset<Material>(path);
                    }
                    else if (path_0.Contains(".prefab"))
                    {
                        //Instantiate(www.assetBundle.LoadAsset<GameObject>(path));
                        //www.assetBundle.LoadAssetWithSubAssets<GameObject>(path);
                        string[] depends = manifest.GetAllDependencies(path_0);
                        foreach(string depend in depends)
                        {
                            Debug.Log("依赖项：" + depend);
                        }
                        GameObject g = Instantiate(www.assetBundle.LoadAsset<GameObject>(path_0));
                        g.transform.SetParent(a.transform);
                        g.SetActive(false);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("load " + path_0 + " failed beacuse " + e.Message);
                }
                //}
                //else
                //{
                //    Debug.Log("fail!");
                //}
            }
            yield break;
        }

        public IEnumerator LoadCharacterPrefab(int unitid)
        {
            yield return new WaitForSeconds(1f);
            WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/AB/" + unit_1 + unitid + ".unity3d");
            unitEffects.Clear();
            if (www.assetBundle != null)
            {
                foreach (string path_0 in www.assetBundle.GetAllAssetNames())
                {
                    try
                    {
                        if (path_0.Contains(".prefab"))
                        {
                            GameObject b = Instantiate(www.assetBundle.LoadAsset<GameObject>(path_0));
                            unitEffects.Add(b);
                            b.SetActive(false);
                        }
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError("load " + path_0 + " failed beacuse " + e.Message);
                    }
                }
            }
            WWW www_2 = new WWW("file:///" + Application.streamingAssetsPath + "/AB/" + unit_2 + unitid + ".unity3d");
            if (www_2.assetBundle != null)
            {
                foreach (string path_0 in www_2.assetBundle.GetAllAssetNames())
                {
                    try
                    {
                        if (path_0.Contains(".prefab"))
                        {
                            GameObject a = Instantiate(www_2.assetBundle.LoadAsset<GameObject>(path_0));
                            //CreateSpine(unitid,a.transform);
                            if (CreateJson)
                            {
                                a.GetComponent<Elements.UnitActionController>().SaveDataToJson();
                                //Debug.LogError("鸽了！");
                            }
                            if (destroyImmediate)
                            {
                                Destroy(a);
                                foreach (GameObject p in unitEffects)
                                {
                                    Destroy(p);
                                }
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError("load " + path_0 + " failed beacuse " + e.Message);
                    }
                }
                //foreach(string path_1 in www_2.assetBundle.)
            }

        }
        private void LoadSpine(int unitid)
        {
            WWW www = new WWW("file:///" + Application.dataPath + "/AB/" + spinePath + unitid + ".unity3d");
            SkeletonDataAsset skeletonData = null;
            foreach (string path_0 in www.assetBundle.GetAllAssetNames())
            {
                if (path_0.Contains(".png"))
                {
                    www.assetBundle.LoadAsset<Texture2D>(path_0);
                }
                else if (path_0.Contains("material"))
                {
                    www.assetBundle.LoadAsset<Material>(path_0);
                }
                else if (path_0.Contains("atlas.txt"))
                {
                    www.assetBundle.LoadAsset<TextAsset>(path_0);
                }
                else if (path_0.Contains("atlas.asset"))
                {
                    www.assetBundle.LoadAsset<AtlasAsset>(path_0);
                }
                else if (path_0.Contains("skeletondata.asset"))
                {
                   skeletonData = www.assetBundle.LoadAsset<SkeletonDataAsset>(path_0);
                }
            }
            BattleSpineController battleSpineController = BattleSpineController.LoadNewSkeletonAnimationGameObject(skeletonData);
            battleSpineController.Initialize(false);

        }
        private void LoadManifest()
        {
            WWW www = new WWW("file:///" + Application.dataPath + "/AB/bdl_resourcedefine.unity3d");

            ResourceDefineScriptableObjectInBdl scriptableObjectInBdl = null;// = www.assetBundle.LoadAsset<ResourceDefineScriptableObjectInBdl>("Bdl/ResourceDefine/ResourceDefine");
            foreach (string path_0 in www.assetBundle.GetAllAssetNames())
                scriptableObjectInBdl = www.assetBundle.LoadAsset<ResourceDefineScriptableObjectInBdl>(path_0);
            this.resourceDefineAllDictionary = new Dictionary<eResourceId, ResourceDefineRecord>();
            this.bundleDefineAllDictionary = new Dictionary<eBundleId, BundleDefineRecord>();
            scriptableObjectInBdl.ResourceDefineArray.ForEach<ResourceDefineRecord>((Action<ResourceDefineRecord>)(_srcRecord => this.resourceDefineAllDictionary.Add(_srcRecord.ResourceId, _srcRecord)));
            scriptableObjectInBdl.BundleDefineArray.ForEach<BundleDefineRecord>((Action<BundleDefineRecord>)(_srcRecord => this.bundleDefineAllDictionary.Add(_srcRecord.BundleId, _srcRecord)));
            FindAllPath1(eResourceId.UNIT_BATTLE);
            
        }
        private void FindAllPath1(eResourceId resourceId)
        {
            if (resourceDefineAllDictionary.ContainsKey(resourceId))
            {
                Debug.Log(resourceDefineAllDictionary[resourceId].PathName);
                resourceDefineAllDictionary[resourceId].PreloadResource.ForEach<eResourceId>(a => FindAllPath1(a));
            }
        }
        private void FindAllPath2(eBundleId bundleId)
        {
            if (bundleDefineAllDictionary.ContainsKey(bundleId))
            {
                Debug.Log(bundleDefineAllDictionary[bundleId].PathName);
            }
        }
        //[MenuItem("PCRTools/加载角色特效")]
        public static void LoadEffectPrefabs()
        {
            int unitid = 101201;
            WWW www = new WWW("file:///" + Application.dataPath + "/AB/" + "all_battleunit_" + unitid + ".unity3d");
            foreach (string path_0 in www.assetBundle.GetAllAssetNames())
            {
                Debug.Log(path_0);
                try
                {
                    if (path_0.Contains(".png"))
                    {

                    }

                    if (path_0.Contains(".prefab"))
                    {
                        GameObject b = Instantiate(www.assetBundle.LoadAsset<GameObject>(path_0));
                        b.SetActive(false);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError("load " + path_0 + " failed beacuse " + e.Message);
                }
            }
            www.assetBundle.Unload(false);
        }

        /*private IEnumerator AddBundleDepend(string bundleName)
        {
            var DependeInfo = ABManifestLoader.GetInstance().RetrivalDependce(bundleName);
            foreach (var dependBundleName in DependeInfo)
            {
                _DicABRelation[bundleName].AddDependence(dependBundleName);
                yield return AddBundleRefrence(bundleName, dependBundleName);
            }
        }*/
    }
}
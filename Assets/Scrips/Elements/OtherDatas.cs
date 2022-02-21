using System;
using UnityEngine;

namespace Elements
{
    public class ResourceDefineScriptableObjectInBdl : ScriptableObject
    {
        public ResourceDefineRecord[] ResourceDefineArray;
        public BundleDefineRecord[] BundleDefineArray;
    }
    [Serializable]
    public class ResourceDefineRecord
    {
        [SerializeField]
        public eResourceId ResourceId;
        [SerializeField]
        public string PathName;
        [SerializeField]
        public eResourceDataType Type;
        [SerializeField]
        public eResourceLoadType LoadType;
        [SerializeField]
        public eResourceId[] PreloadResource;
        [SerializeField]
        public eBundleId[] PreloadBundle;

        public ResourceDefineRecord(
          eResourceId _resourceId,
          string _pathName,
          eResourceDataType _dataType,
          eResourceLoadType _loadType = eResourceLoadType.DOWNLOAD,
          eResourceId[] _preloadResource = null,
          eBundleId[] _preloadBundle = null)
        {
            ResourceId = _resourceId;
            PathName = _pathName;
            Type = _dataType;
            LoadType = _loadType;
            PreloadResource = _preloadResource;
            PreloadBundle = _preloadBundle;
        }
    }
    [Serializable]
    public class BundleDefineRecord
    {
        [SerializeField]
        public eBundleId BundleId;
        [SerializeField]
        public string PathName;

        public BundleDefineRecord(eBundleId _bundleId, string _pathName)
        {
            BundleId = _bundleId;
            PathName = _pathName;
        }
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
            this.ResourceId = _resourceId;
            this.PathName = _pathName;
            this.Type = _dataType;
            this.LoadType = _loadType;
            this.PreloadResource = _preloadResource;
            this.PreloadBundle = _preloadBundle;
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
            this.BundleId = _bundleId;
            this.PathName = _pathName;
        }
    }

}


using System;
using System.Collections.Generic;
using poetools.Core;
using poetools.Core.Dictionary;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class PoetoolsRealtimeCsgSettings : ScriptableObject
{
    [Serializable]
    public struct Settings
    {
        public Material material;
        public Tag[] tags;
    }

    public List<Settings> materialToTag;
}

public class PoetoolsRealtimeCsgLogic
{

}

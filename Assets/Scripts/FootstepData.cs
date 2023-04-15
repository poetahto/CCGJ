using System;
using System.Collections.Generic;
using FMODUnity;
using poetools.Core;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu]
    public class FootstepData : ScriptableObject
    {
        [Serializable]
        public struct Data
        {
            public Tag tag;
            public EventReference sound;
        }
        public List<Data> surfaceData;
        public EventReference defaultSound;
    }
}

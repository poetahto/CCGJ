using FMODUnity;
using poetools.Core;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace
{
    public class ParameterTrigger : TriggerEffect
    {
        public string parameterName;
        public float target;

        protected override void HandleCollisionEnter(GameObject obj)
        {
            if (obj.CompareTag("Player") &&
                obj.TryGetComponent(out NetworkObject netObj) && netObj.IsOwner)
            {
                var music = GameObject.Find("Music").GetComponent<StudioEventEmitter>();
                music.SetParameter(parameterName, target);
            }
        }
    }
}

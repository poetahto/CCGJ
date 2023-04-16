using FMODUnity;
using poetools.Core;
using TriInspector;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace
{
    public class Room3WeirdHallway : MonoBehaviour
    {
        private StudioEventEmitter music;
        public TriggerEvents trigger;
        public Transform start;
        public Transform end;
        private float _completion;
        private bool _isWeird;
        public float fogMax = 0.02f;
        public float fogMin;

        private void Start()
        {
            music = GameObject.Find("Music").GetComponent<StudioEventEmitter>();
        }

        private void Update()
        {
            if (TryGetPlayerInHallway(out GameObject player))
            {
                MakeWeird();

                Vector3 progressVector = end.position - start.position;
                var projPos = start.position + Vector3.Project(player.transform.position - start.position, progressVector.normalized);
                _completion = Mathf.Clamp01((projPos - start.position).magnitude / progressVector.magnitude);
                music.SetParameter("Fog", 1 - _completion);
                RenderSettings.fogDensity = Mathf.Lerp(fogMin, fogMax, 1 - _completion);
            }
        }

        [Button]
        public void MakeWeird()
        {
            if (!_isWeird)
            {
                music.SetParameter("Weirdness", 1);
                _isWeird = true;
            }
        }

        private bool TryGetPlayerInHallway(out GameObject player)
        {
            if (trigger.CurrentRigidbodies.Count > 0)
            {
                foreach (var currentRigidbody in trigger.CurrentRigidbodies)
                {
                    if (currentRigidbody.CompareTag("Player") &&
                        currentRigidbody.TryGetComponent(out NetworkObject netObj) && netObj.IsOwner)
                    {
                        player = currentRigidbody.gameObject;
                        return true;
                    }
                }
            }

            player = null;
            return false;
        }
    }
}

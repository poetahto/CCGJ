using System;
using FMODUnity;
using poetools.Core;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace
{
    public class Room3Entrance : MonoBehaviour
    {

        private StudioEventEmitter music;
        public TriggerEvents trigger;
        public Transform start;
        public Transform end;
        private float _completion;
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
                Vector3 progressVector = end.position - start.position;
                var projPos = start.position + Vector3.Project(player.transform.position - start.position, progressVector.normalized);
                Debug.DrawLine(start.position, player.transform.position, Color.red);
                Debug.DrawRay(start.position, progressVector, Color.blue);
                Debug.DrawLine(start.position, projPos, Color.green);
                _completion = Mathf.Clamp01((projPos - start.position).magnitude / progressVector.magnitude);
                music.SetParameter("Fog", _completion);
                RenderSettings.fogDensity = Mathf.Lerp(fogMin, fogMax, _completion);
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

        private void OnGUI()
        {
            GUILayout.Label($"{_completion}");
        }
    }
}

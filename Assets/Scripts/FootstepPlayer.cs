using System;
using System.Collections.Generic;
using FMODUnity;
using poetools.Core;
using poetools.Core.Dictionary;
using UnityEngine;

namespace DefaultNamespace
{
    public class FootstepPlayer : MonoBehaviour
    {
        public FootstepData data;

        private Dictionary<Tag, EventReference> _surfaceSoundLookup;

        [SerializeField]
        [Tooltip("The distance between steps.")]
        private float stepDistance = 1;

        [SerializeField]
        private bool showDebug;

        private GroundCheck _groundCheck;

        // distance walked since last step.
        private float _elapsedDistance;
        private Vector3 _previousPosition = new Vector3(0, 0, 0);

        private void Start()
        {
            _groundCheck = GetComponent<GroundCheck>();
        }

        private void Update()
        {
            if (_groundCheck.IsGrounded)
            {
                // Update elapsedDistance with how far we have moved this frame.
                _elapsedDistance += Vector3.Distance(transform.position, _previousPosition);

                // Compare elapsedDistance to our step distance to see if we walked far enough
                if (_elapsedDistance >= stepDistance)
                {
                    bool playedSound = false;
                    foreach (var surfaceData in data.surfaceData)
                    {
                        if (_groundCheck.ConnectedCollider.gameObject.HasAllTags(surfaceData.tag))
                        {
                            RuntimeManager.PlayOneShotAttached(surfaceData.sound, gameObject);
                            playedSound = true;
                        }
                    }
                    if (!playedSound)
                        RuntimeManager.PlayOneShotAttached(data.defaultSound, gameObject);

                    // If we did, fire the event and reset elapsedDistance to 0
                    _elapsedDistance = 0;
                }

                _previousPosition = transform.position;
            }
        }

        private void OnGUI()
        {
            if (showDebug)
            {
                GUILayout.Label($"elapsedDistance: {_elapsedDistance}");
            }
        }
    }
}

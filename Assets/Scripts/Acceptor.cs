using System;
using System.Collections.Generic;
using poetools.Core;
using poetools.Core.Abstraction;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Acceptor : TriggerEffect
    {
        public List<NetworkObject> validObjects;
        public UnityEvent onAccept;
        public UnityEvent onRelease;

        public bool suckToCenter = true;

        public NetworkObject AcceptedObject { get; private set; }

        protected override void HandleCollisionEnter(GameObject obj)
        {
            if (obj.TryGetComponent(out NetworkObject netObj) && validObjects.Contains(netObj))
            {
                if (suckToCenter)
                {
                    var grav = netObj.GetComponentInChildren<Gravity>();

                    if (grav)
                        grav.enabled = false;
                }

                AcceptedObject = netObj;
                onAccept.Invoke();
            }
        }

        protected override void HandleCollisionExit(GameObject obj)
        {
            if (obj.TryGetComponent(out NetworkObject netObj) && netObj == AcceptedObject)
            {
                if (suckToCenter)
                {
                    var grav = netObj.GetComponentInChildren<Gravity>();

                    if (grav)
                        grav.enabled = true;
                }

                AcceptedObject = null;
                onRelease.Invoke();
            }
        }

        [SerializeField]
        private float movementSpeed = 1;
        [SerializeField]
        private float maxSpeed = 1;

        private void FixedUpdate()
        {
            if (AcceptedObject != null && AcceptedObject.IsOwner && suckToCenter)
            {
                // AcceptedObject.transform.position = Vector3.MoveTowards(AcceptedObject.transform.position,
                //     transform.position, Time.deltaTime);
                var Rigidbody = AcceptedObject.GetComponent<Rigidbody>();

                Vector3 target = transform.position;
                Vector3 current = AcceptedObject.transform.position;
                Vector3 directionToTarget = target - current;

                Rigidbody.velocity = directionToTarget * movementSpeed / Time.fixedDeltaTime;
                Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, maxSpeed);
            }
        }
    }
}

using System;
using poetools.Core;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace
{
    public class ParentingOnTouch : NetworkBehaviour
    {
        public TriggerEvents[] parentTrigger;

        private void OnEnable()
        {
            foreach (var trigger in parentTrigger)
            {
                // trigger.RigidbodyTriggerEnter += HandleRigidbodyEnter;
                // trigger.RigidbodyTriggerExit += HandleRigidbodyExit;
            }
        }

        private void OnDisable()
        {
            foreach (var trigger in parentTrigger)
            {
                // trigger.RigidbodyTriggerEnter -= HandleRigidbodyEnter;
                // trigger.RigidbodyTriggerExit -= HandleRigidbodyExit;
            }
        }

        // private void HandleRigidbodyExit(Rigidbody obj)
        // {
        //     if (obj.CompareTag("Player") && obj.TryGetComponent(out NetworkObject netObj) && netObj.IsOwner)
        //     {
        //         if (!TryGetComponent(out FixedJoint joint))
        //         {
        //             joint = gameObject.AddComponent<FixedJoint>();
        //             joint.breakForce = 1;
        //         }
        //
        //         joint.connectedBody = null;
        //         // DetachServerRpc(netObj);
        //     }
        // }

        // private void HandleRigidbodyEnter(Rigidbody obj)
        // {
        // }

        private void OnCollisionStay(Collision other)
        {
            var obj = other.rigidbody;
            if (obj && obj.CompareTag("Player") && obj.TryGetComponent(out NetworkObject netObj) && netObj.IsOwner && Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                // AttachToServerRpc(netObj, NetworkObject);
                joint.connectedBody = obj;
            }
            else
            {
                joint.connectedBody = null;
            }
        }

        public Joint joint;

        [ServerRpc]
        private void AttachToServerRpc(NetworkObjectReference target, NetworkObjectReference newParent)
        {
            if (target.TryGet(out NetworkObject netObj) && newParent.TryGet(out NetworkObject parent))
                netObj.TrySetParent(parent);
        }

        [ServerRpc]
        private void DetachServerRpc(NetworkObjectReference target)
        {
            if (target.TryGet(out NetworkObject netObj))
                netObj.TryRemoveParent();
        }
    }
}

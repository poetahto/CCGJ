using poetools.Core.Abstraction;
using poetools.player.Player.Interaction;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace
{
    public class PushController : NetworkBehaviour
    {
        [SerializeField]
        private FPSInteractionLogicContainer interactContainer;

        [SerializeField] private float pushRange;
        [SerializeField] private float throwStrength = 5;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (interactContainer.InteractionLogic.TargetObject != null)
                {
                    // throw object
                    if (interactContainer.InteractionLogic.TargetObject.TryGetComponent(out MovableObject mo))
                    {
                        var viewRay = new Ray(interactContainer.viewDirection.position, interactContainer.viewDirection.forward);
                        mo.HandleInteractStop(gameObject);
                        mo.Rigidbody.velocity = viewRay.direction * throwStrength;
                    }
                }
                else TryPushPlayer();
            }
        }

        private void TryPushPlayer()
        {
            var viewRay = new Ray(interactContainer.viewDirection.position, interactContainer.viewDirection.forward);
            Debug.DrawRay(viewRay.origin, viewRay.direction * pushRange, Color.magenta, 5);
            var hits = Physics.RaycastAll(viewRay, pushRange, ~0);
            foreach (RaycastHit hit in hits)
            {
                if (hit.rigidbody && hit.rigidbody.CompareTag("Player"))
                    PushPlayerServerRpc(viewRay.direction * pushStrength, new NetworkObjectReference(hit.rigidbody.GetComponent<NetworkObject>()));
            }
        }

        [SerializeField]
        private float pushStrength = 5;

        [ServerRpc]
        private void PushPlayerServerRpc(Vector3 velocity, NetworkObjectReference target)
        {
            if (target.TryGet(out NetworkObject netObj))
            {
                HandlePushedClientRpc(velocity, target, new ClientRpcParams{Send = new ClientRpcSendParams{TargetClientIds = new []{netObj.OwnerClientId}}});
            }

        }

        [ClientRpc]
        private void HandlePushedClientRpc(Vector3 velocity, NetworkObjectReference target, ClientRpcParams rpcParams = default)
        {
            if (target.TryGet(out NetworkObject netObj) && netObj.IsOwner)
            {
                netObj.GetComponent<IPhysicsComponent>().Velocity += velocity;
            }
        }
    }
}

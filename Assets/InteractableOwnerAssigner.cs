using poetools.player.Player.Interaction;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace
{
    public class InteractableOwnerAssigner : NetworkBehaviour, IInteractable
    {
        public void HandleInteractStart(GameObject grabber)
        {
            ChangedOwnerServerRpc();
        }

        public void HandleInteractStop(GameObject grabber)
        {
        }

        [ServerRpc(RequireOwnership = false)]
        private void ChangedOwnerServerRpc(ServerRpcParams rpcParams = default)
        {
            var interactableNetObj = GetComponentInParent<NetworkObject>();

            if (interactableNetObj)
                interactableNetObj.ChangeOwnership(rpcParams.Receive.SenderClientId);
        }
    }
}

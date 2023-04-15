using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public class CCGJPlayerBody : NetworkBehaviour
    {
        public Behaviour[] ownedOnlyBehaviors;
        public Renderer bodyRenderer;

        public override void OnNetworkSpawn()
        {
            DisableClientView();
        }

        public void Inhabit(ulong client)
        {
            NetworkObject.ChangeOwnership(client);
            InhabitClientRpc(new ClientRpcParams(){Send = new ClientRpcSendParams{TargetClientIds = new []{client}}});
        }

        public void Free()
        {
            FreeClientRpc(new ClientRpcParams(){Send = new ClientRpcSendParams{TargetClientIds = new []{OwnerClientId}}});
            NetworkObject.RemoveOwnership();
        }

        [ClientRpc]
        private void InhabitClientRpc(ClientRpcParams rpcParams = default)
        {
            EnableClientView();
        }

        [ClientRpc]
        private void FreeClientRpc(ClientRpcParams rpcParams = default)
        {
            DisableClientView();
        }

        private void EnableClientView()
        {
            foreach (var behaviour in ownedOnlyBehaviors)
                behaviour.enabled = true;

            bodyRenderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        }

        private void DisableClientView()
        {
            foreach (var behaviour in ownedOnlyBehaviors)
                behaviour.enabled = false;

            bodyRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
        }
    }
}

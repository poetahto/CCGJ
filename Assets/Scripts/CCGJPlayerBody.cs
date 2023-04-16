using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public class CCGJPlayerBody : NetworkBehaviour
    {
        public Renderer bodyRenderer;
        public GameObject[] ownedObject;
        public GameObject[] inhabitedObject;

        public override void OnNetworkSpawn()
        {
            DisableClientView();
        }

        public override void OnGainedOwnership()
        {
            foreach (var owned in ownedObject)
                owned.SetActive(true);
        }

        public override void OnLostOwnership()
        {
            foreach (var owned in ownedObject)
            {
                owned.SetActive(false);
            }
        }

        public void Inhabit(ulong client)
        {
            NetworkObject.ChangeOwnership(client);
            InhabitClientRpc(new ClientRpcParams(){Send = new ClientRpcSendParams{TargetClientIds = new []{client}}});
        }

        public void Free()
        {
            NetworkObject.RemoveOwnership();
            FreeClientRpc(new ClientRpcParams(){Send = new ClientRpcSendParams{TargetClientIds = new []{OwnerClientId}}});
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
            foreach (var inhabited in inhabitedObject)
                inhabited.SetActive(true);

            bodyRenderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
        }

        private void DisableClientView()
        {
            foreach (var inhabited in inhabitedObject)
                inhabited.SetActive(false);

            bodyRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
        }
    }
}

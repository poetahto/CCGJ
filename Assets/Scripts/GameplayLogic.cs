using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameplayLogic : NetworkBehaviour
    {
        [SerializeField]
        private List<CCGJPlayerBody> playerObjects;

        private Queue<CCGJPlayerBody> _availableObjects;
        private Dictionary<ulong, CCGJPlayerBody> _usedObjects = new Dictionary<ulong, CCGJPlayerBody>();

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                _availableObjects = new Queue<CCGJPlayerBody>(playerObjects);

                NetworkManager.OnClientConnectedCallback += HandleClientConnected;
                NetworkManager.OnClientDisconnectCallback += HandleClientDisconnected;
            }
        }

        public override void OnNetworkDespawn()
        {
            if (IsServer)
            {
                NetworkManager.OnClientConnectedCallback -= HandleClientConnected;
                NetworkManager.OnClientDisconnectCallback -= HandleClientDisconnected;
            }
        }

        private int _cycle;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                SwapServerRpc();
        }

        [ServerRpc(RequireOwnership = false)]
        private void SwapServerRpc()
        {
            var idxA = _cycle;
            _cycle = (_cycle + 1) % 2;
            var idxB = _cycle;

            var bodyA = playerObjects[idxA];
            var bodyB = playerObjects[idxB];

            var clientA = bodyA.OwnerClientId;
            var clientB = bodyB.OwnerClientId;

            bodyA.Free();
            bodyB.Free();

            if (clientA != clientB)
            {
                bodyA.Inhabit(clientB);
                bodyB.Inhabit(clientA);
            }
            else bodyB.Inhabit(clientA);
        }

        private void HandleClientDisconnected(ulong clientId)
        {
            // only release body control if the client owned a body in the first place
            if (_usedObjects.ContainsKey(clientId))
            {
                var body = _usedObjects[clientId];
                body.Free();
                _availableObjects.Enqueue(body);
                _usedObjects.Remove(clientId);
            }
        }

        private void HandleClientConnected(ulong clientId)
        {
            // only give them a body if we have one available.
            if (_availableObjects.Count > 0)
            {
                var body = _availableObjects.Dequeue();
                body.Inhabit(clientId);
                _usedObjects.Add(clientId, body);
            }
        }
    }
}

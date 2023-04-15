using System;
using System.Collections.Generic;
using poetools.Multiplayer;
using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameplayLogic : NetworkBehaviour
    {
        [SerializeField]
        private List<NetworkObject> playerObjects;

        private Queue<NetworkObject> _availableObjects;
        private Dictionary<ulong, NetworkObject> _usedObjects = new Dictionary<ulong, NetworkObject>();

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                _availableObjects = new Queue<NetworkObject>(playerObjects);

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
            {
                var idxA = _cycle;
                _cycle = (_cycle + 1) % 2;
                var idxB = _cycle;
                playerObjects[idxA].GetComponent<NetworkedPlayer>().OnLostOwnership();

                playerObjects[idxB].GetComponent<NetworkedPlayer>().IsHostOwned = IsHost;
                playerObjects[idxB].GetComponent<NetworkedPlayer>().OnGainedOwnership();
            }
        }

        private void HandleClientDisconnected(ulong clientId)
        {
            // only release body control if the client owned a body in the first place
            if (_usedObjects.ContainsKey(clientId))
            {
                var body = _usedObjects[clientId];
                body.RemoveOwnership();
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
                body.GetComponent<NetworkedPlayer>().IsHostOwned = IsHost;
                body.ChangeOwnership(clientId);
                _usedObjects.Add(clientId, body);

                if (IsHost)
                {
                    body.GetComponent<NetworkedPlayer>().OnGainedOwnership();
                }
            }
        }
    }
}

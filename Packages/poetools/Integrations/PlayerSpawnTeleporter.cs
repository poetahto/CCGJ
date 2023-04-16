using Unity.Netcode;
using UnityEngine;

namespace Integrations
{
    public class PlayerSpawnTeleporter : NetworkBehaviour
    {
        public override void OnNetworkSpawn()
        {
            print("spawned");
            var spawnPoint = FindObjectOfType<PlayerSpawnPosition>();

            if (spawnPoint != null)
            {
                var spawnTransform = spawnPoint.transform;
                transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);
                Physics.SyncTransforms();
            }

            NetworkManager.SceneManager.OnSceneEvent += HandleSceneEvent;
        }

        public override void OnNetworkDespawn()
        {
            NetworkManager.SceneManager.OnSceneEvent -= HandleSceneEvent;
        }

        private void HandleSceneEvent(SceneEvent sceneEvent)
        {
            if (sceneEvent.ClientId == OwnerClientId && sceneEvent.SceneEventType == SceneEventType.LoadComplete)
            {
                print("loaded");
                var spawnPoint = FindObjectOfType<PlayerSpawnPosition>();
                if (spawnPoint != null)
                {
                    transform.SetPositionAndRotation(spawnPoint.transform.position, spawnPoint.transform.rotation);
                    Physics.SyncTransforms();
                }
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using UnityEngine;

namespace poetools.Multiplayer
{
    [RequireComponent(typeof(MultiplayerController))]
    public class MultiplayerAutoHost : MonoBehaviour
    {
        public enum StartType
        {
            Direct, Relay,
        }

        public StartType startType;
        public int maxPlayers;

        private async void Start()
        {
            var multiplayerController = GetComponent<MultiplayerController>();
            await Task.Delay(1000);

            switch (startType)
            {
                case StartType.Direct: multiplayerController.DirectStartup.StartHost();
                    break;

                case StartType.Relay: await multiplayerController.RelayStartup.RelayStartHost(maxPlayers);
                    break;

                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}

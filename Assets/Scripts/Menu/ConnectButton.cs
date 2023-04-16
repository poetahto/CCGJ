using poetools.Multiplayer;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Menu
{
    public class ConnectButton : MonoBehaviour
    {
        public ConnectPromptView connectPromptPrefab;
        public Transform parent;

        private void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(HandleButtonClick);
        }

        private void OnDestroy()
        {
            var button = GetComponent<Button>();
            button.onClick.RemoveListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            var instance = Instantiate(connectPromptPrefab, parent);
            instance.OnSubmitJoinCode += HandleCodeSubmitted;
        }

        private static async void HandleCodeSubmitted(string joinCode)
        {
            await MultiplayerController.Singleton.RelayStartup.RelayStartClient(joinCode);
        }
    }
}

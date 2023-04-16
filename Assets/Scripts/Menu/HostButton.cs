using poetools.Multiplayer;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace DefaultNamespace.Menu
{
    public class HostButton : MonoBehaviour
    {
        public HostPromptView hostPromptPrefab;
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

        private async void HandleButtonClick()
        {
            var promptInstance = Instantiate(hostPromptPrefab, parent);
            string joinCode = await MultiplayerController.Singleton.RelayStartup.RelayStartHost(2);
            promptInstance.FinishedCreating(joinCode);
        }
    }
}

using poetools.Multiplayer;
using TMPro;
using TriInspector;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Menu
{
    public class HostPromptView : MonoBehaviour
    {
        public TMP_Text joinCodeDisplay;
        public Button cancelButton;
        public GameObject loadingView;
        public GameObject finishedView;

        [Scene]
        public string gameplayScene;

        private void Start()
        {
            loadingView.SetActive(true);
            finishedView.SetActive(false);
            joinCodeDisplay.text = string.Empty;
        }

        private void OnDestroy()
        {
            cancelButton.onClick.RemoveListener(CancelConnecting);
        }

        public void FinishedCreating(string joinCode)
        {
            loadingView.SetActive(false);
            finishedView.SetActive(true);
            joinCodeDisplay.text = joinCode;
            cancelButton.onClick.AddListener(CancelConnecting);
            NetworkManager.Singleton.OnClientConnectedCallback += HandleClientConnected;
        }

        private async void HandleClientConnected(ulong obj)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= HandleClientConnected;
            await MultiplayerController.Singleton.SceneLoader.Load(gameplayScene);
        }

        private void CancelConnecting()
        {
            NetworkManager.Singleton.Shutdown();
            Destroy(gameObject);
        }
    }
}

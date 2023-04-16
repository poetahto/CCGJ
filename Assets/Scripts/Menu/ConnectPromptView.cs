using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Menu
{
    public class ConnectPromptView : MonoBehaviour
    {
        public TMP_InputField inputField;
        public Button cancelButton;
        public Button connectButton;

        public event Action<string> OnSubmitJoinCode;

        private void Start()
        {
            cancelButton.onClick.AddListener(HandleCancel);
            inputField.onSubmit.AddListener(HandleInputSubmit);
            connectButton.onClick.AddListener(() => HandleInputSubmit(inputField.text));
        }

        private void HandleCancel()
        {
            Destroy(gameObject);
        }

        private void HandleInputSubmit(string input)
        {
            OnSubmitJoinCode?.Invoke(input);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Menu
{
    public class QuitButton : MonoBehaviour
    {
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

        private static void HandleButtonClick()
        {
            Application.Quit();
        }
    }
}
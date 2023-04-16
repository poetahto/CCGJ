using UnityEngine;

namespace poetools.Core
{
    public class CursorSettings : MonoBehaviour
    {
        [SerializeField]
        private CursorLockMode lockMode;

        [SerializeField]
        private bool isVisible;

        private void Start()
        {
            Cursor.lockState = lockMode;
            Cursor.visible = isVisible;
        }
    }
}

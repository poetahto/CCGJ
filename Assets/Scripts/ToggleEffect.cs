using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class ToggleEffect : MonoBehaviour
    {
        public GameObject Target { get; set; }

        public abstract IEnumerator ToggleOn();
        public abstract IEnumerator ToggleOff();
    }
}

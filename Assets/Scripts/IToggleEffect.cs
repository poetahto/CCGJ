using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IToggleEffect
    {
        public GameObject Target { get; set; }

        public IEnumerator ToggleOn();
        public IEnumerator ToggleOff();
    }
}

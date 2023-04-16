using System;
using FMODUnity;
using poetools.Core;
using TriInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class Room2FMODLogic : TriggerEffect
    {
        private StudioEventEmitter music;

        private bool _boxUnlocked;
        private string CurrentParameter => _boxUnlocked ? "HighPlucks" : "NormalPluck";

        private void Start()
        {
            music = GameObject.Find("Music").GetComponent<StudioEventEmitter>();
        }

        protected override void HandleCollisionEnter(GameObject obj)
        {
            if (obj.CompareTag("Player"))
            {
                if (_boxUnlocked)
                {
                    music.SetParameter("Bass", 1);
                }

                music.SetParameter(CurrentParameter, 1);
            }
        }

        protected override void HandleCollisionExit(GameObject obj)
        {
            if (obj.CompareTag("Player") && !_completed)
            {
                music.SetParameter(CurrentParameter, 0);
            }
        }

        [Button]
        public void BoxUnlocked()
        {
            _boxUnlocked = true;
        }

        private bool _completed;

        [Button]
        public void Completed()
        {
            _completed = true;
        }
    }
}

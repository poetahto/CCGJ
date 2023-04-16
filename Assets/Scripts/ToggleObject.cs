using System.Collections;
using Unity.Netcode;
using UnityEngine;

namespace DefaultNamespace
{
    public class ToggleObject : NetworkBehaviour
    {
        [HideInInspector]
        public NetworkVariable<bool> isOn = new NetworkVariable<bool>();
        public bool defaultValue;

        [SerializeReference]
        public ToggleEffect toggleEffect;

        private void Awake()
        {
            if (toggleEffect != null)
                toggleEffect.Target = gameObject;
        }

        public override void OnNetworkSpawn()
        {
            isOn.OnValueChanged += HandleValueChanged;

            if (IsServer)
                isOn.Value = defaultValue;

            HandleValueChanged(default, isOn.Value);
        }

        private void HandleValueChanged(bool oldValue, bool newValue)
        {
            StartCoroutine(newValue ? RunEffectOnCoroutine() : RunEffectOffCoroutine());
        }

        private bool _isAnimating;

        private IEnumerator RunEffectOnCoroutine()
        {
            // if (!_isAnimating)
            // {
            //     _isAnimating = true;
                if (toggleEffect != null)
                    yield return toggleEffect.ToggleOn();
                // else gameObject.SetActive(true);
            //     yield return null;
            //     _isAnimating = false;
            // }
        }

        private IEnumerator RunEffectOffCoroutine()
        {
            // if (!_isAnimating)
            // {
            //     _isAnimating = true;
                if (toggleEffect != null)
                    yield return toggleEffect.ToggleOff();
                // else gameObject.SetActive(false);
                // yield return null;
                // _isAnimating = false;
            // }
        }

        public void ToggleOn()
        {
            SetIsOnServerRpc(true);
        }

        public void ToggleOff()
        {
            SetIsOnServerRpc(false);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetIsOnServerRpc(bool value)
        {
            isOn.Value = value;
        }
    }
}

using poetools.player.Player.Interaction;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Lever : NetworkBehaviour, IInteractable
    {
        [SerializeField] private bool defaultValue;
        [SerializeField] private UnityEvent flippedOn;
        [SerializeField] private UnityEvent flippedOff;

        private NetworkVariable<bool> _isFlipped = new NetworkVariable<bool>();

        public override void OnNetworkSpawn()
        {
            _isFlipped.OnValueChanged += HandleValueChanged;

            if (IsServer)
                _isFlipped.Value = defaultValue;

            HandleValueChanged(default, _isFlipped.Value);
        }

        private void HandleValueChanged(bool previousValue, bool newValue)
        {
            if (newValue)
                flippedOn.Invoke();

            else flippedOff.Invoke();
        }

        public void HandleInteractStart(GameObject grabber)
        {
            SetIsFlippedServerRpc(!_isFlipped.Value);
        }

        public void HandleInteractStop(GameObject grabber)
        {
            // do nothing
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetIsFlippedServerRpc(bool value)
        {
            _isFlipped.Value = value;
        }
    }
}

using Unity.Netcode;

namespace DefaultNamespace
{

    public class ToggleObject : NetworkBehaviour
    {
        public NetworkVariable<bool> isOn = new NetworkVariable<bool>();
        public bool defaultValue;

        public override void OnNetworkSpawn()
        {
            isOn.OnValueChanged += HandleValueChanged;

            if (IsServer)
                isOn.Value = defaultValue;

            HandleValueChanged(default, isOn.Value);
        }

        private void HandleValueChanged(bool oldValue, bool newValue)
        {
            // todo: replace w/ cooler effects
            gameObject.SetActive(newValue);
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

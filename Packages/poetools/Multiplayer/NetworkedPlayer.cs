using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering;

namespace poetools.Multiplayer
{
    public class NetworkedPlayer : NetworkBehaviour
    {
        [SerializeField]
        private Renderer bodyRenderer;

        [SerializeField]
        private Behaviour[] localPlayerOnlyBehaviours;

        public bool IsHostOwned { get; set; }

        private void Awake()
        {
            OnLostOwnership();
        }

        public override void OnGainedOwnership()
        {
            if (!IsServer || IsHostOwned)
            {
                foreach (var behaviour in localPlayerOnlyBehaviours)
                    behaviour.enabled = true;

                bodyRenderer.shadowCastingMode = ShadowCastingMode.ShadowsOnly;
            }
        }

        public override void OnLostOwnership()
        {
            foreach (var behaviour in localPlayerOnlyBehaviours)
                behaviour.enabled = false;

            bodyRenderer.shadowCastingMode = ShadowCastingMode.TwoSided;
        }
    }
}

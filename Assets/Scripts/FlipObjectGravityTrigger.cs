using poetools.Core;
using poetools.Core.Abstraction;
using UnityEngine;

namespace DefaultNamespace
{
    public class FlipObjectGravityTrigger : TriggerEffect
    {
        protected override void HandleCollisionEnter(GameObject obj)
        {
            base.HandleCollisionEnter(obj);

            if (obj.TryGetComponent(out MovableObject _) && obj.TryGetComponent(out Gravity gravity))
                gravity.downDirection *= -1;
        }
    }
}

using UnityEngine;

namespace poetools.Core.Abstraction
{
    public class Gravity : MonoBehaviour
    {
        [SerializeField] private PhysicsComponent physics;
        public Vector3 downDirection = Vector3.down;
        public float amount = -Physics.gravity.y;
        [SerializeField]private float idleGravity;
        [SerializeField] public GroundCheck groundCheck;



        // private IDisposable debug;

        // private void OnDestroy()
        // {
            // debug.Dispose();
        // }

        private void FixedUpdate()
        {

            // _physicsComponent.Velocity = (groundCheck && !groundCheck.IsGrounded) || _physicsComponent.Velocity.y > 0
            //     ? _physicsComponent.Velocity + downDirection * (amount * Time.deltaTime)
            //     : _physicsComponent.Velocity + Vector3.down * idleGravity;
            physics.Velocity += downDirection * (amount * Time.deltaTime);
        }
    }
}

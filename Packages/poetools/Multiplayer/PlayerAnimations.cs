﻿using poetools.Core;
using poetools.Core.Abstraction;
using poetools.player.Player.Crouching;
using Unity.Netcode;
using UnityEngine;

namespace poetools.player.Player
{
    public class PlayerAnimations : NetworkBehaviour
    {
        [SerializeField] private Animator animator;


        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private PhysicsComponent _physics;
        [SerializeField] private FPSCrouchingLogicContainer _fpsCrouching;
        private static readonly int Crouching = Animator.StringToHash("crouching");
        private static readonly int Grounded = Animator.StringToHash("grounded");
        private static readonly int Speed = Animator.StringToHash("speed");

        private void Awake()
        {
            // _groundCheck = GetComponent<GroundCheck>();
            // _physics = GetComponent<IPhysicsComponent>();
            // _fpsCrouching = GetComponent<FPSCrouchingLogicContainer>();
        }

        private void Update()
        {
            if (IsOwner)
            {
                animator.SetBool(Grounded, _groundCheck.IsGrounded);
                animator.SetBool(Crouching, _fpsCrouching.CrouchingLogic.IsCrouching);

                var v = _physics.Velocity;
                v.y = 0;
                animator.SetFloat(Speed, v.magnitude);
            }
        }
    }
}

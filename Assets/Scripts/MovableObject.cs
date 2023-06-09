﻿using JetBrains.Annotations;
using poetools.player.Player.Interaction;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class MovableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] [Range(0, 1)] private float movementSpeed = 0.5f;
        // [SerializeField] private int playerLayer = 6;
        [SerializeField] private float maxSpeed = 10f;
        [SerializeField] private bool applyRotation = true;
        [SerializeField] private bool applyConstraintsWhileMoving = true;
        [SerializeField] private UnityEvent onGrab;

        // The current position of this object
        private Transform _currentPosition;

        //The rotation of object when picked up
        private Quaternion _offsetRot;

        private Transform _cameraTransform;
        private bool _isHeld;

        public bool IsHeld => _isHeld;

        // The position this object is trying to accelerate towards.
        public Transform TargetTransform { get; private set; }

        // Is this object currently colliding with something?
        public bool HasCollision { get; private set; }

        // [PublicAPI] public Interactable Interactable { get; private set; }
        [PublicAPI] public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            // Interactable = GetComponent<Interactable>();



            _currentPosition = new GameObject("Current Position").transform;
            _currentPosition.parent = transform;

            if (Camera.main != null)
                _cameraTransform = Camera.main.transform;

            // _isDoor = Interactable.GetComponent<HingeJoint>() != null;
        }

        // private void OnEnable()
        // {
        //     Interactable.onInteractStart.AddListener(HandleInteractStart);
        //     Interactable.onInteractEnd.AddListener(HandleInteractEnd);
        // }
        //
        // private void OnDisable()
        // {
        //     Interactable.onInteractStart.RemoveListener(HandleInteractStart);
        //     Interactable.onInteractEnd.RemoveListener(HandleInteractEnd);
        // }

        public void HandleInteractStop(GameObject grabber)
        {
            _isHeld = false;

            if (applyConstraintsWhileMoving)
                Rigidbody.constraints = RigidbodyConstraints.None;
        }

        public void HandleInteractStart(GameObject grabber)
        {
            _isHeld = true;

            if (applyConstraintsWhileMoving)
                Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            var target = grabber.GetComponentInChildren<MovingTarget>();
            onGrab.Invoke();

            if (!target)
            {
                TargetTransform = new GameObject($"Moving Target (Automatically added by {name})").transform;
                TargetTransform.gameObject.AddComponent<MovingTarget>();
                TargetTransform.SetParent(grabber.GetComponentInChildren<Camera>().transform);
            }
            else TargetTransform = target.transform;

            TargetTransform.position = transform.position;

            _currentPosition.position = transform.position;

            if (Camera.main != null)
                _cameraTransform = Camera.main.transform;

            _offsetRot = Quaternion.Inverse(_cameraTransform.rotation) * transform.rotation;
        }


        private void Update()
        {
            if (_isHeld)
                RotateTowardsTarget();

        }
        private void FixedUpdate()
        {
            if (_isHeld)
                MoveTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            Vector3 target = TargetTransform.position;
            Vector3 current = _currentPosition.position;
            Vector3 directionToTarget = target - current;

            Rigidbody.velocity = directionToTarget * movementSpeed / Time.fixedDeltaTime;
            Rigidbody.velocity = Vector3.ClampMagnitude(Rigidbody.velocity, maxSpeed);
        }

        private void RotateTowardsTarget()
        {
            if (applyRotation)
                Rigidbody.MoveRotation(_cameraTransform.rotation * _offsetRot);
        }

        private void OnCollisionEnter(Collision other)
        {
            HasCollision = true;
            //
            // if (other.gameObject.layer == playerLayer)
            //     Interactable.InteractEnd();
        }

        private void OnCollisionStay(Collision other)
        {
            // if (other.gameObject.layer == playerLayer)
            //     Interactable.InteractEnd();
        }
        private void OnCollisionExit()
        {
            HasCollision = false;
        }
    }
}

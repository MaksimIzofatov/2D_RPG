
using System;
using Level;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerMover), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandlers))]
    public class Player : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private InputReader _input;
        private PlayerAnimator _animator;
        private CollisionHandlers _collisionHandlers;
        
        private IInteractable _interactable;

        private void Awake()
        {
            _playerMover = GetComponent<PlayerMover>();
            _input = GetComponent<InputReader>();
            _animator = GetComponent<PlayerAnimator>();
            _collisionHandlers = GetComponent<CollisionHandlers>();
        }

        private void OnEnable()
        {
            _collisionHandlers.InteractableObjectIsNear += OnInteractableObjectIsNear;
        }


        private void OnDisable()
        {
            _collisionHandlers.InteractableObjectIsNear -= OnInteractableObjectIsNear;
        }

        private void FixedUpdate()
        {
            _playerMover.Move(_input.DirectionX, _input.DirectionY);
            
            _animator.SetSpeedXY(_input.DirectionX != 0 && _input.DirectionY != 0);
            _animator.SetSpeedX(_input.DirectionX);
            _animator.SetSpeedY(_input.DirectionY);
            
            if(_input.GetIsAddForce())
                _playerMover.AddForce();

            if (_input.GetIsInteract() && _interactable != null)
            {
                _interactable.Interact();
            }
        }
        
        private void OnInteractableObjectIsNear(IInteractable interactable)
        {
            _interactable = interactable;
        }
    }

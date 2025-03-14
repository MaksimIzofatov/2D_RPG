using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandlers), (typeof(PlayerAttacker)))]
    public class Player : MonoBehaviour
    {
        private Mover _mover;
        private InputReader _input;
        private PlayerAnimator _animator;
        private CollisionHandlers _collisionHandlers;
        private PlayerAttacker _attacker;
        
        private IInteractable _interactable;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _input = GetComponent<InputReader>();
            _animator = GetComponent<PlayerAnimator>();
            _collisionHandlers = GetComponent<CollisionHandlers>();
            _attacker = GetComponent<PlayerAttacker>();
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
            _mover.Move(_input.DirectionX, _input.DirectionY);
            _attacker.ChangeDirectionForAttack(_input.DirectionX, _input.DirectionY);
            
            _animator.SetSpeedXY(_input.DirectionX != 0 && _input.DirectionY != 0);
            _animator.SetSpeedX(_input.DirectionX);
            _animator.SetSpeedY(_input.DirectionY);
            
            if(_input.GetIsAddForce())
                _mover.AddForce();

            if (_input.GetIsInteract() && _interactable != null)
            {
                _interactable.Interact();
            }

            if (_input.GetIsAttack())
            {
                _attacker.Attack();
            }
        }
        
        private void OnInteractableObjectIsNear(IInteractable interactable)
        {
            _interactable = interactable;
        }
    }

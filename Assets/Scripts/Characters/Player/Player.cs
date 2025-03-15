using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandlers), (typeof(PlayerAttacker)))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 20;
        
        private Mover _mover;
        private InputReader _input;
        private PlayerAnimator _animator;
        private CollisionHandlers _collisionHandlers;
        private PlayerAttacker _attacker;
        
        private IInteractable _interactable;
        private Health _health;

   
     

        private void Awake()
        {
            _health = new(_maxHealth);  
            
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

            if (_input.GetIsAttack() && _attacker.CanAttack)
            {
                _attacker.Attack();
            }
        }
        
        private void OnInteractableObjectIsNear(IInteractable interactable)
        {
            _interactable = interactable;
        }
        
        public void ApplyDamage(int damage)
        {
            _health.ApplyDamage(damage);
            Debug.Log(_health.CurrentHealth);
        }

        public void ApplyHeal(int heal)
        {
            _health.ApplyHeal(heal);
        }
    }

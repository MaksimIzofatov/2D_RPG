using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Mover), typeof(PlayerAnimator))]
[RequireComponent(typeof(CollisionHandlers), (typeof(PlayerAttacker)))]
    public class Player : MonoBehaviour
    {
        public event Action Died;
        
        [SerializeField] private int _maxHealth = 20;
        [SerializeField] private HealthBar _healthBar;
        
        private Mover _mover;
        private InputReader _input;
        private PlayerAnimator _animator;
        private CollisionHandlers _collisionHandlers;
        private PlayerAttacker _attacker;
        
        private IInteractable _interactable;
        private Health _health;
        private float _previousDirX;
        private float _previousDirY;

   
     

        private void Awake()
        {
            _health = new(_maxHealth); 
            _healthBar.Initialize(_health);
            
            _mover = GetComponent<Mover>();
            _input = GetComponent<InputReader>();
            _animator = GetComponent<PlayerAnimator>();
            _collisionHandlers = GetComponent<CollisionHandlers>();
            _attacker = GetComponent<PlayerAttacker>();
            
        }

        private void OnDied()
        {
            Died?.Invoke();
        }

        private void OnEnable()
        {
            _collisionHandlers.InteractableObjectIsNear += OnInteractableObjectIsNear;
            _health.TakingDamage += OnTakingDamage;
            _health.Died += OnDied;
        }


        private void OnDisable()
        {
            _collisionHandlers.InteractableObjectIsNear -= OnInteractableObjectIsNear;
            _health.TakingDamage -= OnTakingDamage;
            _health.Died -= OnDied;
        }

        private void FixedUpdate()
        {
            if (TimeManager.IsPaused) return;
            
            _mover.Move(_input.DirectionX, _input.DirectionY);
            _attacker.ChangeDirectionForAttack(_input.DirectionX, _input.DirectionY);
            ChangeDirectionForHit(_input.DirectionX, _input.DirectionY);
            
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
        }

        private void OnTakingDamage()
        {
            _animator.SetPreviousDirectionX(_previousDirX);
            _animator.SetPreviousDirectionY(_previousDirY);
            _animator.SetHit();
        }
        
        private void ChangeDirectionForHit(float x, float y)
        {
            if (x > 0.05f)
            {
                _previousDirX = 1;
                _previousDirY = 0;
            }
            else if (x < -0.05f)
            {
                _previousDirX = -1;
                _previousDirY = 0;
            }
            else if (y > 0.05f)
            {
                _previousDirX = 0;
                _previousDirY = 1;
            }
            else if (y < -0.05f)
            {
                _previousDirX = 0;
                _previousDirY = -1;
            }
        }

        public void ApplyHeal(int heal)
        {
            _health.ApplyHeal(heal);
        }
    }

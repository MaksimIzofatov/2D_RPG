using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[RequireComponent(typeof(EnemyVision), typeof(Mover), typeof(EnemyAttacker))]
public class Enemy : MonoBehaviour
    {
    
        [SerializeField] private WayPoints[] _wayPoints;
        [SerializeField] private float _waitTime = 2;
        [SerializeField] private float _maxSqrDistance = 0.03f;
        [SerializeField] private int _maxHealth = 5;
        [SerializeField] private EnemyAnimationEvent _enemyAnimationEvent;
        
        private EnemyStateMachine _stateMachine;
        private Health _health;
        private EnemyAttacker _attacker;
        private Animator _animator;
        private Mover _mover;
        private float _previousDirX;
        private float _previousDirY;

        private void Awake()
        {
            _health = new(_maxHealth);
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            var vision = GetComponent<EnemyVision>();
            _mover = GetComponent<Mover>();
            _attacker = GetComponent<EnemyAttacker>();
            
            _enemyAnimationEvent.Attack += _attacker.Attack;
            _enemyAnimationEvent.EndAttack += _attacker.OnEndAttack;
            _health.TakingDamage += OnTakingDamage;
            
            _stateMachine = new EnemyStateMachine(_animator, vision, _mover, _wayPoints, transform, _maxSqrDistance, _waitTime, _attacker);
        }

        private void OnDestroy()
        {
            _enemyAnimationEvent.Attack -= _attacker.Attack;
            _enemyAnimationEvent.EndAttack -= _attacker.OnEndAttack;
            _health.TakingDamage -= OnTakingDamage;
        }

        private void FixedUpdate()
        {
            if (TimeManager.IsPaused) return;
            
            _stateMachine.Update();
        }

        public void ApplyDamage(int damage)
        {
           _health.ApplyDamage(damage);
           
           if(_health.CurrentHealth <= 0)
               Destroy(gameObject);
        }

        private void OnTakingDamage()
        {
            ChangeDirectionForHit(_mover.DirectionEnemy.x, _mover.DirectionEnemy.y);
            _animator.SetFloat(GlobalConstants.AnimatorParameters.previousDirX, _previousDirX);
            _animator.SetFloat(GlobalConstants.AnimatorParameters.previousDirY, _previousDirY);
            _animator.SetTrigger(GlobalConstants.AnimatorParameters.isHit);            
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
    }
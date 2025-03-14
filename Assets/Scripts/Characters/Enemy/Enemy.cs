using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EnemyVision), typeof(Mover))]
public class Enemy : MonoBehaviour
    {
    
        [SerializeField] private WayPoints[] _wayPoints;
        [SerializeField] private float _waitTime = 2;
        [SerializeField] private float _maxSqrDistance = 0.03f;
        [SerializeField] private float _maxHealth = 5;
        
        private EnemyStateMachine _stateMachine;
        private float _currentHealth;

        private void Start()
        {
            var animator = GetComponent<Animator>();
            var vision = GetComponent<EnemyVision>();
            var mover = GetComponent<Mover>();
            
            _stateMachine = new EnemyStateMachine(animator, vision, mover, _wayPoints, transform, _maxSqrDistance, _waitTime);
            _currentHealth = _maxHealth;
        }

        private void FixedUpdate()
        {
            _stateMachine.Update();
        }

        public void ApplyDamage(float damage)
        {
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                return;
            }
            
            _currentHealth -= damage;
            Debug.Log(_currentHealth);
        }
    }
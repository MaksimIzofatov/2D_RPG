using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

[RequireComponent(typeof(EnemyVision), typeof(Mover))]
public class Enemy : MonoBehaviour
    {
    
        [SerializeField] private WayPoints[] _wayPoints;
        [SerializeField] private float _waitTime = 2;
        [SerializeField] private float _maxSqrDistance = 0.03f;
        [SerializeField] private int _maxHealth = 5;
        
        private EnemyStateMachine _stateMachine;
        private Health _health;

        private void Awake()
        {
            _health = new(_maxHealth);
        }

        private void Start()
        {
            var animator = GetComponent<Animator>();
            var vision = GetComponent<EnemyVision>();
            var mover = GetComponent<Mover>();
            
            _stateMachine = new EnemyStateMachine(animator, vision, mover, _wayPoints, transform, _maxSqrDistance, _waitTime);
        }

        private void FixedUpdate()
        {
            _stateMachine.Update();
        }

        public void ApplyDamage(int damage)
        {
           _health.ApplyDamage(damage);
           
           if(_health.CurrentHealth <= 0)
               Destroy(gameObject);
        }
    }
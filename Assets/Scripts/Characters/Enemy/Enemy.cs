using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EnemyVision), typeof(Mover))]
public class Enemy : MonoBehaviour
    {
    
        [SerializeField] private WayPoints[] _wayPoints;
        [SerializeField] private float _waitTime = 2;
        [SerializeField] private float _maxSqrDistance = 0.03f;
        
        private EnemyStateMachine _stateMachine;

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
    }
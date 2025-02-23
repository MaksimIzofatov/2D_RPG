using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EnemyVision), typeof(Mover))]
public class Enemy : MonoBehaviour
    {
    
        [SerializeField] private WayPoints[] _wayPoints;
        [SerializeField] private float _waitTime = 2;
        
        private int _wayPointIndex;
        private Transform _target;
        private float _maxSqrDistance = 0.03f;
        private bool _isWaiting;
        private float _endWaitTime;
        private Animator _animator;
        private EnemyVision _vision;
        private Mover _mover;
        private bool _isPreviosStateSee;

        private void Start()
        {
            _target = _wayPoints[_wayPointIndex].transform;
            _animator = GetComponent<Animator>();
            _vision = GetComponent<EnemyVision>();
            _mover = GetComponent<Mover>();
        }

        private void FixedUpdate()
        {
            var isSee = _vision.TrySeeTarget(out var target);
            _animator.SetBool("follow", isSee);
            if (isSee)
            {
                _mover.Move(target);
                CalculateDirectionForAnimator(target);
                _isPreviosStateSee = true;
                return; 
            }

            if (isSee == false && _isPreviosStateSee)
            {
                _isPreviosStateSee = false;
                _isWaiting = true;
                _endWaitTime = Time.time + _waitTime;
                _wayPointIndex--;
                ResetAnimations();
                return;
            }

            if (_isWaiting == false)
            {
                _mover.Move(_target);
                CalculateDirectionForAnimator(_target);
            }

            if (IsTargetReached() && _isWaiting == false)
            {
                _isWaiting = true;
                _endWaitTime = Time.time + _waitTime;
                ResetAnimations();
            }

            if (_isWaiting && Time.time >= _endWaitTime)
            {
                _isWaiting = false;
                ChangeTarget();
            }
        }

        private void ResetAnimations()
        {
            _animator.SetFloat("walkX", 0);
            _animator.SetFloat("walkY", 0);
        }


        private void ChangeTarget()
        {
            _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
            _target = _wayPoints[_wayPointIndex].transform;
            
           CalculateDirectionForAnimator(_target);
        }

        private void CalculateDirectionForAnimator(Transform target)
        {
            var direction = _mover.CalculateDirection(target);
            _animator.SetFloat("walkX", direction.x);
            _animator.SetFloat("walkY", direction.y);
            _animator.SetBool("dirX", direction.x > direction.y);
        }

        private bool IsTargetReached()
        {
            float sqrDistance = (transform.position - _target.position).sqrMagnitude;
            
            
            return sqrDistance <= _maxSqrDistance;
        }

    }


public abstract class StateMachine
{
    protected State CurrentState;
    protected Dictionary<Type, State> States;

    public void Update()
    {
        
    }

    public void ChangeState<TState>() where  TState : State
    {
        
    }
}

public abstract class State
{
    protected Transition[] Transitions;
    
    public virtual void Enter(){}

    public virtual void Exit(){}

    public virtual void Update(){}

    public abstract Type TryGetNextState();
}

public abstract class Transition
{
    protected State TargetState;
    public abstract Type TryGetNextState();
}

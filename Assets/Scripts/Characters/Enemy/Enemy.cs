using System;
using System.Collections.Generic;
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


public abstract class StateMachine
{
    protected State CurrentState;
    protected Dictionary<Type, State> States;

    public void Update()
    {
        if(CurrentState == null)
            return;

        CurrentState.Update();
        CurrentState.TryTransit();
    }

    public void ChangeState<TState>() where  TState : State
    {
        if(CurrentState != null && CurrentState.GetType() == typeof(TState))
            return;

        if (States.TryGetValue(typeof(TState), out State newState))
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}

public abstract class State
{
    protected Transition[] Transitions;
    protected Animator Animator;
    protected Mover Mover;

    protected State(StateMachine stateMachine, Animator animator, Mover mover)
    {
        Animator = animator;
        Mover = mover;
    }

    public virtual void Enter(){}

    public virtual void Exit(){}

    public virtual void Update(){}

    public virtual void TryTransit()
    {
        foreach (var transition in Transitions)
        {
            if (transition.IsNeedTransit())
            {
                transition.Transit();
                return;
            }
        }
    }
    protected void CalculateDirectionForAnimator(Transform target)
    {
        var direction = Mover.CalculateDirection(target);
        Animator.SetFloat("walkX", direction.x);
        Animator.SetFloat("walkY", direction.y);
        Animator.SetBool("dirX", direction.x > direction.y);
    }
}

public abstract class Transition
{
    protected StateMachine StateMachine;

    protected Transition(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }

    public abstract bool IsNeedTransit();
    public abstract void Transit();
}

public class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Animator animator, EnemyVision vision, Mover mover, 
        WayPoints[] wayPoints, Transform transform, float maxSqrDistance, float waitTime)
    {
        States = new Dictionary<Type, State>()
        {
            {typeof(PatrolState), new PatrolState(this, animator, vision, mover, wayPoints, transform, maxSqrDistance)},
            {typeof(IdleState), new IdleState(this, animator, mover, vision, waitTime)},
            {typeof(FollowState), new FollowState(this, animator, mover, vision)}
        };
        
        ChangeState<PatrolState>();
    }
}

public class PatrolState : State
{
    private WayPoints[] _wayPoints;
    private int _wayPointIndex;
    private Transform _target;
    private bool _isWaiting;
    
    public PatrolState(StateMachine stateMachine, Animator animator, EnemyVision vision, Mover mover, 
        WayPoints[] wayPoints, Transform transform, float maxSqrDistance) : base(stateMachine, animator, mover)
    {
        _wayPoints = wayPoints;
        _wayPointIndex = -1;

        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, vision),
            new TargetReachedTransition(stateMachine, this, transform, maxSqrDistance)
        };
    }

    public Transform Target => _target;

    public override void Enter()
    {
        ChangeTarget();
        Mover.SetSpeedWalk();
    }

    public override void Update()
    {
        Mover.Move(_target);
    }
    
    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;
            
        CalculateDirectionForAnimator(_target);
    }
}

public class IdleState : State
{
    private float _waitTime;
    private float _endWaitTime;
    public bool IsEndWay => Time.time >= _endWaitTime;
    public IdleState(StateMachine stateMachine, Animator animator, Mover mover, EnemyVision enemyVision, float waitTime) : base(stateMachine, animator, mover)
    {
        _waitTime = waitTime;
        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, enemyVision),
            new EndIdleTransition(stateMachine, this)
        };
    }

    public override void Enter()
    {
        _endWaitTime = Time.time + _waitTime;
        ResetAnimations();
    }
    private void ResetAnimations()
    {
        Animator.SetFloat(GlobalConstants.AnimatorParameters.walkX, 0);
        Animator.SetFloat(GlobalConstants.AnimatorParameters.walkY, 0);
    }
    
}

public class FollowState : State
{
    private EnemyVision _vision;
    private Transform _target;
    private bool _isSeeTarget;
    public FollowState(StateMachine stateMachine, Animator animator, Mover mover, EnemyVision vision) : base(stateMachine, animator, mover)
    {
        _vision = vision;

        Transitions = new Transition[]
        {
            new LossTargetTransition(stateMachine, vision),
        };
    }

    public override void Enter()
    {
        _isSeeTarget = _vision.TrySeeTarget(out _target);
        Mover.SetSpeedRun();
        Animator.SetBool(GlobalConstants.AnimatorParameters.follow, _isSeeTarget);
    }

    public override void Update()
    {
        if (_target != null)
        {
            Mover.Move(_target);
            CalculateDirectionForAnimator(_target);
        }
    }

    public override void Exit()
    {
        _isSeeTarget = false;
        Animator.SetBool(GlobalConstants.AnimatorParameters.follow, _isSeeTarget);
    }
}

public class SeeTargetTransition : Transition
{
    private EnemyVision _vision;
    public SeeTargetTransition(StateMachine stateMachine, EnemyVision enemyVision) : base(stateMachine)
    {
        _vision = enemyVision;
    }

    public override bool IsNeedTransit()
    {
        return _vision.TrySeeTarget(out Transform _);
    }

    public override void Transit()
    {
        StateMachine.ChangeState<FollowState>();
    }
}

public class LossTargetTransition : Transition
{
    private EnemyVision _vision;
    public LossTargetTransition(StateMachine stateMachine, EnemyVision enemyVision) : base(stateMachine)
    {
        _vision = enemyVision;
    }

    public override bool IsNeedTransit()
    {
        return _vision.TrySeeTarget(out Transform _) == false;
    }

    public override void Transit()
    {
        StateMachine.ChangeState<IdleState>();
    }
}

public class EndIdleTransition : Transition
{
    private IdleState _idleState;
    public EndIdleTransition(StateMachine stateMachine, IdleState idleState) : base(stateMachine)
    {
        _idleState = idleState;
    }

    public override bool IsNeedTransit()
    {
        return _idleState.IsEndWay;
    }

    public override void Transit()
    {
        StateMachine.ChangeState<PatrolState>();
    }
}

public class TargetReachedTransition : Transition
{
    private PatrolState _patrolState;
    private Transform _transform;
    private float _maxSqrDistance = 0.03f;
    public TargetReachedTransition(StateMachine stateMachine, PatrolState patrolState, Transform transform, float maxSqrDistance) : base(stateMachine)
    {
        _patrolState = patrolState;
        _transform = transform;
        _maxSqrDistance = maxSqrDistance;
    }

    public override bool IsNeedTransit()
    {
        float sqrDistance = (_transform.position - _patrolState.Target.position).sqrMagnitude;
        return sqrDistance <= _maxSqrDistance;
    }

    public override void Transit()
    {
        StateMachine.ChangeState<IdleState>();
    }
}


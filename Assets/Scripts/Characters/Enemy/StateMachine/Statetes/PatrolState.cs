using UnityEngine;

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
    }

    public override void Update()
    {
        Mover.SetSpeedWalk(_target);
    }
    
    private void ChangeTarget()
    {
        _wayPointIndex = ++_wayPointIndex % _wayPoints.Length;
        _target = _wayPoints[_wayPointIndex].transform;
            
        CalculateDirectionForAnimator(_target);
    }
}
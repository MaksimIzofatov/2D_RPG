using UnityEngine;

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
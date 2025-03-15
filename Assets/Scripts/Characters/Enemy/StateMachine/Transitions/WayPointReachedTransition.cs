using UnityEngine;

public class WayPointReachedTransition : ReachedTransition
{
    public WayPointReachedTransition(StateMachine stateMachine, IMoveState moveState, Transform transform, float sqrDistance) 
        : base(stateMachine, moveState, transform, sqrDistance) { }

    public override void Transit()
    {
        base.Transit();
        StateMachine.ChangeState<IdleState>();
    }
}
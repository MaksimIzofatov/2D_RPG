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
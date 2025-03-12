using UnityEngine;

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
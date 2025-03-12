using UnityEngine;

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
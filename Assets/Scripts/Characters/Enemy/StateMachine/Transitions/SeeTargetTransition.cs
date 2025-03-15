using UnityEngine;

public class SeeTargetTransition : Transition
{
    private EnemyVision _vision;
    private Transform _transform;
    private float _sqrAttackDistance;
    public SeeTargetTransition(StateMachine stateMachine, EnemyVision enemyVision , Transform transform, float sqrAttackDistance) : base(stateMachine)
    {
        _vision = enemyVision;
        _transform = transform;
        _sqrAttackDistance = sqrAttackDistance; 
    }

    public override bool IsNeedTransit() => 
        _vision.TrySeeTarget(out Transform target) && (_transform.position - target.position).sqrMagnitude > _sqrAttackDistance;
    

    public override void Transit()
    {
        base.Transit();
        StateMachine.ChangeState<FollowState>();
    }
}
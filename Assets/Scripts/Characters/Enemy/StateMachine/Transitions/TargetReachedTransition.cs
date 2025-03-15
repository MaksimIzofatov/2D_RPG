    using UnityEngine;

    public class TargetReachedTransition : ReachedTransition
    {
        public TargetReachedTransition(StateMachine stateMachine, IMoveState moveState, Transform transform, float sqrDistance) 
            : base(stateMachine, moveState, transform, sqrDistance) { }

        public override void Transit()
        {
            base.Transit();
            StateMachine.ChangeState<AttackState>();
        }
    }

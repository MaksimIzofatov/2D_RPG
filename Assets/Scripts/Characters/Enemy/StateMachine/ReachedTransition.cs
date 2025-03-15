    using UnityEngine;

    public class ReachedTransition : Transition
    {
        private IMoveState _moveState;
        private Transform _transform;
        private float _sqrDistance;
        public ReachedTransition(StateMachine stateMachine, IMoveState moveState, Transform transform, float sqrDistance) : base(stateMachine)
        {
            _moveState = moveState;
            _transform = transform;
            _sqrDistance = sqrDistance;
        }

        public override bool IsNeedTransit()
        {
            float sqrDistance = (_transform.position - _moveState.Target.position).sqrMagnitude;
            return sqrDistance <= _sqrDistance;
        }
    }

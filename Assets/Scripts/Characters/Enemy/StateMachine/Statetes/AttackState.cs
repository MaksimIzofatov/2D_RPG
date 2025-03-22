    using UnityEngine;

    public class AttackState : State
    {
        private EnemyAttacker _attacker;
        private EnemyVision _vision;
        
        public AttackState(StateMachine stateMachine, Animator animator, Mover mover, EnemyVision enemyVision, EnemyAttacker attacker) 
            : base(stateMachine, animator, mover)
        {
            _attacker = attacker;
            _vision = enemyVision;
            
            Transitions = new Transition[]
            {
                new SeeTargetTransition(stateMachine, enemyVision, mover.transform, _attacker.SqrDistance),
                new LossTargetTransition(stateMachine, enemyVision),
            };
        }

        public override void Enter()
        {
            ResetAnimations();
            _vision.TrySeeTarget(out Transform target);
            var direction = Mover.CalculateDirection(target);
            _attacker.ChangeDirectionForAttack(direction.x, direction.y);
        }

        public override void Update()
        {
            if (_attacker.CanAttack)
            {
                _attacker.StartAttack();
                Animator.SetTrigger(GlobalConstants.AnimatorParameters.isAttack);
            }
        }
        
        private void ResetAnimations()
        {
            Animator.SetFloat(GlobalConstants.AnimatorParameters.walkX, 0);
            Animator.SetFloat(GlobalConstants.AnimatorParameters.walkY, 0);
        }
    }
using UnityEngine;

public class FollowState : State
{
    private EnemyVision _vision;
    private Transform _target;
    private bool _isSeeTarget;
    public FollowState(StateMachine stateMachine, Animator animator, Mover mover, EnemyVision vision) : base(stateMachine, animator, mover)
    {
        _vision = vision;

        Transitions = new Transition[]
        {
            new LossTargetTransition(stateMachine, vision),
        };
    }

    public override void Enter()
    {
        _isSeeTarget = _vision.TrySeeTarget(out _target);
        Animator.SetBool(GlobalConstants.AnimatorParameters.follow, _isSeeTarget);
    }

    public override void Update()
    {
        if (_target != null)
        {
            Mover.SetSpeedRun(_target);
            CalculateDirectionForAnimator(_target);
        }
    }

    public override void Exit()
    {
        _isSeeTarget = false;
        Animator.SetBool(GlobalConstants.AnimatorParameters.follow, _isSeeTarget);
    }
}
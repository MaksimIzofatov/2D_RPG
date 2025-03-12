using UnityEngine;

public abstract class State
{
    protected Transition[] Transitions;
    protected Animator Animator;
    protected Mover Mover;

    protected State(StateMachine stateMachine, Animator animator, Mover mover)
    {
        Animator = animator;
        Mover = mover;
    }

    public virtual void Enter(){}

    public virtual void Exit(){}

    public virtual void Update(){}

    public virtual void TryTransit()
    {
        foreach (var transition in Transitions)
        {
            if (transition.IsNeedTransit())
            {
                transition.Transit();
                return;
            }
        }
    }
    protected void CalculateDirectionForAnimator(Transform target)
    {
        var direction = Mover.CalculateDirection(target);
        Animator.SetFloat(GlobalConstants.AnimatorParameters.walkX, direction.x);
        Animator.SetFloat(GlobalConstants.AnimatorParameters.walkY, direction.y);
        Animator.SetBool(GlobalConstants.AnimatorParameters.isDirX, direction.x > direction.y);
    }
}
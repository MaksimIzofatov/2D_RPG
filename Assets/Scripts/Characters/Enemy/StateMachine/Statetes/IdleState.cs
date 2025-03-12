using UnityEngine;

public class IdleState : State
{
    private float _waitTime;
    private float _endWaitTime;
    public bool IsEndWay => Time.time >= _endWaitTime;
    public IdleState(StateMachine stateMachine, Animator animator, Mover mover, EnemyVision enemyVision, float waitTime) : base(stateMachine, animator, mover)
    {
        _waitTime = waitTime;
        Transitions = new Transition[]
        {
            new SeeTargetTransition(stateMachine, enemyVision),
            new EndIdleTransition(stateMachine, this)
        };
    }

    public override void Enter()
    {
        _endWaitTime = Time.time + _waitTime;
        ResetAnimations();
    }
    private void ResetAnimations()
    {
        Animator.SetFloat(GlobalConstants.AnimatorParameters.walkX, 0);
        Animator.SetFloat(GlobalConstants.AnimatorParameters.walkY, 0);
    }
    
}
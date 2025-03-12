using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Animator animator, EnemyVision vision, Mover mover, 
        WayPoints[] wayPoints, Transform transform, float maxSqrDistance, float waitTime)
    {
        States = new Dictionary<Type, State>()
        {
            {typeof(PatrolState), new PatrolState(this, animator, vision, mover, wayPoints, transform, maxSqrDistance)},
            {typeof(IdleState), new IdleState(this, animator, mover, vision, waitTime)},
            {typeof(FollowState), new FollowState(this, animator, mover, vision)}
        };
        
        ChangeState<PatrolState>();
    }
}
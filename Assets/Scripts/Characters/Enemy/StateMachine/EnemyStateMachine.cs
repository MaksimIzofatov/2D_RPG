using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyStateMachine(Animator animator, EnemyVision vision, Mover mover, 
        WayPoints[] wayPoints, Transform transform, float maxSqrDistance, float waitTime, EnemyAttacker attacker)
    {
        States = new Dictionary<Type, State>()
        {
            {typeof(PatrolState), new PatrolState(this, animator, vision, mover, wayPoints, transform, maxSqrDistance, attacker.SqrDistance)},
            {typeof(IdleState), new IdleState(this, animator, mover, vision, waitTime, attacker.SqrDistance)},
            {typeof(FollowState), new FollowState(this, animator, mover, vision, attacker.SqrDistance)},
            {typeof(AttackState), new AttackState(this, animator, mover, vision, attacker)}
        };
        
        ChangeState<PatrolState>();
    }
    
    
}
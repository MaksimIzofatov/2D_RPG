    using System;
    using UnityEngine;

    public class EnemyAnimationEvent : MonoBehaviour
    {
        public event Action Attack;
        public event Action EndAttack;

        public void InvokeAttackEvent()
        {
            Attack?.Invoke();
        }
        
        public void InvokeEndAttackEvent()
        {
            EndAttack?.Invoke();
        }
    }

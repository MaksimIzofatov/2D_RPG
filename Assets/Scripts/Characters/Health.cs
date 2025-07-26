
    using System;
    using UnityEngine;

    public class Health
    {
        public event Action TakingDamage;
        public event Action<float, float> ValueChanged;
        public event Action Died;
        public int MaxHealth { get; }
        public int CurrentHealth { get; private set; }
        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = MaxHealth;
        }

        public void ApplyDamage(int damage)
        {
            if(damage < 0 ) return;
            ChangeValue(-damage);
            
            TakingDamage?.Invoke();

            if (CurrentHealth == 0)
            {
                Died?.Invoke(); 
            }
        }

        public void ApplyHeal(int heal)
        {
            if(heal < 0 ) return;
            ChangeValue(heal);
        }

        private void ChangeValue(int value)
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, MaxHealth);
            ValueChanged?.Invoke(CurrentHealth, MaxHealth);
        }
    }
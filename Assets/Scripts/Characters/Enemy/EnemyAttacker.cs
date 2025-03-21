    using UnityEngine;

    public class EnemyAttacker : Attacker
    {
        private float _endAttackTime;
        public bool IsAttack { get; private set; }
        
        public override void Attack()
        {
            
            
            var player = Physics2D.OverlapCircle(transform.position + _offsetAttackSphere, 
                _seeRadius, _targetLayer);
            player?.GetComponent<Player>().ApplyDamage(_damage);
        }

        public void StartAttack()
        {
            IsAttack = true;
            _endWaitTime = Time.time + _delay;
        }

        public void OnEndAttack()
        {
            IsAttack = false;
        }
    }

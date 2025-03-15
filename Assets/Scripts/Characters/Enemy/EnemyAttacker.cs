    using UnityEngine;

    public class EnemyAttacker : Attacker
    {
        private float _endAttackTime;
        public bool IsAttack => _endAttackTime > Time.time;
        
        public override void Attack()
        {
            base.Attack();
            ////
            _endAttackTime = Time.time + 1;
            ////
            _animator.SetTrigger(GlobalConstants.AnimatorParameters.isAttack);
            var player = Physics2D.OverlapCircle(transform.position + _offsetAttackSphere, 
                _seeRadius, _targetLayer);
            player?.GetComponent<Player>().ApplyDamage(_damage);
        }
    }

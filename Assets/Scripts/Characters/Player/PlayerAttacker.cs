
    using System;
    using UnityEngine;

    public class PlayerAttacker : Attacker
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        public override void Attack()
        {
            _endWaitTime = Time.time + _delay;
            _animator.SetTrigger(GlobalConstants.AnimatorParameters.isAttack);
            _animator.SetFloat(GlobalConstants.AnimatorParameters.dirAttackX, _offsetAttackSphere.x);
            _animator.SetFloat(GlobalConstants.AnimatorParameters.dirAttackY, _offsetAttackSphere.y);
            var enemy = Physics2D.OverlapCircle(transform.position + _offsetAttackSphere, 
                _seeRadius, _targetLayer);
            enemy?.GetComponent<Enemy>().ApplyDamage(_damage);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + _offsetAttackSphere, _seeRadius);
        }
    }

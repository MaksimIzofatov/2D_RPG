
    using System;
    using UnityEngine;

    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private float _seeRadius = 0.75f;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _damage = 1;

        private Vector3 _offsetAttackSphere =  Vector3.zero;
        private Animator _animator;
        

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Attack()
        {
            _animator.SetTrigger(GlobalConstants.AnimatorParameters.isAttack);
            _animator.SetFloat(GlobalConstants.AnimatorParameters.dirAttackX, _offsetAttackSphere.x);
            _animator.SetFloat(GlobalConstants.AnimatorParameters.dirAttackY, _offsetAttackSphere.y);
            var enemy = Physics2D.OverlapCircle(transform.position + _offsetAttackSphere, 
                _seeRadius, _targetLayer);
            enemy?.GetComponent<Enemy>().ApplyDamage(_damage);
        }

        public void ChangeDirectionForAttack(float x, float y)
        {
            if (x > 0.05f)
            {
                _offsetAttackSphere.x = 1;
                _offsetAttackSphere.y = 0;
            }
            else if (x < -0.05f)
            {
                _offsetAttackSphere.x = -1;
                _offsetAttackSphere.y = 0;
            }
            else if (y > 0.05f)
            {
                _offsetAttackSphere.y = 1;
                _offsetAttackSphere.x = 0;
            }
            else if (y < -0.05f)
            {
                _offsetAttackSphere.y = -1;
                _offsetAttackSphere.x = 0;
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + _offsetAttackSphere, _seeRadius);
        }
    }

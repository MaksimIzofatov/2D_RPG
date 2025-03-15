    using UnityEngine;

    public abstract class Attacker : MonoBehaviour
    {
        [SerializeField] protected float _seeRadius = 0.75f;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected int _damage = 1;

        protected Vector3 _offsetAttackSphere =  Vector3.zero;
        protected Animator _animator;
        

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public abstract void Attack();

        public virtual void ChangeDirectionForAttack(float x, float y)
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

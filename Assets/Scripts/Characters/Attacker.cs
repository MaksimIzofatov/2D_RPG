    using UnityEngine;

    public abstract class Attacker : MonoBehaviour
    {
        [SerializeField] protected float _delay;
        [SerializeField] protected float _seeRadius;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected int _damage;
        [SerializeField] protected float _distance;
        
        public bool CanAttack => _endWaitTime <= Time.time;
        public float SqrDistance => _distance * _distance;
        protected Vector3 _offsetAttackSphere =  Vector3.zero;
        protected Animator _animator;
        protected float _endWaitTime;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public virtual void Attack()
        {
            _endWaitTime = Time.time + _delay;
        }

        public virtual void ChangeDirectionForAttack(float x, float y)
        {
            if (x > 0.05f)
            {
                _offsetAttackSphere.x = _distance;
                _offsetAttackSphere.y = 0;
            }
            else if (x < -0.05f)
            {
                _offsetAttackSphere.x = -_distance;
                _offsetAttackSphere.y = 0;
            }
            else if (y > 0.05f)
            {
                _offsetAttackSphere.y = _distance;
                _offsetAttackSphere.x = 0;
            }
            else if (y < -0.05f)
            {
                _offsetAttackSphere.y = -_distance;
                _offsetAttackSphere.x = 0;
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + _offsetAttackSphere, _seeRadius);
        }
    }

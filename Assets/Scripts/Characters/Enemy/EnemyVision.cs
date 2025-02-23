using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float _seeRadius = 5;
    [SerializeField] private LayerMask _targetLayer;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _seeRadius);
    }
    
    public bool TrySeeTarget(out Transform target)
    {
        target = null;
        var hit = Physics2D.OverlapCircle(transform.position, _seeRadius, _targetLayer);

        if (hit != null)
        {
            target = hit.transform;
            return true;
        }
            
        return false;
    }

}
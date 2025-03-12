using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private const int SPEED_COEFFICIENT = 50;
    
    [SerializeField] private float _speedWalk = 2;
    [SerializeField] private float _speedRun = 5;
    [SerializeField] private float _force = 10;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }
    
    public void AddForce()
    {
        _rb.AddForce(_rb.velocity * _force, ForceMode2D.Impulse);
    }

    public void Move(float directionX, float directionY)
    {
        var x = _speedRun * directionX * SPEED_COEFFICIENT * Time.deltaTime;
        var y = _speedRun * directionY * SPEED_COEFFICIENT * Time.deltaTime;
        
        _rb.velocity = new Vector2(x, y);
    }
    
    private void Move(Transform target, float speed)
    {
        var newPosition = Vector2.MoveTowards(transform.position, target.position, 
            speed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);
    }
    
    public Vector3 CalculateDirection(Transform target)
    {
        var direction = (target.position - transform.position).normalized;
        return direction;
    }

    public void SetSpeedWalk(Transform target) => Move(target, _speedWalk);
    public void SetSpeedRun(Transform target) =>Move(target, _speedRun);
}

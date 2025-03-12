using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    private const int SPEED_COEFFICIENT = 50;
    
    [SerializeField] private float _speed = 5;
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
        var x = _speed * directionX * SPEED_COEFFICIENT * Time.deltaTime;
        var y = _speed * directionY * SPEED_COEFFICIENT * Time.deltaTime;
        
        _rb.velocity = new Vector2(x, y);
    }
    
    public void Move(Transform target)
    {
        var newPosition = Vector2.MoveTowards(transform.position, target.position, 
            _speed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);
    }
    
    public Vector3 CalculateDirection(Transform target)
    {
        var direction = (target.position - transform.position).normalized;
        return direction;
    }

    public void SetSpeedWalk() => _speed = 2;
    public void SetSpeedRun() => _speed = 5;
}

using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const int SPEED_COEFFICIENT = 50;
    
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _force = 1000;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }
    
    public void AddForce()
    {
        _rb.AddForce(_rb.velocity * _force);
    }

    public void Move(float directionX, float directionY)
    {
        var x = _speed * directionX * SPEED_COEFFICIENT * Time.deltaTime;
        var y = _speed * directionY * SPEED_COEFFICIENT * Time.deltaTime;
        
        _rb.velocity = new Vector2(x, y);
    }
}

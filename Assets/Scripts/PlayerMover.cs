using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const int SPEED_COEFFICIENT = 50;
    private const string HORIZONTAL_AXIS = "Horizontal";
    
    [SerializeField] private float _speed = 1;
    
    private Rigidbody2D _rb;
    private float _direction;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        _direction = Input.GetAxis(HORIZONTAL_AXIS);
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_speed * _direction * SPEED_COEFFICIENT * Time.deltaTime, _rb.velocity.y);
    }
}

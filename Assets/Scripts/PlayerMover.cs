using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const int SPEED_COEFFICIENT = 50;
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";
    
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _force = 20;
    
    private Rigidbody2D _rb;
    private float _directionX;
    private float _directionY;
    private bool _isAddForce;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isAddForce = true;
        }
        _directionX = Input.GetAxis(HORIZONTAL_AXIS);
        _directionY = Input.GetAxis(VERTICAL_AXIS);
    }

    private void FixedUpdate()
    {
        var x = _speed * _directionX * SPEED_COEFFICIENT * Time.deltaTime;
        var y = _speed * _directionY * SPEED_COEFFICIENT * Time.deltaTime;

        if (_isAddForce)
        {
            x *= _force;
            y *= _force;
            _isAddForce = false;
        }
        _rb.velocity = new Vector2(x, y);
    }
}

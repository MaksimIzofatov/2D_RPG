
using System;
using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerMover), typeof(PlayerAnimator))]
    public class Player : MonoBehaviour
    {
        private PlayerMover _playerMover;
        private InputReader _input;
        private PlayerAnimator _animator;

        private void Awake()
        {
            _playerMover = GetComponent<PlayerMover>();
            _input = GetComponent<InputReader>();
            _animator = GetComponent<PlayerAnimator>();
        }

        private void FixedUpdate()
        {
            _playerMover.Move(_input.DirectionX, _input.DirectionY);
            
            _animator.SetSpeedXY(_input.DirectionX != 0 && _input.DirectionY != 0);
            _animator.SetSpeedX(_input.DirectionX);
            _animator.SetSpeedY(_input.DirectionY);
            
            if(_input.GetIsAddForce())
                _playerMover.AddForce();
        }
    }

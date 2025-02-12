using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public readonly int runX = Animator.StringToHash(nameof(runX));
    public readonly int runY = Animator.StringToHash(nameof(runY));
    public readonly int runXY = Animator.StringToHash(nameof(runXY));
    [SerializeField] private Animator _animator;

    public void SetSpeedX(float speed)
    {
        _animator.SetFloat(runX, speed);
    }

    public void SetSpeedY(float speed)
    {
        _animator.SetFloat(runY, speed);
    }

    public void SetSpeedXY(bool isTrue)
    {
        _animator.SetBool(runXY, isTrue);
    }
}

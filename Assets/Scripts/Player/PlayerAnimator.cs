using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetSpeedX(float speed)
    {
        _animator.SetFloat(GlobalConstants.AnimatorParameters.runX, speed);
    }

    public void SetSpeedY(float speed)
    {
        _animator.SetFloat(GlobalConstants.AnimatorParameters.runY, speed);
    }

    public void SetSpeedXY(bool isTrue)
    {
        _animator.SetBool(GlobalConstants.AnimatorParameters.runXY, isTrue);
    }
}

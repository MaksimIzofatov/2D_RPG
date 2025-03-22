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

    public void SetHit()
    {
        _animator.SetTrigger(GlobalConstants.AnimatorParameters.isHit);
    }

    public void SetPreviousDirectionX(float directionX)
    {
        _animator.SetFloat(GlobalConstants.AnimatorParameters.previousDirX, directionX);
    }

    public void SetPreviousDirectionY( float directionY)
    {
        _animator.SetFloat(GlobalConstants.AnimatorParameters.previousDirY, directionY);
    }
}

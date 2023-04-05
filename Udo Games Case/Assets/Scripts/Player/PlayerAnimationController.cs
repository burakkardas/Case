using UnityEngine;
using UDOGames;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    public void SetRunAnimation(bool value)
    {
        _animator.SetBool(AnimationType.RUN, value);
    }



    public void PlayDanceAnimation()
    {
        _animator.SetTrigger(AnimationType.DANCE);
    }



    public void PlayAttackAnimation()
    {
        _animator.SetTrigger(AnimationType.ATTACK);
    }
}

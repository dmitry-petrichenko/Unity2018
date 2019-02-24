using System;

namespace Units.OneUnit.Base.GameObject.Animation
{
    public interface IOneUnitAnimationController : IDisposable
    {
        void PlayWalkAnimation();
        void PlayAttackAnimation();
        void PlayIdleAnimation();
        void PlayDieAnimation();
        
        event Action AttackComplete;
        event Action DieComplete;
    }
}
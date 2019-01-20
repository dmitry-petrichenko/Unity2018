using System;

namespace Units.OneUnit.Base.GameObject.Animation
{
    public interface IOneUnitAnimationController
    {
        void PlayWalkAnimation();
        void PlayAttackAnimation();
        void PlayIdleAnimation();
        
        event Action AttackComplete;
    }
}
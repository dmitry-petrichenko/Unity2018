using System;
using Units.OneUnit.Base.GameObject.Animation;

namespace Tests.Scripts.Units.UnitGameObject
{
    public class AnimationControllerMock : IOneUnitAnimationController
    {
        public void Dispose()
        {
        }

        public void PlayWalkAnimation()
        {
            PlayWalkAnimationEvent?.Invoke();
        }

        public void PlayAttackAnimation()
        {
            AttackComplete?.Invoke();
        }

        public void PlayIdleAnimation()
        {
        }

        public void PlayDieAnimation()
        {
            DieComplete?.Invoke();
        }

        public event Action PlayWalkAnimationEvent;
        public event Action AttackComplete;
        public event Action DieComplete;
    }
}
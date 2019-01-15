using UnityEngine;
using Scripts.Units.Settings;

namespace Units
{
    public interface IOneUnitAnimationController
    {
        void PlayWalkAnimation();
        void PlayAttackAnimation();
        void PlayIdleAnimation();
    }
}
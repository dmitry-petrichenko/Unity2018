using UnityEngine;
using ZScripts.Units.Settings;

namespace Units
{
    public interface IOneUnitAnimationController
    {
        void PlayWalkAnimation();
        void PlayAttackAnimation();
        void PlayIdleAnimation();
        void Initialize(IUnitSettings unitSettings);
    }
}
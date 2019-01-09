using Tests.Animation;
using UnityEngine;
using Scripts.Units.Settings;

namespace Units
{
    public class OneUnitAnimationController : IOneUnitAnimationController
    {
        private GameObject _unit;
        private IUnitScript _unitScript;
        
        public void Initialize(IUnitSettings unitSettings)
        {
            _unit = unitSettings.GraphicObject;
            _unitScript = _unit.GetComponentInChildren(typeof(IUnitScript)) as IUnitScript;
        }

        public void PlayIdleAnimation()
        {
            _unitScript.PlayIdleAnimation();
        }
        
        public void PlayAttackAnimation()
        {
            _unitScript.PlayAttackAnimation();
        }
        
        public void PlayWalkAnimation()
        {
            _unitScript.PlayWalkAnimation();
        }
    }
}
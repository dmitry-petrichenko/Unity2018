using Scripts.Units.Settings;

namespace Units.OneUnit.Base.GameObject.Animation
{
    public class OneUnitAnimationController : IOneUnitAnimationController
    {
        private UnityEngine.GameObject _unit;
        private IUnitScript _unitScript;

        public OneUnitAnimationController(IUnitSettings unitSettings)
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
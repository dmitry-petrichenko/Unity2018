using System;
using Scripts.Extensions;
using Scripts.GameLoop;
using Scripts.Units.Settings;

namespace Units.OneUnit.Base.GameObject.Animation
{
    public class OneUnitAnimationController : MyDisposable, IOneUnitAnimationController
    {
        public event Action AttackComplete;

        private readonly IGameLoopController _gameLoopController;
        
        private UnityEngine.GameObject _unit;
        private IUnitScript _unitScript;

        public OneUnitAnimationController(IUnitSettings unitSettings, IGameLoopController gameLoopController)
        {
            _unit = unitSettings.GraphicObject;
            _unitScript = _unit.GetComponentInChildren(typeof(IUnitScript)) as IUnitScript;
            _gameLoopController = gameLoopController;
        }

        public void PlayIdleAnimation()
        {
            _unitScript.PlayIdleAnimation();
        }
        
        public void PlayAttackAnimation()
        {
            _unitScript.PlayAttackAnimation();
            _gameLoopController.DelayStart(TriggerComplete, 1.5f);
        }

        public void PlayWalkAnimation()
        {
            _unitScript.PlayWalkAnimation();
        }
        
        public void PlayDieAnimation()
        {
            _unitScript.PlayDieAnimation();
        }
        
        private void TriggerComplete()
        {
            AttackComplete?.Invoke();
        }

        public void Dispose()
        {
        }
    }
}
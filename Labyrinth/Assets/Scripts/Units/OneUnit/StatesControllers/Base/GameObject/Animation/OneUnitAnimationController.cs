using System;
using Scripts.GameLoop;
using Units.OneUnit.StatesControllers.Base.Settings;

namespace Units.OneUnit.StatesControllers.Base.GameObject.Animation
{
    public class OneUnitAnimationController : Disposable, IOneUnitAnimationController
    {
        public event Action AttackComplete;
        public event Action DieComplete;

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
            _gameLoopController.DelayStart(TriggerAttackComplete, 1.5f);
        }

        public void PlayWalkAnimation()
        {
            _unitScript.PlayWalkAnimation();
        }
        
        public void PlayDieAnimation()
        {
            _unitScript.PlayDieAnimation();
            _gameLoopController.DelayStart(TriggerDieComplete, 3.5f);
        }
        
        private void TriggerAttackComplete()
        {
            AttackComplete?.Invoke();
        }
        
        private void TriggerDieComplete()
        {
            DieComplete?.Invoke();
        }

        protected override void DisposeInternal()
        {
            AttackComplete = null;
            DieComplete = null;
            _unitScript = null;
            base.DisposeInternal();
        }
    }
}
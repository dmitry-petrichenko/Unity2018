using System;
using Units;
using ZScripts.GameLoop;

namespace ZScripts.Units.Behaviour.UnitActions
{
    public class AttackAction : IUnitAction
    {
        public delegate AttackAction Factory();
        
        private float delayTime;
        private IGameLoopController _gameloopController;
        private IOneUnitController _oneUnitController;
        private IOneUnitAnimationController _animationController;
        
        public AttackAction(
            IGameLoopController gameloopController,
            IOneUnitAnimationController animationController)
        {
            _gameloopController = gameloopController;
            _animationController = animationController;
            delayTime = 1.5f;
        }
        
        public void Start()
        {
            _animationController.PlayAttackAnimation();
            _gameloopController.DelayStart(TriggerComplete, delayTime);
        }

        public void Stop()
        {
            
        }

        public void Destroy()
        {
            
        }

        public void Initialize(IOneUnitController oneUnitController)
        {
               
        }
        
        private void TriggerComplete()
        {
            if (OnComplete != null)
            {
                OnComplete();
            } 
        }

        public event Action OnComplete;
    }
}
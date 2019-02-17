using System;
using Scripts.Extensions;
using Units;
using Scripts.GameLoop;
using Units.OneUnit;
using Units.OneUnit.Base;

namespace Scripts.Units.Behaviour.UnitActions
{
    public class AttackAction : MyDisposable, IUnitAction
    {
        public delegate AttackAction Factory();
        
        private float _delayTime;
        private IntVector2 _targetPosition;
        private IGameLoopController _gameloopController;
        private IBaseActionController _baseActionController;
        
        public AttackAction(
            IGameLoopController gameloopController,
            IBaseActionController baseActionController)
        {
            _gameloopController = gameloopController;
            _baseActionController = baseActionController;
            _delayTime = 1.5f;
        }
        
        public void Start()
        {
            _baseActionController.Attack(_targetPosition);
            _gameloopController.DelayStart(TriggerComplete, _delayTime);
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

        public void Initialize(IntVector2 targetPosition)
        {
            _targetPosition = targetPosition;
        }
        
        private void TriggerComplete()
        {
            OnComplete?.Invoke();
        }

        public event Action OnComplete;

        public void Dispose()
        {
            _gameloopController = null;
            _baseActionController = null;
        }
    }
}
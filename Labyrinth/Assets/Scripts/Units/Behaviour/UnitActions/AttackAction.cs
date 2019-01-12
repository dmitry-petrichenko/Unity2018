﻿using System;
using Units;
using Scripts.GameLoop;

namespace Scripts.Units.Behaviour.UnitActions
{
    public class AttackAction : IUnitAction
    {
        public delegate AttackAction Factory();
        
        private float _delayTime;
        private IntVector2 _targetPosition;
        private IGameLoopController _gameloopController;
        private IBaseMovingController _baseMovingController;
        
        public AttackAction(
            IGameLoopController gameloopController,
            IBaseMovingController baseMovingController)
        {
            _gameloopController = gameloopController;
            _baseMovingController = baseMovingController;
            _delayTime = 1.5f;
        }
        
        public void Start()
        {
            _baseMovingController.Attack(_targetPosition);
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
    }
}
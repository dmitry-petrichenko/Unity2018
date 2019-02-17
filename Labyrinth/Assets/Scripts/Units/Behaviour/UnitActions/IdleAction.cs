using System;
using Scripts.Extensions;
using UnityEditor;
using UnityEngine;
using Scripts.GameLoop;
using Units.OneUnit;

namespace Scripts.Units.UnitActions
{
    public class IdleAction : MyDisposable, IUnitAction
    {
        public delegate MoveToPositionAction Factory();
        
        private float delayTime;
        private IGameLoopController _gameloopController;
        private IOneUnitController _oneUnitController;
        
        public IdleAction(IGameLoopController gameloopController)
        {
            _gameloopController = gameloopController;
            delayTime = UnityEngine.Random.Range(1.0f, 5.0f);
        }

        public void Initialize(IOneUnitController oneUnitController)
        {
            _oneUnitController = oneUnitController;
        }
        
        public void Start()
        {
            _oneUnitController.Wait();
            _gameloopController.DelayStart(TriggerComplete, delayTime);
        }

        private void TriggerComplete()
        {
            OnComplete?.Invoke();
        }
        
        public void Stop()
        {
           
        }

        public void Destroy()
        {
            
        }

        public event Action OnComplete;

        public void Dispose()
        {
            _oneUnitController = null;
        }
    }
}
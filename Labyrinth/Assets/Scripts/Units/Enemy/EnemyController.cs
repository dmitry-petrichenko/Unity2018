using System;
using Scripts.Units.Events;
using Units.OccupatedMap;
using Units.OneUnit;
using Units.OneUnit.Info;
using Units.OneUnit.State;
using UnityEngine;

namespace Scripts.Units.Enemy
{
    public class EnemyController : OneUnitController
    {
        public event Action AttackComplete;
        
        public delegate EnemyController Factory();
        
        private readonly IPeacefulBehaviour _peacefulBehaviour; 
        private readonly IAgressiveBehaviour _agressiveBehaviour; 
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap; 

        public EnemyController(
            IPeacefulBehaviour peacefulBehaviour,
            IAgressiveBehaviour agressiveBehaviour,
            IOccupatedPossitionsMap occupatedPossitionsMap,
            IStateControllerExternal stateController,
            ILifeController lifeController,
            IUnitEvents unitEvents,
            IUnitInfoExternal unitInfoExternal
            ) : base(occupatedPossitionsMap, unitEvents, stateController, lifeController, unitInfoExternal)
        {
            _peacefulBehaviour = peacefulBehaviour;
            _agressiveBehaviour = agressiveBehaviour;
            _occupatedPossitionsMap = occupatedPossitionsMap;
            
            Initialize();
        }
            
        void Initialize()
        {
            _peacefulBehaviour.Initialize(this);
            _agressiveBehaviour.Initialize(this);
        }

        public void Animate()
        {
            _peacefulBehaviour.Initialize(this);
            _peacefulBehaviour.Start();
        }

        public override void Attack(IntVector2 position)
        {
           var targetUnit = _occupatedPossitionsMap.GetUnitOnPosition(position);
           if (targetUnit is UnitStub)
               return;
            
            _agressiveBehaviour.Complete += AcctackCompleteHandler;
            _agressiveBehaviour.Start(targetUnit);
        }

        protected override void HealthEndedHandler()
        {
            _peacefulBehaviour.Cancel();
            _agressiveBehaviour.Cancel();
            base.HealthEndedHandler();
        }

        private void AcctackCompleteHandler()
        {
            _agressiveBehaviour.Complete -= AcctackCompleteHandler;
            _agressiveBehaviour.Cancel();
            AttackComplete?.Invoke();
        }
    }
}
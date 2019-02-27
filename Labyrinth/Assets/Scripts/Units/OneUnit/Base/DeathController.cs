using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Extensions;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using Scripts.Units.StateInfo.LivingStates;
using Units.OneUnit.Base.GameObject;

namespace Units.OneUnit.Base
{
    public class DeathController : Disposable, IDeathController
    {
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IUnitStateController _unitState;
        private readonly IEventDispatcher _eventDispatcher;
        
        public DeathController(
            IUnitGameObjectController unitGameObjectController,
            IUnitStateController unitState,
            IEventDispatcher eventDispatcher)
        {
            _unitGameObjectController = unitGameObjectController;
            _unitState = unitState;
            _eventDispatcher = eventDispatcher;

            SubscribeOnEvents();
        }
        
        private void SubscribeOnEvents()
        {
            _eventDispatcher.AddEventListener(UnitEventsTypes.HEALTH_ENDED, HealthEndedHandler);
        }
        
        private void UnsubscribeOnEvents()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.HEALTH_ENDED, new Action(HealthEndedHandler));
        }

        private void HealthEndedHandler()
        {
            _unitState.CurrentState.SetDeadState();
            _unitGameObjectController.SetHealthBarVisible(false);
            _unitGameObjectController.Die();
        }

        protected override void DisposeInternal()
        {
            UnsubscribeOnEvents();
            base.DisposeInternal();
        }
    }
}
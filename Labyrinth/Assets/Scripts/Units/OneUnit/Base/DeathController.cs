using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Units.Events;
using Units.OneUnit.Base.GameObject;

namespace Units.OneUnit.Base
{
    public class DeathController : IDeathController
    {
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IEventDispatcher _eventDispatcher;
        
        public DeathController(
            IUnitGameObjectController unitGameObjectController,
            IEventDispatcher eventDispatcher)
        {
            _unitGameObjectController = unitGameObjectController;
            _eventDispatcher = eventDispatcher;

            SubscribeOnEvents();
        }
        
        private void SubscribeOnEvents()
        {
            _eventDispatcher.AddEventListener(UnitEventsTypes.HEALTH_ENDED, HealthEndedHandler);
        }
        
        private void UnsubscribeOnEvents()
        {
            
        }

        private void HealthEndedHandler()
        {
            _unitGameObjectController.Die();
        }
    }
}
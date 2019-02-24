using ID5D6AAC.Common.EventDispatcher;
using Scripts.Extensions;
using Scripts.Units.Events;
using Scripts.Units.Settings;
using Units.OneUnit.Base.GameObject;

namespace Units.OneUnit.Base
{
    public class HealthController : Disposable, IHealthController
    {
        private readonly IBaseActionController _baseActionController;
        private readonly IUnitSettings _unitSettings;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IEventDispatcher _eventDispatcher;
        
        private int _currentHealth = 0;

        public HealthController(
            IUnitSettings unitSettings,
            IUnitGameObjectController unitGameObjectController,
            IEventDispatcher eventDispatcher)
        {
            _unitGameObjectController = unitGameObjectController;
            _unitSettings = unitSettings;
            _currentHealth = _unitSettings.TotalHealth;
            _eventDispatcher = eventDispatcher;
        }

        public void TakeDamage(int value)
        {
            _currentHealth -= value;
            _unitGameObjectController.SetHealthBarValue((float)_currentHealth / (float)_unitSettings.TotalHealth);
            if (_currentHealth <= 0)
            {
                _eventDispatcher.DispatchEvent(UnitEventsTypes.HEALTH_ENDED);
            }
        }

        public void Dispose()
        {
        }
    }
}
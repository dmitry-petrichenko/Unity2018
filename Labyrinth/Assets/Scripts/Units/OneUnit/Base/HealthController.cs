using Scripts.Units.Settings;
using Units.OneUnit.Base.GameObject;

namespace Units.OneUnit.Base
{
    public class HealthController : IHealthController
    {
        private readonly IBaseActionController _baseActionController;
        private readonly IUnitSettings _unitSettings;
        private readonly IUnitGameObjectController _unitGameObjectController;
        
        private int _currentHealth = 0;

        public HealthController(
            IUnitSettings unitSettings,
            IUnitGameObjectController unitGameObjectController)
        {
            _unitGameObjectController = unitGameObjectController;
            _unitSettings = unitSettings;
            _currentHealth = _unitSettings.TotalHealth;
        }

        public void TakeDamage(int value)
        {
            _currentHealth -= value;
            _unitGameObjectController.SetHealthBarValue((float)_currentHealth / (float)_unitSettings.TotalHealth);
        }
    }
}
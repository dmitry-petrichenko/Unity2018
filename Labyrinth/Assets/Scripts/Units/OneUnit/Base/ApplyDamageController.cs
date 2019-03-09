using Scripts;
using Scripts.Units.Settings;
using Units.OccupatedMap;

namespace Units.OneUnit.Base
{
    public class ApplyDamageController : Disposable, IApplyDamageController
    {
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        private readonly IUnitSettings _unitSettings;
        
        private IOneUnitController _targetUnit;
        
        public ApplyDamageController(
            IUnitSettings unitSettings,
            IOccupatedPossitionsMap occupatedPossitionsMap)
        {
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _unitSettings = unitSettings;
        }

        public void ApplyDamageOnPosition(IntVector2 position)
        {
            _targetUnit = _occupatedPossitionsMap.GetUnitOnPosition(position);
            _targetUnit.TakeDamage(_unitSettings.Attack);
        }

        protected override void DisposeInternal()
        {
            _targetUnit = null;
            base.DisposeInternal();
        }
    }
}
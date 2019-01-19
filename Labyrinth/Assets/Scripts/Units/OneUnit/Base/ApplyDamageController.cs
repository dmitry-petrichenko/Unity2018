using Scripts;
using Scripts.Units.Settings;

namespace Units.OneUnit.Base
{
    public class ApplyDamageController : IApplyDamageController
    {
        private readonly IUnitsTable _unitsTable;
        private readonly IUnitSettings _unitSettings;
        
        private IOneUnitController _targetUnit;
        
        public ApplyDamageController(
            IUnitSettings unitSettings,
            IUnitsTable unitsTable)
        {
            _unitsTable = unitsTable;
            _unitSettings = unitSettings;
        }

        public void ApplyDamageOnPosition(IntVector2 position)
        {
            _targetUnit = _unitsTable.GetUnitOnPosition(position);
            _targetUnit.TakeDamage(_unitSettings.Attack);
        }
    }
}
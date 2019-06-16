using System;
using System.ComponentModel;
using Units.OneUnit;
using Unity.Collections.LowLevel.Unsafe;

namespace Units.Scenarios
{
    public class ChaosUnitController
    {
        private readonly IOneUnitController _unit;
        private readonly Func<IOneUnitController> _otherUnitFactory;

        private IOneUnitController _currentOtherUnit;
        
        public ChaosUnitController(IOneUnitController unit, Func<IOneUnitController> otherUnitFactory)
        {
            _unit = unit;
            _otherUnitFactory = otherUnitFactory;

            Initialize();
        }

        private void Initialize()
        {
            _unit.UnitEvents.HealthEnded += OnHealthEnded;
            AttackOtherUnit();
        }

        private void AttackOtherUnit()
        {
            _currentOtherUnit = _otherUnitFactory.Invoke();
            _currentOtherUnit.UnitEvents.HealthEnded += OnCurrentOtherUnitHealthEnded;
            _unit.Attack(_currentOtherUnit.Position);
        }

        private void OnCurrentOtherUnitHealthEnded()
        {
            if (_currentOtherUnit != null)
            {
                _currentOtherUnit.UnitEvents.HealthEnded -= OnCurrentOtherUnitHealthEnded;
            }

            AttackOtherUnit();
        }

        private void OnHealthEnded()
        {
            Dispose();
        }

        private void Dispose()
        {
            if (_currentOtherUnit != null)
            {
                _currentOtherUnit.UnitEvents.HealthEnded -= OnCurrentOtherUnitHealthEnded;
                _currentOtherUnit = null;
            }
            _unit.UnitEvents.HealthEnded -= OnHealthEnded;
        }
    }
}
using System;
using Scripts.Units.Enemy;
using Units.OneUnit;

namespace Units.Scenarios
{
    public class ChaosUnitController
    {
        private readonly EnemyController _unit;
        private readonly Func<IOneUnitController> _otherUnitFactory;

        private IOneUnitController _currentOtherUnit;
        
        public ChaosUnitController(EnemyController unit, Func<IOneUnitController> otherUnitFactory)
        {
            _unit = unit;
            _otherUnitFactory = otherUnitFactory;

            Initialize();
        }

        private void Initialize()
        {
            _unit.UnitEvents.HealthEnded += OnHealthEnded;
            _unit.AttackComplete += AttackOtherUnit;
            AttackOtherUnit();
        }

        private void AttackOtherUnit()
        {
            _currentOtherUnit = _otherUnitFactory.Invoke();
            if (_currentOtherUnit == null)
            {
                _unit.Wait();
                return;
            }
            _unit.Attack(_currentOtherUnit.Position);
        }

        private void OnHealthEnded()
        {
            Dispose();
        }

        private void Dispose()
        {
            if (_currentOtherUnit != null)
            {
                _currentOtherUnit = null;
            }
            _unit.UnitEvents.HealthEnded -= OnHealthEnded;
        }
    }
}
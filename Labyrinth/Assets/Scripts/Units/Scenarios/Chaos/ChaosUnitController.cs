using System.Collections.Generic;
using System.Linq;
using Scripts;
using Scripts.Units.Enemy;
using Units.OccupatedMap;
using Units.OneUnit;
using Units.Player;

namespace Units.Scenarios
{
    public class ChaosUnitController
    {
        private readonly EnemyController _unit;
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        private readonly UnitsCountNotifier _unitsCountNotifier;
        private readonly ChaosBattlefield.SquareArea _area;
        private readonly UnitNameResolver _unitNameResolver;
        private readonly string _unitName;

        private IOneUnitController _currentOtherUnit;
        
        public ChaosUnitController(EnemyController unit,
            UnitsCountNotifier unitsCountNotifier, 
            IOccupatedPossitionsMap occupatedPossitionsMap,
            UnitNameResolver unitNameResolver,
            ChaosBattlefield.SquareArea area)
        {
            _unit = unit;
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _unitsCountNotifier = unitsCountNotifier;
            _unitNameResolver = unitNameResolver;
            _unitName = _unitNameResolver.ResolveNextName();
            _area = area;

            Initialize();
        }

        private void Initialize()
        {
            _unitsCountNotifier.Increase(_unitName);
            _unit.UnitEvents.HealthEnded += OnHealthEnded;
            _unit.AttackComplete += OnUnitAttackComplete;
            AttackOtherUnit();
        }

        private void OnUnitAttackComplete()
        {
            AttackOtherUnit();
            _unitsCountNotifier.Decrease(_unitName);
        }
        
        private IOneUnitController GetNearestUnitInArea(ChaosBattlefield.SquareArea area, IntVector2 unitPosition)
        {
            var unitsWithDistances = new Dictionary<IOneUnitController, int>();

            var unitsInRegion = _occupatedPossitionsMap.GetUnitsInRegion(area.TopLeft, area.BottomRight);
            
            unitsInRegion.ForEach(u =>
            {
                if (!u.Position.Equals(unitPosition) && !(u is PlayerController))
                {
                    unitsWithDistances.Add(u, u.Position.GetEmpiricalValueForPoint(unitPosition));
                }
            });

            if (unitsWithDistances.Count == 0)
            {
                return null;
            }

            var ordered = unitsWithDistances.OrderBy(u => u.Value);
            
            return ordered.First().Key;
        }

        private void AttackOtherUnit()
        {
            _currentOtherUnit = GetNearestUnitInArea(_area, _unit.Position);
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
            _unit.AttackComplete -= OnUnitAttackComplete;
        }
    }
}
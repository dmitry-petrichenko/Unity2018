using System;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using Scripts.CustomDebug;
using Scripts.Units.Enemy;
using Units.OccupatedMap;
using Units.OneUnit;
using Units.OneUnit.StatesControllers;
using Units.Player;

namespace Units.Scenarios
{
    public class ChaosBattlefield : IActivatable
    {
        private EnemyController.Factory _enemyFactory;
        private IOccupatedPossitionsMap _occupatedPossitionsMap;
        private Random _random;

        private IntVector2 TOP_LEFT;
        private IntVector2 BOTTOM_RIGHT;

        private int ACTIVE_UNITS_COUNT = 3;
        private int _currentUnitsCount;

        public ChaosBattlefield(
            EnemyController.Factory enemyFactory, 
            IOccupatedPossitionsMap occupatedPossitionsMap)
        {
            _enemyFactory = enemyFactory;
            _occupatedPossitionsMap = occupatedPossitionsMap;
            
            TOP_LEFT = new IntVector2(0, 5);
            BOTTOM_RIGHT = new IntVector2(5, 0);
            
            _random = new Random();
        }

        private IntVector2 GetFreePosition(SquareArea area)
        {
            var freePositions = _occupatedPossitionsMap.GetFreePositionsInRegion(area.TopLeft, area.BottomRight);

            if (freePositions.Count == 0)
            {
                ApplicationDebugger.ThrowException("ChaosBattlefield: no free positions");
            }
            var rand = _random.Next(freePositions.Count);

            return freePositions[rand];
        }

        private void CreateAndInitializeUnit(SquareArea area)
        {
            var unit = CreateUnitOnRandomPosition(area);
            InitializeUnit(unit, area);
            _currentUnitsCount++;
        }

        private EnemyController CreateUnitOnRandomPosition(SquareArea area)
        {
            var enemy = _enemyFactory.Invoke();
            var position = GetFreePosition(area);
            enemy.SetOnPosition(position);

            return enemy;
        }

        private void InitializeUnit(EnemyController unit, SquareArea area)
        {
            IOneUnitController GetUnit()
            {
                return GetNearestUnitInArea(area, unit.Position);
            }

            unit.AttackComplete += OnUnitAttackComplete;
            
            void OnUnitAttackComplete()
            {
                unit.AttackComplete -= OnUnitAttackComplete;
                _currentUnitsCount--;
                //SetupUnits(area);
            }
            
            var chaosUnitController = new ChaosUnitController(unit, GetUnit);
        }

        private IOneUnitController GetNearestUnitInArea(SquareArea area, IntVector2 unitPosition)
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

        private void SetupUnits(SquareArea area)
        {
            while (_currentUnitsCount < ACTIVE_UNITS_COUNT)
            {
                CreateAndInitializeUnit(area);
            }
        }

        public void Activate()
        {
            var area = new SquareArea(TOP_LEFT, BOTTOM_RIGHT);
            SetupUnits(area);
        }

        public void Deactivate()
        {
        }

        private class SquareArea
        {
            private IntVector2 _topLeft, _bottomRight;

            public IntVector2 TopLeft => _topLeft;
            public IntVector2 BottomRight => _bottomRight;

            public SquareArea(IntVector2 topLeft, IntVector2 bottomRight)
            {
                _topLeft = topLeft;
                _bottomRight = bottomRight;
            }
        }
        
    }
}
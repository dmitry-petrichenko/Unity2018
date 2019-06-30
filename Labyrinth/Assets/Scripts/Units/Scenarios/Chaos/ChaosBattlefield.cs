using Scripts;
using Scripts.CustomDebug;
using Scripts.Units.Enemy;
using Units.OccupatedMap;
using Units.OneUnit.StatesControllers;
using UnityEngine;
using Random = System.Random;

namespace Units.Scenarios
{
    public class ChaosBattlefield : IActivatable
    {
        private EnemyController.Factory _enemyFactory;
        private IOccupatedPossitionsMap _occupatedPossitionsMap;
        private UnitsCountNotifier _unitsCountNotifier;
        private Random _random;
        private SquareArea _area;

        private IntVector2 TOP_LEFT;
        private IntVector2 BOTTOM_RIGHT;

        private int ACTIVE_UNITS_COUNT = 3;

        public ChaosBattlefield(
            EnemyController.Factory enemyFactory, 
            IOccupatedPossitionsMap occupatedPossitionsMap)
        {
            _enemyFactory = enemyFactory;
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _unitsCountNotifier = new UnitsCountNotifier();
            _unitsCountNotifier.UnitsCountDecreased += OnUnitsCountDecreased;
            
            TOP_LEFT = new IntVector2(0, 10);
            BOTTOM_RIGHT = new IntVector2(10, 0);
            
            _random = new Random();
        }

        private void OnUnitsCountDecreased()
        {
            SetupUnits(_area);
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
            _unitsCountNotifier.Increase();
        }

        private EnemyController CreateUnitOnRandomPosition(SquareArea area)
        {
            Debug.Log("Create Unit");
            var enemy = _enemyFactory.Invoke();
            var position = GetFreePosition(area);
            enemy.SetOnPosition(position);

            return enemy;
        }

        private void InitializeUnit(EnemyController unit, SquareArea area)
        {  
            var chaosUnitController = new ChaosUnitController(unit, _unitsCountNotifier, _occupatedPossitionsMap, area);
        }

        private void SetupUnits(SquareArea area)
        {
            while (_unitsCountNotifier.UnitsCount < ACTIVE_UNITS_COUNT)
            {
                CreateAndInitializeUnit(area);
            }
        }

        public void Activate()
        {
            _area = new SquareArea(TOP_LEFT, BOTTOM_RIGHT);
            SetupUnits(_area);
        }

        public void Deactivate()
        {
        }

        public class SquareArea
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
using System;
using Scripts;
using Scripts.Units.Enemy;
using Units.ExternalAPI;
using Units.OccupatedMap;
using Units.OneUnit;
using Units.PathFinder;
using Units.Player;

namespace Units
{
    public class UnitsController : Disposable,  IUnitsController
    {
        private EnemyController _enemy;
        private EnemyController _enemy2;
        private IPlayerController _player;

        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        private readonly IGrid _grid;
        private readonly EnemyController.Factory _enemyFactory;
        
        public UnitsController(
            EnemyController.Factory enemyFactory, 
            IOccupatedPossitionsMap occupatedPossitionsMap, 
            IGrid grid,
            IPlayerController player)
        {
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _enemyFactory = enemyFactory;
            _grid = grid;
            
            _player = player;


            for (int i = 0; i < 6; i++)
            {
                GenerateEnemy();
            }
        }

        public IPlayer Player => _player;

        public void GenerateEnemy()
        {
            START:
            var point = GetRandomPoint();
            if(!_grid.GetCell(point))
                goto START;
            if (_occupatedPossitionsMap.GetUnitOnPosition(point) is UnitStub)
            {
                var enemy = _enemyFactory.Invoke();
                enemy.SetOnPosition(point);
                enemy.Attack(_player);
            }
            else
            {
                goto START;
            }
        }

        IntVector2 GetRandomPoint()
        {
            int GetNumber()
            {
                Random r = new Random();
                return r.Next(15);
            }

            return new IntVector2(GetNumber(), GetNumber());
        }
        
        protected override void DisposeInternal()
        {
            base.DisposeInternal();
        }
    }
}
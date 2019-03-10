using System;
using Castle.Core.Internal;
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


            for (int i = 0; i < 8; i++)
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
                enemy.UnitEvents.HealthEnded += HealthEndedHandler;
                enemy.Attack(_player);
                //enemy.AttackComplete += AttackCompleteHandler;
                /*
                var un = GetRUn(enemy.Position);
                if (un == null)
                {
                    enemy.Animate();
                }
                else
                {
                    enemy.Attack(un);
                }

                void AttackCompleteHandler()
                {
                    enemy.Attack(GetRUn(enemy.Position));
                }*/
            }
            else
            {
                goto START;
            }
        }

        private IOneUnitController GetRUn(IntVector2 position)
        {
            var list = _occupatedPossitionsMap.GetUnitsInRegion(new IntVector2(0, 0), new IntVector2(15, 15));

            IOneUnitController p = null;
            foreach (var unit in list)
            {
                if (unit.Position.Equals(Player.Position))
                {
                    p = unit;
                }
            }

            if(p!=null)
                list.Remove(p);
            
            if (list.IsNullOrEmpty())
                return null;
            
            var u = NearestUnitResolver.GetNearestUnit(position, list);
            return u;
        }

        private void HealthEndedHandler()
        {
            GenerateEnemy();
        }

        IntVector2 GetRandomPoint()
        {
            int GetNumber()
            {
                Random r = new Random();
                return r.Next(14);
            }

            return new IntVector2(GetNumber(), GetNumber());
        }
        
        protected override void DisposeInternal()
        {
            base.DisposeInternal();
        }
    }
}
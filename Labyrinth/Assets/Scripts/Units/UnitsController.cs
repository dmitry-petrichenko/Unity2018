﻿using Scripts;
using Scripts.Extensions;
using Scripts.Units.Enemy;
using Units.ExternalAPI;
using Units.Player;

namespace Units
{
    public class UnitsController : Disposable,  IUnitsController
    {
        private EnemyController _enemy;
        private EnemyController _enemy2;
        private IPlayerController _player;
        
        public UnitsController(EnemyController.Factory enemyFactory, IPlayerController player)
        {
            _player = player;
            
            _enemy = enemyFactory.Invoke();
            _enemy.SetOnPosition(new IntVector2(2, 0));
            //_enemy.MoveTo(new IntVector2(3, 3));
            _enemy.Attack(player);
            //_enemy.Animate();
  /*          
            EnemyController _enemy4 = enemyFactory.Invoke();
            _enemy4.SetOnPosition(new IntVector2(2, 2));
            //_enemy4.Animate();
            _enemy4.Attack(player);
            
            EnemyController _enemy3 = enemyFactory.Invoke();
            _enemy3.SetOnPosition(new IntVector2(0, 2));
            //_enemy3.Animate();
            _enemy3.Attack(player);
            
            _enemy2 = enemyFactory.Invoke();
            _enemy2.SetOnPosition(new IntVector2(0, 0));
            _enemy2.Attack(player);
            
            EnemyController _enemy5 = enemyFactory.Invoke();
            _enemy5.SetOnPosition(new IntVector2(0, 5));
            _enemy5.Animate();
            
            EnemyController _enemy7 = enemyFactory.Invoke();
            _enemy7.SetOnPosition(new IntVector2(0, 7));*/
        }

        public IPlayer Player => _player;
    }
}
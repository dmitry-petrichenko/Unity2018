using Scripts.Units.Enemy;
using Units.OccupatedMap;
using Units.PathFinder;
using Units.Player;

namespace Units.Scenarios
{
    public class ChaosBattlefield
    {
        private EnemyController.Factory _enemyFactory;
        private IOccupatedPossitionsMap _occupatedPossitionsMap;
        private IGrid grid;

        public ChaosBattlefield(
            EnemyController.Factory enemyFactory, 
            IOccupatedPossitionsMap occupatedPossitionsMap, 
            IGrid grid,
            IPlayerController player)
        {
            
        }
    }
}
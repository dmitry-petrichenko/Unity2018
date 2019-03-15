using Scripts;
using Units.OccupatedMap;
using Units.PathFinder;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class FreePositionsMap : IFreePossitionsMap
    {
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        private readonly IGrid _grid;
        
        public FreePositionsMap(IGrid grid, IOccupatedPossitionsMap occupatedPossitionsMap)
        {
            _grid = grid;
            _occupatedPossitionsMap = occupatedPossitionsMap;
        }

        public bool IsFreePosition(IntVector2 position)
        {
            return _grid.GetCell(position) && _occupatedPossitionsMap.IsVacantPosition(position);
        }
    }
}
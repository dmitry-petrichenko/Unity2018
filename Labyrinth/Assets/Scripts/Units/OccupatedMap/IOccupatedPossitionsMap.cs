using System.Collections.Generic;
using Scripts;
using Units.OneUnit;

namespace Units.OccupatedMap
{
    public interface IOccupatedPossitionsMap
    {
        void AddUnit(IOneUnitController unit);
        void RemoveUnit(IOneUnitController unit);
        List<IntVector2>  GetOccupiedPositions();
        bool IsVacantPosition(IntVector2 position);
        IOneUnitController GetUnitOnPosition(IntVector2 index);
    }
}
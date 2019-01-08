using System.Collections.Generic;

namespace ZScripts.Units
{
    public interface IUnitsTable : IOccupatedPossitionsTable
    {
        bool IsVacantPosition(IntVector2 index);
        List<IntVector2> GetOccupiedPositions();
        void AddUnit(IOneUnitController oneUnitController);
        void removeUnit(IOneUnitController oneUnitController);
        IOneUnitController GetUnitOnPosition(IntVector2 index);
    }
}
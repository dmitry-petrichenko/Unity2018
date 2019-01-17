using System.Collections.Generic;
using Scripts;

namespace Units
{
    public interface IOccupatedPossitionsTable
    {
        void SetVacant(IntVector2 index);
        bool IsVacantPosition(IntVector2 index);
        void SetOccupied(IntVector2 index);
        List<IntVector2> GetOccupiedPositions();
    }
}
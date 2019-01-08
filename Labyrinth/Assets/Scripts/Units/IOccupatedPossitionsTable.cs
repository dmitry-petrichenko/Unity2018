using System.Collections.Generic;

namespace ZScripts.Units
{
    public interface IOccupatedPossitionsTable
    {
        void SetVacant(IntVector2 index);
        bool IsVacantPosition(IntVector2 index);
        void SetOccupied(IntVector2 index);
        List<IntVector2> GetOccupiedPositions();
    }
}
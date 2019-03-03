using System.Collections.Generic;
using Scripts;

namespace Units.OneUnit.Base
{
    public interface IPathGeneratorController
    {
        IntVector2 Destination { get; }
        
        void MoveToPosition(IntVector2 position);
        void MoveToPositionAvoidingOccupiedCells(IntVector2 position, List<IntVector2> cells);
    }
}
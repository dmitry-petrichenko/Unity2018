using System;
using System.Collections.Generic;
using Scripts;

namespace Units.OneUnit.StatesControllers.Base
{
    public interface IMoveStepByStepController
    {
        event Action MovePathComplete;
        event Action<IntVector2> NoWayToDestination;
        event Action<IntVector2> NextTileOccupied;
        
        void MoveTo(List<IntVector2> path);
        void Cancel();
    }
}
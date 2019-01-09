using System;
using System.Collections.Generic;

namespace Scripts.Units
{
    public interface ISubMoveController
    {
        void MoveTo(List<IntVector2> path);
        void Cancel();
        void SetOnPosition(IntVector2 position);
        
        IntVector2 Position { get; }
        bool IsMoving { get; }
        IntVector2 Destination { get; set; }

        event Action MoveToComplete;
        event Action MoveOneStepComplete;
        event Action MoveOneStepStart;
        event Action<IntVector2> NextPositionOccupiedHandler;
        event Action<IntVector2> NoWayToPointHandler;
        
    }
}
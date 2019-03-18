using System;
using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public interface IMotionlessTargetOvertaker
    {
        void MoveToTarget(IntVector2 position);
        event Action TargetPositionChanged;
        event Action MoveToTargetComplete;
    }
}
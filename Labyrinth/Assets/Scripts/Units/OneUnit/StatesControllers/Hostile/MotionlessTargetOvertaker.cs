using System;
using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class MotionlessTargetOvertaker : IMotionlessTargetOvertaker
    {
        public void MoveToTarget(IntVector2 position)
        {
            
        }

        public event Action TargetPositionChanged;
        public event Action MoveToTargetComplete;
    }
}
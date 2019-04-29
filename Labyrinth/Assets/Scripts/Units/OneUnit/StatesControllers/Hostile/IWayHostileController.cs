using System;
using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public interface IWayHostileController
    {
        void MoveToPosition(IntVector2 position);
        void Cancel();
        event Action MoveToPositionComplete;
    }
}
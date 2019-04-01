using System;
using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public interface IWaitObstacleController
    {
        void Wait(IntVector2 position);
        event Action OstacleStateChanged;
    }
}
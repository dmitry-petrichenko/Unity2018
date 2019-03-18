using System;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public interface IWaitObstacleController
    {
        void Wait();
        event Action OstacleStateChanged;
    }
}
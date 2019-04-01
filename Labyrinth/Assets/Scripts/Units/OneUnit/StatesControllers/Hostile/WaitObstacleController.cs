using System;
using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class WaitObstacleController : IWaitObstacleController
    {
        public void Wait(IntVector2 position)
        {
        }

        public event Action OstacleStateChanged;
    }
}
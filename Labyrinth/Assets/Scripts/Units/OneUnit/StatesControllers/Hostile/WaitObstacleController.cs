using System;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class WaitObstacleController : IWaitObstacleController
    {
        public void Wait()
        {
            
        }

        public event Action OstacleStateChanged;
    }
}
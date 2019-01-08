using System;

namespace ZScripts
{
    public class GameEvents : IGameEvents
    {
        public event Action<IntVector2> PlayerPositionChanged;
        
        public void TriggerPlayerPositionChanged(IntVector2 position)
        {
            if (PlayerPositionChanged != null)
            {
                PlayerPositionChanged(position);
            }
        }
    }
}
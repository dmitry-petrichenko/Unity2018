using System;

namespace Scripts
{
    public interface IGameEvents
    {
        event Action<IntVector2> PlayerPositionChanged;
        void TriggerPlayerPositionChanged(IntVector2 position);
    }
} 
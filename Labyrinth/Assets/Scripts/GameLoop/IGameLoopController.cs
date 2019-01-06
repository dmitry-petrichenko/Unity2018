using System;

namespace ZScripts.GameLoop
{
    public interface IGameLoopController
    {
        event Action Updated;
        void DelayStart(Action action, float time);
    }
}
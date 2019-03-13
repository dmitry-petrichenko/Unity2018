using System;
using Scripts;

namespace Units.OneUnit
{
    public interface IPlacidController : IActivatable, IDisposable
    {
        IntVector2 Position { get; }

        void MoveTo(IntVector2 position);
        void Wait();
        void Wait(IntVector2 position);
        void SetOnPosition(IntVector2 position);
    }
}
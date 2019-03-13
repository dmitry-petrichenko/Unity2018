using System;
using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public interface IHostileController : IActivatable, IDisposable
    {
        void Cancel();
        void Attack(IntVector2 position);
    }
}
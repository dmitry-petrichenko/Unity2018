using System;
using Scripts;

namespace Units.OneUnit
{
    public interface IHostileController : IActivatable, IDisposable
    {
        void Cancel();
        void Attack(IntVector2 position);
    }
}
using System;
using Scripts;

namespace Units.OneUnit
{
    public interface IAttackController : IDisposable
    {
        void Cancel();
        void Attack(IntVector2 position);
        void TakeDamage(int value);
    }
}
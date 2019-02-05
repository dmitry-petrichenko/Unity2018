using System;

namespace Units.OneUnit.Base
{
    public interface IHealthController : IDisposable
    {
        void TakeDamage(int value);
    }
}
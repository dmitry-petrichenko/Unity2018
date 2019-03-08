using System;

namespace Units.OneUnit
{
    public interface ILifeController
    {
        event Action HealthEnded;
        
        void TakeDamage(int value);
    }
}
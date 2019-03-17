using System;
using Scripts;
using Units.OneUnit.StatesControllers;

namespace Units.OneUnit
{
    public interface IOneUnitApi : IPositional, IDisposable
    {        
        void MoveTo(IntVector2 position);
        void Wait();
        void Wait(IntVector2 position);
        void SetOnPosition(IntVector2 position);
        void Attack(IntVector2 position);
        void TakeDamage(int value);
    }
}
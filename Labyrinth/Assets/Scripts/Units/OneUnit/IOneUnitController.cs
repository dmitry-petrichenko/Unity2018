using System;
using Scripts;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using Units.OneUnit.Info;

namespace Units.OneUnit
{
    public interface IOneUnitController : IDisposable
    {
        IntVector2 Position { get; }
        IUnitInfoExternal StateInfo { get; }
        IUnitEvents UnitEvents { get; }
       
        void MoveTo(IntVector2 position);
        void Wait();
        void Wait(IntVector2 position);
        void SetOnPosition(IntVector2 position);
        void Attack(IntVector2 position);
        void TakeDamage(int value);
    }
}
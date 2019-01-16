using System;
using Scripts.Units.Events;
using Units;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public interface IOneUnitController
    {
        IntVector2 Position { get; }
        IUnitStateInfo UnitStateInfo { get; }
        IUnitEvents UnitEvents { get; }
       
        void MoveTo(IntVector2 position);
        void Wait();
        void Wait(IntVector2 position);
        void SetOnPosition(IntVector2 position);
    }
}
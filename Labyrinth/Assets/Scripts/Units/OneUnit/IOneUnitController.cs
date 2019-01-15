using System;
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
       
        void MoveTo(IntVector2 position);
        void Wait();
        void Wait(IntVector2 position);
        void SetOnPosition(IntVector2 position);
        
        event Action<IntVector2> PositionChanged;
        event Action MovePathComplete;        
        event Action MoveTileComplete;
    }
}
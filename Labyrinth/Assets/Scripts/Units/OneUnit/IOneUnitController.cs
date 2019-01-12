using System;
using Units;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;

namespace Scripts.Units
{
    public interface IOneUnitController : IOneUnitServicesContainer
    {
        IntVector2 Position { get; }
        void MoveTo(IntVector2 position);
        void Wait();
        void Wait(IntVector2 position);
        
        event Action<IntVector2> PositionChanged;
        event Action MovePathComplete;        
        event Action MoveTileStart;
        event Action MoveTileComplete;
    }
}
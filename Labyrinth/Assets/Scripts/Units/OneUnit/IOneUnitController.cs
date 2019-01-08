using System;
using Units;
using ZScripts.Units.Rotation;
using ZScripts.Units.Settings;

namespace ZScripts.Units
{
    public interface IOneUnitController : IOneUnitServicesContainer
    {
        IntVector2 Position { get; }
        void MoveTo(IntVector2 position);
        void Wait();
        void Wait(IntVector2 position);
        
        event Action<IntVector2> PositionChanged;
        event Action MoveToComplete;        
        event Action MoveOneStepStart;
        event Action MoveOneStepComplete;
    }
}
using System;
using Units.OneUnit;
using Units.OneUnit.StatesControllers;

namespace Scripts.Units.Events
{
    public interface IUnitEvents : IDisposable
    {
        event Action<IntVector2> PositionChanged;
        event Action MovePathComplete;
        event Action MoveTileComplete;
        event Action AttackComplete;
        event Action HealthEnded;
        event Action DieComplete;
        void AddPositionChangedHandler(Action handler, IPositional subscriber);
        void RemovePositionChangedHandler(Action handler, IPositional subscriber);
    }
}
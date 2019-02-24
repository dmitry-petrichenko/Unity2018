using System;

namespace Scripts.Units.Events
{
    public interface IUnitEvents : IDisposable
    {
        event Action<IntVector2> PositionChanged;
        event Action MovePathComplete;
        event Action MoveTileComplete;
        event Action AttackComplete;
        event Action Died;
        event Action DieComplete;
    }
}
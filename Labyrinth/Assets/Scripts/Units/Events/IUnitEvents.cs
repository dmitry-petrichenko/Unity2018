using System;

namespace Scripts.Units.Events
{
    public interface IUnitEvents
    {
        event Action<IntVector2> PositionChanged;
        event Action MovePathComplete;
        event Action MoveTileComplete;
    }
}
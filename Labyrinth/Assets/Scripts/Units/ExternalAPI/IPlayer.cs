using System;
using Scripts;

namespace Units.ExternalAPI
{
    public interface IPlayer
    {
        event Action<IntVector2> PositionChanged;
        
        void MoveTo(IntVector2 position);
        
        object GraphicObject { get; }
        IntVector2 Position { get; }
    }
}
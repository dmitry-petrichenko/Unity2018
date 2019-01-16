using System;
using Scripts;
using Scripts.Units.Events;

namespace Units.ExternalAPI
{
    public interface IPlayer
    {
        void MoveTo(IntVector2 position);
        
        object GraphicObject { get; }
        IntVector2 Position { get; }
        IUnitEvents UnitEvents { get; }
    }
}
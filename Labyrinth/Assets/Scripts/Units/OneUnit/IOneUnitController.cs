using Scripts;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;

namespace Units.OneUnit
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
        void TakeDamage(int value);
    }
}
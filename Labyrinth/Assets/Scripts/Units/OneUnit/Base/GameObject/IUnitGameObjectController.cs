using System;
using Scripts;

namespace Units.OneUnit.Base.GameObject
{
    public interface IUnitGameObjectController
    {
        IntVector2 Position { get; }
        bool IsMoving { get; }
        
        void MoveTo(IntVector2 position);
        void Wait();
        void Wait(IntVector2 position);
        void Attack(IntVector2 position);
        void SetOnPosition(IntVector2 position);
        void SetHealthBarValue(float value);
        
        event Action MoveComplete;
    }
}
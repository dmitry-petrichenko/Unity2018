using System;
using Scripts;

namespace Units.OneUnit.Base.GameObject
{
    public interface IUnitGameObjectController : IDisposable
    {
        IntVector2 Position { get; }
        bool IsMoving { get; }
        
        void MoveTo(IntVector2 position);
        void Wait();
        void Wait(IntVector2 position);
        void Attack(IntVector2 position);
        void Die();
        void SetOnPosition(IntVector2 position);
        void SetHealthBarValue(float value);
        void SetHealthBarVisible(bool value);
        
        event Action MoveComplete;
    }
}
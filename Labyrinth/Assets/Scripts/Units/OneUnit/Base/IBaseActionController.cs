using System;
using System.Collections.Generic;
using Scripts;

namespace Units.OneUnit.Base
{
    public interface IBaseActionController : IDisposable
    {
        void Wait();
        void Wait(IntVector2 position);
        void Attack(IntVector2 position);
        void MoveTo(IntVector2 position);
        void MoveTo(List<IntVector2> path);
        void Cancel();
        void SetOnPosition(IntVector2 position);
        void TakeDamage(int value);
        
        IntVector2 Position { get; }
        IntVector2 Destination { get; }
        bool IsMoving { get; }
    }
}
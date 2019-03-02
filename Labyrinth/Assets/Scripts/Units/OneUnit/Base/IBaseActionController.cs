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
        
        event Action<IntVector2> NoWayToAttackDestination;
        event Action<IntVector2> NoWayToWalkDestination;
        event Action<IntVector2> NextTileOccupied;
        event Action MovePathComplete;
    }

    public interface IBaseActionControllerInternal : IDisposable
    {
        void RaiseNoWayToAttackDestination(IntVector2 position);
        void RaiseNoWayToWalkDestination(IntVector2 position);
        void RaiseNextTileOccupied(IntVector2 position);
        void RaiseMovePathComplete();
    }
}
using System;
using System.Collections.Generic;
using Scripts;

namespace Units.OneUnit.Base
{
    public interface IBaseActionController : IDisposable
    {
        void Wait();
        void WaitPosition(IntVector2 position);
        void Attack(IntVector2 position);
        void MoveToPosition(IntVector2 position);
        void MoveToAvoidingOccupiedCells(IntVector2 position, List<IntVector2> cells);
        void Cancel();
        void SetOnPosition(IntVector2 position);
        void TakeDamage(int value);
        
        void SetAttackState();
        void SetPlacidState();
        
        IntVector2 Position { get; }
        IntVector2 Destination { get; }
        
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
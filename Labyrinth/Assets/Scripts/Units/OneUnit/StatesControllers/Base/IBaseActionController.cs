using System;
using System.Collections.Generic;
using Scripts;

namespace Units.OneUnit.StatesControllers.Base
{
    public interface IBaseActionController : IPositional, IDisposable
    {
        void Wait();
        void WaitPosition(IntVector2 position);
        void Attack(IntVector2 position);
        void MoveToPosition(IntVector2 position);
        void SetOnPosition(IntVector2 position);
        void SetHealthBarValue(float value);
        void MoveToAndMakeAction(IntVector2 position, Action action);
        void Die();
        
        IntVector2 Position { get; }
        IntVector2 Destination { get; }
        
        event Action<IntVector2> NoWayToDestination;
        event Action MovePathComplete;
        event Action MoveTileComplete;
        event Action MoveTileStart;
        event Action AttackComplete;
        event Action DieComplete;
    }
}
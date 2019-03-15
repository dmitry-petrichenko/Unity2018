using System;
using Scripts;
using Units.OneUnit.StatesControllers.Base;

namespace Tests.Scripts.Units.StatesControllers.Hostile
{
    public class BaseActionControlleMock : IBaseActionController
    {
        private IntVector2 _position;
        private IntVector2 _destination;

        public void RiseNoWayToDestination(IntVector2 occupiedPositin)
        {
            NoWayToDestination?.Invoke(occupiedPositin);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Wait()
        {
            throw new NotImplementedException();
        }

        public void WaitPosition(IntVector2 position)
        {
            throw new NotImplementedException();
        }

        public void Attack(IntVector2 position)
        {
            throw new NotImplementedException();
        }

        public void MoveToPosition(IntVector2 position)
        {
            throw new NotImplementedException();
        }

        public void SetOnPosition(IntVector2 position)
        {
            throw new NotImplementedException();
        }

        public void SetHealthBarValue(float value)
        {
            throw new NotImplementedException();
        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        public IntVector2 Position => _position;

        public IntVector2 Destination => _destination;

        public event Action<IntVector2> NoWayToDestination;
        public event Action MovePathComplete;
        public event Action MoveTileComplete;
        public event Action MoveTileStart;
        public event Action AttackComplete;
        public event Action DieComplete;
    }
}
using System;
using Scripts;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using UnityEngine;

namespace Units.OneUnit
{
    public class UnitStub : Disposable, IOneUnitController
    {
        private UnitEventsStub _unitEvents;
        
        public UnitStub(IntVector2 position)
        {
            Position = position;
            _unitEvents = new UnitEventsStub();
        }

        public IntVector2 Position { get; }
        public IUnitStateExternal StateInfo { get; }
        public IUnitEvents UnitEvents => _unitEvents;
        
        public void MoveTo(IntVector2 position) {}

        public void Wait() {}

        public void Wait(IntVector2 position) {}

        public void SetOnPosition(IntVector2 position) {}
        
        public void Attack(IntVector2 position) {}

        public void TakeDamage(int value) {Debug.Log("damage " + value);}
    }
    
    public class UnitEventsStub : Disposable, IUnitEvents 
    {
        public event Action<IntVector2> PositionChanged;
        public event Action MovePathComplete;
        public event Action MoveTileComplete;
        public event Action AttackComplete;
        public event Action Died;
        public event Action DieComplete;
    }
}
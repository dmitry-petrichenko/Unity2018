using System;
using Scripts;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using UnityEngine;

namespace Units.OneUnit
{
    public class OneUnitController : IOneUnitController
    {
        private readonly IUnitEvents _unitEvents;
        private readonly MoveController _moveController;
        private readonly IAttackController _attackController;
        
        private IUnitsTable _unitsTable;
        private IUnitState _stateInfo;

        public OneUnitController(
            IUnitsTable unitsTable,
            MoveController moveController,
            IAttackController attackController,
            IUnitEvents unitEvents,
            IUnitState stateInfo)
        {
            _stateInfo = stateInfo;
            _unitsTable = unitsTable;
            _moveController = moveController;            
            _unitEvents = unitEvents;
            _attackController = attackController;
            
            _unitsTable.AddUnit(this);
        }

        public IUnitState StateInfo => _stateInfo;
        public IUnitEvents UnitEvents => _unitEvents;
        public IntVector2 Position => _moveController.Position;

        public void SetOnPosition(IntVector2 position) => _moveController.SetOnPosition(position);
        public void Attack(IntVector2 position)
        {
            _attackController.Attack(position);
        }

        public void TakeDamage(int value)
        {
            _attackController.TakeDamage(value);
        }

        public void MoveTo(IntVector2 position) => _moveController.MoveTo(position);

        public void Wait() => _moveController.Wait();
        
        public void Wait(IntVector2 position) => _moveController.Wait(position);
    }

    public class UnitStub : IOneUnitController
    {
        private UnitEventsStub _unitEvents;
        
        public UnitStub(IntVector2 position)
        {
            Position = position;
            _unitEvents = new UnitEventsStub();
        }

        public IntVector2 Position { get; }
        public IUnitState StateInfo { get; }
        public IUnitEvents UnitEvents => _unitEvents;
        
        public void MoveTo(IntVector2 position) {}

        public void Wait() {}

        public void Wait(IntVector2 position) {}

        public void SetOnPosition(IntVector2 position) {}
        
        public void Attack(IntVector2 position) {}

        public void TakeDamage(int value) {Debug.Log("damage " + value);}
    }
    
    public class UnitEventsStub : IUnitEvents 
    {
        public event Action<IntVector2> PositionChanged;
        public event Action MovePathComplete;
        public event Action MoveTileComplete;
        public event Action AttackComplete;
        public event Action Died;
    }
}
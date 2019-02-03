using Scripts;
using Scripts.Units;
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
        private IStateInfo _stateInfo;

        public OneUnitController(
            IUnitsTable unitsTable,
            MoveController moveController,
            IAttackController attackController,
            IUnitEvents unitEvents,
            IStateInfo stateInfo)
        {
            _stateInfo = stateInfo;
            _unitsTable = unitsTable;
            _moveController = moveController;            
            _unitEvents = unitEvents;
            _attackController = attackController;
            
            _unitsTable.AddUnit(this);
        }

        public IStateInfo StateInfo => _stateInfo;
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
        public UnitStub(IntVector2 position)
        {
            Position = position;
        }

        public IntVector2 Position { get; }
        public IStateInfo StateInfo { get; }
        public IUnitEvents UnitEvents { get; }
        
        public void MoveTo(IntVector2 position) {}

        public void Wait() {}

        public void Wait(IntVector2 position) {}

        public void SetOnPosition(IntVector2 position) {}
        
        public void Attack(IntVector2 position) {}

        public void TakeDamage(int value) {Debug.Log("damage " + value);}
    }
}
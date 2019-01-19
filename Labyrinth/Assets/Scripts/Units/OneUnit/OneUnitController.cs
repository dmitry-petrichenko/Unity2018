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
        private IUnitStateInfo _unitStateInfo;

        public OneUnitController(
            IUnitsTable unitsTable,
            MoveController moveController,
            IAttackController attackController,
            IUnitEvents unitEvents,
            IUnitStateInfo unitStateInfo)
        {
            _unitStateInfo = unitStateInfo;
            _unitsTable = unitsTable;
            _moveController = moveController;            
            _unitEvents = unitEvents;
            _attackController = attackController;
            
            _unitsTable.AddUnit(this);
        }

        public IUnitStateInfo UnitStateInfo => _unitStateInfo;
        public IUnitEvents UnitEvents => _unitEvents;
        public IntVector2 Position => _moveController.Position;

        public void SetOnPosition(IntVector2 position) => _moveController.SetOnPosition(position);
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
        public IUnitStateInfo UnitStateInfo { get; }
        public IUnitEvents UnitEvents { get; }
        
        public void MoveTo(IntVector2 position) {}

        public void Wait() {}

        public void Wait(IntVector2 position) {}

        public void SetOnPosition(IntVector2 position) {}
        public void TakeDamage(int value) {Debug.Log("damage " + value);}
    }
}
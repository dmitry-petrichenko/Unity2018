using Scripts;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using UnityEngine;

namespace Units.OneUnit
{
    public class OneUnitController : Disposable, IOneUnitController
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
            _unitEvents.DieComplete += DieCompleteHandler;
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

        protected override void DisposeInternal()
        {
            _unitEvents.DieComplete -= DieCompleteHandler;
            base.DisposeInternal();
        }

        private void DieCompleteHandler()
        {
            Debug.Log("Die complete");
            Dispose();
        }
    }
}
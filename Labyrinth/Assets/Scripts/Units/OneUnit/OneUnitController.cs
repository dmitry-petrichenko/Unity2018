using Scripts;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using Scripts.Units.StateInfo.LivingStates;
using Units.OneUnit.Info;
using UnityEngine;

namespace Units.OneUnit
{
    public class OneUnitController : Disposable, IOneUnitController
    {
        private readonly IUnitEvents _unitEvents;
        private readonly ILivingStateControllerExternal _livingStateControllerExternal;
        private readonly IUnitInfoExternal _unitInfoExternal;
        
        private IUnitsTable _unitsTable;

        public OneUnitController(
            IUnitsTable unitsTable,
            IUnitEvents unitEvents,
            ILivingStateControllerExternal livingStateControllerExternal,
            IUnitInfoExternal unitInfoExternal)
        {
            _unitsTable = unitsTable;          
            _unitEvents = unitEvents;
            _livingStateControllerExternal = livingStateControllerExternal;
            _unitInfoExternal = unitInfoExternal;
            
            _unitsTable.AddUnit(this);
            _unitEvents.DieComplete += DieCompleteHandler;
        }

        public IUnitInfoExternal StateInfo => _unitInfoExternal;
        public IUnitEvents UnitEvents => _livingStateControllerExternal.CurrentState.UnitEvents;
        public IntVector2 Position => _livingStateControllerExternal.CurrentState.Position;

        public void SetOnPosition(IntVector2 position)
        { 
            _livingStateControllerExternal.CurrentState.SetOnPosition(position);
        }
        
        public void Attack(IntVector2 position)
        {
            _livingStateControllerExternal.CurrentState.Attack(position);
        }

        public void TakeDamage(int value)
        {
            _livingStateControllerExternal.CurrentState.TakeDamage(value);
        }

        public void MoveTo(IntVector2 position)
        { 
            _livingStateControllerExternal.CurrentState.MoveTo(position);
        }

        public void Wait()
        {
            _livingStateControllerExternal.CurrentState.Wait();
        }

        public void Wait(IntVector2 position)
        {
            _livingStateControllerExternal.CurrentState.Wait(position);
        }

        protected override void DisposeInternal()
        {
            _unitsTable.removeUnit(this);
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
using System.Collections.Generic;
using Scripts;
using Scripts.Units.StateInfo.BaseState;
using Units.OneUnit.Base.GameObject;

namespace Units.OneUnit.Base
{
    public class MoveStepByStepController : Disposable, IMoveStepByStepController
    {
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IStateControllerExternal _stateController;
        
        private List<IntVector2> _path;
        private IUnitsTable _unitsTable;
        private IntVector2 _nextOccupiedPossition;
        
        public IntVector2 Destination { get; set; }

        public MoveStepByStepController(
            IStateControllerExternal stateController,
            IUnitsTable unitsTable,
            IUnitGameObjectController unitGameObjectController
            )
        {
            _unitsTable = unitsTable;
            _unitGameObjectController = unitGameObjectController;
            _stateController = stateController;
        }   

        public void MoveTo(List<IntVector2> path)
        {
            Cancel();
            if (path.Count == 0)
            {
                _stateController.CurrentState.RaiseNoWayToDestination(_nextOccupiedPossition);
                return;
            }
            _unitGameObjectController.MoveComplete += MoveNextStep;
            _path = path;
            MoveNextStep();
        }

        public void Cancel()
        {
            _path = null;
            _unitGameObjectController.MoveComplete -= MoveNextStep;
        }

        private void MoveNextStep()
        {
            IntVector2 nextPosition;
            if (_path.Count > 0)
            {
                nextPosition = GetNextPossition();
                if (IsPositionOccupated(nextPosition)) return;
                UpdateOccupationMap(nextPosition, _unitGameObjectController.Position);
                _unitGameObjectController.MoveTo(nextPosition);
            }
            else
            {
                Cancel();
                _stateController.CurrentState.RaiseMovePathComplete();
            }
        }

        private IntVector2 GetNextPossition()
        {
            IntVector2 nextPosition = _path[0];
            _path.RemoveAt(0);

            return nextPosition;
        }

        private bool IsPositionOccupated(IntVector2 nextPosition)
        {
            if (!_unitsTable.IsVacantPosition(nextPosition))
            {
                _nextOccupiedPossition = nextPosition;
                _stateController.CurrentState.RaiseNextTileOccupied(_nextOccupiedPossition);

                return true;
            }

            return false;
        }

        private void UpdateOccupationMap(IntVector2 newPosition, IntVector2 previousPosition)
        {
            _unitsTable.SetOccupied(newPosition);
            _unitsTable.SetVacant(previousPosition);
        }

        protected override void DisposeInternal()
        {
            Cancel();
            base.DisposeInternal();
        }
    }
}
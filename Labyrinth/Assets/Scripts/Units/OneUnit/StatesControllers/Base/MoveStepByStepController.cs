using System;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using Units.OccupatedMap;
using Units.OneUnit.StatesControllers.Base.GameObject;

namespace Units.OneUnit.StatesControllers.Base
{
    public class MoveStepByStepController : Disposable, IMoveStepByStepController
    {        
        public event Action MovePathComplete;
        public event Action<IntVector2> NoWayToDestination;
        public event Action<IntVector2> NextTileOccupied;
        
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        
        private List<IntVector2> _path;
        private IntVector2 _nextOccupiedPossition;

        public IntVector2 _destination;

        public MoveStepByStepController(
            IUnitGameObjectController unitGameObjectController,
            IOccupatedPossitionsMap occupatedPossitionsMap)
        {
            _unitGameObjectController = unitGameObjectController;
            _occupatedPossitionsMap = occupatedPossitionsMap;
        }

        public void MoveTo(List<IntVector2> path)
        {
            Cancel();
            _unitGameObjectController.MoveTileComplete += MoveNextStep;
            _path = path;
            _destination = _path.Last();
            MoveNextStep();
        }

        public void Cancel()
        {
            _path = null;
            _destination = IntVector2Constant.UNASSIGNET;
            _unitGameObjectController.MoveTileComplete -= MoveNextStep;
        }

        private void MoveNextStep()
        {
            IntVector2 nextPosition;
            if (_path.Count > 0)
            {
                nextPosition = GetNextPossition();
                if (IsPositionOccupated(nextPosition)) return;
                _unitGameObjectController.MoveTo(nextPosition);
            }
            else
            {
                Cancel();
                MovePathComplete?.Invoke();
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
            if (!_occupatedPossitionsMap.IsVacantPosition(nextPosition))
            {
                _nextOccupiedPossition = nextPosition;

                if (_nextOccupiedPossition.Equals(_destination))
                {
                    NoWayToDestination?.Invoke(_nextOccupiedPossition);
                }
                else
                {
                    NextTileOccupied?.Invoke(_nextOccupiedPossition);
                }

                return true;
            }

            return false;
        }

        protected override void DisposeInternal()
        {
            Cancel();
            base.DisposeInternal();
        }
    }
}
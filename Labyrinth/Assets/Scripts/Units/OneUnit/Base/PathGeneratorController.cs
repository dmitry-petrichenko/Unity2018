using System.Collections.Generic;
using Scripts;
using Scripts.Units.StateInfo.BaseState;
using Units.OneUnit.Base.GameObject;
using Units.PathFinder;

namespace Units.OneUnit.Base
{
    public class PathGeneratorController : IPathGeneratorController
    {
        public IntVector2 Destination { get; private set; }

        private ChangeDirrectionAfterMoveTileCompleteController _changeDirrectionAfterMoveTileCompleteController;
        private IPathFinderController _pathFinderController;
        private IUnitGameObjectController _unitGameObjectController;
        private IStateControllerExternal2 _stateController;
        
        public PathGeneratorController(
            ChangeDirrectionAfterMoveTileCompleteController changeDirrectionAfterMoveTileCompleteController,
            IPathFinderController pathFinderController,
            IStateControllerExternal2 stateController,
            IUnitGameObjectController unitGameObjectController)
        {
            _changeDirrectionAfterMoveTileCompleteController = changeDirrectionAfterMoveTileCompleteController;
            _pathFinderController = pathFinderController;
            _unitGameObjectController = unitGameObjectController;
            _stateController = stateController;
        }

        public void MoveToPosition(IntVector2 position)
        {
            List<IntVector2> path = _pathFinderController.GetPath(_unitGameObjectController.Position, position, null);
            if (!IsValidPath(position, path)) return;
            Destination = position;
            _changeDirrectionAfterMoveTileCompleteController.MoveTo(path);
        }

        public void MoveToPositionAvoidingOccupiedCells(IntVector2 position, List<IntVector2> cells)
        {
            List<IntVector2> path = _pathFinderController.GetPath(_unitGameObjectController.Position, position, cells);
            if (!IsValidPath(position, path)) return;
            Destination = position;
            _changeDirrectionAfterMoveTileCompleteController.MoveTo(path);
        }

        private bool IsValidPath(IntVector2 targetPosition, List<IntVector2> path)
        {
            if (path.Count == 0)
            {
                _stateController.CurrentState.RaiseNoWayToDestination(targetPosition);
                return false;
            }

            return true;
        }
    }
}
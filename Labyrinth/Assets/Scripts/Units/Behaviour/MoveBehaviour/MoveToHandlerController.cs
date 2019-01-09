using System.Collections.Generic;
using UnityEngine;
using Scripts.Units.PathFinder;

namespace Scripts.Units
{
    //If position changed when unit is moving.
    public class MoveToHandlerController
    {
        private ISubMoveController _subMoveController;
        private IntVector2 _newPosition;
        private IPathFinderController _pathFinderController;

        public MoveToHandlerController(
            IPathFinderController pathFinderController,
            ISubMoveController subMoveController)
        {
            _subMoveController = subMoveController;
            _pathFinderController = pathFinderController;
        }

        public void MoveTo(IntVector2 position)
        {
            if (_subMoveController.IsMoving)
            {
                _newPosition = position;
                ChangeDirrection();
            }
            else
            {
                MoveToDirrection(position);
            }
        }

        private void MoveToDirrection(IntVector2 position)
        {
            List<IntVector2> path = _pathFinderController.GetPath(_subMoveController.Position, position, null);
            _subMoveController.Destination = position;
            _subMoveController.MoveTo(path);
        }
        
        private void ChangeDirrection()
        {
            _subMoveController.MoveOneStepComplete -= OnChangeDirrectionMoveCmplete;
            _subMoveController.Cancel();
            _subMoveController.MoveOneStepComplete += OnChangeDirrectionMoveCmplete;
        }
        
        private void OnChangeDirrectionMoveCmplete()
        {
            _subMoveController.MoveOneStepComplete -= OnChangeDirrectionMoveCmplete;
            MoveToDirrection(_newPosition);
        }
    }
}
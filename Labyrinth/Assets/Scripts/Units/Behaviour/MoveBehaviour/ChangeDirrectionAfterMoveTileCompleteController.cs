using System;
using System.Collections.Generic;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Units.Events;
using Scripts.Units.PathFinder;

namespace Scripts.Units
{
    public class ChangeDirrectionAfterMoveTileCompleteController
    {
        private ISubMoveController _subMoveController;
        private IntVector2 _newPosition;
        private IPathFinderController _pathFinderController;
        private readonly IEventDispatcher _eventDispatcher;
        
        public ChangeDirrectionAfterMoveTileCompleteController(
            IPathFinderController pathFinderController,
            ISubMoveController subMoveController, 
            IEventDispatcher eventDispatcher)
        {
            _subMoveController = subMoveController;
            _pathFinderController = pathFinderController;
            _eventDispatcher = eventDispatcher;
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
            _eventDispatcher.RemoveEventListener(UnitEvents.MOVE_TILE_COMPLETE, new Action(OnChangeDirrectionMoveCmplete));
            _subMoveController.Cancel();
            _eventDispatcher.AddEventListener(UnitEvents.MOVE_TILE_COMPLETE, OnChangeDirrectionMoveCmplete);
        }
        
        private void OnChangeDirrectionMoveCmplete()
        {
            _eventDispatcher.RemoveEventListener(UnitEvents.MOVE_TILE_COMPLETE, new Action(OnChangeDirrectionMoveCmplete));
            MoveToDirrection(_newPosition);
        }
    }
}
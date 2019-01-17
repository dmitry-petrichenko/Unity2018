using System;
using System.Collections.Generic;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Units.Events;
using Units.PathFinder;

namespace Units.OneUnit.Base
{
    public class ChangeDirrectionAfterMoveTileCompleteController
    {
        private IMoveStepByStepController _moveStepByStepController;
        private IntVector2 _newPosition;
        private IPathFinderController _pathFinderController;
        private readonly IEventDispatcher _eventDispatcher;
        
        public ChangeDirrectionAfterMoveTileCompleteController(
            IPathFinderController pathFinderController,
            IMoveStepByStepController moveStepByStepController, 
            IEventDispatcher eventDispatcher)
        {
            _moveStepByStepController = moveStepByStepController;
            _pathFinderController = pathFinderController;
            _eventDispatcher = eventDispatcher;
        }

        public void MoveTo(IntVector2 position)
        {
            if (_moveStepByStepController.IsMoving)
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
            List<IntVector2> path = _pathFinderController.GetPath(_moveStepByStepController.Position, position, null);
            _moveStepByStepController.Destination = position;
            _moveStepByStepController.MoveTo(path);
        }
        
        private void ChangeDirrection()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, new Action(OnChangeDirrectionMoveCmplete));
            _moveStepByStepController.Cancel();
            _eventDispatcher.AddEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, OnChangeDirrectionMoveCmplete);
        }
        
        private void OnChangeDirrectionMoveCmplete()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, new Action(OnChangeDirrectionMoveCmplete));
            MoveToDirrection(_newPosition);
        }
    }
}
using System;
using System.Collections.Generic;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Units.Events;
using Units.OneUnit.Base.GameObject;
using Units.PathFinder;

namespace Units.OneUnit.Base
{
    public class ChangeDirrectionAfterMoveTileCompleteController : Disposable
    {
        private IMoveStepByStepController _moveStepByStepController;
        private IntVector2 _newPosition;
        private IPathFinderController _pathFinderController;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IUnitGameObjectController _unitGameObjectController;
        
        public ChangeDirrectionAfterMoveTileCompleteController(
            IPathFinderController pathFinderController,
            IMoveStepByStepController moveStepByStepController, 
            IUnitGameObjectController unitGameObjectController,
            IEventDispatcher eventDispatcher)
        {
            _moveStepByStepController = moveStepByStepController;
            _pathFinderController = pathFinderController;
            _eventDispatcher = eventDispatcher;
            _unitGameObjectController = unitGameObjectController;
        }

        public void MoveTo(IntVector2 position)
        {
            if (_unitGameObjectController.IsMoving)
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
            List<IntVector2> path = _pathFinderController.GetPath(_unitGameObjectController.Position, position, null);
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

        protected override void DisposeInternal()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, new Action(OnChangeDirrectionMoveCmplete));
            base.DisposeInternal();
        }
    }
}
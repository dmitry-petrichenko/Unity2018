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
        private List<IntVector2> _newPath;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IUnitGameObjectController _unitGameObjectController;
        
        public ChangeDirrectionAfterMoveTileCompleteController(
            IMoveStepByStepController moveStepByStepController, 
            IUnitGameObjectController unitGameObjectController,
            IEventDispatcher eventDispatcher)
        {
            _moveStepByStepController = moveStepByStepController;
            _eventDispatcher = eventDispatcher;
            _unitGameObjectController = unitGameObjectController;
        }
        
        public void MoveTo(List<IntVector2> path)
        {
            if (_unitGameObjectController.IsMoving)
            {
                _newPath = path;
                ChangeDirrectionPath();
            }
            else
            {
                MoveToDirrectionPath(path);
            }
        }
        
        private void ChangeDirrectionPath()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, new Action(OnChangeDirrectionMovePathCmplete));
            _moveStepByStepController.Cancel();
            _eventDispatcher.AddEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, OnChangeDirrectionMovePathCmplete);
        }
        
        private void MoveToDirrectionPath(List<IntVector2> path)
        {
            _moveStepByStepController.MoveTo(path);
        }

        private void OnChangeDirrectionMovePathCmplete()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, new Action(OnChangeDirrectionMovePathCmplete));
            MoveToDirrectionPath(_newPath);
        }

        protected override void DisposeInternal()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, new Action(OnChangeDirrectionMovePathCmplete));
            base.DisposeInternal();
        }
    }
}
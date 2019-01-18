﻿using System.Collections.Generic;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Units;
using Scripts.Units.Events;
using Units.OneUnit.Base.GameObject;

namespace Units.OneUnit.Base
{
    public class MoveStepByStepController : IMoveStepByStepController
    {
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IEventDispatcher _eventDispatcher;
        
        private List<IntVector2> _path;
        private IUnitsTable _unitsTable;
        private IntVector2 _nextOccupiedPossition;
        
        public IntVector2 Destination { get; set; }

        public MoveStepByStepController(
            IUnitsTable unitsTable,
            IEventDispatcher eventDispatcher,
            IUnitGameObjectController unitGameObjectController
            )
        {
            _unitsTable = unitsTable;
            _eventDispatcher = eventDispatcher;
            _unitGameObjectController = unitGameObjectController;
        }   
        
        public void SetOnPosition(IntVector2 position)
        {
            _unitGameObjectController.SetOnPosition(position);
            _unitsTable.SetOccupied(position);
        }

        public IntVector2 Position => _unitGameObjectController.Position;

        public void Wait()
        {
            _unitGameObjectController.Wait();
        }
        
        public void Wait(IntVector2 position)
        {
            _unitGameObjectController.Wait(position);
        }

        public void MoveTo(List<IntVector2> path)
        {
            Reset();
            if (path.Count == 0)
            {
                _eventDispatcher.DispatchEvent(UnitEventsTypes.NO_WAY_TO_TILE, _nextOccupiedPossition);
                return;
            }
            _unitGameObjectController.MoveComplete += MoveNextStep;
            _path = path;
            MoveNextStep();
        }

        private void Reset()
        {
            _path = null;
            _unitGameObjectController.MoveComplete -= MoveNextStep;
        }

        public void Cancel()
        {
            _unitGameObjectController.MoveComplete -= MoveNextStep;
        }

        public bool IsMoving => _unitGameObjectController.IsMoving; //TODO move to state

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
                Reset();
                _eventDispatcher.DispatchEvent(UnitEventsTypes.MOVE_PATH_COMPLETE);
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
                _eventDispatcher.DispatchEvent(UnitEventsTypes.NEXT_TILE_OCCUPATED, _nextOccupiedPossition);

                return true;
            }

            return false;
        }

        private void UpdateOccupationMap(IntVector2 newPosition, IntVector2 previousPosition)
        {
            _unitsTable.SetOccupied(newPosition);
            _unitsTable.SetVacant(previousPosition);
        }
    }
}
using System;
using System.Collections.Generic;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Units.Events;
using Scripts.Units.PathFinder;

namespace Scripts.Units
{
    public class MoveConsideringOccupatedController : IDisposable
    {
        private readonly IUnitsTable _unitsTable;
        private readonly IPathFinderController _pathFinderController;
        private readonly IBaseMovingController _baseMovingController;
        private readonly IEventDispatcher _eventDispatcher;
        private List<IntVector2> _occupiedPossitions;
        
        public MoveConsideringOccupatedController(
            IUnitsTable unitsTable,
            IPathFinderController pathFinderController,
            IBaseMovingController baseMovingController,
            IEventDispatcher eventDispatcher
            )
        {
            _unitsTable = unitsTable;
            _baseMovingController = baseMovingController;
            _pathFinderController = pathFinderController;
            _eventDispatcher = eventDispatcher;

            Initialize();
        }

        public void Initialize()
        {
            SubscribeOnEvent();            
        }
        
        public void Dispose()
        {
            UnsubscribeFromEvent();
        }

        private void SubscribeOnEvent()
        {
            _eventDispatcher.AddEventListener<IntVector2>(UnitEvents.NEXT_TILE_OCCUPATED, NextPositionOccupiedHandler);
        }
        
        private void UnsubscribeFromEvent()
        {
            _eventDispatcher.RemoveEventListener(UnitEvents.NEXT_TILE_OCCUPATED,
                new Action<IntVector2>(NextPositionOccupiedHandler));
        }

        private void NextPositionOccupiedHandler(IntVector2 occupiedPosition)
        {
            _baseMovingController.Cancel();
            _occupiedPossitions = _unitsTable.GetOccupiedPositions();
            RemoveCurrentUnitPosition();
            List<IntVector2> newPath = _pathFinderController.GetPath(_baseMovingController.Position,
                _baseMovingController.Destination, _occupiedPossitions);
            _baseMovingController.MoveTo(newPath);
        }

        private void RemoveCurrentUnitPosition()
        {
            List<IntVector2> copy;
            copy = CopyDictionary(_occupiedPossitions);
            _occupiedPossitions = copy;
            _occupiedPossitions.Remove(_baseMovingController.Position);
        }
        
        private List<IntVector2> CopyDictionary(List<IntVector2> value)
        {
            List<IntVector2> copy = new List<IntVector2>();
            foreach (var index in value)
            {
                copy.Add(index);
            }

            return copy;
        }
    }
}
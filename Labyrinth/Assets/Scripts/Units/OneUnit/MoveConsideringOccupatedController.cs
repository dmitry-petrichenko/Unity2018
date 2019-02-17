using System;
using System.Collections.Generic;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Extensions;
using Scripts.Units;
using Scripts.Units.Events;
using Units.OneUnit.Base;
using Units.PathFinder;

namespace Units.OneUnit
{
    public class MoveConsideringOccupatedController : MyDisposable
    {
        private readonly IUnitsTable _unitsTable;
        private readonly IPathFinderController _pathFinderController;
        private readonly IBaseActionController _baseActionController;
        private readonly IEventDispatcher _eventDispatcher;
        private List<IntVector2> _occupiedPossitions;
        
        public MoveConsideringOccupatedController(
            IUnitsTable unitsTable,
            IPathFinderController pathFinderController,
            IBaseActionController baseActionController,
            IEventDispatcher eventDispatcher
            )
        {
            _unitsTable = unitsTable;
            _baseActionController = baseActionController;
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
            _eventDispatcher.AddEventListener<IntVector2>(UnitEventsTypes.NEXT_TILE_OCCUPATED, NextPositionOccupiedHandler);
        }
        
        private void UnsubscribeFromEvent()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.NEXT_TILE_OCCUPATED,
                new Action<IntVector2>(NextPositionOccupiedHandler));
        }

        private void NextPositionOccupiedHandler(IntVector2 occupiedPosition)
        {
            _baseActionController.Cancel();
            _occupiedPossitions = _unitsTable.GetOccupiedPositions();
            RemoveCurrentUnitPosition();
            List<IntVector2> newPath = _pathFinderController.GetPath(_baseActionController.Position,
                _baseActionController.Destination, _occupiedPossitions);
            _baseActionController.MoveTo(newPath);
        }

        private void RemoveCurrentUnitPosition()
        {
            List<IntVector2> copy;
            copy = CopyDictionary(_occupiedPossitions);
            _occupiedPossitions = copy;
            _occupiedPossitions.Remove(_baseActionController.Position);
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
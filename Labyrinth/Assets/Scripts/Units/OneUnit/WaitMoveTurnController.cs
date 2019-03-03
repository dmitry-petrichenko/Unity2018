using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Extensions;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using Units.OneUnit.Base;
using Units.OneUnit.Info;
using UnityEngine;

namespace Units.OneUnit
{
    public class WaitMoveTurnController : Disposable
    {
        private readonly IUnitsTable _unitsTable;
        private readonly IMovingRandomizer _movingRandomizer;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IBaseActionController _baseActionController;
        private readonly IUnitInfoInternal _unitInfo;
        
        private IOneUnitController _targetUnit;
        private IntVector2 _occupiedPoint;
        private IOneUnitController _oneUnitController;
        
        public WaitMoveTurnController(
            IUnitsTable unitsTable,
            IMovingRandomizer movingRandomizer,
            IEventDispatcher eventDispatcher,
            IUnitInfoInternal unitInfo,
            IBaseActionController baseActionController
            )
        {
            _unitsTable = unitsTable;
            _movingRandomizer = movingRandomizer;
            _eventDispatcher = eventDispatcher;
            _baseActionController = baseActionController;
            _unitInfo = unitInfo;

            SubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _baseActionController.NoWayToWalkDestination += NoWayToPointHandler;
        }
        
        private void UnsubscribeOnEvents()
        {
            _baseActionController.NoWayToWalkDestination -= NoWayToPointHandler;
        }

        private void NoWayToPointHandler(IntVector2 occupiedPoint)
        {
            _occupiedPoint = occupiedPoint;
            WaitUnitOnPosition(_occupiedPoint);
        }

        private void WaitUnitOnPosition(IntVector2 position)
        {
            _targetUnit = _unitsTable.GetUnitOnPosition(position);
            if (Equals(_targetUnit.StateInfo.WaitPosition, _baseActionController.Position))
            {
                IntVector2 newPosition = _movingRandomizer.GetRandomPoint(_baseActionController.Position);
                _baseActionController.MoveToPosition(newPosition);
                return;
            }

            _baseActionController.WaitPosition(_targetUnit.Position);
            _unitInfo.SetWaitPosition(position);
            _targetUnit.UnitEvents.PositionChanged += TargetUnitPositionChanged;
        }

        private void TargetUnitPositionChanged(IntVector2 position)
        {
            _targetUnit.UnitEvents.PositionChanged -= TargetUnitPositionChanged;
            if (_unitsTable.IsVacantPosition(_occupiedPoint))
            {
                MoveToDestination();
            }
            else
            {
                WaitUnitOnPosition(_occupiedPoint);
            } 
        }

        private void MoveToDestination()
        {
            _baseActionController.MoveToPosition(_baseActionController.Destination);
        }

        protected override void DisposeInternal()
        {
            UnsubscribeOnEvents();
            if (_targetUnit != null)
            {
                _targetUnit.UnitEvents.PositionChanged -= TargetUnitPositionChanged;
                _targetUnit = null;
            }
            base.DisposeInternal();
        }
    }
}
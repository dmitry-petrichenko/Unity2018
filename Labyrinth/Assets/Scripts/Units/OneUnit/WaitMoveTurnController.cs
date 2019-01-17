﻿using Scripts;
using Scripts.Units;
using Scripts.Units.StateInfo;
using Units.OneUnit.Base;

namespace Units.OneUnit
{
    public class WaitMoveTurnController
    {
        private readonly IUnitsTable _unitsTable;
        private readonly IMovingRandomizer _movingRandomizer;
        private readonly IUnitStateInfo _unitStateInfo;
        private readonly INoWayEventRouter _noWayEventRouter;
        private readonly IBaseActionController _baseActionController;
        
        private IOneUnitController _targetUnit;
        private IntVector2 _occupiedPoint;
        private IOneUnitController _oneUnitController;
        
        public WaitMoveTurnController(
            IUnitsTable unitsTable,
            IMovingRandomizer movingRandomizer,
            IUnitStateInfo unitStateInfo,
            INoWayEventRouter noWayEventRouter,
            IBaseActionController baseActionController
            )
        {
            _unitsTable = unitsTable;
            _movingRandomizer = movingRandomizer;
            _unitStateInfo = unitStateInfo;
            _noWayEventRouter = noWayEventRouter;
            _baseActionController = baseActionController;
            
            _noWayEventRouter.NoWayToPointHandler += NoWayToPointHandler;
        }

        private void NoWayToPointHandler(IntVector2 occupiedPoint)
        {
            _occupiedPoint = occupiedPoint;
            WaitUnitOnPosition(_occupiedPoint);
        }

        private void WaitUnitOnPosition(IntVector2 position)
        {
            _targetUnit = _unitsTable.GetUnitOnPosition(position);
            if (Equals(_targetUnit.UnitStateInfo.WaitPosition, _baseActionController.Position))
            {
                IntVector2 newPosition = _movingRandomizer.GetRandomPoint(_baseActionController.Position);
                _baseActionController.MoveTo(newPosition);
                return;
            }

            _baseActionController.Wait(_targetUnit.Position);
            _unitStateInfo.WaitPosition = position;
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
            _baseActionController.MoveTo(_baseActionController.Destination);
        }
    }
}
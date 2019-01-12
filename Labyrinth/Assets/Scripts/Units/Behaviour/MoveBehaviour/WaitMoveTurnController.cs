using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public class WaitMoveTurnController
    {
        private IOneUnitController _oneUnitController;
        private readonly IUnitsTable _unitsTable;
        private IOneUnitController _targetUnit;
        private IntVector2 _occupiedPoint;
        private IMovingRandomizer _movingRandomizer;
        private IOneUnitMotionController _motionController;
        private IUnitStateInfo _unitStateInfo;
        private INoWayEventRouter _noWayEventRouter;
        private ISubMoveController _subMoveController;
        
        public WaitMoveTurnController(
            IUnitsTable unitsTable,
            IMovingRandomizer movingRandomizer,
            IOneUnitMotionController oneUnitMotionController,
            IUnitStateInfo unitStateInfo,
            INoWayEventRouter noWayEventRouter,
            ISubMoveController subMoveController
            )
        {
            _unitsTable = unitsTable;
            _movingRandomizer = movingRandomizer;
            _motionController = oneUnitMotionController;
            _unitStateInfo = unitStateInfo;
            _noWayEventRouter = noWayEventRouter;
            _subMoveController = subMoveController;
        }
        
        public void Initialize(IOneUnitController oneUnitController)
        {
            _oneUnitController = oneUnitController;
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
            if (Equals(_targetUnit.UnitStateInfo.WaitPosition, _motionController.Position))
            {
                IntVector2 newPosition = _movingRandomizer.GetRandomPoint(_motionController.Position);
                _oneUnitController.MoveTo(newPosition);
                return;
            }

            _oneUnitController.Wait(_targetUnit.Position);
            _unitStateInfo.WaitPosition = position;
            _targetUnit.PositionChanged += TargetUnitPositionChanged;
        }

        private void TargetUnitPositionChanged(IntVector2 position)
        {
            _targetUnit.PositionChanged -= TargetUnitPositionChanged;
            if (_unitsTable.IsVacantPosition(_occupiedPoint))
            {
                MoveToDestination(_subMoveController.Destination);
            }
            else
            {
                WaitUnitOnPosition(_occupiedPoint);
            } 
        }

        private void MoveToDestination(IntVector2 destination)
        {
            _oneUnitController.MoveTo( _subMoveController.Destination);
        }
    }
}
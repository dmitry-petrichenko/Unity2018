using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public class WaitMoveTurnController
    {
        private readonly IUnitsTable _unitsTable;
        private readonly IMovingRandomizer _movingRandomizer;
        private readonly IUnitStateInfo _unitStateInfo;
        private readonly INoWayEventRouter _noWayEventRouter;
        private readonly IBaseMovingController _baseMovingController;
        
        private IOneUnitController _targetUnit;
        private IntVector2 _occupiedPoint;
        private IOneUnitController _oneUnitController;
        
        public WaitMoveTurnController(
            IUnitsTable unitsTable,
            IMovingRandomizer movingRandomizer,
            IUnitStateInfo unitStateInfo,
            INoWayEventRouter noWayEventRouter,
            IBaseMovingController baseMovingController
            )
        {
            _unitsTable = unitsTable;
            _movingRandomizer = movingRandomizer;
            _unitStateInfo = unitStateInfo;
            _noWayEventRouter = noWayEventRouter;
            _baseMovingController = baseMovingController;
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
            if (Equals(_targetUnit.UnitStateInfo.WaitPosition, _baseMovingController.Position))
            {
                IntVector2 newPosition = _movingRandomizer.GetRandomPoint(_baseMovingController.Position);
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
                MoveToDestination();
            }
            else
            {
                WaitUnitOnPosition(_occupiedPoint);
            } 
        }

        private void MoveToDestination()
        {
            _oneUnitController.MoveTo(_baseMovingController.Destination);
        }
    }
}
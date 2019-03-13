using Scripts;
using Units.OccupatedMap;
using Units.OneUnit.Base;
using Units.OneUnit.Info;

namespace Units.OneUnit
{
    public class NoWayPlacidController : Disposable, IActivatable
    {
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        private readonly IMovingRandomizer _movingRandomizer;
        private readonly IBaseActionController _baseActionController;
        private readonly IUnitInfoInternal _unitInfo;
        
        private IOneUnitController _targetUnit;
        private IntVector2 _occupiedPoint;
        private IOneUnitController _oneUnitController;
        
        public NoWayPlacidController(
            IOccupatedPossitionsMap occupatedPossitionsMap,
            IMovingRandomizer movingRandomizer,
            IUnitInfoInternal unitInfo,
            IBaseActionController baseActionController
            )
        {
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _movingRandomizer = movingRandomizer;
            _baseActionController = baseActionController;
            _unitInfo = unitInfo;
        }
        
        public void Activate()
        {
            SubscribeOnEvents();
        }

        public void Deactivate()
        {
            UnsubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _baseActionController.NoWayToDestination += NoWayToPointHandler;
        }
        
        private void UnsubscribeOnEvents()
        {
            _baseActionController.NoWayToDestination -= NoWayToPointHandler;
        }

        private void NoWayToPointHandler(IntVector2 occupiedPoint)
        {
            _occupiedPoint = occupiedPoint;
            WaitUnitOnPosition(_occupiedPoint);
        }

        private void WaitUnitOnPosition(IntVector2 position)
        {
            _targetUnit = _occupatedPossitionsMap.GetUnitOnPosition(position);
            if (Equals(_targetUnit.DynamicInfo.WaitPosition, _baseActionController.Position))
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
            if (_occupatedPossitionsMap.IsVacantPosition(_occupiedPoint))
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
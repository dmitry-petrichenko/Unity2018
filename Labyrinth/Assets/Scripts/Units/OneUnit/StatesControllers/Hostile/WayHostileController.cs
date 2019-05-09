using System;
using Scripts;
using Units.OneUnit.StatesControllers.Base;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class WayHostileController : IWayHostileController
    {
        private readonly IFreePointToGoResolver _freePointToGoResolver;
        private readonly IWaitObstacleController _waitObstacleController;
        private readonly IBaseActionController _baseActionController;

        private IntVector2 _freePointToGo;

        private IntVector2 AttackPosition { get; set; }

        public WayHostileController(
            IFreePointToGoResolver freePointToGoResolver,
            IBaseActionController baseActionController,
            IWaitObstacleController waitObstacleController)
        {
            _freePointToGoResolver = freePointToGoResolver;
            _waitObstacleController = waitObstacleController;
            _baseActionController = baseActionController;

            AttackPosition = IntVector2Constant.UNASSIGNET;
        }

        public void Cancel()
        {
            Reset();
        }

        public event Action MoveToPositionComplete;

        public void MoveToPosition(IntVector2 position)
        {
            if (IsPointSufficient(position, _baseActionController.Position))
            {
                Complete();
            }
            else
            {
                Reset();
                AttackPosition = position;
                _baseActionController.NoWayToDestination += NoWayToPositionHandler;
                TryMoveToPosition(position);
            }
        }

        private void TryMoveToPosition(IntVector2 position)
        {
            _freePointToGo = _freePointToGoResolver.GetFreePoint(position);
            _baseActionController.MovePathComplete += MovePathCompleteHandler;
            _baseActionController.MoveToPosition(_freePointToGo);
        }

        private void MovePathCompleteHandler()
        {
            _baseActionController.MovePathComplete -= MovePathCompleteHandler;
            if (IsPointSufficient(AttackPosition, _freePointToGo))
            {
                Complete();
            }
            else
            {
                _waitObstacleController.Wait(_freePointToGo);
                _waitObstacleController.OstacleStateChanged += OstacleStateChangedHandler;
            }
        }

        private void OstacleStateChangedHandler()
        {
            _waitObstacleController.OstacleStateChanged -= OstacleStateChangedHandler;
            TryMoveToPosition(AttackPosition);
        }

        private bool IsPointSufficient(IntVector2 attackPosition, IntVector2 currentPosition)
        {
            return attackPosition.GetAdjacentPoints().Contains(currentPosition);
        }

        private void NoWayToPositionHandler(IntVector2 obj)
        {
            _baseActionController.MovePathComplete -= MovePathCompleteHandler;
            TryMoveToPosition(AttackPosition);
        }

        private void Complete()
        {
            Reset();
            MoveToPositionComplete?.Invoke();
        }

        private void Reset()
        {
            AttackPosition = IntVector2Constant.UNASSIGNET;
            _waitObstacleController.Cancel();
            _baseActionController.NoWayToDestination -= NoWayToPositionHandler;
        }

        public void DisposeInternal()
        {
            Reset();
            MoveToPositionComplete = null;
        }
    }
}
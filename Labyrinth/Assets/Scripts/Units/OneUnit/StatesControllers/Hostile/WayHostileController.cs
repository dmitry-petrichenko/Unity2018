using System;
using Scripts;
using Units.OneUnit.StatesControllers.Base;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class WayHostileController : Disposable, IWayHostileController
    {
        public event Action MoveToPositionComplete;
        
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
            _freePointToGo = IntVector2Constant.UNASSIGNET;
        }

        public void Cancel()
        {
            Reset();
        }

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
            var freePointToGo = _freePointToGoResolver.GetFreePoint(position);
            if (_freePointToGo.Equals(IntVector2Constant.UNASSIGNET))
            {
                _freePointToGo = freePointToGo;
                BaseMoveToPosition(_freePointToGo);
                return;
            }
            
            if (freePointToGo.GetEmpiricalValueForPoint(AttackPosition) < _freePointToGo.GetEmpiricalValueForPoint(AttackPosition))
            {
                _freePointToGo = freePointToGo;
                BaseMoveToPosition(_freePointToGo);
            }
            else
            {
                Wait(_freePointToGo, AttackPosition);
            }
        }

        private void BaseMoveToPosition(IntVector2 position)
        {
            _baseActionController.MovePathComplete += MovePathCompleteHandler;
            _baseActionController.MoveToPosition(position);
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
                Wait(_freePointToGo, AttackPosition);
            }
        }

        private void Wait(IntVector2 position, IntVector2 attackPosition)
        {
            _waitObstacleController.SetAttackPosition(attackPosition);
            _waitObstacleController.Wait(position);
            _waitObstacleController.OstacleStateChanged += OstacleStateChangedHandler;
        }

        private void OstacleStateChangedHandler()
        {
            _waitObstacleController.OstacleStateChanged -= OstacleStateChangedHandler;
            _waitObstacleController.Cancel();
            TryMoveToPosition(AttackPosition);
        }

        private bool IsPointSufficient(IntVector2 attackPosition, IntVector2 currentPosition)
        {
            return attackPosition.GetAdjacentPoints().Contains(currentPosition);
        }

        private void NoWayToPositionHandler(IntVector2 obj)
        {
            _baseActionController.MovePathComplete -= MovePathCompleteHandler;
            _freePointToGo = IntVector2Constant.UNASSIGNET;
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

        protected override void DisposeInternal()
        {
            Reset();
            MoveToPositionComplete = null;
            base.DisposeInternal();
        }
    }
}
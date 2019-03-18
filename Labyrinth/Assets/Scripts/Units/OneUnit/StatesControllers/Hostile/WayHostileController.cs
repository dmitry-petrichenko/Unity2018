using System;
using Scripts;
using Units.OneUnit.Info;
using Units.OneUnit.StatesControllers.Base;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class WayHostileController : IWayHostileController
    {
        private readonly IFreePointToGoResolver _freePointToGoResolver;
        private readonly IWaitObstacleController _waitObstacleController;
        private readonly IBaseActionController _baseActionController;
        private readonly IUnitInfoExternal _unitInfo;

        private IntVector2 _freePointToGo;
        
        public WayHostileController(
            IFreePointToGoResolver freePointToGoResolver,
            IBaseActionController baseActionController,
            IUnitInfoExternal unitInfo,
            IWaitObstacleController waitObstacleController)
        {
            _freePointToGoResolver = freePointToGoResolver;
            _waitObstacleController = waitObstacleController;
            _baseActionController = baseActionController;
            _unitInfo = unitInfo;

            SubscribeOnEvents();
        }

        public event Action MoveToPositionComplete;
        
        public void MoveToPosition(IntVector2 position)
        {
            TryMoveToPosition(position);
        }
        
        private void TryMoveToPosition(IntVector2 position)
        {
            _freePointToGo = _freePointToGoResolver.GetFreePoint();
            _baseActionController.MovePathComplete += MovePathCompleteHandler;
            _baseActionController.MoveToPosition(_freePointToGo);
        }

        private void MovePathCompleteHandler()
        {
            _baseActionController.MovePathComplete -= MovePathCompleteHandler;
            if (IsPointSufficient(_freePointToGo))
            {
                MoveToPositionComplete.Invoke();
            }
            else
            {
                _waitObstacleController.Wait();
                _waitObstacleController.OstacleStateChanged += OstacleStateChangedHandler;
            }
        }

        private void OstacleStateChangedHandler()
        {
            _waitObstacleController.OstacleStateChanged -= OstacleStateChangedHandler;
            TryMoveToPosition(_unitInfo.AttackTarget.Position);
        }

        private bool IsPointSufficient(IntVector2 position)
        {
            return true;
        }
        
        private void NoWayToPositionHandler(IntVector2 obj)
        {
            _baseActionController.MovePathComplete -= MovePathCompleteHandler;
            TryMoveToPosition(_unitInfo.AttackTarget.Position);
        }
        
        private void SubscribeOnEvents()
        {
            _baseActionController.NoWayToDestination += NoWayToPositionHandler;
        }

        private void UnsubscribeFromEvents()
        {
            _baseActionController.NoWayToDestination -= NoWayToPositionHandler;
        }
    }
}
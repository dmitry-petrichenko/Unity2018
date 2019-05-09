using System;
using System.Collections.Generic;
using Scripts;
using Scripts.CustomDebug;
using Units.OneUnit.Info;
using Units.OneUnit.StatesControllers.Base;
using UnitTests.StatesControllers.Hostile;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class NoWayHostileController : Disposable, IActivatable
    {
        private readonly IFreePossitionsMap _freePossitionsMap;
        private readonly IUnitInfoExternal _unitInfo;
        private readonly IBaseActionController _baseActionController;
        private readonly IAdjacentPointsResolver _adjacentPointsResolver;

        private List<KeyValuePair<IntVector2, int>> _freePositions;

        public NoWayHostileController(
            IFreePossitionsMap freePossitionsMap,
            IUnitInfoExternal unitInfo,
            IAdjacentPointsResolver adjacentPointsResolver,
            IBaseActionController baseActionController)
        {
            _freePossitionsMap = freePossitionsMap;
            _unitInfo = unitInfo;
            _baseActionController = baseActionController;
            _adjacentPointsResolver = adjacentPointsResolver;
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
            _baseActionController.NoWayToDestination += NoWayToAttackPointHandler;
        }

        private void UnsubscribeOnEvents()
        {
            _baseActionController.NoWayToDestination -= NoWayToAttackPointHandler;
        }

        private void NoWayToAttackPointHandler(IntVector2 position)
        {
            IntVector2 freePosition = GetFirstFreePositionInUnitRange(GetAttackTargetPosition());
            if (_baseActionController.Destination.Equals(freePosition))
            {
                freePosition = GetFirstFreePositionInUnitRange(GetAttackTargetPosition(), 2);
                _baseActionController.Wait();
            }

            _baseActionController.MoveToPosition(freePosition);
        }

        private IntVector2 GetFirstFreePositionInUnitRange(IntVector2 position, int range = 1)
        {
            var adjacentPoints = _adjacentPointsResolver.GetFreeAdjacentUnitPoints(position, _freePossitionsMap.IsFreePosition, range);

            _freePositions = new List<KeyValuePair<IntVector2, int>>();
            adjacentPoints.ForEach(point => AddFreePosition(_freePositions, point));

            _freePositions.Sort(
                delegate(KeyValuePair<IntVector2, int> pair1,
                    KeyValuePair<IntVector2, int> pair2)
                {
                    return pair1.Value.CompareTo(pair2.Value);
                }
            );

            return _freePositions[0].Key;
        }

        private void AddFreePosition(List<KeyValuePair<IntVector2, int>> positions, IntVector2 position)
        {
            positions.Add(new KeyValuePair<IntVector2, int>(position, _baseActionController.Position.GetEmpiricalValueForPoint(position)));
        }

        private IntVector2 GetAttackTargetPosition()
        {
            if (_unitInfo.AttackTarget == null || _unitInfo.AttackTarget.Position.Equals(IntVector2Constant.UNASSIGNET))
            {
                ApplicationDebugger.ThrowException(GetType().Name + ": " + "unitInfo.AttackTarget cannot be NULL");
            }

            return _unitInfo.AttackTarget.Position;
        }

        protected override void DisposeInternal()
        {
            _freePositions?.Clear();
            UnsubscribeOnEvents();
            base.DisposeInternal();
        }
    }
}
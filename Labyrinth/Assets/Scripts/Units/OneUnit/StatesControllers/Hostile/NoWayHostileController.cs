using System;
using System.Collections.Generic;
using Scripts;
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

        private List<KeyValuePair<IntVector2, int>> _freePositions;

        public NoWayHostileController(
            IFreePossitionsMap freePossitionsMap,
            IUnitInfoExternal unitInfo,
            IBaseActionController baseActionController)
        {
            _freePossitionsMap = freePossitionsMap;
            _unitInfo = unitInfo;
            _baseActionController = baseActionController;
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
            _baseActionController.MoveToPosition(freePosition);
        }

        private IntVector2 GetFirstFreePositionInUnitRange(IntVector2 position)
        {
            var adjacentPoints = AdjacentPointsResolver.GetFreeAdjacentUnitPoints(position, _freePossitionsMap.IsFreePosition);

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
            positions.Add(new KeyValuePair<IntVector2, int>(position, GetH(position)));
        }

        private int GetH(IntVector2 intVector2)
        {
            return Math.Abs(_baseActionController.Position.x - intVector2.x) +
                Math.Abs(_baseActionController.Position.y - intVector2.y);
        }

        private IntVector2 GetAttackTargetPosition()
        {
            if (_unitInfo.AttackTarget == null || _unitInfo.AttackTarget.Position.Equals(IntVector2Constant.UNASSIGNET))
            {
                throw new Exception(GetType().Name + ": " + "unitInfo.AttackTarget cannot be NULL");
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
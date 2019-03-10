using System;
using System.Collections.Generic;
using Scripts;
using Units.OccupatedMap;
using Units.OneUnit.Base;
using Units.OneUnit.Info;
using Units.PathFinder;
using UnityEngine;

namespace Units.OneUnit
{
    public class OvertakeOccupatedPositionController : Disposable, IActivatable
    {
        private IOneUnitController _oneUnitController;
        private IOccupatedPossitionsMap _occupatedPossitionsMap;
        private IGrid _grid;
        private IUnitInfoExternal _unitInfo;
        private List<KeyValuePair<IntVector2, int>> _freePositions;
        private IBaseActionController _baseActionController;
        
        public OvertakeOccupatedPositionController(
            IOccupatedPossitionsMap occupatedPossitionsMap,
            IUnitInfoExternal unitInfo,
            IBaseActionController baseActionController,
            IGrid grid
            )
        {
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _unitInfo = unitInfo;
            _baseActionController = baseActionController;
            
            _grid = grid;
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
            Debug.Log("NoWayToAttackPointHandler");
            IntVector2 freePosition = GetFirstFreePositionInUnitRange(_unitInfo.AttackTarget.Position);
            if (Equals(freePosition, IntVector2Constant.UNASSIGNET))
            {
                Debug.Log("wait " + position);
                _baseActionController.WaitPosition(position);
                return;
            }
            
            _baseActionController.MoveToPosition(freePosition);
        }

        private IntVector2 GetFirstFreePositionInUnitRange(IntVector2 position)
        {
            _freePositions = new List<KeyValuePair<IntVector2, int>>();
            var adjacentPoints = position.GetAdjacentPoints(IsFreePosition);
            adjacentPoints.ForEach(point => AddFreePosition(point));

            _freePositions.Sort(
                delegate(KeyValuePair<IntVector2, int> pair1,
                    KeyValuePair<IntVector2, int> pair2)
                {
                    return pair1.Value.CompareTo(pair2.Value);
                }
            );

            if (_freePositions.Count > 0)
            {
                return _freePositions[0].Key;
            }

            return IntVector2Constant.UNASSIGNET;
        }

        private void AddFreePosition(IntVector2 position)
        {
            _freePositions.Add(new KeyValuePair<IntVector2, int>(position, GetH(position)));
        }

        private int GetH(IntVector2 intVector2)
        {
            return Math.Abs(_baseActionController.Position.x - intVector2.x) +
                Math.Abs(_baseActionController.Position.y - intVector2.y);
        }
        
        private bool IsFreePosition(IntVector2 position)
        {
            return _grid.GetCell(position) && _occupatedPossitionsMap.IsVacantPosition(position);
        }

        protected override void DisposeInternal()
        {
            _freePositions?.Clear();
            UnsubscribeOnEvents();
            base.DisposeInternal();
        }
    }
}
using System;
using System.Collections.Generic;
using Scripts;
using Units.OneUnit.Base;
using Units.OneUnit.Info;
using Units.PathFinder;

namespace Units.OneUnit
{
    public class OvertakeOccupatedPositionController : Disposable
    {
        private IOneUnitController _oneUnitController;
        private IUnitsTable _unitsTable;
        private IGrid _grid;
        private IUnitInfoExternal _unitInfo;
        private List<KeyValuePair<IntVector2, int>> _freePositions;
        private IBaseActionController _baseActionController;
        
        public OvertakeOccupatedPositionController(
            IUnitsTable unitsTable,
            IUnitInfoExternal unitInfo,
            IBaseActionController baseActionController,
            IGrid grid
            )
        {
            _unitsTable = unitsTable;
            _unitInfo = unitInfo;
            _baseActionController = baseActionController;
            
            _grid = grid;
            
            SubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _baseActionController.NoWayToAttackDestination += NoWayToAttackPointHandler;
        }

        private void UnsubscribeOnEvents()
        {
            _baseActionController.NoWayToAttackDestination -= NoWayToAttackPointHandler;
        }

        private void NoWayToAttackPointHandler(IntVector2 position)
        {
            IntVector2 freePosition = GetFirstFreePositionInUnitRange(_unitInfo.AttackTarget.Position);
            if (Equals(freePosition, IntVector2Constant.UNASSIGNET))
            {
                _baseActionController.WaitPosition(position);
                return;
            }
            
            _baseActionController.MoveToPosition(freePosition);
        }

        private IntVector2 GetFirstFreePositionInUnitRange(IntVector2 position)
        {
            _freePositions = new List<KeyValuePair<IntVector2, int>>();
            IntVector2 position1 = new IntVector2(position.x - 1, position.y + 1);
            if (IsFreePosition(position1))
            {
                AddFreePosition(position1);
            }
            position1 = new IntVector2(position.x - 1, position.y);
            if (IsFreePosition(position1))
            {
                AddFreePosition(position1);
            }
            position1 = new IntVector2(position.x - 1, position.y - 1);
            if (IsFreePosition(position1))
            {
                AddFreePosition(position1);
            }
            position1 = new IntVector2(position.x, position.y - 1);
            if (IsFreePosition(position1)) 
            {
                AddFreePosition(position1);
            }
            position1 = new IntVector2(position.x + 1, position.y - 1);
            if (IsFreePosition(position1)) 
            {
                AddFreePosition(position1);
            }
            position1 = new IntVector2(position.x + 1, position.y + 1);
            if (IsFreePosition(position1)) 
            {
                AddFreePosition(position1);
            }
            position1 = new IntVector2(position.x, position.y + 1);
            if (IsFreePosition(position1)) 
            {
                AddFreePosition(position1);
            }
            position1 = new IntVector2(position.x + 1, position.y);
            if (IsFreePosition(position1))
            {
                AddFreePosition(position1);
            }
            
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
            return _grid.GetCell(position) && _unitsTable.IsVacantPosition(position);
        }

        protected override void DisposeInternal()
        {
            _freePositions?.Clear();
            UnsubscribeOnEvents();
            base.DisposeInternal();
        }
    }
}
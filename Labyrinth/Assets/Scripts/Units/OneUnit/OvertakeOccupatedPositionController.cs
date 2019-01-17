using System;
using System.Collections.Generic;
using Scripts.Units.PathFinder;
using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public class OvertakeOccupatedPositionController
    {
        private readonly INoWayEventRouter _noWayEventRouter;
        private IOneUnitController _oneUnitController;
        private IUnitsTable _unitsTable;
        private IUnitStateInfo _unitStateInfo;
        private IGrid _grid;
        private List<KeyValuePair<IntVector2, int>> _freePositions;
        
        public OvertakeOccupatedPositionController(
            INoWayEventRouter noWayEventRouter,
            IUnitsTable unitsTable,
            IUnitStateInfo unitStateInfo,
            IGrid grid
            )
        {
            _noWayEventRouter = noWayEventRouter;
            _unitsTable = unitsTable;
            _unitStateInfo = unitStateInfo;
            _grid = grid;
        }
        
        public void Initialize(IOneUnitController oneUnitController)
        {
            _oneUnitController = oneUnitController;
            _noWayEventRouter.NoWayToAttackPointHandler += NoWayToAttackPointHandler;
        }

        private void NoWayToAttackPointHandler(IntVector2 position)
        {
            IntVector2 freePosition = GetFirstFreePositionInUnitRange(_unitStateInfo.AttackTarget.Position);
            if (Equals(freePosition, IntVector2.UNASSIGNET))
            {
                _oneUnitController.Wait(position);
                return;
            }
            
            _oneUnitController.MoveTo(freePosition);
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

            return IntVector2.UNASSIGNET;
        }

        private void AddFreePosition(IntVector2 position)
        {
            _freePositions.Add(new KeyValuePair<IntVector2, int>(position, GetH(position)));
        }

        private int GetH(IntVector2 intVector2)
        {
            return Math.Abs(_oneUnitController.Position.x - intVector2.x) +
                Math.Abs(_oneUnitController.Position.y - intVector2.y);
        }
        
        private bool IsFreePosition(IntVector2 position)
        {
            return _grid.GetCell(position) && _unitsTable.IsVacantPosition(position);
        }
    }
}
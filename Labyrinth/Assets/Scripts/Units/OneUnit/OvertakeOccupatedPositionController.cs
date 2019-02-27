﻿using System;
using System.Collections.Generic;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Extensions;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using Units.PathFinder;

namespace Units.OneUnit
{
    public class OvertakeOccupatedPositionController : Disposable
    {
        private readonly IEventDispatcher _eventDispatcher;
        
        private IOneUnitController _oneUnitController;
        private IUnitsTable _unitsTable;
        private IUnitStateController _stateInfo;
        private IGrid _grid;
        private List<KeyValuePair<IntVector2, int>> _freePositions;
        
        public OvertakeOccupatedPositionController(
            IEventDispatcher eventDispatcher,
            IUnitsTable unitsTable,
            IUnitStateController stateInfo,
            IGrid grid
            )
        {
            _eventDispatcher = eventDispatcher;
            _unitsTable = unitsTable;
            _stateInfo = stateInfo;
            _grid = grid;
        }
        
        public void Initialize(IOneUnitController oneUnitController)
        {
            SubscribeOnEvents();
            _oneUnitController = oneUnitController;
        }

        private void SubscribeOnEvents()
        {
            _eventDispatcher.AddEventListener<IntVector2>(UnitEventsTypes.NO_WAY_TO_ATTACK_DESTINATION, NoWayToAttackPointHandler);
        }

        private void UnsubscribeOnEvents()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.NO_WAY_TO_ATTACK_DESTINATION, new Action<IntVector2>(NoWayToAttackPointHandler));
        }

        private void NoWayToAttackPointHandler(IntVector2 position)
        {
            IntVector2 freePosition = GetFirstFreePositionInUnitRange(_stateInfo.CurrentState.AttackTarget.Position);
            if (Equals(freePosition, IntVector2Constant.UNASSIGNET))
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

            return IntVector2Constant.UNASSIGNET;
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

        protected override void DisposeInternal()
        {
            _freePositions.Clear();
            UnsubscribeOnEvents();
            base.DisposeInternal();
        }
    }
}
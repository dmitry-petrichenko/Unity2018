using System;
using System.Collections.Generic;
using System.Resources;
using Units;
using UnityEditor;
using UnityEngine;
using Scripts.Units.Rotation;

namespace Scripts.Units
{
    public class SubMoveController : EventDispatcher, ISubMoveController
    {
        private IOneUnitServicesContainer _oneUnitServicesContainer;
        private IOneUnitRotationController _rotationController;
        private IOneUnitAnimationController _animationController;
        private IOneUnitMotionController _motionController;
        private List<IntVector2> _path;
        private IUnitsTable _unitsTable;
        private IntVector2 _nextOccupiedPossition;

        public IntVector2 Destination { get; set; }

        public SubMoveController(
            IUnitsTable unitsTable,
            IOneUnitRotationController oneUnitRotationController,
            IOneUnitAnimationController oneUnitAnimationController,
            IOneUnitMotionController oneUnitMotionController
            )
        {
            _unitsTable = unitsTable;
            _rotationController = oneUnitRotationController;
            _animationController = oneUnitAnimationController;
            _animationController = oneUnitAnimationController;
            _motionController = oneUnitMotionController;

            Initialize();
        }
        
        public void Initialize()
        {
            _motionController.MoveStart += StartMoveHandler;
        }

        private void StartMoveHandler()
        {
            DispatchEvent(MoveOneStepStart);
        }

        public void MoveTo(List<IntVector2> path)
        {
            Reset();
            if (path.Count == 0)
            {
                if (NoWayToPointHandler != null)
                {
                    NoWayToPointHandler(_nextOccupiedPossition);
                }
                return;
            }
            _motionController.MoveComplete += MoveStepCompleteHandler;
            _motionController.MoveComplete += MoveNextStep;
            _path = path;
            MoveNextStep();
        }

        private void Reset()
        {
            _path = null;
            _motionController.MoveComplete -= MoveNextStep;
            _motionController.MoveComplete -= MoveStepCompleteHandler;
        }

        private void MoveStepCompleteHandler()
        {         
            if (MoveOneStepComplete != null)
            {
                MoveOneStepComplete();
            }
        }

        public void Cancel()
        {
            _motionController.MoveComplete -= MoveNextStep;
        }

        public IntVector2 Position
        {
            get { return _motionController.Position; }
        }

        public bool IsMoving
        {
            get { return _motionController.IsMoving; }
        }

        private void MoveNextStep()
        {
            IntVector2 nextPosition;
            if (_path.Count > 0)
            {
                nextPosition = GetNextPossition();
                if (IsPositionOccupated(nextPosition)) return;
                UpdateOccupationMap(nextPosition, _motionController.Position);
                _rotationController.Rotate(_motionController.Position, nextPosition);
                _animationController.PlayWalkAnimation();
                _motionController.MoveToPosition(nextPosition);
            }
            else
            {
                if (MoveToComplete != null)
                {
                    MoveToComplete();
                }
                Reset();
            }
        }

        private IntVector2 GetNextPossition()
        {
            IntVector2 nextPosition = _path[0];
            _path.RemoveAt(0);

            return nextPosition;
        }

        private bool IsPositionOccupated(IntVector2 nextPosition)
        {
            if (!_unitsTable.IsVacantPosition(nextPosition))
            {
                if (NextPositionOccupiedHandler != null)
                {
                    _nextOccupiedPossition = nextPosition;
                    NextPositionOccupiedHandler(nextPosition);
                }

                return true;
            }

            return false;
        }

        private void UpdateOccupationMap(IntVector2 newPosition, IntVector2 previousPosition)
        {
            _unitsTable.SetOccupied(newPosition);
            _unitsTable.SetVacant(previousPosition);
        }
        
        public void SetOnPosition(IntVector2 position)
        {
            _motionController.SetOnPosition(position);
            _unitsTable.SetOccupied(Position);
        }

        public event Action MoveToComplete;
        public event Action MoveOneStepComplete;
        public event Action<IntVector2> NextPositionOccupiedHandler;
        public event Action<IntVector2> NoWayToPointHandler;
        public event Action MoveOneStepStart;
    }
}
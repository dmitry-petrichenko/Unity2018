using System;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Units.PathFinder;

namespace Scripts.Units
{
    public class TargetOvertaker : EventDispatcher
    {
        public event Action Complete;
        public event Action StartFollow;
        public event Action TargetMoved;
        
        private IOneUnitController _target;
        private IOneUnitController _oneUnitController;
        
        private readonly IPathFinderController _pathFinderController;

        public TargetOvertaker(IPathFinderController pathFinderController)
        {
            _pathFinderController = pathFinderController;
        }

        public void Initialize(IOneUnitController oneUnitController)
        {
            _oneUnitController = oneUnitController;
        }
        
        public void Overtake(IOneUnitController target)
        {
            _target = target;
            _target.MoveOneStepComplete += OnTargetPositionChanged;
            
            MoveToTarget();
        }

        private void OnUnitCompleteMoveTo()
        {
            _oneUnitController.MoveToComplete -= OnUnitCompleteMoveTo;
            DispatchEvent(Complete);
        }

        private void OnTargetPositionChanged(/*IntVector2 _position*/)
        {
            IntVector2 position = _target.Position;
            
            if (TargetPositionInUnitRange(position))
            {
                DispatchEvent(TargetMoved);
            }
            else
            {
                DispatchEvent(StartFollow);
                MoveToTarget();
            }
        }

        private void MoveToTarget()
        {
            _oneUnitController.MoveToComplete += OnUnitCompleteMoveTo;
            List<IntVector2> path =
                _pathFinderController.GetPath(_target.Position, _oneUnitController.Position, null);

            if (Equals(path[0], _oneUnitController.Position))
            {
                DispatchEvent(Complete);
            }
            else
            {
                _oneUnitController.MoveTo(path[0]);
            }

        }

        private bool TargetPositionInUnitRange(IntVector2 position)
        {
            if (IsInPosition(new IntVector2(position.x - 1, position.y + 1))) return true;
            if (IsInPosition(new IntVector2(position.x - 1, position.y))) return true;
            if (IsInPosition(new IntVector2(position.x - 1, position.y - 1))) return true;
            if (IsInPosition(new IntVector2(position.x, position.y - 1))) return true;
            if (IsInPosition(new IntVector2(position.x + 1, position.y - 1))) return true;
            if (IsInPosition(new IntVector2(position.x + 1, position.y + 1))) return true;
            if (IsInPosition(new IntVector2(position.x, position.y + 1))) return true;
            if (IsInPosition(new IntVector2(position.x + 1, position.y))) return true;

            return false;
        }

        private bool IsInPosition(IntVector2 position)
        {
            if (_oneUnitController.Position.x == position.x &&
                _oneUnitController.Position.y == position.y)
            {
                return true;
            }
            
            return false;
        }

        public void Cancel()
        {
            Debug.Log("Overtake -=");
            _target.MoveOneStepComplete += OnTargetPositionChanged;
        }
    }
}
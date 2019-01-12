using System;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Units.PathFinder;

namespace Scripts.Units
{
    public class TargetOvertaker
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
            _target.MoveTileComplete += OnTargetPositionChanged;
            
            MoveToTarget();
        }

        private void OnUnitCompleteMoveTo()
        {
            _oneUnitController.MovePathComplete -= OnUnitCompleteMoveTo;
            Complete?.Invoke();
        }

        private void OnTargetPositionChanged()
        {
            IntVector2 position = _target.Position;
            
            if (TargetPositionInUnitRange(position))
            {
                TargetMoved?.Invoke();
            }
            else
            {
                StartFollow?.Invoke();
                MoveToTarget();
            }
        }

        private void MoveToTarget()
        {
            _oneUnitController.MovePathComplete += OnUnitCompleteMoveTo;
            List<IntVector2> path =
                _pathFinderController.GetPath(_target.Position, _oneUnitController.Position, null);

            if (Equals(path[0], _oneUnitController.Position))
            {
                Complete?.Invoke();
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
            _target.MoveTileComplete += OnTargetPositionChanged;
        }
    }
}
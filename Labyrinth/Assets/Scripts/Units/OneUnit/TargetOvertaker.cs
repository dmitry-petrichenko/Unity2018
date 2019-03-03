using System;
using System.Collections.Generic;
using Scripts;
using Scripts.Extensions;
using Scripts.Units.Events;
using Units.OneUnit.Base;
using Units.PathFinder;
using UnityEngine;

namespace Units.OneUnit
{
    public class TargetOvertaker : Disposable
    {
        public event Action Complete;
        public event Action StartFollow;
        public event Action TargetMoved;

        private readonly IUnitEvents _unitEvents;
        private readonly IBaseActionController _baseActionController;
        private readonly IPathFinderController _pathFinderController;
        
        private IOneUnitController _target;

        public TargetOvertaker(
            IPathFinderController pathFinderController,
            IUnitEvents unitEvents,
            IBaseActionController baseActionController)
        {
            _pathFinderController = pathFinderController;
            _baseActionController = baseActionController;
            _unitEvents = unitEvents;
        }
        
        public void Overtake(IOneUnitController target)
        {
            _target = target;
            _target.UnitEvents.MoveTileComplete += OnTargetPositionChanged;
            
            MoveToTarget();
        }

        private void OnUnitCompleteMoveTo()
        {
            _unitEvents.MoveTileComplete -= OnUnitCompleteMoveTo;
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
            _unitEvents.MovePathComplete += OnUnitCompleteMoveTo;
            List<IntVector2> path =
                _pathFinderController.GetPath(_target.Position, _baseActionController.Position, null);

            if (Equals(path[0], _baseActionController.Position))
            {
                _unitEvents.MoveTileComplete -= OnUnitCompleteMoveTo;
                _target.UnitEvents.MoveTileComplete -= OnTargetPositionChanged;
                Complete?.Invoke();
            }
            else
            {
                _baseActionController.MoveTo(path[0]);
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
            if (_baseActionController.Position.x == position.x &&
                _baseActionController.Position.y == position.y)
            {
                return true;
            }
            
            return false;
        }

        public void Cancel()
        {
            _unitEvents.MovePathComplete -= OnUnitCompleteMoveTo;
            if (_target != null)
            {
                _target.UnitEvents.MoveTileComplete -= OnTargetPositionChanged;
                _target = null;
            }
        }

        protected override void DisposeInternal()
        {
            Cancel();
            base.DisposeInternal();
        }
    }
}
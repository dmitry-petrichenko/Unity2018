using System;
using System.Collections.Generic;
using Scripts;
using Scripts.Units.Events;
using Units.OneUnit.StatesControllers.Base;
using Units.PathFinder;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class TargetOvertaker : Disposable
    {
        public event Action OvertakeComplete;

        private readonly IUnitEvents _unitEvents;
        private readonly IWayHostileController _wayHostileController;
        private readonly IBaseActionController _baseActionController;
        
        private IOneUnitController _target;

        public TargetOvertaker(
            IWayHostileController wayHostileController,
            IBaseActionController baseActionController)
        {
            _wayHostileController = wayHostileController;
            _baseActionController = baseActionController;
        }
        
        public void Overtake(IOneUnitController target)
        {
            Cancel();
            
            _target = target;
            _target.UnitEvents.PositionChanged += OnTargetPositionChanged;
            
            CheckComplete(_target.Position);
        }
        
        public void Cancel()
        {
            _wayHostileController.MoveToPositionComplete -= WayHostileControllerOnMoveToPositionComplete;
            if (_target != null)
            {
                _target.UnitEvents.PositionChanged -= OnTargetPositionChanged;
                _target = null; 
            }
        }

        private void Complete()
        {
            Cancel();
            OvertakeComplete?.Invoke();
        }
        
        private void CheckComplete(IntVector2 position)
        {
            if (TargetPositionInUnitRange(position))
            {
                Complete();
            }
            else
            {
                MoveToPosition(position);
            }
        }

        private void OnTargetPositionChanged(IntVector2 position)
        {
            _wayHostileController.MoveToPositionComplete -= WayHostileControllerOnMoveToPositionComplete;
            CheckComplete(position);
        }

        private void MoveToPosition(IntVector2 position)
        {
            _wayHostileController.MoveToPositionComplete += WayHostileControllerOnMoveToPositionComplete;
            _wayHostileController.MoveToPosition(position);
        }

        private void WayHostileControllerOnMoveToPositionComplete()
        {
            _wayHostileController.MoveToPositionComplete -= WayHostileControllerOnMoveToPositionComplete;
            CheckComplete(_target.Position);
        }

        private bool TargetPositionInUnitRange(IntVector2 position)
        {
            return position.GetAdjacentPoints().Contains(_baseActionController.Position);
        }

        protected override void DisposeInternal()
        {
            Cancel();
            base.DisposeInternal();
        }
    }
}
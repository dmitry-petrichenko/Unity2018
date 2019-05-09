using System;
using Scripts;
using Scripts.CustomDebug;
using Units.OneUnit.StatesControllers.Base.GameObject;
using UnityEngine;

namespace Units.OneUnit.StatesControllers.Base
{
    public class BaseActionController : Disposable, IBaseActionController
    {   
        private readonly IMoveStepByStepController _moveStepByStepController;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IApplyDamageController _applyDamageController;
        private readonly IPathGeneratorController _pathGeneratorController;
        private readonly MoveConsideringOccupatedController _moveConsideringOccupatedController;
        
        public IntVector2 Position => _unitGameObjectController.Position;
        public IntVector2 Destination => _pathGeneratorController.Destination;

        public BaseActionController(
            IMoveStepByStepController moveStepByStepController,
            IApplyDamageController applyDamageController,
            IUnitGameObjectController unitGameObjectController,
            MoveConsideringOccupatedController moveConsideringOccupatedController,
            IPathGeneratorController pathGeneratorController)
        {
            _moveStepByStepController = moveStepByStepController;
            _unitGameObjectController = unitGameObjectController;
            _applyDamageController = applyDamageController;
            _pathGeneratorController = pathGeneratorController;
            _moveConsideringOccupatedController = moveConsideringOccupatedController;

            _pathGeneratorController.NoWayToDestination += NoWayToDestinationHandler;
            _moveStepByStepController.NoWayToDestination += NoWayToDestinationHandler;
        }

        public void Attack(IntVector2 position)
        {
            IsValidPosition(position);
            IsValidNearActionPosition(position);
            _applyDamageController.ApplyDamageOnPosition(position);
            _unitGameObjectController.Attack(position);
        }

        public void SetHealthBarValue(float value)
        {
            _unitGameObjectController.SetHealthBarValue(value);
        }

        public void Die()
        {
            _unitGameObjectController.SetHealthBarVisible(false);
            _unitGameObjectController.Die();
        }

        public void MoveToPosition(IntVector2 position)
        {
            IsValidPosition(position);
            _pathGeneratorController.MoveToPosition(position);
        } 
        
        public void MoveToAndMakeAction(IntVector2 position, Action action)
        {
            _moveToAction = action;
            _moveStepByStepController.MovePathComplete += MoveToAndMakeActionHandler;
            _pathGeneratorController.MoveToPosition(position);
        }

        private Action _moveToAction;
        private void MoveToAndMakeActionHandler()
        {
            _moveToAction?.Invoke();
            _moveStepByStepController.MovePathComplete -= MoveToAndMakeActionHandler;
            _moveToAction = null;
        }

        public void Wait() => _unitGameObjectController.Wait();
        
        public void WaitPosition(IntVector2 position)
        {
            IsValidPosition(position);
            IsValidNearActionPosition(position);
            _unitGameObjectController.Wait(position);
        } 
        
        public void SetOnPosition(IntVector2 position)
        {
            IsValidPosition(position);
            _unitGameObjectController.SetOnPosition(position);
        }

        public event Action<IntVector2> NoWayToDestination;
        
        public event Action MovePathComplete
        {
            add =>  _moveStepByStepController.MovePathComplete += value;
            remove =>  _moveStepByStepController.MovePathComplete -= value;
        }
        
        public event Action MoveTileComplete
        {
            add =>  _unitGameObjectController.MoveTileComplete += value;
            remove =>  _unitGameObjectController.MoveTileComplete -= value;
        }
        
        public event Action MoveTileStart
        {
            add =>  _unitGameObjectController.MoveTileStart += value;
            remove =>  _unitGameObjectController.MoveTileStart -= value;
        }
            
        public event Action AttackComplete
        {
            add => _unitGameObjectController.AttackComplete += value;
            remove => _unitGameObjectController.AttackComplete -= value;
        }
        
        public event Action DieComplete
        {
            add => _unitGameObjectController.DieComplete += value;
            remove => _unitGameObjectController.DieComplete -= value;
        }

        protected override void DisposeInternal()
        {
            _pathGeneratorController.NoWayToDestination -= NoWayToDestinationHandler;
            _moveStepByStepController.NoWayToDestination -= NoWayToDestinationHandler;
            _moveStepByStepController.MovePathComplete -= MoveToAndMakeActionHandler;
            base.DisposeInternal();
        }

        private void NoWayToDestinationHandler(IntVector2 position)
        {
            NoWayToDestination?.Invoke(position);
        }

        private void IsValidPosition(IntVector2 position)
        {
            if (position.Equals(IntVector2Constant.UNASSIGNET))
            {
                ApplicationDebugger.ThrowException(GetType().Name + " Not valid position");
            }
        }
        
        private void IsValidNearActionPosition(IntVector2 position)
        {
            var adjacentPoints = _unitGameObjectController.Position.GetAdjacentPoints();
            if(!adjacentPoints.Contains(position))
            {
                ApplicationDebugger.ThrowException(GetType().Name + " Not valid position for action");
            }
        }
    }
}
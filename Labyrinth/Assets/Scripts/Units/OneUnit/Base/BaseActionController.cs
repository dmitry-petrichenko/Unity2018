using System;
using System.Collections.Generic;
using Scripts;
using Scripts.Units.StateInfo.BaseState;
using Units.OneUnit.Base.GameObject;

namespace Units.OneUnit.Base
{
    public class BaseActionController : Disposable, IBaseActionController, IBaseActionControllerInternal
    {
        private readonly ChangeDirrectionAfterMoveTileCompleteController _changeDirrectionAfterMoveTileCompleteController;
        private readonly IMoveStepByStepController _moveStepByStepController;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IApplyDamageController _applyDamageController;
        private readonly IHealthController _healthController;
        private readonly IDeathController _deathController;
        private readonly IStateControllerInternal _stateController;
        private readonly IUnitsTable _unitsTable;

        public BaseActionController(
            ChangeDirrectionAfterMoveTileCompleteController changeDirrectionAfterMoveTileCompleteController,
            IMoveStepByStepController moveStepByStepController,
            IApplyDamageController applyDamageController,
            IHealthController healthController,
            IUnitGameObjectController unitGameObjectController,
            IStateControllerInternal stateController,
            IUnitsTable unitsTable,
            IDeathController deathController)
        {
            _moveStepByStepController = moveStepByStepController;
            _changeDirrectionAfterMoveTileCompleteController = changeDirrectionAfterMoveTileCompleteController;
            _unitGameObjectController = unitGameObjectController;
            _applyDamageController = applyDamageController;
            _healthController = healthController;
            _stateController = stateController;
            _deathController = deathController;
            _unitsTable = unitsTable;
            
            _stateController.InitializeBaseActionController(this);
        }

        public void Attack(IntVector2 position)
        {
            _applyDamageController.ApplyDamageOnPosition(position);
            _unitGameObjectController.Attack(position);
        }

        public void TakeDamage(int value) { _healthController.TakeDamage(value); }
        public void SetAttackState()
        {
            _stateController.SetAttackState();
        }

        public void SetPlacidState()
        {
            _stateController.SetPlacidState();
        }

        public void MoveTo(IntVector2 position) => _changeDirrectionAfterMoveTileCompleteController.MoveTo(position);

        public void MoveTo(List<IntVector2> path) => _moveStepByStepController.MoveTo(path);
        
        public void Wait() => _unitGameObjectController.Wait();
        
        public void Wait(IntVector2 position) => _unitGameObjectController.Wait(position);
        
        public void Cancel() => _moveStepByStepController.Cancel();
        
        public void SetOnPosition(IntVector2 position)
        {
            _unitGameObjectController.SetOnPosition(position);
            _unitsTable.SetOccupied(position);
        }

        public IntVector2 Position => _unitGameObjectController.Position;
        public IntVector2 Destination => _moveStepByStepController.Destination;
        public event Action<IntVector2> NoWayToAttackDestination;
        public event Action<IntVector2> NoWayToWalkDestination;
        public event Action<IntVector2> NextTileOccupied;
        public event Action MovePathComplete;
        public void RaiseNoWayToAttackDestination(IntVector2 position)
        {
            NoWayToAttackDestination?.Invoke(position);
        }

        public void RaiseNoWayToWalkDestination(IntVector2 position)
        {
            NoWayToWalkDestination?.Invoke(position);
        }

        public void RaiseNextTileOccupied(IntVector2 position)
        {
            NextTileOccupied?.Invoke(position);
        }

        public void RaiseMovePathComplete()
        {
            MovePathComplete?.Invoke();
        }
    }
}
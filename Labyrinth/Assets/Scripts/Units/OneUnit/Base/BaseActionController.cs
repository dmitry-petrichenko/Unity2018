using System.Collections.Generic;
using Scripts;
using Scripts.Units;
using Units.OneUnit.Base.GameObject;

namespace Units.OneUnit.Base
{
    public class BaseActionController : IBaseActionController
    {
        private readonly ChangeDirrectionAfterMoveTileCompleteController _changeDirrectionAfterMoveTileCompleteController;
        private readonly IMoveStepByStepController _moveStepByStepController;
        private readonly IUnitGameObjectController _unitGameObjectController;
        private readonly IApplyDamageController _applyDamageController;
        private readonly IHealthController _healthController;
        private readonly IDeathController _deathController;

        public BaseActionController(
            ChangeDirrectionAfterMoveTileCompleteController changeDirrectionAfterMoveTileCompleteController,
            IMoveStepByStepController moveStepByStepController,
            IApplyDamageController applyDamageController,
            IHealthController healthController,
            IUnitGameObjectController unitGameObjectController,
            IDeathController deathController)
        {
            _moveStepByStepController = moveStepByStepController;
            _changeDirrectionAfterMoveTileCompleteController = changeDirrectionAfterMoveTileCompleteController;
            _unitGameObjectController = unitGameObjectController;
            _applyDamageController = applyDamageController;
            _healthController = healthController;
            _deathController = deathController;
        }

        public void Attack(IntVector2 position)
        {
            _applyDamageController.ApplyDamageOnPosition(position);
            _unitGameObjectController.Attack(position);
        }

        public void TakeDamage(int value) { _healthController.TakeDamage(value); }

        public void MoveTo(IntVector2 position) => _changeDirrectionAfterMoveTileCompleteController.MoveTo(position);

        public void MoveTo(List<IntVector2> path) => _moveStepByStepController.MoveTo(path);
        
        public void Wait() => _moveStepByStepController.Wait();
        
        public void Wait(IntVector2 position) => _moveStepByStepController.Wait(position);
        
        public void Cancel() => _moveStepByStepController.Cancel();
        
        public void SetOnPosition(IntVector2 position) => _moveStepByStepController.SetOnPosition(position);

        public IntVector2 Position => _moveStepByStepController.Position;
        public IntVector2 Destination => _moveStepByStepController.Destination;
        public bool IsMoving => _moveStepByStepController.IsMoving;
    }
}
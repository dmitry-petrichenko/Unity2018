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

        public BaseActionController(
            ChangeDirrectionAfterMoveTileCompleteController changeDirrectionAfterMoveTileCompleteController,
            IMoveStepByStepController moveStepByStepController,
            IUnitGameObjectController unitGameObjectController)
        {
            _moveStepByStepController = moveStepByStepController;
            _changeDirrectionAfterMoveTileCompleteController = changeDirrectionAfterMoveTileCompleteController;
            _unitGameObjectController = unitGameObjectController;
        }

        public void Attack(IntVector2 position) => _unitGameObjectController.Attack(position);

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
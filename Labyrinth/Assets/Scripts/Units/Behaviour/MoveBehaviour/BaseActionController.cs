using System.Collections.Generic;

namespace Scripts.Units
{
    public class BaseActionController : IBaseActionController
    {
        private readonly ChangeDirrectionAfterMoveTileCompleteController _changeDirrectionAfterMoveTileCompleteController;
        private readonly IMoveStepByStepController _moveStepByStepController;

        public BaseActionController(
            ChangeDirrectionAfterMoveTileCompleteController changeDirrectionAfterMoveTileCompleteController,
            IMoveStepByStepController moveStepByStepController)
        {
            _moveStepByStepController = moveStepByStepController;
            _changeDirrectionAfterMoveTileCompleteController = changeDirrectionAfterMoveTileCompleteController;
        }

        public void Attack(IntVector2 position) => _moveStepByStepController.Attack(position);

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
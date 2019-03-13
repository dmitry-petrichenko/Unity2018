using System.Collections.Generic;
using Scripts;
using Units.OneUnit.Base.GameObject;

namespace Units.OneUnit.Base
{
    public class ChangeDirrectionAfterMoveTileCompleteController : Disposable
    {
        private IMoveStepByStepController _moveStepByStepController;
        private IntVector2 _newPosition;
        private List<IntVector2> _newPath;
        private readonly IUnitGameObjectController _unitGameObjectController;
        
        public ChangeDirrectionAfterMoveTileCompleteController(
            IMoveStepByStepController moveStepByStepController, 
            IUnitGameObjectController unitGameObjectController)
        {
            _moveStepByStepController = moveStepByStepController;
            _unitGameObjectController = unitGameObjectController;
        }
        
        public void MoveTo(List<IntVector2> path)
        {
            if (_unitGameObjectController.IsMoving)
            {
                _newPath = path;
                ChangeDirrectionPath();
            }
            else
            {
                MoveToDirrectionPath(path);
            }
        }
        
        private void ChangeDirrectionPath()
        {
            _unitGameObjectController.MoveTileComplete -= OnChangeDirrectionMovePathCmplete;
            _moveStepByStepController.Cancel();
            _unitGameObjectController.MoveTileComplete += OnChangeDirrectionMovePathCmplete;
        }
        
        private void MoveToDirrectionPath(List<IntVector2> path)
        {
            _moveStepByStepController.MoveTo(path);
        }

        private void OnChangeDirrectionMovePathCmplete()
        {
            _unitGameObjectController.MoveTileComplete -= OnChangeDirrectionMovePathCmplete;
            MoveToDirrectionPath(_newPath);
        }

        protected override void DisposeInternal()
        {
            _unitGameObjectController.MoveTileComplete -= OnChangeDirrectionMovePathCmplete;
            base.DisposeInternal();
        }
    }
}
using System;

namespace Scripts.Units
{
    public class MoveController
    {
        private IOneUnitController _unitController;
        private ISubMoveController _subMoveController;
        private BaseMovingController _baseMovingController;
        private WaitMoveTurnController _waitMoveTurnController;

        public event Action MoveToComplete;
        
        public MoveController(
            BaseMovingController baseMovingController,
            ISubMoveController subMoveController,
            MoveConsideringOccupatedController moveConsideringOccupatedController,
            WaitMoveTurnController waitMoveTurnController
            )
        {
            _waitMoveTurnController = waitMoveTurnController;
            _subMoveController = subMoveController;
            _baseMovingController = baseMovingController;
        }
        
        public void Initialize(IOneUnitController unitController)
        {
            _unitController = unitController;
            
            _waitMoveTurnController.Initialize(_unitController);
        }
        
        public void MoveTo(IntVector2 position)
        {
            _baseMovingController.MoveTo(position);
        }
        
        public void SetOnPosition(IntVector2 position)
        {
            _subMoveController.SetOnPosition(position);
        }
    }
}
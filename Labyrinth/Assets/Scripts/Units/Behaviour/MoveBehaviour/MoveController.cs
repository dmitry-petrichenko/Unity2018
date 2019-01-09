using System;

namespace Scripts.Units
{
    public class MoveController
    {
        private IOneUnitController _unitController;
        private ISubMoveController _subMoveController;
        private MoveToHandlerController _moveToHandlerController;
        private WaitMoveTurnController _waitMoveTurnController;

        public event Action MoveToComplete;
        public event Action MoveOneStepComplete;
        public event Action MoveOneStepStart;
        
        public MoveController(
            MoveToHandlerController moveToHandlerController,
            ISubMoveController subMoveController,
            MoveConsideringOccupatedController moveConsideringOccupatedController,
            WaitMoveTurnController waitMoveTurnController
            )
        {
            _waitMoveTurnController = waitMoveTurnController;
            _subMoveController = subMoveController;
            _moveToHandlerController = moveToHandlerController;
        }
        
        public void Initialize(IOneUnitController unitController)
        {
            _unitController = unitController;
            
            _subMoveController.MoveToComplete += MoveToCompleteHandler;
            _subMoveController.MoveOneStepComplete += MoveOneStepCompleteHandler;
            _subMoveController.MoveOneStepStart += StartMoveHandler;
            
            _waitMoveTurnController.Initialize(_unitController);
        }

        private void MoveOneStepCompleteHandler()
        {
            MoveOneStepComplete?.Invoke();
        }

        private void MoveToCompleteHandler()
        {
            MoveToComplete?.Invoke();
        }

        private void StartMoveHandler()
        {
            MoveOneStepStart?.Invoke();
        }
        
        public void MoveTo(IntVector2 position)
        {
            _moveToHandlerController.MoveTo(position);
        }
        
        public void SetOnPosition(IntVector2 position)
        {
            _subMoveController.SetOnPosition(position);
        }
    }
}
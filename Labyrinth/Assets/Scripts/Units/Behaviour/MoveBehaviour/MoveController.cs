using System;

namespace ZScripts.Units
{
    public class MoveController : EventDispatcher
    {
        private IOneUnitController _unitController;
        private ISubMoveController _subMoveController;
        private MoveToHandlerController _moveToHandlerController;
        private MoveConsideringOccupatedController _moveConsideringOccupatedController;
        private WaitMoveTurnController _waitMoveTurnController;

        public event Action MoveToComplete;
        public event Action MoveOneStepComplete;
        public event Action MoveOneStepStart;
        
        public MoveController(
            MoveToHandlerController moveToHandlerController,
            ISubMoveController subMoveController,
            MoveConsideringOccupatedController moveConsideringOccupatedController,
            WaitMoveTurnController waitMoveTurnController,
            OvertakeOccupatedPositionController overtakeOccupatedPositionController
            )
        {
            _waitMoveTurnController = waitMoveTurnController;
            _subMoveController = subMoveController;
            _moveToHandlerController = moveToHandlerController;
            _moveConsideringOccupatedController = moveConsideringOccupatedController;
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
            DispatchEvent(MoveOneStepComplete);
        }

        private void MoveToCompleteHandler()
        {
            DispatchEvent(MoveToComplete);
        }

        private void StartMoveHandler()
        {
            DispatchEvent(MoveOneStepStart);
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
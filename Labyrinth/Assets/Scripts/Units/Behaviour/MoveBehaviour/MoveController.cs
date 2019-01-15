using System;

namespace Scripts.Units
{
    public class MoveController
    {
        private IOneUnitController _unitController;
        private IBaseMovingController _baseMovingController;
        private WaitMoveTurnController _waitMoveTurnController;

        public event Action MoveToComplete;
        
        public MoveController(
            IBaseMovingController baseMovingController,
            MoveConsideringOccupatedController moveConsideringOccupatedController,
            WaitMoveTurnController waitMoveTurnController
            )
        {
            _waitMoveTurnController = waitMoveTurnController;
            _baseMovingController = baseMovingController;
        }
        
        public void Initialize(IOneUnitController unitController)
        {
            _unitController = unitController;
            
            _waitMoveTurnController.Initialize(_unitController);
        }
        
        public IntVector2 Position => _baseMovingController.Position;
        
        public void MoveTo(IntVector2 position) => _baseMovingController.MoveTo(position);
        
        public void Wait() => _baseMovingController.Wait();
        
        public void Wait(IntVector2 position) => _baseMovingController.Wait(position);
        
        public void SetOnPosition(IntVector2 position) => _baseMovingController.SetOnPosition(position);
    }
}
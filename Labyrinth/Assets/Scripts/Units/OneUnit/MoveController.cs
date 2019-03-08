using System;
using Scripts;
using Units.OneUnit.Base;

namespace Units.OneUnit
{
    public class MoveController : Disposable, IMoveController, IActivatable
    {
        public event Action MoveToComplete;
        
        private readonly MoveConsideringOccupatedController _moveConsideringOccupatedController;
        private readonly WaitMoveTurnController _waitMoveTurnController;
        private readonly IBaseActionController _baseActionController;

        public MoveController(
            IBaseActionController baseActionController,
            MoveConsideringOccupatedController moveConsideringOccupatedController,
            WaitMoveTurnController waitMoveTurnController
            )
        {
            _baseActionController = baseActionController;
            _waitMoveTurnController = waitMoveTurnController;
            _moveConsideringOccupatedController = moveConsideringOccupatedController;
        }
        
        public IntVector2 Position => _baseActionController.Position;
        
        public void MoveTo(IntVector2 position)
        {
            _baseActionController.MoveToPosition(position);
        }

        public void Wait() => _baseActionController.Wait();
        
        public void Wait(IntVector2 position) => _baseActionController.WaitPosition(position);
        
        public void SetOnPosition(IntVector2 position) => _baseActionController.SetOnPosition(position);
        
        public void Activate()
        {
            _waitMoveTurnController.Activate();
            _moveConsideringOccupatedController.Activate();
        }

        public void Deactivate()
        {
            _waitMoveTurnController.Deactivate();
            _moveConsideringOccupatedController.Deactivate();
        }
    }
}
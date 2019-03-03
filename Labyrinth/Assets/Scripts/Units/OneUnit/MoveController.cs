using System;
using Scripts;
using Units.OneUnit.Base;

namespace Units.OneUnit
{
    public class MoveController : Disposable
    {
        private IBaseActionController _baseActionController;

        public event Action MoveToComplete;
        
        public MoveController(
            IBaseActionController baseActionController,
            MoveConsideringOccupatedController moveConsideringOccupatedController,
            WaitMoveTurnController waitMoveTurnController
            )
        {
            _baseActionController = baseActionController;
        }
        
        public IntVector2 Position => _baseActionController.Position;
        
        public void MoveTo(IntVector2 position)
        {
            _baseActionController.SetPlacidState();
            _baseActionController.MoveToPosition(position);
        }

        public void Wait() => _baseActionController.Wait();
        
        public void Wait(IntVector2 position) => _baseActionController.WaitPosition(position);
        
        public void SetOnPosition(IntVector2 position) => _baseActionController.SetOnPosition(position);
    }
}
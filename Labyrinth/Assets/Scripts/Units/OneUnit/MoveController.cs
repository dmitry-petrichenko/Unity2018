using System;
using Scripts;
using Scripts.Extensions;
using Scripts.Units.StateInfo;
using Units.OneUnit.Base;

namespace Units.OneUnit
{
    public class MoveController : MyDisposable
    {
        private IBaseActionController _baseActionController;
        private IUnitState _unitState;

        public event Action MoveToComplete;
        
        public MoveController(
            IBaseActionController baseActionController,
            IUnitState unitState,
            MoveConsideringOccupatedController moveConsideringOccupatedController,
            WaitMoveTurnController waitMoveTurnController
            )
        {
            _unitState = unitState;
            _baseActionController = baseActionController;
        }
        
        public IntVector2 Position => _baseActionController.Position;
        
        public void MoveTo(IntVector2 position)
        {
            _unitState.SetState(_unitState.GetWalkState());
            _baseActionController.MoveTo(position);
        }

        public void Wait() => _baseActionController.Wait();
        
        public void Wait(IntVector2 position) => _baseActionController.Wait(position);
        
        public void SetOnPosition(IntVector2 position) => _baseActionController.SetOnPosition(position);

        public void Dispose()
        {
        }
    }
}
using System;
using Scripts;
using Units.OneUnit.StatesControllers.Base;

namespace Units.OneUnit.StatesControllers.Placid
{
    public class PlacidController : Disposable, IPlacidController, IActivatable
    {
        public event Action MoveToComplete;
        
        private readonly NoWayPlacidController _noWayPlacidController;
        private readonly IBaseActionController _baseActionController;

        public PlacidController(
            IBaseActionController baseActionController,
            NoWayPlacidController noWayPlacidController
            )
        {
            _baseActionController = baseActionController;
            _noWayPlacidController = noWayPlacidController;
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
            _noWayPlacidController.Activate();
        }

        public void Deactivate()
        {
            _noWayPlacidController.Deactivate();
        }
    }
}
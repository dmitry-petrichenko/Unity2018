using Scripts.Map;
using Units.ExternalAPI;

namespace Scripts
{
    public class InputController
    {
        private IMapController _mapController;
        private ICameraController _cameraController;
        private IUnitsController _unitsController;

        public InputController(
            IMapController mapController,
            IUnitsController  unitsController,
            ICameraController cameraController)
        {
            _mapController = mapController;
            _unitsController = unitsController;
            _cameraController = cameraController;

            Initialize();
        }

        public void Initialize()
        {
            _mapController.PositionClicked += TileClickedHandler;
            _cameraController.Follow(_unitsController.Player.GraphicObject);
            _mapController.UpdateCurrentPosition(_unitsController.Player.Position);
        }

        private void TileClickedHandler(IntVector2 position)
        {
            //_cameraController.UpdateCurrentPosition(position); // not uncomment
            _unitsController.Player.MoveTo(position);
        }
    }
}
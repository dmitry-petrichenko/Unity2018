using Scripts.Map;
using Scripts.Map.View;
using Units.ExternalAPI;

namespace Scripts
{
    public class InputController
    {
        private IMapController _mapController;
        private IMapViewController _mapViewController;
        private ICameraController _cameraController;
        private IUnitsController _unitsController;

        public InputController(
            IMapController mapController,
            IUnitsController  unitsController,
            ICameraController cameraController,
            IMapViewController mapViewController)
        {
            _mapController = mapController;
            _mapViewController = mapViewController;
            _unitsController = unitsController;
            _cameraController = cameraController;

            Initialize();
        }

        public void Initialize()
        {
            _mapViewController.TileClicked += TileClickedHandler;
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
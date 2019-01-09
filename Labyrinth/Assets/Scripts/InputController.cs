using Scripts.Map;
using Scripts.Map.View;
using Scripts.Settings;
using Scripts.Units.Player;

namespace Scripts
{
    public class InputController
    {
        private IMapController _mapController;
        private IMapViewController _mapViewController;
        private ICameraController _cameraController;
        private IPlayerController _playerController;
        private ISettings _setings;

        public InputController(
            IMapController mapController,
            ISettings setings,
            IPlayerController  playerController,
            ICameraController cameraController,
            IMapViewController mapViewController)
        {
            _mapController = mapController;
            _mapViewController = mapViewController;
            _playerController = playerController;
            _setings = setings;
            _cameraController = cameraController;

            Initialize();
        }

        public void Initialize()
        {
            _mapViewController.TileClicked += TileClickedHandler;

            _cameraController.Follow(_playerController.UnitSettings.GraphicObject);

            _mapController.UpdateCurrentPosition(_playerController.Position);
        }

        private void TileClickedHandler(IntVector2 position)
        {
            //_cameraController.UpdateCurrentPosition(position); // not uncomment
            _playerController.MoveTo(position);
        }
    }
}
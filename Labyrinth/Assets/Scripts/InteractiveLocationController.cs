using Scripts.Map;
using Units.ExternalAPI;

namespace Scripts
{
    public class InteractiveLocationController
    {
        private IMapController _mapController;
        private IUnitsController _unitsController;
        private ICameraController _cameraController;

        public InteractiveLocationController(
            IMapController mapController,
            IUnitsController unitsController,
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
            
            _unitsController.Player.UnitEvents.PositionChanged += PlayerPositionChanged;
        }
        
        private void TileClickedHandler(IntVector2 position)
        {
            _unitsController.Player.MoveTo(position);
        }

        private void PlayerPositionChanged(IntVector2 position)
        {
            _mapController.UpdateCurrentPosition(position);
        }
    }
}
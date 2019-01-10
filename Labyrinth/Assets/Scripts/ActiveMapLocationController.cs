using Scripts.Map;
using Units.ExternalAPI;

namespace Scripts
{
    public class ActiveMapLocationController
    {
        private IMapController _mapController;
        private IUnitsController _unitsController;

        public ActiveMapLocationController(IMapController mapController,
            IUnitsController unitsController)
        {
            _mapController = mapController;
            _unitsController = unitsController;

            Initialize();
        }

        public void Initialize()
        {
            _unitsController.Player.PositionChanged += PlayerPositionChanged;
        }

        private void PlayerPositionChanged(IntVector2 position)
        {
            _mapController.UpdateCurrentPosition(position);
        }
    }
}
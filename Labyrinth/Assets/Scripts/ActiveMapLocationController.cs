using Scripts.Map;
using Scripts.Units.Player;

namespace Scripts
{
    public class ActiveMapLocationController
    {
        private IMapController _mapController;
        private IPlayerController _playerController;

        public ActiveMapLocationController(IMapController mapController,
            IPlayerController playerController)
        {
            _mapController = mapController;
            _playerController = playerController;

            Initialize();
        }

        public void Initialize()
        {
            _playerController.PositionChanged += PlayerPositionChanged;
        }

        private void PlayerPositionChanged(IntVector2 position)
        {
            _mapController.UpdateCurrentPosition(position);
        }
    }
}
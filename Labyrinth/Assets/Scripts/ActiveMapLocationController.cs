using ZScripts.Map;
//using ZScripts.Units;
//using ZScripts.Units.Player;

namespace ZScripts
{
    public class ActiveMapLocationController
    {
        //private IMapController _mapController;
        //private IPlayerController _playerController;

        public ActiveMapLocationController(//IMapController mapController,
            /*IPlayerController playerController*/)
        {
            //_mapController = mapController;
            //_playerController = playerController;

            Initialize();
        }

        public void Initialize()
        {
            //_playerController.PositionChanged += PlayerPositionChanged;
        }

        private void PlayerPositionChanged(IntVector2 position)
        {
            //_mapController.UpdateCurrentPosition(position);
        }
    }
}
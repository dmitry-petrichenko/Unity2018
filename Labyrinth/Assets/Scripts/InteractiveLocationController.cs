using Scripts.Map;
using Units.ExternalAPI;
using UnityEngine;

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
            _mapController.PositionClickedLeft += TileClickedLeftHandler;
            _mapController.PositionClickedRight += TileClickedRightHandler;
            _cameraController.Follow(_unitsController.Player.GraphicObject);
            _mapController.UpdateCurrentPosition(_unitsController.Player.Position);
            
            _unitsController.Player.UnitEvents.PositionChanged += PlayerPositionChanged;
        }
        
        private void TileClickedRightHandler(IntVector2 position)
        {
            _unitsController.Player.Attack(position);
        }
        
        private void TileClickedLeftHandler(IntVector2 position)
        {
            _unitsController.Player.MoveTo(position);
        }

        private void PlayerPositionChanged(IntVector2 position)
        {
            _mapController.UpdateCurrentPosition(position);
        }
    }
}
using System.Collections.Generic;
using Scripts.Map.Controllers;
using Scripts.Map.Info;

namespace Scripts.Map
{
    public class MapController : IMapController
    {
        private readonly MapInfoUpdateController _mapInfoUpdateController;
        private readonly MapViewUpdateController _mapViewUpdateController;

        public MapController(
            MapViewUpdateController mapViewUpdateController,
            MapInfoUpdateController mapInfoUpdateController)
        {
            _mapViewUpdateController = mapViewUpdateController;
            _mapInfoUpdateController = mapInfoUpdateController;

            Initialize();
        }

        private void Initialize()
        {
            _mapInfoUpdateController.DestroyTilesHandler += DestroyTilesHandler;
            _mapInfoUpdateController.InitializeTilesHandler += InitializeTilesHandler;
        }

        public void UpdateCurrentPosition(IntVector2 position)
        {
            _mapInfoUpdateController.UpdateCurrentPosition(position);
        }

        private void DestroyTilesHandler(List<IMapTileInfo> tilesInfo)
        {
            _mapViewUpdateController.DestroyTiles(tilesInfo);
        }

        private void InitializeTilesHandler(List<IMapTileInfo> tilesInfo)
        {
            _mapViewUpdateController.InitializeTiles(tilesInfo);
        }
    }
}
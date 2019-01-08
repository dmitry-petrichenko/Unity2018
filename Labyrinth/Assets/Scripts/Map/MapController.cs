using System.Collections.Generic;
using ZScripts.Map.Controllers;
using ZScripts.Map.Info;
using ZScripts.Settings;

namespace ZScripts.Map
{
    public class MapController : IMapController
    {
        private readonly MapInfoUpdateController _mapInfoUpdateController;
        private readonly MapViewUpdateController _mapViewUpdateController;
        private readonly ISettings _settings;

        public MapController(
            MapViewUpdateController mapViewUpdateController,
            MapInfoUpdateController mapInfoUpdateController,
            ISettings settings)
        {
            _mapViewUpdateController = mapViewUpdateController;
            _mapInfoUpdateController = mapInfoUpdateController;
            _settings = settings;

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
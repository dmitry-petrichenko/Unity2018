using System.Collections.Generic;
using Scripts.Settings;

namespace Scripts.Map.Info
{
    public class MapInfoController : IMapInfoController
    {
        private Dictionary<IntVector2, IMapTileInfo> _mapTilesInfo = new Dictionary<IntVector2, IMapTileInfo>();
        private IMapInfoInitializer _mapInfoInitializer;
        private MapInfoStoreController _mapInfoStoreController;
        private readonly IMapSectorController _mapSectorController;
        private readonly ISettings _settings;

        public MapInfoController(ISettings settings, IMapSectorController mapSectorController)
        {
            _mapSectorController = mapSectorController;
            _settings = settings;
            Initialize();
        }

        public void Initialize()
        {
            _mapInfoInitializer = new MapInfoInitializer();
        }

        public IMapTileInfo GetMapTileInfo(IntVector2 position)
        {
            if (MapTilesInfo.ContainsKey(position))
            {
                return MapTilesInfo[position];
            }
            else
            {
                return _mapInfoInitializer.CreateEmptyTileInfo(position);
            }
        }

        public Dictionary<IntVector2, IMapTileInfo> MapTilesInfo
        {
            get { return _mapSectorController.ActiveTiles; }
        }
    }
}
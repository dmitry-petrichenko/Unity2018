using System.Collections.Generic;
using UnityEngine;
using ZScripts.Settings;
using ZScripts.Units;
using ZScripts.Units.Player;

namespace ZScripts.Map.Info
{
    public partial class MapSectorController : IMapSectorController
    {
        private Dictionary<IntVector2, IMapTileInfo> _activeTiles = new Dictionary<IntVector2, IMapTileInfo>();
        
        private readonly ISettings _settings;
        private readonly IGameEvents _gameEvents;
        private readonly IMapInfoStoreController _mapInfoStoreController;
        private readonly SectorLifecycleController _sectorLifecycleController;
        
        public MapSectorController(
            ISettings settings,
            IGameEvents gameEvents,
            IMapInfoStoreController mapInfoStoreController,
            SectorLifecycleController sectorLifecycleController
            )
        {
            _settings = settings;
            _gameEvents = gameEvents;
            _mapInfoStoreController = mapInfoStoreController;
            _sectorLifecycleController = sectorLifecycleController;
            _sectorLifecycleController.Initialize(_activeTiles);
            
            try
            {
                InitializeCurrentSector();
                _gameEvents.PlayerPositionChanged += PlayerPositionChangedHandler;
            }
            catch 
            {
                Debug.Log("currentSector must be initialized");
            }
        }
        
        private void PlayerPositionChangedHandler(IntVector2 position)
        {
            UpdateCurrentSector(position);
            UpdateAdjacents(position);
            RemoveUnusedTiles();
        }
        
                
        private void RemoveUnusedTiles()
        {
            _sectorLifecycleController.UpdateSectors();
        }
                
        public Dictionary<IntVector2, IMapTileInfo> ActiveTiles
        {
            get { return _activeTiles;  }
        }
    }
}
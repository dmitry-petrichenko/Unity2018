using System;
using System.Collections.Generic;
using ZScripts.Map.Info;
using ZScripts.Settings;

namespace ZScripts.Map.Controllers
{
    public class MapInfoUpdateController
    {
        public delegate void InfoUpdateHandler(List<IMapTileInfo> tiles);

        public event InfoUpdateHandler DestroyTilesHandler;
        public event InfoUpdateHandler InitializeTilesHandler;

        private IMapInfoController _mapInfoController;
        private IntVector2 _currentPosition;
        private int _mapSectionSize;
        private int _halfActiveAreaX;
        private int _halfActiveAreaY;
        private int _updateTime;
        private Dictionary<IntVector2, MapTileInfoContainer> _mapTileInfoContainers;
        private List<IMapTileInfo> _tilesInfoToDestroy;
        private List<IMapTileInfo> _tilesInfoToInitialize;
        private ISettings _settings;

        public MapInfoUpdateController(ISettings settings, IMapInfoController mapInfoController)
        {
            _settings = settings;
            _mapInfoController = mapInfoController;

            Initialize();
        }

        public void Initialize()
        {
            _mapTileInfoContainers = new Dictionary<IntVector2, MapTileInfoContainer>();
            _mapSectionSize = _settings.MapSectionSize;
            _halfActiveAreaX = _halfActiveAreaY = _settings.ActiveAreaSize / 2;

            _currentPosition = IntVector2.UNASSIGNET;
        }

        public void UpdateCurrentPosition(IntVector2 currentPosition)
        {
            int positionDisplacementX = currentPosition.x - _currentPosition.x;
            int positionDisplacementY = currentPosition.y - _currentPosition.y;

            if (Math.Abs(positionDisplacementX) >= _mapSectionSize ||
                Math.Abs(positionDisplacementY) >= _mapSectionSize)
            {
                UpdateTilesInfoTime(currentPosition);
                ResetTiles();
            }
        }

        private void UpdateTilesInfoTime(IntVector2 currentPosition)
        {
            _tilesInfoToInitialize = new List<IMapTileInfo>();

            IntVector2 tileIndex;
            IMapTileInfo mapTileInfo;
            MapTileInfoContainer mapTileInfoContainer;

            _updateTime = DateTime.Now.Millisecond;

            for (int x = -_halfActiveAreaX; x < _halfActiveAreaX; x++)
            {
                for (int y = -_halfActiveAreaY; y < _halfActiveAreaY; y++)
                {
                    tileIndex = new IntVector2(x + currentPosition.x, y + currentPosition.y);
                    if (!_mapTileInfoContainers.ContainsKey(tileIndex))
                    {
                        mapTileInfo = _mapInfoController.GetMapTileInfo(tileIndex);
                        mapTileInfoContainer = new MapTileInfoContainer(mapTileInfo, _updateTime);
                        _mapTileInfoContainers.Add(tileIndex, mapTileInfoContainer);
                        _tilesInfoToInitialize.Add(mapTileInfo);
                    }
                    else
                    {
                        _mapTileInfoContainers[tileIndex].InitializeTime = _updateTime;
                    }
                }
            }

            _currentPosition = currentPosition;

            InitializeTilesHandler(_tilesInfoToInitialize);
        }

        private void ResetTiles()
        {
            _tilesInfoToDestroy = new List<IMapTileInfo>();
            Dictionary<IntVector2, MapTileInfoContainer> _newMapTileInfoContainers =
                new Dictionary<IntVector2, MapTileInfoContainer>();
            foreach (MapTileInfoContainer tileContainer in _mapTileInfoContainers.Values)
            {
                if (tileContainer.InitializeTime != _updateTime)
                {
                    _tilesInfoToDestroy.Add(tileContainer.MapTileInfo);
                }
                else
                {
                    _newMapTileInfoContainers.Add(tileContainer.MapTileInfo.Index, tileContainer);
                }
            }

            _mapTileInfoContainers = _newMapTileInfoContainers;
            DestroyTilesHandler(_tilesInfoToDestroy);
        }
    }

    class MapTileInfoContainer
    {
        public IMapTileInfo MapTileInfo;
        public float InitializeTime;

        public MapTileInfoContainer(IMapTileInfo MapTileInfo, float InitializeTime)
        {
            this.MapTileInfo = MapTileInfo;
            this.InitializeTime = InitializeTime;
        }
    }
}
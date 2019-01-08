using System.Collections.Generic;
    
namespace ZScripts.Map.Info
{
    public class MapInfoInitializer : IMapInfoInitializer
    {
        private ISectorInfo _sectorInfo;
        private Dictionary<IntVector2, IMapTileInfo> _sectorTilesInfo = new Dictionary<IntVector2, IMapTileInfo>();

        public Dictionary<IntVector2, IMapTileInfo> MapTilesInfo
        {
            get { return _sectorTilesInfo; }
        }
        
        public ISectorInfo SectorInfo
        {
            get { return _sectorInfo; }
        }
        
        public void InitializeSector(Dictionary<IntVector2, IMapTileInfo> mapTilesInfo, ISectorInfo sectorInfo)
        {
            _sectorTilesInfo = mapTilesInfo;
            _sectorInfo = sectorInfo;
        }

        public void CreateSector(IntVector2 index, IntVector2 startPoint, IntVector2 size)
        {
            _sectorInfo = new SectorInfo();
            _sectorInfo.startPoint = new IntVector2(startPoint.x, startPoint.y);
            _sectorInfo.size = new IntVector2(size.x, size.y);
            _sectorInfo.index = index;

            MapTileInfo mapTileInfo;

            for (int i = startPoint.x; i < startPoint.x + size.x; i+= 2/*MainEditorController.SCALE*/)
            {
                for (int j = startPoint.y; j < startPoint.y + size.y; j+= 2/*MainEditorController.SCALE*/)
                {
                    InitializePlane(new IntVector2(i, j));
                    /*
                    mapTileInfo = new MapTileInfo();
                    mapTileInfo.Initialize(MapTileType.Square, new IntVector2(i, j), new IntVector2(i, j));
                    _sectorTilesInfo[new IntVector2(i, j)] = mapTileInfo;
                    */
                }
            }
        }

        public void InitializeCube(IntVector2 position)
        {
            MapTileInfo mapTileInfo;

            mapTileInfo = new MapTileInfo();
            mapTileInfo.Initialize(MapTileType.Cube, position, new IntVector2(position.x, position.y));
            _sectorTilesInfo[new IntVector2(position.x, position.y)] = mapTileInfo;

            mapTileInfo = new MapTileInfo();
            mapTileInfo.Initialize(MapTileType.Cube, position, new IntVector2(position.x + 1, position.y));
            _sectorTilesInfo[new IntVector2(position.x + 1, position.y)] = mapTileInfo;

            mapTileInfo = new MapTileInfo();
            mapTileInfo.Initialize(MapTileType.Cube, position, new IntVector2(position.x, position.y + 1));
            _sectorTilesInfo[new IntVector2(position.x, position.y + 1)] = mapTileInfo;

            mapTileInfo = new MapTileInfo();
            mapTileInfo.Initialize(MapTileType.Cube, position, new IntVector2(position.x + 1, position.y + 1));
            _sectorTilesInfo[new IntVector2(position.x + 1, position.y + 1)] = mapTileInfo;
        }
        
        public void InitializePlane(IntVector2 position)
        {
            MapTileInfo mapTileInfo;

            mapTileInfo = new MapTileInfo();
            mapTileInfo.Initialize(MapTileType.Square, position, position);
            _sectorTilesInfo[new IntVector2(position.x, position.y)] = mapTileInfo;
            
            mapTileInfo = new MapTileInfo();
            mapTileInfo.Initialize(MapTileType.Square, position, new IntVector2(position.x + 1, position.y));
            _sectorTilesInfo[new IntVector2(position.x + 1, position.y)] = mapTileInfo;

            mapTileInfo = new MapTileInfo();
            mapTileInfo.Initialize(MapTileType.Square, position, new IntVector2(position.x, position.y + 1));
            _sectorTilesInfo[new IntVector2(position.x, position.y + 1)] = mapTileInfo;

            mapTileInfo = new MapTileInfo();
            mapTileInfo.Initialize(MapTileType.Square, position, new IntVector2(position.x + 1, position.y + 1));
            _sectorTilesInfo[new IntVector2(position.x + 1, position.y + 1)] = mapTileInfo;
        }

        public IMapTileInfo CreateEmptyTileInfo(IntVector2 position)
        {
            MapTileInfo mapTileInfo = new MapTileInfo();
            mapTileInfo.Initialize(MapTileType.Empty, position, position);

            return mapTileInfo;
        }
    }
}
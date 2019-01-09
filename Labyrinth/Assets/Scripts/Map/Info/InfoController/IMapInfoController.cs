using System.Collections.Generic;

namespace Scripts.Map.Info
{
    public interface IMapInfoController
    {
        IMapTileInfo GetMapTileInfo(IntVector2 position);
        Dictionary<IntVector2, IMapTileInfo> MapTilesInfo { get; }
    }
}
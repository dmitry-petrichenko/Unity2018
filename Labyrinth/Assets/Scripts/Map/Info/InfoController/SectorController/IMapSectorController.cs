using System.Collections.Generic;

namespace Scripts.Map.Info
{
    public interface IMapSectorController
    {
        Dictionary<IntVector2, IMapTileInfo> ActiveTiles { get; }
    }
}
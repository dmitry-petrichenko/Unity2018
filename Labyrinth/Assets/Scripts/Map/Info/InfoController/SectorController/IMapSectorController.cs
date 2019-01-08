using System.Collections.Generic;

namespace ZScripts.Map.Info
{
    public interface IMapSectorController
    {
        Dictionary<IntVector2, IMapTileInfo> ActiveTiles { get; }
    }
}
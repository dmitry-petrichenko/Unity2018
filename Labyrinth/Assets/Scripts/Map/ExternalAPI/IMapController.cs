using System;
using System.Collections.ObjectModel;
using Scripts.Map.Info;

namespace Scripts.Map
{
    public interface IMapController
    {
        ReadOnlyDictionary<IntVector2, IMapTileInfo> MapTiles { get; }
        event Action<IntVector2> PositionClicked;
        void UpdateCurrentPosition(IntVector2 position);
    }
}
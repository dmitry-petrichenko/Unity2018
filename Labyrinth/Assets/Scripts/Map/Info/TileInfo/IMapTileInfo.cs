﻿namespace Scripts.Map.Info
{
    public interface IMapTileInfo : ITileView
    {
        IntVector2 Index { get; set; }
    }
}
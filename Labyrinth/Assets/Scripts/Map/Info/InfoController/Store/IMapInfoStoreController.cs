using System.Collections.Generic;

namespace Scripts.Map.Info
{
    public interface IMapInfoStoreController
    {
        ISectorInfo UploadSectorInfo(IntVector2 index);
        Dictionary<IntVector2, IMapTileInfo> UploadSectorData(IntVector2 index);
        void SaveSector(ISectorInfo info, Dictionary<IntVector2, IMapTileInfo> data);
    }
}
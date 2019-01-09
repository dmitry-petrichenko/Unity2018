using System.Collections.Generic;

namespace Scripts.Map.Info
 {
     public interface IMapInfoInitializer
     {
         void InitializeSector(Dictionary<IntVector2, IMapTileInfo> mapTilesInfo, ISectorInfo sectorInfo);
         void CreateSector(IntVector2 index, IntVector2 startPoint, IntVector2 size);
         void InitializeCube(IntVector2 position);
         void InitializePlane(IntVector2 position);
         IMapTileInfo CreateEmptyTileInfo(IntVector2 position);
         Dictionary<IntVector2, IMapTileInfo> MapTilesInfo { get; }
         ISectorInfo SectorInfo { get; } 
     }
 }
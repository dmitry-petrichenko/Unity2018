using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;

namespace ZScripts.Map.Info
{
    public class SectorLifecycleController
    {
        private readonly IMapInfoStoreController _mapInfoStoreController;
        private Dictionary<IntVector2, IntVector2> _activeSectors = new Dictionary<IntVector2, IntVector2>();
        private Dictionary<IntVector2, IntVector2> _loadedSections = new Dictionary<IntVector2, IntVector2>();
        private Dictionary<IntVector2, ISectorInfo> _loadedSectorInfos = new Dictionary<IntVector2, ISectorInfo>();
        private Dictionary<IntVector2, IMapTileInfo> _activeTiles;

        public Dictionary<IntVector2, ISectorInfo> LoadedSectorInfos
        {
            get { return _loadedSectorInfos; }
        }

        public SectorLifecycleController(IMapInfoStoreController mapInfoStoreController)
        {
            _mapInfoStoreController = mapInfoStoreController;
        }
        
        public void Initialize(Dictionary<IntVector2, IMapTileInfo> activeTiles)
        {
            _activeTiles = activeTiles;
        }
        
        public void AddActiveSector(IntVector2 sectorIndex)
        {
            _activeSectors[sectorIndex] = sectorIndex;
        }
        
        public void UpdateSectors()
        {
            Dictionary<IntVector2, IntVector2> _sectorsToRemove;
            Dictionary<IntVector2, IntVector2> _sectorsToUpload;

            _sectorsToRemove = DictionarySubtraction(_loadedSections, _activeSectors);
            _sectorsToUpload = DictionarySubtraction(_activeSectors, _loadedSections);

            _loadedSections = CopyDictionary(_activeSectors);
            _activeSectors = new Dictionary<IntVector2, IntVector2>();

            UploadSectors(_sectorsToUpload);
            RemoveSectors(_sectorsToRemove);
        }

        private void RemoveSectors(Dictionary<IntVector2, IntVector2> sectorsToRemove)
        {
            foreach (var index in sectorsToRemove)
            {
                if (_loadedSectorInfos.ContainsKey(index.Key))
                {
                    RemoveTilesInSection(_loadedSectorInfos[index.Key]);
                    _loadedSectorInfos.Remove(index.Key);
                }
            }
        }
        
        private void RemoveTilesInSection(ISectorInfo sectionInfo)
        {
            IntVector2 index;
            for (int i = sectionInfo.startPoint.x; i < sectionInfo.startPoint.x + sectionInfo.size.x; i++)
            {
                for (int j = sectionInfo.startPoint.y; j < sectionInfo.startPoint.y + sectionInfo.size.y; j++)
                {
                    index = new IntVector2(i, j);
                    if (_activeTiles.ContainsKey(index))
                    {
                        _activeTiles.Remove(index);
                    }
                }
            }
        }

        private void UploadSectors(Dictionary<IntVector2, IntVector2> sectorsToUpload)
        {
            Dictionary<IntVector2, IMapTileInfo> uploadedTiles;
            ISectorInfo sectorInfo;

            foreach (KeyValuePair<IntVector2, IntVector2> index in sectorsToUpload)
            {
                sectorInfo = _mapInfoStoreController.UploadSectorInfo(index.Key);
                if (sectorInfo == null)
                {
                    continue;
                }
                _loadedSectorInfos[sectorInfo.index] = sectorInfo;
                uploadedTiles = _mapInfoStoreController.UploadSectorData(index.Key);
                
                UploadSector(uploadedTiles);
            }
        }

        private void UploadSector(Dictionary<IntVector2, IMapTileInfo> uploadedTiles)
        {
            foreach (var info in uploadedTiles)
            {
                _activeTiles[info.Key] = info.Value;
            }
        }

        private Dictionary<IntVector2, IntVector2> DictionarySubtraction(
            Dictionary<IntVector2, IntVector2> minued,
            Dictionary<IntVector2, IntVector2> subtranend
        )
        {
            Dictionary<IntVector2, IntVector2> minuedCopy = CopyDictionary(minued);
            foreach (var sectorInfo in subtranend)
            {
                if (minuedCopy.ContainsKey(sectorInfo.Key))
                {
                    minuedCopy.Remove(sectorInfo.Key);
                }
            }

            return minuedCopy;
        }

        private Dictionary<IntVector2, IntVector2> CopyDictionary(Dictionary<IntVector2, IntVector2> value)
        {
            Dictionary<IntVector2, IntVector2> copy = new Dictionary<IntVector2, IntVector2>();
            foreach (var sectorInfo in value)
            {
                copy[sectorInfo.Key] = sectorInfo.Value;
            }

            return copy;
        }
    }
}
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using Scripts.Settings;

namespace Scripts.Map.Info
{
    public class MapInfoStoreController : IMapInfoStoreController
    {
        private ISettings _settings;
        private string _jsonString;
        private JsonData _infoJson;
        private const string PREFIX = "sector_"; 
        private const string INFO_POSTFIX = "_info.json"; 
        private const string DATA_POSTFIX = "_data.json"; 

        public MapInfoStoreController(ISettings settings)
        {
            _settings = settings;
        }
        
        public ISectorInfo UploadSectorInfo(IntVector2 index)
        {
            ISectorInfo sectorInfo = new SectorInfo();

            string path = _settings.MapsResourcesLocation + GetSectorInfoName(index);
            if (!File.Exists(path))
            {
                return null;
            }
            _jsonString = File.ReadAllText(path);
            _infoJson = JsonMapper.ToObject(_jsonString);
            
            sectorInfo.startPoint = GetIntVector2FromProperty(_infoJson, "startPoint");
            sectorInfo.index = GetIntVector2FromProperty(_infoJson, "index");
            sectorInfo.size = GetIntVector2FromProperty(_infoJson, "size");
            
            return sectorInfo;
        }

        public Dictionary<IntVector2, IMapTileInfo> UploadSectorData(IntVector2 index)
        {
            Dictionary<IntVector2, IMapTileInfo> data = new Dictionary<IntVector2, IMapTileInfo>();

            string path = _settings.MapsResourcesLocation + GetSectorDataName(index);
            if(!File.Exists(path))
            {
                return null;
            }
            
            _jsonString = File.ReadAllText(path);
            _infoJson = JsonMapper.ToObject(_jsonString);
            
            int count = _infoJson.Count;
            IntVector2 position;
            IMapTileInfo mapTileInfo;
            
            for (int i = 0; i < count; i++)
            {
                position = GetIntVector2FromProperty(_infoJson[i], "Position");
                mapTileInfo = GetMapTileInfoFromData(_infoJson[i]["TileInfo"]);
                data[position] = mapTileInfo;
            }  

            return data;
        }

        public void SaveSector(ISectorInfo info, Dictionary<IntVector2, IMapTileInfo> data)
        {
            string sectorInfo = JsonMapper.ToJson(info);
            File.WriteAllText(
                _settings.MapsResourcesLocation + GetSectorInfoName(info.index),
                sectorInfo
                );
            
            string sectorData = SectorDataToJson(data);
            File.WriteAllText(
                _settings.MapsResourcesLocation + GetSectorDataName(info.index),
                sectorData
            );
        }
        
        private string SectorDataToJson(Dictionary<IntVector2, IMapTileInfo> data)
        {
            SectorTileInfoContainer[] tiles = new SectorTileInfoContainer[data.Count];
            int i = 0;
            foreach (var info in data)
            {
                tiles[i] = new SectorTileInfoContainer(info.Key, info.Value);
                i++;
            }
            return JsonMapper.ToJson(tiles);
        }

        private string GetSectorInfoName(IntVector2 index)
        {
            string name;

            name = PREFIX + index.x.ToString() + "_" + index.y.ToString() + INFO_POSTFIX;
            
            return name;
        }
        
        private string GetSectorDataName(IntVector2 index)
        {
            string name;

            name = PREFIX + index.x.ToString() + "_" + index.y.ToString() + DATA_POSTFIX;
            
            return name;
        }
        
        IntVector2 GetIntVector2FromProperty(JsonData jsonData, string property)
        {
            IntVector2 intVector2;
            intVector2.x = (int) jsonData[property]["x"];
            intVector2.y = (int) jsonData[property]["y"];

            return intVector2;
        }
        
        MapTileInfo GetMapTileInfoFromData(JsonData jsonData)
        {
            MapTileInfo tileInfo;

            tileInfo = new MapTileInfo();
            tileInfo.Initialize((int) jsonData["Type"], GetIntVector2FromProperty(jsonData, "ViewPosition"),
                GetIntVector2FromProperty(jsonData, "Index"));

            return tileInfo;
        }
    }
    
    public class SectorTileInfoContainer
    {
        public IntVector2 Position { get; private set; }
        public IMapTileInfo TileInfo { get; private set; }

        public SectorTileInfoContainer(IntVector2 Position, IMapTileInfo TileInfo)
        {
            this.Position = Position;
            this.TileInfo = TileInfo;
        }
    }
}
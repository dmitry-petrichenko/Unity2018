using System.Collections.Generic;
using ZScripts.Map.Info;

namespace ZScripts.Units.PathFinder
{
    public class Grid : IGrid
    {
        private Dictionary<IntVector2, bool> _gridValue = new Dictionary<IntVector2, bool>();
        private IMapInfoController _mapInfoController;
        private Dictionary<IntVector2, IMapTileInfo> _mapTilesInfo = new Dictionary<IntVector2, IMapTileInfo>();

        public Grid(IMapInfoController mapInfoController)
        {
            _mapInfoController = mapInfoController;

        }

        public bool GetCell(IntVector2 index)
        {
            if (_mapInfoController.MapTilesInfo.ContainsKey(index))
            {
                return _mapInfoController.GetMapTileInfo(index).IsEmpty();
            }
            else
            {
                return false;
            }
        }
    }
}
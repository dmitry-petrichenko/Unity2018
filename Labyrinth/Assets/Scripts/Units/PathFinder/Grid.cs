﻿using Scripts;
using Scripts.Map;

namespace Units.PathFinder
{
    public class Grid : Disposable, IGrid
    {
        private IMapController _mapController;

        public Grid(IMapController mapController)
        {
            _mapController = mapController;
        }

        public bool GetCell(IntVector2 index)
        {
            if (_mapController.MapTiles.ContainsKey(index))
            {
                return _mapController.MapTiles[index].IsEmpty();
            }
            else
            {
                return false;
            }
        }

        protected override void DisposeInternal()
        {
            _mapController = null;
            base.DisposeInternal();
        }
    }
}
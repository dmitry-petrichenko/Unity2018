﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Scripts.Map.Controllers;
using Scripts.Map.Info;
using Scripts.Map.View;
//TODO EF2 split class MapLocationApdater 
namespace Scripts.Map
{
    public class MapController : IMapController
    {
        public event Action<IntVector2> PositionClickedLeft;
        public event Action<IntVector2> PositionClickedRight;
        
        private readonly MapInfoUpdateController _mapInfoUpdateController;
        private readonly MapViewUpdateController _mapViewUpdateController;
        private readonly IMapViewController _mapViewController;
        private readonly IMapInfoController _mapInfoController;

        public MapController(
            MapViewUpdateController mapViewUpdateController,
            MapInfoUpdateController mapInfoUpdateController,
            IMapViewController mapViewController,
            IMapInfoController mapInfoController)
        {
            _mapViewUpdateController = mapViewUpdateController;
            _mapInfoUpdateController = mapInfoUpdateController;
            _mapViewController = mapViewController;
            _mapInfoController = mapInfoController;

            Initialize();
        }

        private void Initialize()
        {
            _mapViewController.LeftClicked += TileClickedLeftHandler;
            _mapViewController.RightClicked += TileClickedRightHandler;
            _mapInfoUpdateController.DestroyTilesHandler += DestroyTilesHandler;
            _mapInfoUpdateController.InitializeTilesHandler += InitializeTilesHandler;
        }
        
        private void TileClickedLeftHandler(IntVector2 position)
        {
            PositionClickedLeft?.Invoke(position);
        }
        
        private void TileClickedRightHandler(IntVector2 position)
        {
            PositionClickedRight?.Invoke(position);
        }

        public void UpdateCurrentPosition(IntVector2 position)
        {
            _mapInfoUpdateController.UpdateCurrentPosition(position);
        }

        private void DestroyTilesHandler(List<IMapTileInfo> tilesInfo)
        {
            _mapViewUpdateController.DestroyTiles(tilesInfo);
        }

        private void InitializeTilesHandler(List<IMapTileInfo> tilesInfo)
        {
            _mapViewUpdateController.InitializeTiles(tilesInfo);
        }

        public ReadOnlyDictionary<IntVector2, IMapTileInfo> MapTiles => 
            new ReadOnlyDictionary<IntVector2, IMapTileInfo>(_mapInfoController.MapTilesInfo);
    }
}
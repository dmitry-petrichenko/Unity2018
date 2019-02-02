using System.Collections.Generic;
using Scripts;
using Scripts.Map.Info;
using Scripts.Map.View;
using Scripts.Settings;
using Tests.Map.Scripts;
using UnityEngine;
using CameraController = Scripts.CameraController;

public class MainEditorController : MonoBehaviour
{
    public GameObject EmptySquare, CubeSuare;
    public Camera Camera;
    
    public static int SCALE = 2;
    
    private const int SECTOR_SIZE = 8;

    private EditorMapViewController _mapViewController;
    private Dictionary<IntVector2, IMapTileInfo> _mapTilesInfo = new Dictionary<IntVector2, IMapTileInfo>();
    private ISectorInfo _sectorInfo;
    private IMapInfoInitializer _mapInfoInitializer;
    private IMapInfoStoreController _mapInfoStoreController;
    private EditorGraphicsController _graphicsController;
    private GameSettings _settingsList;
    private CameraController _cameraController;
    private IntVector2 _cameraPosition;
    private GameLoopController _gameLoopController;
    
    public void SaveMap()
    {
        _mapInfoStoreController.SaveSector(_mapInfoInitializer.SectorInfo, _mapInfoInitializer.MapTilesInfo);
    }

    void Start()
    {
        InitializeSettings();
        InitializeCameraController();
        InitializeEditorGraphicsController();
        
        _mapInfoInitializer = new MapInfoInitializer();
        _mapInfoStoreController = new MapInfoStoreController(_settingsList);
        
        //UPLOAD MAP FROM DISK
        _mapTilesInfo = UploadNewSectorByIndex(0,0);
        //------------------
        // INITIALIZE NEW SECTOR
        //_mapTilesInfo = InitializeNewSectorByIndex(0,0);
        
        _mapViewController = new EditorMapViewController();
        _mapViewController.Initialize(_graphicsController);
        _mapViewController.InitializeTiles(_mapTilesInfo);
        
        _cameraPosition = new IntVector2(0, 0);
        _cameraController.UpdateCurrentPosition(_cameraPosition);
        
        InitializeMouseClickListener();
    }

    private Dictionary<IntVector2, IMapTileInfo> UploadNewSectorByIndex(int x, int y)
    {
        _mapTilesInfo = _mapInfoStoreController.UploadSectorData(new IntVector2(x, y));
        _sectorInfo = _mapInfoStoreController.UploadSectorInfo(new IntVector2(x, y));
        _mapInfoInitializer.InitializeSector(_mapTilesInfo, _sectorInfo);

        return _mapInfoInitializer.MapTilesInfo;
    }

    private Dictionary<IntVector2, IMapTileInfo> InitializeNewSectorByIndex(int x, int y)
    {
        IntVector2 sectorIndex = new IntVector2(x, y);
        _mapInfoInitializer.CreateSector(
            sectorIndex, 
            new IntVector2(SECTOR_SIZE * SCALE * sectorIndex.x, SECTOR_SIZE * SCALE * sectorIndex.y),
            new IntVector2(SECTOR_SIZE * SCALE, SECTOR_SIZE * SCALE));
        return _mapInfoInitializer.MapTilesInfo;
    }

    private void InitializeSettings()
    {
        _settingsList = new GameSettings();
        _settingsList.Initialize();
    }
    
    private void InitializeCameraController()
    {
        _gameLoopController = new GameLoopController(this);
        _cameraController = new CameraController(Camera, _gameLoopController);
    }

    private void InitializeEditorGraphicsController()
    {
        _graphicsController = new EditorGraphicsController();
        _graphicsController.Initialize(gameObject, EmptySquare, CubeSuare);
        _graphicsController.LeftClicked += TileClickHandler;
        _graphicsController.RightClicked += RightClickHandler;
    }

    private void InitializeMouseClickListener()
    {
        MouseClickListener _mouseClickListener = gameObject.GetComponent<MouseClickListener>();
        _mouseClickListener.DownButtonClicked += DownButtonClicked;
        _mouseClickListener.UpButtonClicked += UpButtonClicked;
        _mouseClickListener.LeftButtonClicked += LeftButtonClicked;
        _mouseClickListener.RightButtonClicked += RightButtonClicked;
    }

    private void DownButtonClicked()
    {
        _cameraController.UpdateCurrentPosition(new IntVector2(_cameraPosition.x, _cameraPosition.y += 2));
    }
    
    private void UpButtonClicked()
    {
        _cameraController.UpdateCurrentPosition(new IntVector2(_cameraPosition.x, _cameraPosition.y -= 2));
    }
    
    private void LeftButtonClicked()
    {
        _cameraController.UpdateCurrentPosition(new IntVector2(_cameraPosition.x -= 2, _cameraPosition.y));
    }
    
    private void RightButtonClicked()
    {
        _cameraController.UpdateCurrentPosition(new IntVector2(_cameraPosition.x += 2, _cameraPosition.y));
    }

    private void RightClickHandler(IntVector2 position)
    {
        position = new IntVector2(position.x * SCALE, position.y * SCALE);
        _mapInfoInitializer.InitializePlane(position);
        _mapViewController.UpdateTile(position);
    }

    private void TileClickHandler(IntVector2 position)
    {
        position = new IntVector2(position.x * SCALE, position.y * SCALE);
        _mapInfoInitializer.InitializeCube(position);
        _mapViewController.UpdateTile(position);
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace ZScripts.Map.View
{
    public class MapViewController : IMapViewController
    {
        private GameObject _mainScene, _cube, _empty, _square;
        private Dictionary<IntVector2, GameObject> _activeGameObjects;
        private MouseClickListener _mouseClickListener;
        private MapGraphicsSettings _settings;

        public MapViewController(MapGraphicsSettings settings, GameObject mainScene) 
        {
            _settings = settings;
            _mainScene = mainScene;
            Initialize();
        }

        public void Initialize()
        {
            _cube = _settings.Cube;
            _empty = _settings.Empty;
            _square = _settings.Square;
            _activeGameObjects = new Dictionary<IntVector2, GameObject>();
        
            _mouseClickListener = _mainScene.AddComponent<MouseClickListener>();
            _mouseClickListener.TileClicked += TileClickedHandler;
            _mouseClickListener.TileClicked += RightClickedHandler;
        }

        private void TileClickedHandler(IntVector2 position)
        {
            if (TileClicked != null)
                TileClicked(position);
        }

        private void RightClickedHandler(IntVector2 position)
        {
            if (RightClicked != null)
                RightClicked(position);
        }

        public void InitializePlane(IntVector2 position)
        {
        }

        public void InitializeSquare(IntVector2 position)
        {
            var gameObject = Object.Instantiate(_square, new Vector3(position.x, 0, position.y), Quaternion.identity,
                _mainScene.transform);
            AddActiveGameObject(position, gameObject);
        }

        public void InitializeCube(IntVector2 position)
        {
            var gameObject = Object.Instantiate(_cube, new Vector3(position.x, 0, position.y), Quaternion.identity,
                _mainScene.transform);
            AddActiveGameObject(position, gameObject);
        }

        public void InitializeEmpty(IntVector2 position)
        {
            var gameObject = Object.Instantiate(_empty, new Vector3(position.x, 0, position.y), Quaternion.identity,
                _mainScene.transform);
            AddActiveGameObject(position, gameObject);
        }

        public void DestroyTile(IntVector2 position)
        {
            if (!_activeGameObjects.ContainsKey(position))
                return;

            var gameObject = _activeGameObjects[position];
            Object.Destroy(gameObject);
            _activeGameObjects.Remove(position);
        }

        public event TileClickHandler TileClicked;
        public event TileClickHandler RightClicked;

        private void AddActiveGameObject(IntVector2 position, GameObject gameObject)
        {
            _activeGameObjects.Add(position, gameObject);
        }
    }
}
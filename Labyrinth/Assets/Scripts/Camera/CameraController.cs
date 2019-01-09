using UnityEngine;
using Scripts.GameLoop;

namespace Scripts
{
    public class CameraController : ICameraController
    {
        private Camera _camera;
        private int _yPosition;
        private GameObject _gameObject;
        private IGameLoopController _gameLoopController;
        private Vector3 _offset;

        public CameraController(
            Camera camera,
            IGameLoopController gameLoopController)
        {
            _camera = camera;
            _gameLoopController = gameLoopController;
            _yPosition = 15;
        }

        public void UpdateCurrentPosition(IntVector2 position)
        {
            _camera.transform.position = new Vector3(position.x, _yPosition, position.y);
        }

        public void Follow<T>(T gameObject)
        {
            _gameLoopController.Updated += UpdateHandler;
            _gameObject = gameObject as GameObject;
            _offset = _camera.transform.position - _gameObject.transform.position;
        }

        private void UpdateHandler()
        {
            _camera.transform.position = _gameObject.transform.position + _offset;
        }
    }
}
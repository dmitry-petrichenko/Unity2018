using UnityEngine;

namespace Scripts._Client.Units.SingleUnit.Player
{
    public class PlayerViewObject : IUnityUnitViewObject
    {
        private GameObject _gameObject;
        private string _prefabPath = "Units/Resources/RedMage/unit_prefab";

        public PlayerViewObject()
        {
            _gameObject = InstantiatePrefabFromResourcePath(_prefabPath);
        }

        private UnityEngine.GameObject InstantiatePrefabFromResourcePath(string path)
        {
            UnityEngine.Object prefab = Resources.Load(path);
            UnityEngine.GameObject newObject = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
    
            return newObject;
        }

        public GameObject GameObject => _gameObject;
    }
}
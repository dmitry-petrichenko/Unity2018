using System.Globalization;
using System.IO;
using LitJson;
using Scripts.Extensions;
using UnityEngine;

namespace Scripts.Units.Settings
{
    public abstract class UnitSettings : Disposable, IUnitSettings
    {
        public float MotionSpeed { get; private set; }
        public GameObject GraphicObject { get; private set; }
        public float RotationSpeed { get; private set; }
        public int Attack { get; private set; }
        public int TotalHealth { get; private set; }

        public static string UNITS_ASSETS_PATH = "Units/Resources/";

        private string _prefabPath;
        private string _jsonString;
        private JsonData _infoJson;

        public UnitSettings() { }

        public void Initialize(string settingsPath)
        {
            _jsonString = File.ReadAllText(settingsPath);
            _infoJson = JsonMapper.ToObject(_jsonString);
            
            MotionSpeed = float.Parse((string)_infoJson["MotionSpeed"], CultureInfo.InvariantCulture.NumberFormat);
            _prefabPath = (string) _infoJson["PrefabPath"];
            RotationSpeed = float.Parse((string)_infoJson["RotationSpeed"], CultureInfo.InvariantCulture.NumberFormat);
            Attack = 25;
            TotalHealth = 100;
                
            GraphicObject = InstantiatePrefabFromResourcePath(_prefabPath);
        }

        private GameObject InstantiatePrefabFromResourcePath(string path)
        {
            //https://answers.unity.com/questions/313398/is-it-possible-to-get-a-prefab-object-from-its-ass.html
            UnityEngine.Object pPrefab = Resources.Load(path);
            GameObject pNewObject = (GameObject)GameObject.Instantiate(pPrefab, Vector3.zero, Quaternion.identity);

            return pNewObject;
        }

    }
}
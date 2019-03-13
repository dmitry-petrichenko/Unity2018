using Scripts.Units.Settings;
using Units.OneUnit.StatesControllers.Base.Settings;
using UnityEngine;

namespace Tests.Units.Scripts
{
    public class TestUnitSettings : IUnitSettings
    {
        public float MotionSpeed { get; }
        public int Attack { get; }
        public int TotalHealth { get; }

        public GameObject GraphicObject { get; private set; }

        public void Initialize(string settingsPath) {}

        public void SetGraphicObject(GameObject gameObject)
        {
            GraphicObject = gameObject;
        }
    }
}
using UnityEngine;

namespace Scripts.Units.Settings
{
    public interface IUnitSettings
    {
        float MotionSpeed { get; }
        int Attack { get; }
        int TotalHealth { get; }
        GameObject GraphicObject { get; }
        void Initialize(string settingsPath);
    }
}
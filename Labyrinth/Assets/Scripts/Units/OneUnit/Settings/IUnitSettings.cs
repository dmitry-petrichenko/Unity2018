using UnityEngine;

namespace ZScripts.Units.Settings
{
    public interface IUnitSettings
    {
        float MotionSpeed { get; }
        GameObject GraphicObject { get; }
        void Initialize(string settingsPath);
    }
}
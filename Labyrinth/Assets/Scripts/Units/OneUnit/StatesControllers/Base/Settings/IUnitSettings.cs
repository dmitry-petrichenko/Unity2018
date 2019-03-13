namespace Units.OneUnit.StatesControllers.Base.Settings
{
    public interface IUnitSettings
    {
        float MotionSpeed { get; }
        int Attack { get; }
        int TotalHealth { get; }
        UnityEngine.GameObject GraphicObject { get; }
        void Initialize(string settingsPath);
    }
}
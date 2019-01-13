using Units;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public interface IOneUnitServicesContainer
    {
        void Initialize(string settingsPath);
        
        IOneUnitMotionController MotionController { get; }
        IOneUnitAnimationController AnimationController { get; }
        IOneUnitRotationController RotationController { get; }
        IUnitSettings UnitSettings { get; }
        IUnitStateInfo UnitStateInfo { get; }
    }
}
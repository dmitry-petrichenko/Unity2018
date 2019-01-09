using Units;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public interface IOneUnitServicesContainer
    {
        IOneUnitMotionController MotionController { get; set; }
        IOneUnitAnimationController AnimationController { get; set; }
        IOneUnitRotationController RotationController { get; set; }
        IUnitSettings UnitSettings { get; }
        IUnitStateInfo UnitStateInfo { get; }
    }
}
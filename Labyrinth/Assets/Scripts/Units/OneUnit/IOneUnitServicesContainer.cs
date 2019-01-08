using Units;
using ZScripts.Units.Rotation;
using ZScripts.Units.Settings;
using ZScripts.Units.StateInfo;

namespace ZScripts.Units
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
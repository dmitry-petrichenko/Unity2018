using Units;
using ZScripts.Settings;
using ZScripts.Units.Rotation;
using ZScripts.Units.Settings;
using ZScripts.Units.StateInfo;

namespace ZScripts.Units
{
    public interface IOneUnitServices
    {
        MoveController MoveController { get; }
        AttackController AttackController { get; }
        IUnitsTable UnitsTable { get; }
        IOneUnitMotionController OneUnitMotionController { get; }
        IOneUnitAnimationController OneUnitAnimationController { get; }
        IOneUnitRotationController OneUnitRotationController { get; }
        IUnitStateInfo UnitStateInfo { get; }
        ISettings Settings { get; }
        IUnitSettings UnitSettings { get; }
        IPeacefulBehaviour PeacefulBehaviour { get; }
        IAgressiveBehaviour AgressiveBehaviour { get; }
        IGameEvents GameEvents { get; }
    }
}
using ID5D6AAC.Common.EventDispatcher;
using Units;
using Scripts.Settings;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;

namespace Scripts.Units
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
        IEventDispatcher EventDispatcher { get; }
        IBaseMovingController BaseMovingController { get; }
    }
}
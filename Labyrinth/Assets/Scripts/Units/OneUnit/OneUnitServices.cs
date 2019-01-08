using Units;
using ZScripts.Settings;
using ZScripts.Units.Rotation;
using ZScripts.Units.Settings;
using ZScripts.Units.StateInfo;

namespace ZScripts.Units
{
    public class OneUnitServices : IOneUnitServices
    {
        public MoveController MoveController { get; }
        public AttackController AttackController { get; }
        public IUnitsTable UnitsTable { get; }
        public IOneUnitMotionController OneUnitMotionController { get; }
        public IOneUnitAnimationController OneUnitAnimationController { get; }
        public IOneUnitRotationController OneUnitRotationController { get; }
        public IUnitStateInfo UnitStateInfo { get; }
        public ISettings Settings { get; }
        public IUnitSettings UnitSettings { get; }
        public IPeacefulBehaviour PeacefulBehaviour { get; }
        public IAgressiveBehaviour AgressiveBehaviour { get; }
        public IGameEvents GameEvents { get; }
    }
}
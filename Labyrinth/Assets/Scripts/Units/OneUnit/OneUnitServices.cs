using Units;
using Scripts.Settings;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public class OneUnitServices : IOneUnitServices
    {
        public OneUnitServices(MoveController moveController, AttackController attackController, IUnitsTable unitsTable, IOneUnitMotionController oneUnitMotionController, IOneUnitAnimationController oneUnitAnimationController, IOneUnitRotationController oneUnitRotationController, IUnitStateInfo unitStateInfo, ISettings settings, IUnitSettings unitSettings, IPeacefulBehaviour peacefulBehaviour, IAgressiveBehaviour agressiveBehaviour, IGameEvents gameEvents)
        {
            MoveController = moveController;
            AttackController = attackController;
            UnitsTable = unitsTable;
            OneUnitMotionController = oneUnitMotionController;
            OneUnitAnimationController = oneUnitAnimationController;
            OneUnitRotationController = oneUnitRotationController;
            UnitStateInfo = unitStateInfo;
            Settings = settings;
            UnitSettings = unitSettings;
            PeacefulBehaviour = peacefulBehaviour;
            AgressiveBehaviour = agressiveBehaviour;
            GameEvents = gameEvents;
        }

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
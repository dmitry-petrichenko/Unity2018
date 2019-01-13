using ID5D6AAC.Common.EventDispatcher;
using Units;
using Scripts.Settings;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public class OneUnitServices : IOneUnitServices
    {
        public OneUnitServices(
            MoveController moveController, 
            AttackController attackController, 
            IUnitsTable unitsTable, 
            IOneUnitMotionController oneUnitMotionController, 
            IOneUnitAnimationController oneUnitAnimationController, 
            IOneUnitRotationController oneUnitRotationController, 
            IUnitStateInfo unitStateInfo, ISettings settings, 
            IUnitSettings unitSettings, 
            IPeacefulBehaviour peacefulBehaviour, 
            IAgressiveBehaviour agressiveBehaviour,
            IGameEvents gameEvents,
            IEventDispatcher eventDispatcher,
            IBaseMovingController baseMovingController)
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
            EventDispatcher = eventDispatcher;
            BaseMovingController = baseMovingController;
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
        public IEventDispatcher EventDispatcher { get; }
        public IBaseMovingController BaseMovingController { get; }
    }
}
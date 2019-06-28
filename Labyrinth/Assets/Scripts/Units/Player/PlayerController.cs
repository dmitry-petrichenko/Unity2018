using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Units.Events;
using Scripts.Units.Settings;
using Units.OccupatedMap;
using Units.OneUnit;
using Units.OneUnit.Info;
using Units.OneUnit.State;
using Units.OneUnit.StatesControllers.Base.Settings;

namespace Units.Player
{
    public class PlayerController : OneUnitController, IPlayerController
    {
        private IGameEvents _gameEvents;
        private readonly IEventDispatcher _eventDispatcher;
        private IUnitSettings _unitSettings;

        public PlayerController(
            IGameEvents gameEvents,
            IEventDispatcher eventDispatcher,
            IUnitSettings unitSettings,
            IOccupatedPossitionsMap occupatedPossitionsMap,
            IStateControllerExternal stateController,
            ILifeController lifeController,
            IUnitEvents unitEvents,
            IUnitInfoExternal unitInfo
        ) : base(occupatedPossitionsMap, unitEvents, stateController, lifeController, unitInfo)
        {
            _gameEvents = gameEvents;
            _eventDispatcher = eventDispatcher;
            _unitSettings = unitSettings;

            Initialize();
        }

        private void Initialize()
        {
            UnitEvents.MovePathComplete += Wait;
            SetOnPosition(new IntVector2(5, 5));
        }

        public object GraphicObject => _unitSettings.GraphicObject;

        protected override void DisposeInternal()
        {
            UnitEvents.MovePathComplete -= Wait;
            base.DisposeInternal();
        }
    }
}
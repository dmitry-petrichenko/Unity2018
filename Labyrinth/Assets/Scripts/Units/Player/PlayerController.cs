using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Units.Events;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;
using Scripts.Units.StateInfo.LivingStates;
using Units.OneUnit;

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
            IUnitsTable unitsTable,
            ILivingStateControllerExternal livingStateControllerExternal,
            IUnitEvents unitEvents,
            IUnitStateController stateInfo
        ) : base(unitsTable, unitEvents, livingStateControllerExternal, stateInfo)
        {
            _gameEvents = gameEvents;
            _eventDispatcher = eventDispatcher;
            _unitSettings = unitSettings;

            Initialize();
        }

        private void Initialize()
        {
            UnitEvents.MovePathComplete += Wait;
            _eventDispatcher.AddEventListener(UnitEventsTypes.MOVE_TILE_START, MoveTileStartHandler);
            SetOnPosition(new IntVector2(1, 1));
        }

        private void MoveTileStartHandler() => _gameEvents.TriggerPlayerPositionChanged(Position);

        public object GraphicObject => _unitSettings.GraphicObject;

        protected override void DisposeInternal()
        {
            UnitEvents.MovePathComplete -= Wait;
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_START, new Action(MoveTileStartHandler));
            base.DisposeInternal();
        }
    }
}
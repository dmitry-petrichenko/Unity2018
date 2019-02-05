using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Units.Events;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;
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
            MoveController moveController,
            IAttackController attackController,
            IUnitEvents unitEvents,
            IUnitState stateInfo
        ): base(unitsTable, moveController, attackController, unitEvents, stateInfo)
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

        public void Dispose()
        {
            UnitEvents.MovePathComplete -= Wait;
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_START, new Action(MoveTileStartHandler));
        }
    }
}
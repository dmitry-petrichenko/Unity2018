using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Units.Events;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;
using Scripts.Units.StateInfo.LivingStates;
using Units.ExternalAPI;
using Units.OneUnit;
using Units.OneUnit.Info;
using Units.OneUnit.State1E;

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
            IStateControllerExternal stateController,
            IUnitEvents unitEvents,
            IUnitInfoExternal unitInfo
        ) : base(unitsTable, unitEvents, stateController, unitInfo)
        {
            _gameEvents = gameEvents;
            _eventDispatcher = eventDispatcher;
            _unitSettings = unitSettings;

            Initialize();
        }

        private void Initialize()
        {
            UnitEvents.MovePathComplete += Wait;
            SetOnPosition(new IntVector2(1, 1));
        }

        public object GraphicObject => _unitSettings.GraphicObject;

        protected override void DisposeInternal()
        {
            UnitEvents.MovePathComplete -= Wait;
            base.DisposeInternal();
        }
    }
}
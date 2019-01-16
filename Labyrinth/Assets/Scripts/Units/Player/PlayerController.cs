using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Units.Events;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;

namespace Scripts.Units.Player
{
    public class PlayerController : IPlayerController, IDisposable
    {
        private IGameEvents _gameEvents;
        private readonly IOneUnitController _oneUnitController;
        private readonly IEventDispatcher _eventDispatcher;
        private IUnitSettings _unitSettings;

        public PlayerController(
            IGameEvents gameEvents,
            IOneUnitController oneUnitController,
            IEventDispatcher eventDispatcher,
            IUnitSettings unitSettings
        )
        {
            _gameEvents = gameEvents;
            _oneUnitController = oneUnitController;
            _eventDispatcher = eventDispatcher;
            _unitSettings = unitSettings;

            Initialize();
        }

        private void Initialize()
        {
            _oneUnitController.UnitEvents.MovePathComplete += Wait;
            _eventDispatcher.AddEventListener(UnitEventsTypes.MOVE_TILE_START, MoveTileStartHandler);
            _oneUnitController.SetOnPosition(new IntVector2(1, 1));
        }

        private void MoveTileStartHandler() => _gameEvents.TriggerPlayerPositionChanged(Position);

        public void Attack(IntVector2 position) {}

        public void SetOnPosition(IntVector2 position) => _oneUnitController.SetOnPosition(position);

        public IUnitStateInfo UnitStateInfo => _oneUnitController.UnitStateInfo;
        
        public IUnitEvents UnitEvents => _oneUnitController.UnitEvents;

        public void MoveTo(IntVector2 position) => _oneUnitController.MoveTo(position);

        public void Wait() => _oneUnitController.Wait();

        public void Wait(IntVector2 position) => _oneUnitController.Wait(position);

        public object GraphicObject => _unitSettings.GraphicObject;
        public IntVector2 Position => _oneUnitController.Position;

        public void Dispose()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_START, new Action(MoveTileStartHandler));
        }
    }
}
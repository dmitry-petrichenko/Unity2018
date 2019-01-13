using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Settings;
using Scripts.Units.Events;

namespace Scripts.Units.Player
{
    public class PlayerController : IPlayerController, IDisposable
    {
        private ISettings _settings;
        private IGameEvents _gameEvents;
        private readonly IOneUnitController _oneUnitController;
        private readonly IEventDispatcher _eventDispatcher;
        
        public PlayerController(
            ISettings settings,
            IGameEvents gameEvents,
            IOneUnitController oneUnitController,
            IEventDispatcher eventDispatcher
            )
        {
            _settings = settings;
            _gameEvents = gameEvents;
            _oneUnitController = oneUnitController;
            _eventDispatcher = eventDispatcher;
            
            Initialize();
        }
        
        private void Initialize()
        {
            //base.Initialize(_settings.UnitsResourcesLocation + "RedMage.json");
            _oneUnitController.Initialize(_settings.UnitsResourcesLocation + "RedMage.json");

            _oneUnitController.MovePathComplete += _oneUnitController.Wait;
            _eventDispatcher.AddEventListener(UnitEvents.MOVE_TILE_START, MoveTileStartHandler);
            _oneUnitController.SetOnPosition(new IntVector2(1, 1));
        }

        private void MoveTileStartHandler()
        {
            _gameEvents.TriggerPlayerPositionChanged(Position);
        }

        public void Attack(IntVector2 position)
        {
        }

        public event Action<IntVector2> PositionChanged
        {
            add => _oneUnitController.PositionChanged += value;
            remove => _oneUnitController.PositionChanged -= value;
        }
        
        public void MoveTo(IntVector2 position)
        {
            _oneUnitController.MoveTo(position);
        }

        public object GraphicObject => _oneUnitController.UnitSettings.GraphicObject;
        public IntVector2 Position => _oneUnitController.Position;

        public IOneUnitController o => _oneUnitController; //TODO change

        public void Dispose()
        {
            _eventDispatcher.RemoveEventListener(UnitEvents.MOVE_TILE_START, new Action(MoveTileStartHandler));
        }
    }
}
using System;
using ID5D6AAC.Common.EventDispatcher;

namespace Scripts.Units.Events
{
    public class UnitEvents : IUnitEvents, IDisposable
    {
        public event Action<IntVector2> PositionChanged;
        public event Action MovePathComplete;
        public event Action MoveTileComplete;
        
        private readonly IEventDispatcher _eventDispatcher;
        private readonly MoveController _moveController;
        
        public UnitEvents(IEventDispatcher eventDispatcher, 
            MoveController moveController)
        {
            _eventDispatcher = eventDispatcher;
            _moveController = moveController;

            SubscribeOnEvents();
        }

        public void Dispose() => UnsubscribeFromEvents();
        
        private void SubscribeOnEvents()
        {
            _eventDispatcher.AddEventListener(UnitEventsTypes.MOVE_PATH_COMPLETE, MovePathCompleteHandler);
            _eventDispatcher.AddEventListener(UnitEventsTypes.MOVE_TILE_START, MoveTileStartHandler);
            _eventDispatcher.AddEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, MoveTileCompleteHandler);
        }

        private void UnsubscribeFromEvents()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_PATH_COMPLETE, new Action(MovePathCompleteHandler));
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_START, new Action(MoveTileStartHandler));
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, new Action(MoveTileCompleteHandler));
        }
        
        private void MoveTileStartHandler() => PositionChanged?.Invoke(_moveController.Position);
        
        private void MoveTileCompleteHandler() => MoveTileComplete?.Invoke();

        private void MovePathCompleteHandler() => MovePathComplete?.Invoke();
    }
}
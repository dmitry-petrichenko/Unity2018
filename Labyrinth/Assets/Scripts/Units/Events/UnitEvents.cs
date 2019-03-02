using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Extensions;
using Units.OneUnit;
using Units.OneUnit.Base;

namespace Scripts.Units.Events
{
    public class UnitEvents : Disposable, IUnitEvents
    {
        public event Action<IntVector2> PositionChanged;
        public event Action MovePathComplete;
        public event Action MoveTileComplete;
        public event Action AttackComplete;
        public event Action Died;
        public event Action DieComplete;
        
        private readonly IEventDispatcher _eventDispatcher;
        private readonly MoveController _moveController;
        private readonly IBaseActionController _baseActionController;
        
        public UnitEvents(
            IEventDispatcher eventDispatcher, 
            IBaseActionController baseActionController,
            MoveController moveController)
        {
            _eventDispatcher = eventDispatcher;
            _moveController = moveController;
            _baseActionController = baseActionController;

            SubscribeOnEvents();
        }
        
        protected override void DisposeInternal()
        {
            UnsubscribeFromEvents();
            base.DisposeInternal();
        }
        
        private void SubscribeOnEvents()
        {
            _baseActionController.MovePathComplete += MovePathCompleteHandler;
            //_eventDispatcher.AddEventListener(UnitEventsTypes.MOVE_PATH_COMPLETE, MovePathCompleteHandler);
            _eventDispatcher.AddEventListener(UnitEventsTypes.MOVE_TILE_START, MoveTileStartHandler);
            _eventDispatcher.AddEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, MoveTileCompleteHandler);
            _eventDispatcher.AddEventListener(UnitEventsTypes.ATTACK_COMPLETE, AttackCompleteHandler);
            _eventDispatcher.AddEventListener(UnitEventsTypes.HEALTH_ENDED, HealthEndedHandler);
            _eventDispatcher.AddEventListener(UnitEventsTypes.DIE_COMPLETE, DieCompleteHandler);
        }

        private void UnsubscribeFromEvents()
        {
            _baseActionController.MovePathComplete -= MovePathCompleteHandler;
            //_eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_PATH_COMPLETE, new Action(MovePathCompleteHandler));
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_START, new Action(MoveTileStartHandler));
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.MOVE_TILE_COMPLETE, new Action(MoveTileCompleteHandler));
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.ATTACK_COMPLETE, new Action(AttackCompleteHandler));
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.HEALTH_ENDED, new Action(HealthEndedHandler));
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.DIE_COMPLETE, new Action(DieCompleteHandler));
        }
        
        private void MoveTileStartHandler() => PositionChanged?.Invoke(_moveController.Position);
        
        private void MoveTileCompleteHandler() => MoveTileComplete?.Invoke();

        private void MovePathCompleteHandler() => MovePathComplete?.Invoke();
        
        private void AttackCompleteHandler() => AttackComplete?.Invoke();
        
        private void HealthEndedHandler() => Died?.Invoke();
        
        private void DieCompleteHandler() => DieComplete?.Invoke();
    }
}
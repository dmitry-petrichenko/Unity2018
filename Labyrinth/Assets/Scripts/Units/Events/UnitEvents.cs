using System;
using ID5D6AAC.Common.EventDispatcher;
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
        public event Action HealthEnded;
        public event Action DieComplete;
        
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IMoveController _moveController;
        private readonly IBaseActionController _baseActionController;
        
        public UnitEvents(
            IEventDispatcher eventDispatcher, 
            IBaseActionController baseActionController,
            IMoveController moveController)
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
            _baseActionController.MoveTileStart += MoveTileStartHandler;
            _baseActionController.MoveTileComplete += MoveTileCompleteHandler;
            _baseActionController.AttackComplete += AttackCompleteHandler;
            _baseActionController.DieComplete += DieCompleteHandler;
            _eventDispatcher.AddEventListener(UnitEventsTypes.HEALTH_ENDED, HealthEndedHandler);
        }

        private void UnsubscribeFromEvents()
        {
            _baseActionController.MovePathComplete -= MovePathCompleteHandler;
            _baseActionController.MoveTileStart -= MoveTileStartHandler;
            _baseActionController.MoveTileComplete -= MoveTileCompleteHandler;
            _baseActionController.AttackComplete -= AttackCompleteHandler;
            _baseActionController.DieComplete -= DieCompleteHandler;
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.HEALTH_ENDED, new Action(HealthEndedHandler));
        }
        
        private void MoveTileStartHandler() => PositionChanged?.Invoke(_moveController.Position);
        
        private void MoveTileCompleteHandler() => MoveTileComplete?.Invoke();

        private void MovePathCompleteHandler() => MovePathComplete?.Invoke();
        
        private void AttackCompleteHandler() => AttackComplete?.Invoke();
        
        private void HealthEndedHandler() => HealthEnded?.Invoke();
        
        private void DieCompleteHandler() => DieComplete?.Invoke();
    }
}
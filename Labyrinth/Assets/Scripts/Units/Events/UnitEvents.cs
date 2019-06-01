using System;
using Units.OneUnit;
using Units.OneUnit.StatesControllers;
using Units.OneUnit.StatesControllers.Base;
using Units.OneUnit.StatesControllers.Placid;

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
        
        private readonly IPlacidController _placidController;
        private readonly IBaseActionController _baseActionController;
        private readonly ILifeController _lifeController;
        private readonly IWaveEventRiser _waveEventRiser;
        
        public UnitEvents(
            IBaseActionController baseActionController,
            ILifeController lifeController,
            IWaveEventRiser waveEventRiser,
            IPlacidController placidController)
        {
            _placidController = placidController;
            _baseActionController = baseActionController;
            _lifeController = lifeController;
            _waveEventRiser = waveEventRiser;

            SubscribeOnEvents();
        }

        public void AddPositionChangedHandler(Action handler, IPositional subscriber)
        {
            _waveEventRiser.AddStateChangedHandler(handler, subscriber);
        }

        public void RemovePositionChangedHandler(Action handler, IPositional subscriber)
        {
            _waveEventRiser.RemovePositionChangedHandler(handler, subscriber);
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
            _lifeController.HealthEnded += HealthEndedHandler;
        }

        private void UnsubscribeFromEvents()
        {
            _baseActionController.MovePathComplete -= MovePathCompleteHandler;
            _baseActionController.MoveTileStart -= MoveTileStartHandler;
            _baseActionController.MoveTileComplete -= MoveTileCompleteHandler;
            _baseActionController.AttackComplete -= AttackCompleteHandler;
            _baseActionController.DieComplete -= DieCompleteHandler;
            _lifeController.HealthEnded -= HealthEndedHandler;
        }
        
        private void MoveTileStartHandler() => PositionChanged?.Invoke(_placidController.Position);
        
        private void MoveTileCompleteHandler() => MoveTileComplete?.Invoke();

        private void MovePathCompleteHandler() => MovePathComplete?.Invoke();
        
        private void AttackCompleteHandler() => AttackComplete?.Invoke();
        
        private void HealthEndedHandler() => HealthEnded?.Invoke();
        
        private void DieCompleteHandler() => DieComplete?.Invoke();
    }
}
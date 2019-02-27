using ID5D6AAC.Common.EventDispatcher;
using Scripts.Units.Events;
using Scripts.Units.StateInfo.LivingStates.States;

namespace Scripts.Units.StateInfo.LivingStates
{
    public class LivingStateController : ILivingStateControllerInternal, ILivingStateControllerExternal
    {
        private readonly AliveLivingState _aliveLivingState;
        private readonly DeadLivingState _deadLivingState;
        private readonly IEventDispatcher _eventDispatcher;

        private ILivingState _currentState;
        
        public LivingStateController(
            AliveLivingState aliveLivingState,
            IEventDispatcher eventDispatcher,
            DeadLivingState deadLivingState)
        {
            _aliveLivingState = aliveLivingState;
            _deadLivingState = deadLivingState;
            _eventDispatcher = eventDispatcher;

            SetAlive();
            _eventDispatcher.AddEventListener(UnitEventsTypes.HEALTH_ENDED, HealthEndedHandler);
        }

        private void HealthEndedHandler()
        {
            SetDead();
        }

        public void SetAlive()
        {
            _currentState = _aliveLivingState;
        }

        public void SetDead()
        {
            _currentState = _deadLivingState;
        }

        public ILivingState CurrentState => _currentState;
    }
}
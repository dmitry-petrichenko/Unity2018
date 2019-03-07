namespace Units.OneUnit.State1E
{
    public class StateController : IStateControllerExternal, IStateControllerInternal
    {
        private readonly IPlacidState _placidState;
        private readonly IHostileState _hostileState;

        private IState _currentState;
        
        public StateController(
            IPlacidState placidState,
            IHostileState hostileState)
        {
            _placidState = placidState;
            _placidState.InitializeStateController(this);
            
            _hostileState = hostileState;
            _hostileState.InitializeStateController(this);
            
            SetState(GetPlacidState());
        }

        public void SetState(IState state)
        {
            _currentState?.Deactivate();
            _currentState = state;
            _currentState?.Activate();
        }

        public IState GetPlacidState()
        {
            return _placidState;
        }

        public IState GetHostileState()
        {
            return _hostileState;
        }

        public IState CurrentState => _currentState;
    }
}
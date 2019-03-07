namespace Units.OneUnit.State1E
{
    public class StateController : Disposable, IStateControllerExternal, IStateControllerInternal
    {
        private readonly IPlacidState _placidState;
        private readonly IHostileState _hostileState;
        private readonly IDeadState _deadState;

        private IState _currentState;
        
        public StateController(
            IPlacidState placidState,
            IHostileState hostileState,
            IDeadState deadState)
        {
            _placidState = placidState;
            _placidState.InitializeStateController(this);
            
            _hostileState = hostileState;
            _hostileState.InitializeStateController(this);

            _deadState = deadState;
            _deadState.InitializeStateController(this);
            
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
        
        public IState GetDeadState()
        {
            return _deadState;
        }

        public IState CurrentState => _currentState;

        protected override void DisposeInternal()
        {
            _currentState = null;
            base.DisposeInternal();
        }
    }
}
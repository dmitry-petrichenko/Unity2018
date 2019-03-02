using Scripts.Units.StateInfo.BaseState.States;
using Units.OneUnit.Base;

namespace Scripts.Units.StateInfo.BaseState
{
    public class StateController : IStateControllerExternal, IStateControllerInternal, IStateControllerMutator
    {
        private IState _currentState;
        private IState _attackState;
        private IState _placidState;

        private IBaseActionControllerInternal _baseActionController;
        
        public IState CurrentState => _currentState;
        
        public StateController()
        {
        }

        public void InitializeBaseActionController(IBaseActionControllerInternal baseActionController)
        {
            _baseActionController = baseActionController;
            InitializeStates();
            SetState(GetPlacidState());
        }
 
        public void SetAttackState()
        {
            CheckInitialization();
            CurrentState.SetAttackState();
        }

        public void SetPlacidState()
        {
            CheckInitialization();
            CurrentState.SetPlacidState();
        }

        // IStateControllerMutator
        public void SetState(IState state)
        {
            _currentState = state;
        }

        public IState GetAttackState()
        {
            return _attackState;
        }

        public IState GetPlacidState()
        {
            return _placidState;
        }
        //--------------------------
        
        private void InitializeStates()
        {
            _attackState = new AttackState(_baseActionController, this);
            _placidState = new PlacidState(_baseActionController, this);
        }

        private void CheckInitialization()
        {
            if (_baseActionController == null)
                throw new System.Exception("BaseActionController not initialized");
            if (CurrentState == null)
                throw new System.Exception("StateController.CurrentState not initialized");
        }
    }
}
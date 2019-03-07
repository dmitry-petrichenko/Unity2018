using Scripts.Units.StateInfo.BaseState.States;
using Units.OneUnit.Base;

namespace Scripts.Units.StateInfo.BaseState
{
    public class StateController2 : IStateControllerExternal2, IStateControllerInternal2, IStateControllerMutator
    {
        private IState _currentState;
        private IState _attackState;
        private IState _placidState;

        private IBaseActionControllerInternal _baseActionController;
        
        public IState CurrentState => _currentState;
        
        public StateController2()
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
            CheckStatesInitialized();
            return _attackState;
        }

        public IState GetPlacidState()
        {
            CheckStatesInitialized();
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
            {
                throw new System.Exception("BaseActionController not initialized");
            }
            if (CurrentState == null)
            {
                throw new System.Exception("StateController.CurrentState not initialized");
            } 
        }
        
        private void CheckStatesInitialized()
        {
            if (_attackState == null && _placidState == null)
            {
                throw new System.Exception("States are not initialized");
            }   
        }  
    }
}
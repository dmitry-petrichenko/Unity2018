namespace Scripts.Units.StateInfo
{
    public class UnitStateController : IUnitStateController, IUnitStateControllerInternal
    {
        private IUnitState2 _attackState;
        private IUnitState2 _walkState;
        private IUnitState2 _deadState;
        
        public UnitStateController()
        {
            _walkState = new WalkUnitState(this);
            _attackState = new AttackUnitState(this);
            _deadState = new DeadUnitState();

            SetInternalState(GetWalkState());
        }

        public IUnitState2 CurrentState { get; private set; }
       
        public void SetInternalState(IUnitState2 state)
        {
            CurrentState = state;
        }

        public IUnitState2 GetAttackState()
        {
            return _attackState;
        }

        public IUnitState2 GetWalkState()
        {
            return _walkState;
        }

        public IUnitState2 GetDeadState()
        {
            return _deadState;
        }
    }
}
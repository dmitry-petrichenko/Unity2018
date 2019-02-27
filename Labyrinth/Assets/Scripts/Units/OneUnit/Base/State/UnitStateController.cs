using Scripts.Units.StateInfo.UnitStates;

namespace Scripts.Units.StateInfo
{
    public class UnitStateController : IUnitStateController, IUnitStateControllerInternal
    {
        private IUnitState _attackState;
        private IUnitState _walkState;
        private IUnitState _deadState;
        
        public UnitStateController()
        {
            _walkState = new WalkUnitState(this);
            _attackState = new AttackUnitState(this);
            _deadState = new DeadUnitState();

            SetStateInternal(GetWalkState());
        }

        public IUnitState CurrentState { get; private set; }
       
        public void SetStateInternal(IUnitState state)
        {
            CurrentState = state;
        }

        public IUnitState GetAttackState()
        {
            return _attackState;
        }

        public IUnitState GetWalkState()
        {
            return _walkState;
        }

        public IUnitState GetDeadState()
        {
            return _deadState;
        }
    }
}
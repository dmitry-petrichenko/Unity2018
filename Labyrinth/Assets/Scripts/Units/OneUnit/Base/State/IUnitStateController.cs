namespace Scripts.Units.StateInfo
{
    public interface IUnitStateController
    {
        IUnitState2 CurrentState { get; }
        
    }
    
    public interface IUnitStateControllerInternal
    {
        void SetInternalState(IUnitState2 state);

        IUnitState2 GetAttackState();
        IUnitState2 GetWalkState();
        IUnitState2 GetDeadState();
    }
}
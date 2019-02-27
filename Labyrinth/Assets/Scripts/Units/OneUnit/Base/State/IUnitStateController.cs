namespace Scripts.Units.StateInfo
{
    public interface IUnitStateController
    {
        IUnitState CurrentState { get; } 
    }
    
    public interface IUnitStateControllerInternal
    {
        void SetStateInternal(IUnitState state);

        IUnitState GetAttackState();
        IUnitState GetWalkState();
        IUnitState GetDeadState();
    }
}
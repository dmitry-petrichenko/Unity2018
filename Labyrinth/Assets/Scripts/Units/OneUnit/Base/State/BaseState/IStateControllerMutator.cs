namespace Scripts.Units.StateInfo.BaseState
{
    public interface IStateControllerMutator
    {
        void SetState(IState state);
        IState GetAttackState();
        IState GetPlacidState();
    }
}
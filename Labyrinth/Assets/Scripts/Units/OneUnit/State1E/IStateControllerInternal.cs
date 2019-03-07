namespace Units.OneUnit.State1E
{
    public interface IStateControllerInternal
    {
        void SetState(IState state);
        IState GetPlacidState();
        IState GetHostileState();
        IState CurrentState { get; }
    }
}
using Units.OneUnit.State.States;

namespace Units.OneUnit.State
{
    public interface IStateControllerInternal
    {
        void SetState(IState state);
        IState GetPlacidState();
        IState GetHostileState();
        IState GetDeadState();
        IState CurrentState { get; }
    }
}
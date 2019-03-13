using Units.OneUnit.State.States;

namespace Units.OneUnit.State
{
    public interface IStateControllerExternal
    {
        IState CurrentState { get; }
    }
}
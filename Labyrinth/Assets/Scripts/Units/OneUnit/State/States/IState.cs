using Units.OneUnit.StatesControllers;

namespace Units.OneUnit.State.States
{
    public interface IState : IOneUnitApi, IActivatable
    {
        void InitializeStateController(IStateControllerInternal stateController);
        void Die();
    }
}
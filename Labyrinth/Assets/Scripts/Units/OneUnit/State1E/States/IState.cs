namespace Units.OneUnit.State1E
{
    public interface IState : IOneUnitApi, IActivatable
    {
        void InitializeStateController(IStateControllerInternal stateController);
    }
}
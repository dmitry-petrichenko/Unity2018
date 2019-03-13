using Scripts;
using Units.OneUnit.Info;

namespace Units.OneUnit.State.States
{
    public class PlacidState : IPlacidState
    {
        public IntVector2 Position => _placidController.Position;
        public IUnitInfoExternal DynamicInfo { get; }
        
        private readonly IPlacidController _placidController;
        private readonly ILifeController _lifeController;
        
        private IStateControllerInternal _stateController;
        
        public PlacidState(
            IPlacidController placidController,
            ILifeController lifeController)
        {
            _placidController = placidController;
            _lifeController = lifeController;
        }

        public void InitializeStateController(IStateControllerInternal stateController)
        {
            _stateController = stateController;
        }
        
        public void Die()
        {
            _stateController.SetState(_stateController.GetDeadState());
        }

        public void Activate()
        {
            _placidController.Activate();
        }

        public void Deactivate()
        {
            _placidController.Deactivate();
        }

        public void Dispose()
        {
        }
        
        public void MoveTo(IntVector2 position)
        {
            _placidController.MoveTo(position);
        }

        public void Wait()
        {
            _placidController.Wait();
        }

        public void Wait(IntVector2 position)
        {
            _placidController.Wait(position);
        }

        public void SetOnPosition(IntVector2 position)
        {
            _placidController.SetOnPosition(position);
        }

        public void Attack(IntVector2 position)
        {
            _stateController.SetState(_stateController.GetHostileState());
            _stateController.CurrentState.Attack(position);
        }

        public void TakeDamage(int value)
        {
            _lifeController.TakeDamage(value);
        }
    }
}
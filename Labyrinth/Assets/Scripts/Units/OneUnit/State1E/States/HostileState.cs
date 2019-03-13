using Scripts;
using Units.OneUnit.Info;

namespace Units.OneUnit.State1E
{
    public class HostileState : IHostileState
    {
        public IntVector2 Position => _placidController.Position;
        
        private readonly IPlacidController _placidController;
        private readonly IHostileController _hostileController;
        private readonly ILifeController _lifeController;
        
        private IStateControllerInternal _stateController;
        
        public HostileState(
            IPlacidController placidController,
            IHostileController hostileController,
            ILifeController lifeController)
        {
            _placidController = placidController;
            _hostileController = hostileController;
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
            _hostileController.Activate();
        }

        public void Deactivate()
        {
            _hostileController.Deactivate();
        }

        public void Dispose()
        {
        }
        
        public void MoveTo(IntVector2 position)
        {
            _stateController.SetState(_stateController.GetPlacidState());
            _stateController.CurrentState.MoveTo(position);
        }

        public void Wait()
        {
            _stateController.SetState(_stateController.GetPlacidState());
            _stateController.CurrentState.Wait();
        }

        public void Wait(IntVector2 position)
        {
            _stateController.SetState(_stateController.GetPlacidState());
            _stateController.CurrentState.Wait();
        }

        public void SetOnPosition(IntVector2 position)
        {
            _placidController.SetOnPosition(position);
        }

        public void Attack(IntVector2 position)
        {
            _hostileController.Attack(position);
        }

        public void TakeDamage(int value)
        {
            _lifeController.TakeDamage(value);
        }
    }
}
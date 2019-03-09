using Scripts;
using Units.OneUnit.Info;

namespace Units.OneUnit.State1E
{
    public class HostileState : IHostileState
    {
        public IntVector2 Position => _moveController.Position;
        
        private readonly IMoveController _moveController;
        private readonly IAttackController _attackController;
        private readonly ILifeController _lifeController;
        
        private IStateControllerInternal _stateController;
        
        public HostileState(
            IMoveController moveController,
            IAttackController attackController,
            ILifeController lifeController)
        {
            _moveController = moveController;
            _attackController = attackController;
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
            _attackController.Activate();
        }

        public void Deactivate()
        {
            _attackController.Deactivate();
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
            _moveController.SetOnPosition(position);
        }

        public void Attack(IntVector2 position)
        {
            _attackController.Attack(position);
        }

        public void TakeDamage(int value)
        {
            _lifeController.TakeDamage(value);
        }
    }
}
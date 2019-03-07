using Scripts;
using Units.OneUnit.Info;

namespace Units.OneUnit.State1E
{
    public class PlacidState : IPlacidState
    {
        public IntVector2 Position { get; }
        public IUnitInfoExternal DynamicInfo { get; }
        
        private readonly IMoveController _moveController;
        private readonly IAttackController _attackController;
        
        private IStateControllerInternal _stateController;
        
        public PlacidState(
            IMoveController moveController,
            IAttackController attackController)
        {
            _moveController = moveController;
            _attackController = attackController;
        }

        public void InitializeStateController(IStateControllerInternal stateController)
        {
            _stateController = stateController;
        }

        public void Activate()
        {
            _moveController.Activate();
        }

        public void Deactivate()
        {
            _moveController.Deactivate();
        }

        public void Dispose()
        {
        }
        
        public void MoveTo(IntVector2 position)
        {
            _moveController.MoveTo(position);
        }

        public void Wait()
        {
            _moveController.Wait();
        }

        public void Wait(IntVector2 position)
        {
            _moveController.Wait(position);
        }

        public void SetOnPosition(IntVector2 position)
        {
            _moveController.SetOnPosition(position);
        }

        public void Attack(IntVector2 position)
        {
            _stateController.SetState(_stateController.GetHostileState());
            _stateController.CurrentState.Attack(position);
        }

        public void TakeDamage(int value)
        {
            _attackController.TakeDamage(value);
        }
    }
}
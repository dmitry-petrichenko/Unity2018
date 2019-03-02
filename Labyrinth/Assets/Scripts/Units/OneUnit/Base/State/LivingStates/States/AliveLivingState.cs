using Scripts.Units.Events;
using Units.OneUnit;
using Units.OneUnit.Info;

namespace Scripts.Units.StateInfo.LivingStates.States
{
    public class AliveLivingState : ILivingState
    {   
        private readonly IUnitEvents _unitEvents;
        private readonly MoveController _moveController;
        private readonly IAttackController _attackController;
        
        public AliveLivingState(
            MoveController moveController,
            IAttackController attackController,
            IUnitEvents unitEvents)
        {
            _unitEvents = unitEvents;
            _moveController = moveController;
            _attackController = attackController;
        }

        public void Dispose()
        {
        }

        public IntVector2 Position => _moveController.Position;
        public IUnitInfoExternal StateInfo { get; }

        public IUnitEvents UnitEvents => _unitEvents;
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
            _attackController.Attack(position);
        }

        public void TakeDamage(int value)
        {
            _attackController.TakeDamage(value);
        }
    }
}
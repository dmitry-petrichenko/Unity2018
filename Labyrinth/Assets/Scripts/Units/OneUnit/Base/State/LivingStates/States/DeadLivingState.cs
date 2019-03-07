using Scripts.Units.Events;
using Units.OneUnit;
using Units.OneUnit.Info;

namespace Scripts.Units.StateInfo.LivingStates.States
{
    public class DeadLivingState : ILivingState
    {
        private readonly IMoveController _moveController;
        
        public DeadLivingState(IMoveController moveController)
        {
            _moveController = moveController;
        }

        public void Dispose()
        {
        }

        public IntVector2 Position => _moveController.Position;
        public IUnitInfoExternal DynamicInfo { get; }
        public IUnitEvents UnitEvents => new UnitEventsStub();
        public void MoveTo(IntVector2 position)
        {
        }

        public void Wait()
        {
        }

        public void Wait(IntVector2 position)
        {
        }

        public void SetOnPosition(IntVector2 position)
        {
        }

        public void Attack(IntVector2 position)
        {
        }

        public void TakeDamage(int value)
        {
        }
    }
}
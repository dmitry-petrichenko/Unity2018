using Scripts.Units.Events;
using Units.OneUnit;

namespace Scripts.Units.StateInfo.LivingStates.States
{
    public class DeadLivingState : ILivingState
    {    
        public void Dispose()
        {
        }

        public IntVector2 Position { get; }
        public IUnitStateExternal StateInfo { get; }
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
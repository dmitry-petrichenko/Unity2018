using Scripts;

namespace Units.OneUnit.State1E
{
    public class DeadState : IDeadState
    {
        public IntVector2 Position { get; }
        
        public void Dispose() {}

        public void MoveTo(IntVector2 position) {}

        public void Wait() {}

        public void Wait(IntVector2 position) {}

        public void SetOnPosition(IntVector2 position) {}

        public void Attack(IntVector2 position) {}

        public void TakeDamage(int value) {}
        
        public void Activate() {}

        public void Deactivate() {}

        public void InitializeStateController(IStateControllerInternal stateController)
        {
        }
    }
}
using Scripts.Units.StateInfo.LivingStates.States;

namespace Scripts.Units.StateInfo.LivingStates
{
    public interface ILivingStateControllerInternal
    {
        void SetAlive();
        void SetDead();
    }
    
    public interface ILivingStateControllerExternal
    {
        ILivingState CurrentState { get; }
    }
}
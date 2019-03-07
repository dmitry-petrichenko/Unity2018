using Units.OneUnit.Base;

namespace Scripts.Units.StateInfo.BaseState
{
    public interface IStateControllerInternal2
    {
        void InitializeBaseActionController(IBaseActionControllerInternal baseActionController);
        void SetAttackState();
        void SetPlacidState();
    }
}
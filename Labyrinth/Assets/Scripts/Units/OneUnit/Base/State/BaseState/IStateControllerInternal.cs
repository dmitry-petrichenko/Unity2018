using Units.OneUnit.Base;

namespace Scripts.Units.StateInfo.BaseState
{
    public interface IStateControllerInternal
    {
        void InitializeBaseActionController(IBaseActionControllerInternal baseActionController);
        void SetAttackState();
        void SetPlacidState();
    }
}
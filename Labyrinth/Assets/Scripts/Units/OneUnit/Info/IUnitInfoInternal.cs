using Scripts;

namespace Units.OneUnit.Info
{
    public interface IUnitInfoInternal
    {
        void SetAttackTarget(IOneUnitController target);
        void ResetAttackTarget();
        void SetWaitPosition(IntVector2 position);
        void ResetWaitPosition();
    }
}
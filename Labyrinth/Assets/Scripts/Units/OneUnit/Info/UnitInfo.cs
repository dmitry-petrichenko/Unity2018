using Scripts;

namespace Units.OneUnit.Info
{
    public class UnitInfo : IUnitInfoExternal, IUnitInfoInternal 
    {
        public IOneUnitController AttackTarget { get; private set; }
        public IntVector2 WaitPosition { get; private set; }
        
        public void SetAttackTarget(IOneUnitController target)
        {
            AttackTarget = target;
        }

        public void ResetAttackTarget()
        {
            AttackTarget = null;
        }

        public void SetWaitPosition(IntVector2 position)
        {
            WaitPosition = position;
        }

        public void ResetWaitPosition()
        {
            WaitPosition = IntVector2Constant.UNASSIGNET;
        }
    }
}
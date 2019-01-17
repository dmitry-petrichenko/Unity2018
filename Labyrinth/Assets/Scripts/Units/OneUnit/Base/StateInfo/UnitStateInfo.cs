using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public class UnitStateInfo : IUnitStateInfo
    {
        public UnitStateInfo()
        {
            WaitPosition = IntVector2.UNASSIGNET;
        }
        
        public IntVector2 WaitPosition { get; set; }
        public bool IsAttacking { get; set; }
        public IOneUnitController AttackTarget { get; set; }
    }
}
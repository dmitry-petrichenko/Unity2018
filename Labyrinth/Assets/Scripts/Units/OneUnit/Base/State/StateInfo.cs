using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public class StateInfo : IStateInfo
    {
        public StateInfo()
        {
            WaitPosition = IntVector2Constant.UNASSIGNET;
        }
        
        public IntVector2 WaitPosition { get; set; }
        public bool IsAttacking { get; set; }
        public IOneUnitController AttackTarget { get; set; }
        public string NoWayToTileEvent { get; }
        public string NextTileOccupatedEvent { get; }
        public string MovePathCompleteEvent { get; }
    }
}
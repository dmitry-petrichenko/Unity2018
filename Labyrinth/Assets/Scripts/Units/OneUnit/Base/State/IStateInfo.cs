using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public interface IStateInfo
    {
        IntVector2 WaitPosition { get; set; }
        IOneUnitController AttackTarget { get; set; }
        
        string NoWayToTileEvent { get; }
        string NextTileOccupatedEvent { get; }
        string MovePathCompleteEvent { get; }
        
        
    }
}
using Units.OneUnit;

namespace Scripts.Units.StateInfo.StateInfo
{
    public interface IUnitStateInfo : IUnitStateInfoInternal, IUnitStateInfoExternal
    {
    }
    
    public interface IUnitStateInfoExternal
    {
        IntVector2 WaitPosition { get; }
        IOneUnitController AttackTarget { get; }
    }
    
    public interface IUnitStateInfoInternal
    {
        string NoWayToTileEvent { get; }
        string NextTileOccupatedEvent { get; }
        string MovePathCompleteEvent { get; }

        void SetWaitPosition(IntVector2 position);
        void ResetWaitPosition();
        void SetAttackTarget(IOneUnitController target);
        void ResetAttackTarget();
    }
}
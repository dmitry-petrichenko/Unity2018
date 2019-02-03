namespace Scripts.Units.StateInfo
{
    public interface IUnitState
    {
        void SetState(IStateInfo stateInfo);
        IStateInfo GetWalkState();
        IStateInfo GetAttackState();
        string NoWayToTileEvent { get; }
        string NextTileOccupatedEvent { get; }
        string MovePathCompleteEvent { get; }
    }
}
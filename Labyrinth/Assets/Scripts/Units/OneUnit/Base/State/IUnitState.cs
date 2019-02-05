using System;
using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public interface IUnitState : IDisposable
    {
        IntVector2 WaitPosition { get; set; }
        IOneUnitController AttackTarget { get; set; }
        void SetState(IStateInfo stateInfo);
        IStateInfo GetWalkState();
        IStateInfo GetAttackState();
        string NoWayToTileEvent { get; }
        string NextTileOccupatedEvent { get; }
        string MovePathCompleteEvent { get; }
    }
}
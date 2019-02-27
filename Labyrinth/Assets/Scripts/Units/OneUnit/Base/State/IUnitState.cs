using System;
using Scripts.Units.Events;
using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public interface IUnitState : IUnitStateExternal
    {
        void SetWalkState();
        void SetAttackState();
        void SetDeadState();
        
        string NoWayToTileEvent { get; }
        string NextTileOccupatedEvent { get; }
        string MovePathCompleteEvent { get; }
		
        IntVector2 WaitPosition { get; set; }
        IOneUnitController AttackTarget { get; set; }
    }
}
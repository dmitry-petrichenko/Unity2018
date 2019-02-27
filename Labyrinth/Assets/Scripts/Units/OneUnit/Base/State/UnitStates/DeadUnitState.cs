using System;
using Units.OneUnit;

namespace Scripts.Units.StateInfo.UnitStates
{
    public class DeadUnitState : IUnitState
    {
        private readonly IUnitStateControllerInternal _unitStateController;
        public DeadUnitState()
        {
        }

        public void SetWalkState()
        {
        }

        public void SetAttackState()
        {
        }

        public void SetDeadState()
        {
        }
        
        public string NoWayToTileEvent => String.Empty;
        public string NextTileOccupatedEvent => String.Empty;
        public string MovePathCompleteEvent => String.Empty;
        public IntVector2 WaitPosition { get; set; }
        public IOneUnitController AttackTarget { get; set; }
    }
}
using System;
using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public class DeadStateInfo : IStateInfo
    {
        public IntVector2 WaitPosition { get; set; }
        public IOneUnitController AttackTarget { get; set; }

        public string NoWayToTileEvent => String.Empty;
        public string NextTileOccupatedEvent => String.Empty;
        public string MovePathCompleteEvent => String.Empty;
    }
}
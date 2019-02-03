using Scripts.Units.Events;
using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public class WalkStateInfo : IStateInfo
    {
        public IntVector2 WaitPosition { get; set; }
        public bool IsAttacking { get; set; }
        public IOneUnitController AttackTarget { get; set; }

        public string NoWayToTileEvent => UnitEventsTypes.NO_WAY_TO_WALK_DESTINATION;
        public string NextTileOccupatedEvent => UnitEventsTypes.NEXT_TILE_OCCUPATED;
        public string MovePathCompleteEvent => UnitEventsTypes.MOVE_PATH_COMPLETE;
    }
}
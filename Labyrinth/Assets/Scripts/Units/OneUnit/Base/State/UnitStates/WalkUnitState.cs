using Scripts.Units.Events;
using Scripts.Units.StateInfo.LivingStates;
using Units.OneUnit;

namespace Scripts.Units.StateInfo.UnitStates
{
    public class WalkUnitState : IUnitState
    {
        private readonly IUnitStateControllerInternal _unitStateController;
        private readonly ILivingStateControllerInternal _livingStateControllerInternal;
        
        public WalkUnitState(IUnitStateControllerInternal unitStateController)
        {
            _unitStateController = unitStateController;
        }

        public void SetWalkState()
        {
        }

        public void SetAttackState()
        {
            _unitStateController.SetStateInternal(_unitStateController.GetAttackState());
        }

        public void SetDeadState()
        {
            _unitStateController.SetStateInternal(_unitStateController.GetDeadState());
        }

        public string NoWayToTileEvent => UnitEventsTypes.NO_WAY_TO_WALK_DESTINATION;
        public string NextTileOccupatedEvent => UnitEventsTypes.NEXT_TILE_OCCUPATED;
        public string MovePathCompleteEvent => UnitEventsTypes.MOVE_PATH_COMPLETE;
        public IntVector2 WaitPosition { get; set; }
        public IOneUnitController AttackTarget { get; set; }
    }
}
using Scripts.Units.Events;
using Units.OneUnit;

namespace Scripts.Units.StateInfo.UnitStates
{
    public class AttackUnitState : IUnitState
    {
        private readonly IUnitStateControllerInternal _unitStateController;
        public AttackUnitState(IUnitStateControllerInternal unitStateController)
        {
            _unitStateController = unitStateController;
        }

        public void SetWalkState()
        {
            _unitStateController.SetInternalState(_unitStateController.GetAttackState());
        }

        public void SetAttackState()
        {
        }

        public void SetDeadState()
        {
            _unitStateController.SetInternalState(_unitStateController.GetDeadState());
        }

        public string NoWayToTileEvent => UnitEventsTypes.NO_WAY_TO_ATTACK_DESTINATION;
        public string NextTileOccupatedEvent => UnitEventsTypes.NEXT_TILE_OCCUPATED;
        public string MovePathCompleteEvent => UnitEventsTypes.MOVE_PATH_COMPLETE;
        public IntVector2 WaitPosition { get; set; }
        public IOneUnitController AttackTarget { get; set; }
    }
}
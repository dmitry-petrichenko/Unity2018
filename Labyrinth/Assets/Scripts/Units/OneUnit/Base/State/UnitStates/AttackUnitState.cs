using Scripts.Units.Events;
using Scripts.Units.StateInfo.LivingStates;
using Units.OneUnit;

namespace Scripts.Units.StateInfo.UnitStates
{
    public class AttackUnitState : IUnitState
    {
        private readonly IUnitStateControllerInternal _unitStateController;
        private readonly ILivingStateControllerInternal _livingStateControllerInternal;
        
        public AttackUnitState(
            IUnitStateControllerInternal unitStateController
            /*ILivingStateControllerInternal livingStateControllerInternal*/)
        {
            _unitStateController = unitStateController;
            //_livingStateControllerInternal = livingStateControllerInternal;
        }

        public void SetWalkState()
        {
            _unitStateController.SetStateInternal(_unitStateController.GetAttackState());
        }

        public void SetAttackState()
        {
        }

        public void SetDeadState()
        {
            _livingStateControllerInternal.SetDead();
            _unitStateController.SetStateInternal(_unitStateController.GetDeadState());
        }

        public string NoWayToTileEvent => UnitEventsTypes.NO_WAY_TO_ATTACK_DESTINATION;
        public string NextTileOccupatedEvent => UnitEventsTypes.NEXT_TILE_OCCUPATED;
        public string MovePathCompleteEvent => UnitEventsTypes.MOVE_PATH_COMPLETE;
        public IntVector2 WaitPosition { get; set; }
        public IOneUnitController AttackTarget { get; set; }
    }
}
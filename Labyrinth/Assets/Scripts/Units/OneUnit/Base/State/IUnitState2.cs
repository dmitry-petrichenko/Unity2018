using System;
using Scripts.Units.Events;
using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public interface IUnitState2 : IUnitStateExternal
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
    
    public class WalkUnitState : IUnitState2
    {
        private readonly IUnitStateControllerInternal _unitStateController;
        public WalkUnitState(IUnitStateControllerInternal unitStateController)
        {
            _unitStateController = unitStateController;
        }

        public void SetWalkState()
        {
        }

        public void SetAttackState()
        {
            _unitStateController.SetInternalState(_unitStateController.GetAttackState());
        }

        public void SetDeadState()
        {
            _unitStateController.SetInternalState(_unitStateController.GetDeadState());
        }

        public string NoWayToTileEvent => UnitEventsTypes.NO_WAY_TO_WALK_DESTINATION;
        public string NextTileOccupatedEvent => UnitEventsTypes.NEXT_TILE_OCCUPATED;
        public string MovePathCompleteEvent => UnitEventsTypes.MOVE_PATH_COMPLETE;
        public IntVector2 WaitPosition { get; set; }
        public IOneUnitController AttackTarget { get; set; }
    }
    
    public class AttackUnitState : IUnitState2
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
    
    public class DeadUnitState : IUnitState2
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
using Scripts.Extensions;
using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public class UnitState : Disposable, IUnitState
    {
        private IStateInfo _stateInfo;
        private IStateInfo _walkState;
        private IStateInfo _attackState;
        
        public IntVector2 WaitPosition { get; set; }
        public IOneUnitController AttackTarget { get; set; }

        public UnitState()
        {
            WaitPosition = IntVector2Constant.UNASSIGNET;
            
            _walkState = new WalkStateInfo();
            _attackState = new AttackStateInfo();
            SetState(_walkState);
        }

        public void SetState(IStateInfo stateInfo)
        {
            _stateInfo = stateInfo;
        }

        public IStateInfo GetWalkState()
        {
            return _walkState;
        }
        
        public IStateInfo GetAttackState()
        {
            return _attackState;
        }

        public string NoWayToTileEvent => _stateInfo.NoWayToTileEvent;
        public string NextTileOccupatedEvent => _stateInfo.NextTileOccupatedEvent;
        public string MovePathCompleteEvent => _stateInfo.MovePathCompleteEvent;

        public void Dispose()
        {
            
        }
    }
}
using Scripts;
using Units.OccupatedMap;
using Units.OneUnit.StatesControllers.Base;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class HostileController : Disposable, IHostileController
    {
        private IBaseActionController _baseActionController;
        private TargetOvertaker _targetOvertaker;
        private IOneUnitController _targetUnit;
        
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        private readonly NoWayHostileController _noWayHostileController;
        private readonly IWayHostileController _wayHostileController;

        public HostileController(
            IBaseActionController baseActionController,
            TargetOvertaker targetOvertaker,
            NoWayHostileController noWayHostileController,
            IWayHostileController wayHostileController,
            IOccupatedPossitionsMap occupatedPossitionsMap)
        {
            _baseActionController = baseActionController;
            _targetOvertaker = targetOvertaker;
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _noWayHostileController = noWayHostileController;
            _wayHostileController = wayHostileController;
        }

        public void Activate()
        {
            _noWayHostileController.Activate();
        }

        public void Deactivate()
        {
            Cancel();
            _noWayHostileController.Deactivate();
        }

        public void Cancel()
        {
            _wayHostileController.MoveToPositionComplete -= OvertakeTargetHandler;
            _wayHostileController.Cancel();
            //_targetOvertaker.Complete -= OvertakeTargetHandler;
            //_targetOvertaker.Cancel();
        }
        
        public void Attack(IntVector2 position)
        {
            _targetUnit = _occupatedPossitionsMap.GetUnitOnPosition(position);
            _wayHostileController.MoveToPositionComplete += OvertakeTargetHandler;
            _wayHostileController.MoveToPosition(_targetUnit.Position);
            //_targetOvertaker.Complete += OvertakeTargetHandler;
            //_targetOvertaker.Overtake(_targetUnit);
        }

        private void OvertakeTargetHandler()
        {
            _wayHostileController.MoveToPositionComplete -= OvertakeTargetHandler;
            //_targetOvertaker.Complete -= OvertakeTargetHandler;
            _baseActionController.Attack(_targetUnit.Position);
        }

        protected override void DisposeInternal()
        {
            Cancel();
            base.DisposeInternal();
        }
    }
}
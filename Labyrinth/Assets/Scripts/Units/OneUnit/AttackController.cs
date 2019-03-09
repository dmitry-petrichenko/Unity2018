using Scripts;
using Units.OccupatedMap;
using Units.OneUnit.Base;

namespace Units.OneUnit
{
    public class AttackController : Disposable, IAttackController
    {
        private IBaseActionController _baseActionController;
        private TargetOvertaker _targetOvertaker;
        private IOneUnitController _targetUnit;
        
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        private readonly OvertakeOccupatedPositionController _overtakeOccupatedPositionController;

        public AttackController(
            IBaseActionController baseActionController,
            TargetOvertaker targetOvertaker,
            OvertakeOccupatedPositionController overtakeOccupatedPositionController,
            IOccupatedPossitionsMap occupatedPossitionsMap)
        {
            _baseActionController = baseActionController;
            _targetOvertaker = targetOvertaker;
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _overtakeOccupatedPositionController = overtakeOccupatedPositionController;
        }

        public void Activate()
        {
            _overtakeOccupatedPositionController.Activate();
        }

        public void Deactivate()
        {
            Cancel();
            _overtakeOccupatedPositionController.Deactivate();
        }

        public void Cancel()
        {
            _targetOvertaker.Complete -= OvertakeTargetHandler;
            _targetOvertaker.Cancel();
        }
        
        public void Attack(IntVector2 position)
        {
            _targetUnit = _occupatedPossitionsMap.GetUnitOnPosition(position);
            _targetOvertaker.Complete += OvertakeTargetHandler;
            _targetOvertaker.Overtake(_targetUnit);
        }

        private void OvertakeTargetHandler()
        {
            _targetOvertaker.Complete -= OvertakeTargetHandler;
            _baseActionController.Attack(_targetUnit.Position);
        }

        protected override void DisposeInternal()
        {
            Cancel();
            base.DisposeInternal();
        }
    }
}
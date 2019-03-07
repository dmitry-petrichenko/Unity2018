using Scripts;
using Scripts.Extensions;
using Scripts.Units.StateInfo;
using Units.OneUnit.Base;

namespace Units.OneUnit
{
    public class AttackController : Disposable, IAttackController
    {
        private IBaseActionController _baseActionController;
        private TargetOvertaker _targetOvertaker;
        private IOneUnitController _targetUnit;
        
        private readonly IUnitsTable _unitsTable;
        private readonly OvertakeOccupatedPositionController _overtakeOccupatedPositionController;

        public AttackController(
            IBaseActionController baseActionController,
            TargetOvertaker targetOvertaker,
            OvertakeOccupatedPositionController overtakeOccupatedPositionController,
            IUnitsTable unitsTable)
        {
            _baseActionController = baseActionController;
            _targetOvertaker = targetOvertaker;
            _unitsTable = unitsTable;
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
            _targetUnit = _unitsTable.GetUnitOnPosition(position);
            _baseActionController.SetAttackState();
            _targetOvertaker.Complete += OvertakeTargetHandler;
            _targetOvertaker.Overtake(_targetUnit);
        }

        private void OvertakeTargetHandler()
        {
            _targetOvertaker.Complete -= OvertakeTargetHandler;
            _baseActionController.Attack(_targetUnit.Position);
        }

        public void TakeDamage(int value)
        {
            _baseActionController.TakeDamage(value);
        }

        protected override void DisposeInternal()
        {
            Cancel();
            base.DisposeInternal();
        }
    }
}
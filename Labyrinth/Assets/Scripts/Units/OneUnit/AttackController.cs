using Scripts;
using Scripts.Extensions;
using Scripts.Units.StateInfo;
using Units.OneUnit.Base;

namespace Units.OneUnit
{
    public class AttackController : Disposable, IAttackController
    {
        private IBaseActionController _baseActionController;
        private TargetOvertaker2 _targetOvertaker;
        private IOneUnitController _targetUnit;
        
        private readonly IUnitsTable _unitsTable;

        public AttackController(
            IBaseActionController baseActionController,
            TargetOvertaker2 targetOvertaker,
            OvertakeOccupatedPositionController overtakeOccupatedPositionController,
            IUnitsTable unitsTable)
        {
            _baseActionController = baseActionController;
            _targetOvertaker = targetOvertaker;
            _unitsTable = unitsTable;
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
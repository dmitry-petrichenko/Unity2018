using Scripts.Units.Events;
using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public class OneUnitController : IOneUnitController
    {
        private readonly IUnitEvents _unitEvents;
        
        private MoveController _moveController;
        private IUnitsTable _unitsTable;
        private IUnitStateInfo _unitStateInfo;

        public OneUnitController(
            IUnitsTable unitsTable,
            MoveController moveController,
            IUnitEvents unitEvents,
            IUnitStateInfo unitStateInfo)
        {
            _unitStateInfo = unitStateInfo;
            _unitsTable = unitsTable;
            _moveController = moveController;            
            _unitEvents = unitEvents;
            
            _unitsTable.AddUnit(this);
        }

        public IUnitStateInfo UnitStateInfo => _unitStateInfo;
        public IUnitEvents UnitEvents => _unitEvents;
        public IntVector2 Position => _moveController.Position;

        public void SetOnPosition(IntVector2 position) => _moveController.SetOnPosition(position);
        
        public void MoveTo(IntVector2 position) => _moveController.MoveTo(position);

        public void Wait() => _moveController.Wait();
        
        public void Wait(IntVector2 position) => _moveController.Wait(position);
    }
}
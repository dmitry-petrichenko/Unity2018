using Scripts;
using Units.OccupatedMap;
using Units.OneUnit.StatesControllers.Base;
using UnityEngine;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class HostileController : Disposable, IHostileController
    {
        private IBaseActionController _baseActionController;
        private IOneUnitController _targetUnit;
        
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        private readonly TargetOvertaker _targetOvertaker;

        public HostileController(
            IBaseActionController baseActionController,
            TargetOvertaker targetOvertaker,
            IOccupatedPossitionsMap occupatedPossitionsMap)
        {
            _baseActionController = baseActionController;
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _targetOvertaker = targetOvertaker;
        }

        public void Activate()
        {
        }

        public void Deactivate()
        {
            Cancel();
        }

        public void Cancel()
        {
            _targetOvertaker.OvertakeComplete -= OvertakeTargetHandler;
            _targetOvertaker.Cancel();
        }
        
        public void Attack(IntVector2 position)
        {
            _targetUnit = _occupatedPossitionsMap.GetUnitOnPosition(position);
            if (_targetUnit is UnitStub)
                return;
            
            _targetUnit = _occupatedPossitionsMap.GetUnitOnPosition(position);
            _targetOvertaker.OvertakeComplete += OvertakeTargetHandler;
            _targetOvertaker.Overtake(_targetUnit);
        }

        private void OvertakeTargetHandler()
        {
            Cancel();
            Debug.Log(_targetUnit.Position.ToString() + " OvertakeTargetHandler");
            _baseActionController.Attack(_targetUnit.Position);
        }

        protected override void DisposeInternal()
        {
            Cancel();
            base.DisposeInternal();
        }
    }
}
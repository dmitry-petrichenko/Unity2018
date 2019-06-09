using System.Collections.Generic;
using Scripts;
using Units.OneUnit;

namespace Units.OccupatedMap
{
    public class OccupatedPossitionsMap : Disposable, IOccupatedPossitionsMap
    {
        List<IOneUnitController> _unitsList = new List<IOneUnitController>();
        
        public void AddUnit(IOneUnitController oneUnitController)
        {
            _unitsList.Add(oneUnitController);
        }

        public void RemoveUnit(IOneUnitController oneUnitController)
        {
            _unitsList.Remove(oneUnitController);
        }

        public List<IntVector2> GetOccupiedPositions()
        {
            var occupiedPossitions = new List<IntVector2>();
            foreach (var oneUnitController in _unitsList)
            {
                occupiedPossitions.Add(oneUnitController.Position);
            }

            return occupiedPossitions;
        }

        public List<IntVector2> GetOccupiedPositionsExcept(IntVector2 position)
        {
            var positions = GetOccupiedPositions();
            positions = RemovePosition(position, positions);
            return positions;
        }

        public bool IsVacantPosition(IntVector2 position)
        {
            if (GetUnitOnPosition(position) is UnitStub)
                return true;
            return false;
        }
        
        public IOneUnitController GetUnitOnPosition(IntVector2 index)
        {
            foreach (var oneUnitController in _unitsList)
            {
                if (oneUnitController.Position.Equals(index))
                {
                    return oneUnitController;
                }
            }

            return new UnitStub(index);
        }
        
        private List<IntVector2> RemovePosition(IntVector2 position, List<IntVector2> occupiedPossitions)
        {
            List<IntVector2> copy;
            copy = CopyDictionary(occupiedPossitions);
            occupiedPossitions = copy;
            occupiedPossitions.Remove(position);
            
            return occupiedPossitions;
        }
        
        private List<IntVector2> CopyDictionary(List<IntVector2> value)
        {
            List<IntVector2> copy = new List<IntVector2>();
            foreach (var index in value)
            {
                copy.Add(index);
            }

            return copy;
        }

        protected override void DisposeInternal()
        {
            _unitsList.Clear();
            _unitsList = null;
            base.DisposeInternal();
        }
    }
}
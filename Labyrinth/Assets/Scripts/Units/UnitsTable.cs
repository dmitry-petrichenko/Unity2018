using System.Collections.Generic;

namespace ZScripts.Units
{
    public class UnitsTable : OccupatedPossitionsTable, IUnitsTable
    {
        List<IOneUnitController> _unitsList = new List<IOneUnitController>();

        public void AddUnit(IOneUnitController oneUnitController)
        {
            _unitsList.Add(oneUnitController);
        }

        public void removeUnit(IOneUnitController oneUnitController)
        {
            _unitsList.Remove(oneUnitController);
        }

        public IOneUnitController GetUnitOnPosition(IntVector2 index)
        {
            foreach (var oneUnitController in _unitsList)
            {
                if (oneUnitController.Position.x == index.x &&
                    oneUnitController.Position.y == index.y)
                {
                    return oneUnitController;
                }
            }

            return null;
        }
        
        
    }
}
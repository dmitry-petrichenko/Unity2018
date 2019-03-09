using System.Collections.Generic;
using Scripts;
using Units.OneUnit;

namespace Units
{
    public class UnitsTable : OccupatedPossitionsTable, IUnitsTable
    {
        List<IOneUnitController> _unitsList = new List<IOneUnitController>();

        public void AddUnit(IOneUnitController oneUnitController)
        {
            _unitsList.Add(oneUnitController);
            SetOccupied(oneUnitController.Position);
        }

        public void removeUnit(IOneUnitController oneUnitController)
        {
            _unitsList.Remove(oneUnitController);
            SetVacant(oneUnitController.Position);
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

            return new UnitStub(index);
        }
    }
}
using System.Collections.Generic;
using Scripts.Units.PathFinder;

namespace Scripts.Units
{
    public class MoveConsideringOccupatedController
    {
        private readonly IUnitsTable _unitsTable;
        private readonly IPathFinderController _pathFinderController;
        private readonly ISubMoveController _subMoveController;
        private List<IntVector2> _occupiedPossitions;
        
        public MoveConsideringOccupatedController(
            IUnitsTable unitsTable,
            IPathFinderController pathFinderController,
            ISubMoveController subMoveController
            )
        {
            _unitsTable = unitsTable;
            _subMoveController = subMoveController;
            _pathFinderController = pathFinderController;

            Initialize();
        }

        public void Initialize()
        {
            _subMoveController.NextPositionOccupiedHandler += NextPositionOccupiedHandler;
        }

        private void NextPositionOccupiedHandler(IntVector2 occupiedPosition)
        {
            _subMoveController.Cancel();
            _occupiedPossitions = _unitsTable.GetOccupiedPositions();
            RemoveCurrentUnitPosition();
            List<IntVector2> newPath = _pathFinderController.GetPath(_subMoveController.Position,
                _subMoveController.Destination, _occupiedPossitions);
            _subMoveController.MoveTo(newPath);
        }

        private void RemoveCurrentUnitPosition()
        {
            List<IntVector2> copy;
            copy = CopyDictionary(_occupiedPossitions);
            _occupiedPossitions = copy;
            _occupiedPossitions.Remove(_subMoveController.Position);
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
    }
}
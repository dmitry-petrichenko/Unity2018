using System.Collections.Generic;
using Scripts;
using Units.OneUnit.Base.GameObject;
using Units.PathFinder;

namespace Units.OneUnit.Base
{
    public class PathGeneratorController : IPathGeneratorController
    {
        public IntVector2 Destination { get; private set; }

        private ChangeDirrectionAfterMoveTileCompleteController _changeDirrectionAfterMoveTileCompleteController;
        private IPathFinderController _pathFinderController;
        private IUnitGameObjectController _unitGameObjectController;
        
        public PathGeneratorController(
            ChangeDirrectionAfterMoveTileCompleteController changeDirrectionAfterMoveTileCompleteController,
            IPathFinderController pathFinderController,
            IUnitGameObjectController unitGameObjectController)
        {
            _changeDirrectionAfterMoveTileCompleteController = changeDirrectionAfterMoveTileCompleteController;
            _pathFinderController = pathFinderController;
            _unitGameObjectController = unitGameObjectController;
        }

        public void MoveToPosition(IntVector2 position)
        {
            List<IntVector2> path = _pathFinderController.GetPath(_unitGameObjectController.Position, position, null);
            Destination = position;
            _changeDirrectionAfterMoveTileCompleteController.MoveTo(path);
        }

        public void MoveToPositionAvoidingOccupiedCells(IntVector2 position, List<IntVector2> cells)
        {
            List<IntVector2> path = _pathFinderController.GetPath(_unitGameObjectController.Position, position, cells);
            Destination = position;
            _changeDirrectionAfterMoveTileCompleteController.MoveTo(path);
        }
    }
}
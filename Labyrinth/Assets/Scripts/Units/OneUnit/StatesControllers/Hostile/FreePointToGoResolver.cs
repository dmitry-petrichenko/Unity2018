using System.Collections.Generic;
using System.Linq;
using Scripts;
using Scripts.CustomDebug;
using Units.OccupatedMap;
using Units.OneUnit.StatesControllers.Base;
using Units.PathFinder;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class FreePointToGoResolver : IFreePointToGoResolver
    {
        private readonly IPathFinderController _pathFinder;
        private readonly IAdjacentPointsResolver _adjacentPointsResolver;
        private readonly IFreePossitionsMap _freePossitionsMap;
        private readonly IBaseActionController _baseActionController;
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;

        public FreePointToGoResolver(
            IAdjacentPointsResolver adjacentPointsResolver,
            IPathFinderController pathFinderController,
            IBaseActionController baseActionController,
            IOccupatedPossitionsMap occupatedPossitionsMap,
            IFreePossitionsMap freePossitionsMap)
        {
            _freePossitionsMap = freePossitionsMap;
            _adjacentPointsResolver = adjacentPointsResolver;
            _pathFinder = pathFinderController;
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _baseActionController = baseActionController;
        }

        public IntVector2 GetFreePoint(IntVector2 position)
        {
            var adjacentPoints = _adjacentPointsResolver.GetFreeAdjacentUnitPoints(position,
                IsValidPosition);

            var occupiedPossitions = _occupatedPossitionsMap.GetOccupiedPositionsExcept(_baseActionController.Position);

            List<IntVector2Info> vectorInfos = new List<IntVector2Info>();
            adjacentPoints.ForEach(i => { vectorInfos.Add(new IntVector2Info(i, position, _baseActionController.Position, occupiedPossitions));});

            var result = vectorInfos.OrderBy(i => i.AcceptableIndex).ToList();

            for (int i = 0; i < result.Count; i++)
            {
                if (IsAchivable(result[i].Vector))
                {
                    return result[i].Vector;
                }
            }

            if (!result.Any())
                ApplicationDebugger.ThrowException("FreePointToGoResolver: There is no available points");

            return IntVector2Constant.UNASSIGNET;
        }

        private bool IsValidPosition(IntVector2 position)
        {
            var isFree = _freePossitionsMap.IsFreePosition(position);

            if (!isFree)
                return false;

            return true;
        }

        private bool IsAchivable(IntVector2 position)
        {
            var occupiedPossitions = _occupatedPossitionsMap.GetOccupiedPositionsExcept(_baseActionController.Position);
            var path = _pathFinder.GetPath(position, _baseActionController.Position, occupiedPossitions);
            var IsAchievable = path.Any();

            return IsAchievable;
        }
        
        private class IntVector2Info
        {
            public IntVector2 Vector => _vector;
            public int AcceptableIndex => _acceptableIndex;

            private readonly IPathFinderController _pathFinder;

            private IntVector2 _vector;
            private IntVector2 _targetPossition;
            private IntVector2 _unitPosition;
            private List<IntVector2> _occupatedPossitions;
            private int _acceptableIndex;

            public IntVector2Info(IntVector2 vector, IntVector2 targetPossition, IntVector2 unitPosition, List<IntVector2> occupatedPossitions)
            {
                _vector = vector;
                _targetPossition = targetPossition;
                _unitPosition = unitPosition;
                _occupatedPossitions = occupatedPossitions;

                InitializeAcceptableIndex();
            }
            
            private void InitializeAcceptableIndex()
            {
                var empiricalValueToUnit = _vector.GetEmpiricalValueForPoint(_unitPosition);
                var empiricalValueToTarget = _vector.GetEmpiricalValueForPoint(_targetPossition);
                _acceptableIndex = empiricalValueToUnit + empiricalValueToTarget;
            }
        }
    }
}
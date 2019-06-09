using System;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using Scripts.CustomDebug;
using Units.OneUnit.Info;
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

        public FreePointToGoResolver(
            IAdjacentPointsResolver adjacentPointsResolver,
            IPathFinderController pathFinderController,
            IBaseActionController baseActionController,
            IFreePossitionsMap freePossitionsMap)
        {
            _freePossitionsMap = freePossitionsMap;
            _adjacentPointsResolver = adjacentPointsResolver;
            _pathFinder = pathFinderController;
            _baseActionController = baseActionController;
        }

        public IntVector2 GetFreePoint(IntVector2 position)
        {
            var adjacentPoints = _adjacentPointsResolver.GetFreeAdjacentUnitPoints(position,
                _freePossitionsMap.IsFreePosition);

            List<IntVector2Info> vectorInfos = new List<IntVector2Info>();
            adjacentPoints.ForEach(i => { vectorInfos.Add(new IntVector2Info(i, position, _baseActionController.Position, _pathFinder));});

            var result = vectorInfos.Where(i => i.IsAchievable).OrderBy(i => i.AcceptableIndex).ToList();

            if (!result.Any())
                ApplicationDebugger.ThrowException("FreePointToGoResolver: There is no available points");

            return result[0].Vector;
        }

        private class IntVector2Info
        {
            public IntVector2 Vector => _vector;
            public int AcceptableIndex => _acceptableIndex;
            public bool IsAchievable => _isAchievable;

            private readonly IPathFinderController _pathFinder;

            private IntVector2 _vector;
            private IntVector2 _targetPossition;
            private IntVector2 _unitPosition;
            private int _acceptableIndex;
            private bool _isAchievable;

            public IntVector2Info(IntVector2 vector, IntVector2 targetPossition, IntVector2 unitPosition, IPathFinderController pathFinder)
            {
                _vector = vector;
                _targetPossition = targetPossition;
                _unitPosition = unitPosition;
                _pathFinder = pathFinder;

                InitializeAchievable();
                InitializeAcceptableIndex();
            }

            private void InitializeAchievable()
            {
                var path = _pathFinder.GetPath(_vector, _unitPosition, new List<IntVector2>());
                _isAchievable = path.Any();
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
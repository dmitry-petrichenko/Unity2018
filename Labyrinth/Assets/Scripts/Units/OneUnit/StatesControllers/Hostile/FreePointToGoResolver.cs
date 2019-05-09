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
        private readonly IWayHostileControllerParameters _parameters;

        public FreePointToGoResolver(
            IWayHostileControllerParameters parameters,
            IAdjacentPointsResolver adjacentPointsResolver,
            IPathFinderController pathFinderController,
            IFreePossitionsMap freePossitionsMap)
        {
            _parameters = parameters;
            _freePossitionsMap = freePossitionsMap;
            _adjacentPointsResolver = adjacentPointsResolver;
            _pathFinder = pathFinderController;
        }

        public IntVector2 GetFreePoint(IntVector2 position)
        {
            var adjacentPoints = _adjacentPointsResolver.GetFreeAdjacentUnitPoints(position,
                _freePossitionsMap.IsFreePosition);

            List<IntVector2Info> vectorInfos = new List<IntVector2Info>();
            adjacentPoints.ForEach(i => { vectorInfos.Add(new IntVector2Info(i, position, _parameters.UnitPosition, _pathFinder));});

            var result = vectorInfos.Where(i => i.IsAchievable).OrderBy(i => i.EmpiricalValue).ToList();

            if (!result.Any())
                ApplicationDebugger.ThrowException("FreePointToGoResolver: There is no available points");

            return result[0].Vector;
        }

        private class IntVector2Info
        {
            public IntVector2 Vector => _vector;
            public int EmpiricalValue => _empiricalValue;
            public bool IsAchievable => _isAchievable;

            private readonly IPathFinderController _pathFinder;

            private IntVector2 _vector;
            private IntVector2 _targetPossition;
            private IntVector2 _unitPosition;
            private int _empiricalValue;
            private bool _isAchievable;

            public IntVector2Info(IntVector2 vector, IntVector2 targetPossition, IntVector2 unitPosition, IPathFinderController pathFinder)
            {
                _vector = vector;
                _targetPossition = targetPossition;
                _unitPosition = unitPosition;
                _pathFinder = pathFinder;

                InitializeEmpiricalValue();
                InitializeAchievable();
            }

            private void InitializeAchievable()
            {
                var path = _pathFinder.GetPath(_vector, _unitPosition, new List<IntVector2>());
                _isAchievable = path.Any();
            }

            private void InitializeEmpiricalValue()
            {
                _empiricalValue = _vector.GetEmpiricalValueForPoint(_targetPossition);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using Units.OneUnit.StatesControllers.Hostile;

namespace UnitTests.StatesControllers.Hostile
{
    public class AdjacentPointsResolver : IAdjacentPointsResolver
    {
        public List<IntVector2> GetFreeAdjacentUnitPoints(IntVector2 unitPosition, Predicate<IntVector2> isValid, int radiusValue = 1)
        {
            var adjacentPoints = new List<IntVector2>();
            while (radiusValue < 5)
            {
                adjacentPoints = unitPosition.GetAdjacentPoints(isValid, radiusValue);
                if (adjacentPoints.Any())
                {
                    return adjacentPoints;
                }
            }

            throw new Exception("AdjacentPointsResolver: There are no adjacent points in unit range");
        }
    }
}
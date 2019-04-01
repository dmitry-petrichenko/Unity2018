using System;
using System.Collections.Generic;
using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public interface IAdjacentPointsResolver
    {
        List<IntVector2> GetFreeAdjacentUnitPoints(IntVector2 unitPosition, Predicate<IntVector2> isValid,
            int radiusValue = 1);
    }
}
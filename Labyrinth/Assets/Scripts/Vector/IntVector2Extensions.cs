using System;
using System.Collections.Generic;

namespace Scripts
{
    public static class IntVector2Extensions
    {
        public static List<IntVector2> GetAdjacentPoints(this IntVector2 point, Predicate<IntVector2> isValid = null)
        {
            var resultList = new List<IntVector2>();
            IntVector2 tempPoint;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    tempPoint = new IntVector2(point.x + i, point.y + j);
                    var free = isValid != null ? isValid.Invoke(tempPoint) : true;
                    if (free && !tempPoint.Equals(point))
                    {
                        resultList.Add(new IntVector2(point.x + i, point.y + j));
                    }
                }
            }

            return resultList;
        }
    }
    
    public struct IntVector2Constant
    {
        public static IntVector2 UNASSIGNET = new IntVector2(-1000, -1000);
    }
}
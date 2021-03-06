using System;
using System.Collections.Generic;
using Scripts.CustomDebug;

namespace Scripts
{
    public static class IntVector2Extensions
    {
        public static int GetEmpiricalValueForPoint(this IntVector2 point, IntVector2 otherPoint)
        {
            if (otherPoint.Equals(IntVector2Constant.UNASSIGNET) || point.Equals(IntVector2Constant.UNASSIGNET))
                ApplicationDebugger.ThrowException("IntVector2 EmpiricalValue for UNASSIGNET IntVector2");

            var t = Math.Sqrt(Math.Pow(point.x - otherPoint.x,2) + Math.Pow(point.y - otherPoint.y,2));
            double r1 = t * 10.0;
            int r = (int)r1;
            
            return r;
        }
        
        public static List<IntVector2> GetAdjacentPoints(this IntVector2 point, Predicate<IntVector2> isValid = null, int radiusValue = 1)
        {
            if (radiusValue <= 0)
            {
                ApplicationDebugger.ThrowException("IntVector2 radiusValue cannot be less than 1");
            }

            var resultList = new List<IntVector2>();
            var b = GetPointsInRange(point, isValid, radiusValue);
            var s = GetPointsInRange(point, isValid, radiusValue - 1);
            b.ForEach(i =>
            {
                if (!s.Contains(i))
                {
                    resultList.Add(i);
                }
            });

            return resultList;
        }

        private static List<IntVector2> GetPointsInRange(IntVector2 point, Predicate<IntVector2> isValid = null, int range = 0)
        {
            var resultList = new List<IntVector2>();
            IntVector2 tempPoint;
            for (int i = -range; i <= range; i++)
            {
                for (int j = -range; j <= range; j++)
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
        public static IntVector2 UNASSIGNET = new IntVector2(Int32.MinValue, Int32.MinValue);
    }
}
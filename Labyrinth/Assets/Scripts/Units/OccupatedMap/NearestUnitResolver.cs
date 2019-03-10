using System;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using Units.OneUnit;

namespace Units.OccupatedMap
{
    public class NearestUnitResolver
    {
        public static IOneUnitController GetNearestUnit(IntVector2 targetPosition, List<IOneUnitController> units)
        {
            var list = new List<UnitInfo>();
            foreach (var unit in units)
            {
                if (unit.Position.Equals(targetPosition))
                    continue;
                
                list.Add(new UnitInfo(unit, GetDistance(targetPosition, unit.Position)));
            }

            if (list.Count == 0)
                return null;
            
             list.Sort();
            return list.First().Unit;
        }
        
        private static double GetDistance(IntVector2 position1, IntVector2 position2)
        {
            float a = position2.x - position1.x;
            float b = position2.y - position1.y;
            
            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
        }

        private class UnitInfo : IComparable<UnitInfo>
        {
            public IOneUnitController Unit;
            public double Distance;
            
            public UnitInfo(IOneUnitController unit, double distance)
            {
                Distance = distance;
                Unit = unit;
            }

            public int CompareTo(UnitInfo other)
            {
                if (Distance < other.Distance)
                    return -1;
                if(Distance > other.Distance)
                    return 1;

                return 0;
            }
        }
    }
}
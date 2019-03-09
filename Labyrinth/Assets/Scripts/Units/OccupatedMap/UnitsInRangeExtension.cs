using System.Collections.Generic;
using Scripts;
using Units.OneUnit;

namespace Units.OccupatedMap
{
    public static class UnitsInRegionExtension
    {
        public static List<IOneUnitController> GetUnitsInRegion(this IOccupatedPossitionsMap possitionsMap, 
            IntVector2 point1, IntVector2 point2)
        {
            var units = new List<IOneUnitController>();
            var loopParamsI = new LoopPaprams(point1.x, point2.x);
            for (int i = loopParamsI.StartValue; loopParamsI.Condition(i); i = loopParamsI.Operation(i))
            {
                var loopParamsJ = new LoopPaprams(point1.y, point2.y);
                for (int j = loopParamsJ.StartValue; loopParamsJ.Condition(j); j = loopParamsJ.Operation(j))
                {
                    if (!possitionsMap.IsVacantPosition(new IntVector2(i, j)))
                    {
                        units.Add(possitionsMap.GetUnitOnPosition(new IntVector2(i, j)));
                    }
                }
            }
            return units;
        }
        
        private class LoopPaprams
        {
            private int _a, _b;
            private  bool _isPositive;

            public LoopPaprams(int a, int b)
            {
                _a = a;
                _b = b;
                _isPositive = _a < _b;
            }

            public int StartValue => _a;

            public int Operation(int value)
            {
                if (_isPositive)
                    return ++value;
                
                return --value;
            }

            public bool Condition(int value)
            {
                if (_isPositive)
                    return value <= _b;
                
                return value >= _b;
            }
        }
    }
}
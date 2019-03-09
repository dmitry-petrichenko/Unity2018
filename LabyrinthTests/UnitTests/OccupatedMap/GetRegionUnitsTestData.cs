using System.Collections;
using System.Collections.Generic;
using Scripts;
using Units.OneUnit;

namespace Tests.Scripts.Units.OccupatedMap
{
    public class GetRegionUnitsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { GetUnitsList01(), GetNegativePositions01(), new IntVector2(-1, -1), new IntVector2(-10, -10)};
            yield return new object[] { GetUnitsList01(), GetPositivePositions01(), new IntVector2(1, 1), new IntVector2(20, 20)};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public static List<OneUnitControllerMock> GetUnitsList01()
        {
            List<OneUnitControllerMock> list = new List<OneUnitControllerMock>
            {
                new OneUnitControllerMock(new IntVector2(-4, -1)),
                new OneUnitControllerMock(new IntVector2(-6, -2)),
                new OneUnitControllerMock(new IntVector2(1, 2)),
                new OneUnitControllerMock(new IntVector2(10, 15))
            };

            return list;
        }

        public static List<IOneUnitController> GetPositivePositions01()
        {
            List<IOneUnitController> list = new List<IOneUnitController>
            {
                new OneUnitControllerMock(new IntVector2(1, 2)),
                new OneUnitControllerMock(new IntVector2(10, 15))
            };

            return list;
        }
        
        public static List<IOneUnitController> GetNegativePositions01()
        {
            List<IOneUnitController> list = new List<IOneUnitController>
            {
                new OneUnitControllerMock(new IntVector2(-4, -1)),
                new OneUnitControllerMock(new IntVector2(-6, -2))
            };

            return list;
        }
    }
}
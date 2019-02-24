using System.Collections;
using System.Collections.Generic;
using Scripts;

namespace Tests.Scripts.Units.PathFinder
{
    public class PathFinderTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new IntVector2(0, 0), new IntVector2(5, 5), GetList0_0_5_5()};
            yield return new object[] { new IntVector2(0, 1), new IntVector2(0, 3), GetList0_1_0_3()};
        }

        public static List<IntVector2> GetList0_0_5_5()
        {
            List<IntVector2> list = new List<IntVector2>
            {
                new IntVector2(1, 1),
                new IntVector2(2, 1),
                new IntVector2(3, 2),
                new IntVector2(2, 3),
                new IntVector2(1, 4),
                new IntVector2(2, 5),
                new IntVector2(3, 5),
                new IntVector2(4, 5),
                new IntVector2(5, 5)
            };

            return list;
        }

        public static List<IntVector2> GetList0_1_0_3()
        {
            List<IntVector2> list = new List<IntVector2>
            {
                new IntVector2(1, 1),
                new IntVector2(2, 1),
                new IntVector2(3, 2),
                new IntVector2(2, 3),
                new IntVector2(1, 3),
                new IntVector2(0, 3)
            };

            return list;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
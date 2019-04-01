using System.Collections;
using System.Collections.Generic;
using Scripts;

namespace TestProject1
{
    public class GetFreePointThowNoValidPoints : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new IntVector2(20, 20), new IntVector2(10, 10), GetPathFinderValidList(), GetAdjacentUnitPoints1(), new IntVector2(8, 8), false};
            yield return new object[] { new IntVector2(0, 0), new IntVector2(10, 10), GetPathFinderEmptyList(), GetAdjacentUnitPoints2(), new IntVector2(-2, -1), true};
            yield return new object[] { new IntVector2(2, 2), new IntVector2(10, 10), GetPathFinderValidList(), GetAdjacentUnitPoints1(), new IntVector2(3, 3), false};
            yield return new object[] { new IntVector2(0, 0), new IntVector2(10, 10), GetPathFinderEmptyList(), GetAdjacentUnitPoints3(), new IntVector2(-2, -1), true};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        List<IntVector2> GetPathFinderValidList()
        {
            var list = new List<IntVector2>();
            list.Add(new IntVector2(0, 1)); // Any
            return list;
        }

        List<IntVector2> GetPathFinderEmptyList()
        {
            var list = new List<IntVector2>();
            return list;
        }

        List<IntVector2> GetAdjacentUnitPoints1()
        {
            var list = new List<IntVector2>();
            list.Add(new IntVector2(5, 5));
            list.Add(new IntVector2(8, 8));
            list.Add(new IntVector2(6, 6));
            list.Add(new IntVector2(4, 4));
            list.Add(new IntVector2(5, 4));
            list.Add(new IntVector2(3, 3));
            return list;
        }

        List<IntVector2> GetAdjacentUnitPoints2()
        {
            var list = new List<IntVector2>();
            list.Add(new IntVector2(5, 5));
            list.Add(new IntVector2(6, 6));
            list.Add(new IntVector2(7, 7));
            list.Add(new IntVector2(8, -8));
            list.Add(new IntVector2(-9, -9));
            return list;
        }

        List<IntVector2> GetAdjacentUnitPoints3()
        {
            var list = new List<IntVector2>();
            list.Add(new IntVector2(3, 2));
            list.Add(new IntVector2(4, 7));
            list.Add(new IntVector2(-2, -1));
            list.Add(new IntVector2(6, 9));
            list.Add(new IntVector2(7, 9));
            list.Add(new IntVector2(10, 19));
            return list;
        }
    }
}
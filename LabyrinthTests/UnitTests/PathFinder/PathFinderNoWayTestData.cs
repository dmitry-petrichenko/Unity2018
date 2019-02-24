using System.Collections;
using System.Collections.Generic;
using Scripts;

namespace Tests.Scripts.Units.PathFinder
{
    public class PathFinderNoWayTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new IntVector2(0, 0), new IntVector2(0, 5)};
            yield return new object[] { new IntVector2(0, 1), new IntVector2(0, 4)};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
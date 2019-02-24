using System.Collections;
using System.Collections.Generic;
using Scripts;

namespace Tests.Scripts.Units.PathFinder
{
    public class PathFinderDestinationTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new IntVector2(0, 0), new IntVector2(0, 2)};
            yield return new object[] { new IntVector2(0, 1), new IntVector2(0, 2)};
            yield return new object[] { new IntVector2(4, 1), new IntVector2(2, 2)};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
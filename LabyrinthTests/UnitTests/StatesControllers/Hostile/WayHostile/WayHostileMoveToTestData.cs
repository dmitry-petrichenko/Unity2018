using System.Collections;
using System.Collections.Generic;
using Scripts;

namespace UnitTests.StatesControllers.Hostile.WayHostile
{
    public class WayHostileMoveToTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new IntVector2(20, 20), new IntVector2(10, 10), false};
            yield return new object[] { new IntVector2(200, -200), new IntVector2(200, -201), true};
            yield return new object[] { new IntVector2(-1000, 300), new IntVector2(-1000, 299), true};
            yield return new object[] { new IntVector2(0, 0), new IntVector2(-10, 9), false};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
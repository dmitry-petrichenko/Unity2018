using System.Collections;
using System.Collections.Generic;
using Scripts;

namespace Tests.Scripts.Units.Vectors
{
    public class IntVector2TestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new IntVector2(0, 0), GetAdjastments(new IntVector2(0, 0))};
            yield return new object[] { new IntVector2(-100, -31), GetAdjastments(new IntVector2(-100, -31))};
            yield return new object[] { new IntVector2(2310, 112), GetAdjastments(new IntVector2(2310, 112))};
            yield return new object[] { new IntVector2(-999, 887), GetAdjastments(new IntVector2(-999, 887))};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private List<IntVector2> GetAdjastments(IntVector2 point)
        {
            var adjastments = new List<IntVector2>();
            adjastments.Add(new IntVector2(point.x + 1, point.y + 1));
            adjastments.Add(new IntVector2(point.x + 1, point.y - 1));
            adjastments.Add(new IntVector2(point.x + 1, point.y));
            adjastments.Add(new IntVector2(point.x - 1, point.y + 1));
            adjastments.Add(new IntVector2(point.x - 1, point.y - 1));
            adjastments.Add(new IntVector2(point.x - 1, point.y));
            adjastments.Add(new IntVector2(point.x, point.y + 1));
            adjastments.Add(new IntVector2(point.x, point.y - 1));

            return adjastments;
        }
    }
}
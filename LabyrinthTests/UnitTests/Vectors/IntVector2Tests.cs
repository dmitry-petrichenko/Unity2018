using System;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using Xunit;

namespace Tests.Scripts.Units.Vectors
{
    public class IntVector2Tests
    {
        [Theory]
        [ClassData(typeof(IntVector2TestData))]
        public void GetAdjacentPoints_ShouldReturnCorrectPoints(IntVector2 taggetPoint, List<IntVector2> expectedAdjastments)
        {
            // Act

            var y = new IntVector2(0, 0);
            var l = y.GetAdjacentPoints(null, 2);

            var actualAdjastments = taggetPoint.GetAdjacentPoints();

            var actual = new List<IntVector2Comparable>();
            var expected = new List<IntVector2Comparable>();

            actualAdjastments.ForEach(i => actual.Add(new IntVector2Comparable(i)));
            expectedAdjastments.ForEach(i => expected.Add(new IntVector2Comparable(i)));

            actual.Sort();
            expected.Sort();

            actualAdjastments.Clear();
            actual.ForEach(i => actualAdjastments.Add(i.Point));
            expectedAdjastments.Clear();
            expected.ForEach(i => expectedAdjastments.Add(i.Point));

            // Assert
            Assert.True(actualAdjastments.SequenceEqual(expectedAdjastments));
        }

        private struct IntVector2Comparable : IComparable<IntVector2Comparable>
        {
            public IntVector2 Point;

            public IntVector2Comparable(IntVector2 point)
            {
                Point = point;
            }

            private int Sum => Point.x + Point.y;

            public int CompareTo(IntVector2Comparable other)
            {
                if (Sum < other.Sum)
                    return -1;
                if (Sum > other.Sum)
                    return 1;
                if (Point.x < other.Point.x)
                    return -1;
                if (Point.x > other.Point.x)
                    return 1;

                return 0;
            }
        }
    }


}
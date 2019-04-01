using System.Collections;
using System.Collections.Generic;
using Scripts;

namespace UnitTests.StatesControllers.Hostile.WayHostile
{
    public class OstacleStateChangedTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new IntVector2(20, 20), GenerateInternimPoints(new IntVector2(20, 20)), 2, 1};
            yield return new object[] { new IntVector2(-3, -2000), GenerateInternimPoints(new IntVector2(-3, -2000)), 2, 1};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        List<IntVector2> GenerateInternimPoints(IntVector2 attackPoint)
        {
            var list = new List<IntVector2>();
            list.Add(new IntVector2(attackPoint.x + 22, attackPoint.y + 75));
            list.Add(new IntVector2(attackPoint.x + 12, attackPoint.y + 35));
            list.Add(new IntVector2(attackPoint.x + 1, attackPoint.y));
            return list;
        }
    }

}
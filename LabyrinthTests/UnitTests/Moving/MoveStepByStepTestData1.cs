using System.Collections;
using System.Collections.Generic;
using Scripts;

namespace Tests.Scripts.Units.Moving
{
    public class MoveStepByStepTestData1 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { GetList0_1_3_2(), 3, 3 };
            yield return new object[] { GetList0_0_5_5(), 5, 5 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public static List<IntVector2> GetList0_1_3_2()
        {
            List<IntVector2> list = new List<IntVector2>
            {
                new IntVector2(1, 1),
                new IntVector2(2, 1),
                new IntVector2(3, 2)
            };

            return list;
        }
        
        public static List<IntVector2> GetList0_0_5_5()
        {
            List<IntVector2> list = new List<IntVector2>
            {
                new IntVector2(1, 1),
                new IntVector2(2, 2),
                new IntVector2(3, 3),
                new IntVector2(4, 4),
                new IntVector2(5, 5)
            };

            return list;
        }
    }
    
    public class MoveStepByStepTestData2 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { GetList0_1_3_2(), new IntVector2(2, 1) };
            yield return new object[] { GetList0_0_5_5(), new IntVector2(5, 5) };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public static List<IntVector2> GetList0_1_3_2()
        {
            List<IntVector2> list = new List<IntVector2>
            {
                new IntVector2(1, 1),
                new IntVector2(2, 1),
                new IntVector2(3, 2)
            };

            return list;
        }
        
        public static List<IntVector2> GetList0_0_5_5()
        {
            List<IntVector2> list = new List<IntVector2>
            {
                new IntVector2(1, 1),
                new IntVector2(2, 2),
                new IntVector2(3, 3),
                new IntVector2(4, 4),
                new IntVector2(5, 5)
            };

            return list;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Scripts
{
    [Serializable]
    public struct IntVector2:IEquatable<IntVector2>
    { 
        public int x;
        public int y;

        public IntVector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public bool Equals(IntVector2 other)
        {
            return other.x == x && other.y == y;
        }
    }
}
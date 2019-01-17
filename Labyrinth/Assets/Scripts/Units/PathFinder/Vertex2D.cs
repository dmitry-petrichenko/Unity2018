using System;
using Scripts;

namespace Units.PathFinder
{
    public class Vertex2D
    {
        private Vertex2D _parent;
        private IntVector2 _index;
        private int _g, _h;

        public IntVector2 Index
        {
            get { return _index; }
        } 
        
        public int F
        {
            get { return G + H; }
        } 
        
        public int G
        {
            get { return _g; }
        } 
        
        public int H
        {
            get { return _h; }
        } 
        
        public Vertex2D Parent
        {
            get { return _parent; }
        } 
        
        public void Initialize(IntVector2 index, Vertex2D parent, IntVector2 destinationPoint)
        {
            _index = index;
            _parent = parent;

            if (_parent != null)
            {
                if (Index.x != parent.Index.x && Index.y != parent.Index.y)
                {
                    _g = parent.G + 14;
                }
                else
                {
                    _g = parent.G + 10;
                }
            
                _h = 10 * (Math.Abs(_index.x - destinationPoint.x) + Math.Abs(_index.y - destinationPoint.y));
            }
            else
            {
                _g = _h = 0;
            }
        }
    }
}
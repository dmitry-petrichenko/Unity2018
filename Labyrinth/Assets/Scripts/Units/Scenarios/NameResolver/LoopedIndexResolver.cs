namespace Units.Scenarios
{
    public class LoopedIndexResolver
    {
        private int _length;
        private int _currentIndex;
        
        public LoopedIndexResolver(int length)
        {
            _length = length;
            _currentIndex = _length;
        }

        public int ResolveNextIndex()
        {
            _currentIndex++;
            if (_currentIndex >= _length)
            {
                _currentIndex = 0;
            }
            
            return _currentIndex;
        }
    }
}
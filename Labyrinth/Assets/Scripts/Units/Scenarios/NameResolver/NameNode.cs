namespace Units.Scenarios
{
    public class NameNode
    {
        private string _name;
        private LoopedIndexResolver _loopedIndexResolver;
        
        public NameNode(string name)
        {
            _name = name;
            _loopedIndexResolver = new LoopedIndexResolver(1000000000);
        }

        public string GetNextName()
        {
            return _name + "_" + _loopedIndexResolver.ResolveNextIndex();
        }
        
    }
}
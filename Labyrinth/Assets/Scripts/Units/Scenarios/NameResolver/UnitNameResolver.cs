using System.Collections.Generic;
using UnityEditor;

namespace Units.Scenarios
{
    public class UnitNameResolver
    {
        private List<NameNode> _nodes;
        private LoopedIndexResolver _loopedIndexResolver;

        public UnitNameResolver()
        {
            Initialize();
        }

        private void Initialize()
        {
            _nodes = new List<NameNode>();
            _nodes.Add(new NameNode("alpha"));
            _nodes.Add(new NameNode("beta"));
            _nodes.Add(new NameNode("gamma"));
            _nodes.Add(new NameNode("delta"));
            _nodes.Add(new NameNode("epsilon"));
            _nodes.Add(new NameNode("eta"));
            _nodes.Add(new NameNode("theta"));
            _nodes.Add(new NameNode("iota"));
            
            _loopedIndexResolver = new LoopedIndexResolver(_nodes.Count);
        }

        public string ResolveNextName()
        {
            return _nodes[_loopedIndexResolver.ResolveNextIndex()].GetNextName();
        }
    }
}
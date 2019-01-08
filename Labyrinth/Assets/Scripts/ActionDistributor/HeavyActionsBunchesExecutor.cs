using System;
using System.Collections.Generic;
using UnityEditor;
using ZScripts.ActionDistributor;

namespace FeatureDistri
{
    public class HeavyActionsBunchesExecutor
    {
        public delegate HeavyActionsBunchesExecutor Factory();
        
        private List<HeavyActionsBunch> _heavyActionsBunches;
        private HeavyActionsBunch _currentBunch;
        private int _bunchSize;
        private IHeavyActionDistributor _heavyActionDistributor;
        
        public HeavyActionsBunchesExecutor(IHeavyActionDistributor heavyActionDistributor)
        {
            _heavyActionDistributor = heavyActionDistributor;
        }

        public void Initialize(int bunchSize)
        {
            _bunchSize = bunchSize;
            _heavyActionsBunches = new List<HeavyActionsBunch>();
            CreateNewBanch();
        }

        private void BunchCompleteHandler()
        {
            _currentBunch.BunchComplete -= BunchCompleteHandler;
            CreateNewBanch();
        }

        private void CreateNewBanch()
        {
            _currentBunch = new HeavyActionsBunch(_bunchSize);
            _currentBunch.BunchComplete += BunchCompleteHandler;
            _heavyActionsBunches.Add(_currentBunch);
        }

        public void AddAction(Action action)
        {
            _currentBunch.AddAction(action);
        }
        
        public void Execute()
        {
            int len = _heavyActionsBunches.Count;
            for (int i = 0; i < len; i++)
            {
                _heavyActionDistributor.InvokeDistributed(_heavyActionsBunches[i].Execute);
            }
        }
    }
}

using System.Collections.Generic;
using Scripts.ActionDistributor;
using Units.OneUnit;

namespace Scripts.Units
{
    public class UnitBehaviourGenerator
    {
        private List<IUnitAction> _actions;
        private IUnitAction _currentUnitAction;
        private IHeavyActionDistributor _heavyActionDistributor;
        
        public UnitBehaviourGenerator(IHeavyActionDistributor heavyActionDistributor)
        {
            _heavyActionDistributor = heavyActionDistributor;
        }

        public void Initialize(IOneUnitController oneUnitController, List<IUnitAction> actions)
        {
            _actions = actions;
        }

        public void Start()
        {
            Proceed();
        }

        public void Stop()
        {
            if (_currentUnitAction != null)
            {
                _currentUnitAction.Destroy();
                _currentUnitAction.OnComplete -= Proceed;
            } 
        }
        
        private void Proceed()
        {
            Stop();

            _currentUnitAction = GenerateUnitAction();
            _currentUnitAction.OnComplete += Proceed;
            _heavyActionDistributor.InvokeDistributed(_currentUnitAction.Start);
        }
        
        private IUnitAction GenerateUnitAction()
        {
            IUnitAction action;
            int a = (int)UnityEngine.Random.Range(0.0f, _actions.Count);
            action = _actions[a];
            
            return action;
        }
    }
}
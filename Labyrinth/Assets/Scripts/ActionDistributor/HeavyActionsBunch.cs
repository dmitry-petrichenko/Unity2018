using System;

namespace FeatureDistri
{
    public class HeavyActionsBunch
    {
        private Action[] _heavyActions;
        private int _bunchSize;
        private int _inflation;

        public event Action BunchComplete;
        public HeavyActionsBunch(int bunchSize)
        {
            _bunchSize = bunchSize;
            _heavyActions = new Action[_bunchSize];
        }

        public void AddAction(Action action)
        {
            _heavyActions[_inflation] = action;

            _inflation+=1;

            if (_inflation>= _bunchSize)
            {
                BunchComplete();
            }
        }

        public void Execute()
        {
            foreach (var action in _heavyActions)
            {
                if (action != null)
                {
                    action.Invoke();
                }
            }
        }
    }
}

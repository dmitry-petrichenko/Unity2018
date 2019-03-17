using System;
using System.Collections.Generic;
using Units.OneUnit.StatesControllers;
using Units.OneUnit.StatesControllers.Base;

namespace Scripts.Units.Events
{
    public class DeadNotifier : IDeadNotifier
    {
        private readonly IBaseActionController _baseActionController;
        
        private Dictionary<IPositional, Action> _units;
        
        public DeadNotifier(IBaseActionController baseActionController)
        {
            _baseActionController = baseActionController;
            _units = new Dictionary<IPositional, Action>();
        }

        public void AddDeadListener(IPositional unit, Action listener)
        {
            _units.Add(unit, listener);
        }

        public void RemoveDeadListener(IPositional unit, Action listener)
        {
            if (_units.ContainsKey(unit))
                _units.Remove(unit);
        }

        public void NotifyDead()
        {
            var i = 1;
            List<IntVector2> rangePoints;
            while (i < 5)
            {
                rangePoints = _baseActionController.Position.GetAdjacentPoints(null, i);
                NotifyUnitsInRange(rangePoints);
                i++;
            }
        }

        private void NotifyUnitsInRange(List<IntVector2> rangePoints)
        {
            foreach (var point in rangePoints)
            {
                foreach (var keyValuePair in _units)
                {
                    if (keyValuePair.Key.Position.Equals(point))
                    {
                        keyValuePair.Value.Invoke();
                    }
                }
            }
        }
    }
}
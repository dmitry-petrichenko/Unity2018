using System;
using UnityEngine;

namespace Units.Scenarios
{
    public class UnitsCountNotifier
    {
        public int UnitsCount => _unitsCount;

        public event Action UnitsCountDecreased;

        private int _unitsCount;
        
        public UnitsCountNotifier()
        {
            _unitsCount = 0;
        }

        public void Increase()
        {
            _unitsCount++;
            Debug.Log("Increase" + _unitsCount);
        }

        public void Decrease()
        {
            _unitsCount--;
            Debug.Log("Decrease" + _unitsCount);
            UnitsCountDecreased?.Invoke();
        }
    }
}
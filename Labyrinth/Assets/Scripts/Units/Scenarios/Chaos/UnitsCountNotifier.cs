using System;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Scenarios
{
    public class UnitsCountNotifier
    {
        public int UnitsCount => _unitNames.Count;

        public event Action UnitsCountDecreased;

        private HashSet<String> _unitNames;
        
        public UnitsCountNotifier()
        {
            _unitNames = new HashSet<string>();
        }

        public void Increase(string name)
        {
            Debug.Log("Increase " + name);
            _unitNames.Add(name);
        }

        public void Decrease(string name)
        {
            Debug.Log("Decrease " + name);
            if(_unitNames.Contains(name)){}
            {
                _unitNames.Remove(name);
                UnitsCountDecreased?.Invoke();
            }
        }
    }
}
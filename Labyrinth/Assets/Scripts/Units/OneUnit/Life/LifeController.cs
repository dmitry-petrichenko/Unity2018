using System;
using Scripts.Units.Settings;
using Units.OneUnit.StatesControllers.Base;
using Units.OneUnit.StatesControllers.Base.Settings;
using UnityEngine;

namespace Units.OneUnit
{
    public class LifeController : Disposable, ILifeController
    {
        public event Action HealthEnded;
        
        private readonly IBaseActionController _baseActionController;
        
        private int _currentHealth = 0;
        private int _totalHealth = 0;

        public LifeController(
            IUnitSettings unitSettings,
            IBaseActionController baseActionController)
        {
            _currentHealth = unitSettings.TotalHealth;
            _totalHealth = unitSettings.TotalHealth;

            _baseActionController = baseActionController;
        }

        public void TakeDamage(int value)
        {
            _currentHealth -= value;
            _baseActionController.SetHealthBarValue((float)_currentHealth / (float)_totalHealth);
            if (_currentHealth <= 0)
            {
                HealthEnded?.Invoke();
                _baseActionController.Die();
            }
        }
    }
}
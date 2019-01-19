using Scripts;
using Units.OneUnit;
using Units.OneUnit.Base;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Units.Player
{
    public class PlayerAttackController : IAttackController
    {
        private IBaseActionController _baseActionController;
        
        public PlayerAttackController(IBaseActionController baseActionController)
        {
            _baseActionController = baseActionController;
        }

        public void Initialize(IOneUnitController unitController)
        {
            
        }

        public void Cancel()
        {
            
        }

        public void Attack(IntVector2 position)
        {
        }

        public void TakeDamage(int value)
        {
            _baseActionController.TakeDamage(value);
        }
    }
}
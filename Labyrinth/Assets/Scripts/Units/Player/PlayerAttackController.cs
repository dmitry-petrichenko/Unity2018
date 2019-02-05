using Scripts;
using Units.OneUnit;
using Units.OneUnit.Base;

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
            _baseActionController.Attack(position);
        }

        public void TakeDamage(int value)
        {
            _baseActionController.TakeDamage(value);
        }

        public void Dispose()
        {
        }
    }
}
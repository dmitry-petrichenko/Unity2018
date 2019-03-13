using Scripts;
using Units.OneUnit;
using Units.OneUnit.StatesControllers.Base;
using Units.OneUnit.StatesControllers.Hostile;

namespace Units.Player
{
    public class PlayerHostileController : Disposable, IHostileController
    {
        private IBaseActionController _baseActionController;
        
        public PlayerHostileController(IBaseActionController baseActionController)
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

        public void Activate()
        {
            throw new System.NotImplementedException();
        }

        public void Deactivate()
        {
            throw new System.NotImplementedException();
        }
    }
}
using Scripts;
using Units.OneUnit;
using Units.OneUnit.StatesControllers.Hostile;

namespace Units.Enemy
{
    public class EnemyHostileController : Disposable, IHostileController
    {
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
            
        }

        protected override void DisposeInternal()
        {
            base.DisposeInternal();
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
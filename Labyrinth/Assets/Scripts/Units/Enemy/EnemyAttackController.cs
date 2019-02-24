using Scripts;
using Units.OneUnit;

namespace Units.Enemy
{
    public class EnemyAttackController : Disposable, IAttackController
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
    }
}
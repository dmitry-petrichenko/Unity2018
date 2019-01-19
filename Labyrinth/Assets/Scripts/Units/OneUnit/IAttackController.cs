using Scripts;

namespace Units.OneUnit
{
    public interface IAttackController
    {
        void Initialize(IOneUnitController unitController);
        void Cancel();
        void Attack(IntVector2 position);
        void TakeDamage(int value);
    }
}
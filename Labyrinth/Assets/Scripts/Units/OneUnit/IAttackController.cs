using Scripts;

namespace Units.OneUnit
{
    public interface IAttackController
    {
        void Cancel();
        void Attack(IntVector2 position);
        void TakeDamage(int value);
    }
}
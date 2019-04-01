using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public interface IWayHostileControllerParameters
    {
        IntVector2 AttackPosition { get; }
        IntVector2 UnitPosition { get; }
    }
}
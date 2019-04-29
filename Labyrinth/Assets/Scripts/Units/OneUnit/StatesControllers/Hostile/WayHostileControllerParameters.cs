using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class WayHostileControllerParameters : IWayHostileControllerParameters
    {
        public IntVector2 AttackPosition { get; }
        public IntVector2 UnitPosition { get; }
    }
}
using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public interface IFreePossitionsMap
    {
        bool IsFreePosition(IntVector2 position);
    }
}
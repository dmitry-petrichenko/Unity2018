using Scripts;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public interface IFreePointToGoResolver
    {
        IntVector2 GetFreePoint(IntVector2 position);
    }
}
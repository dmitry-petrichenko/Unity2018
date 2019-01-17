using Scripts;

namespace Units.OneUnit
{
    public interface IMovingRandomizer
    {
        IntVector2 GetRandomPoint(IntVector2 unitPosition);
    }
}
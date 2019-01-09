namespace Scripts.Units
{
    public interface IMovingRandomizer
    {
        IntVector2 GetRandomPoint(IntVector2 unitPosition);
    }
}
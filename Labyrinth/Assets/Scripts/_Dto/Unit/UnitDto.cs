namespace _Dto.Unit
{
    public interface IUnitDto
    {
        FloatVector2 Position { get; }
        UnitAnimationType AnimationType { get; }
        UnitType Type { get; }
        float RotationValue { get; }
    }
    
    public class UnitDto : IUnitDto
    {
        public FloatVector2 Position { get; }
        public UnitAnimationType AnimationType { get; }
        public UnitType Type { get; }
        public float RotationValue { get; }
    }
}
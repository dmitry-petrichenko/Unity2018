using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public interface IUnitStateExternal
    {
        IntVector2 WaitPosition { get; }
        IOneUnitController AttackTarget { get; set; }
    }
}
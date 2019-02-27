using Units.OneUnit;

namespace Scripts.Units.StateInfo
{
    public interface IUnitStateExternal
    {
        IntVector2 WaitPosition { get; set; }
        IOneUnitController AttackTarget { get; set; }
    }
}
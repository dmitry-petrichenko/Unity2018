using Scripts;

namespace Units.OneUnit.Info
{
    public interface IUnitInfoExternal
    {
        IOneUnitController AttackTarget { get; }
        IntVector2 WaitPosition { get; }
    }
}
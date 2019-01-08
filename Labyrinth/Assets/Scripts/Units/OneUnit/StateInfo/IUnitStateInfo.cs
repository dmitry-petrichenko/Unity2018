namespace ZScripts.Units.StateInfo
{
    public interface IUnitStateInfo
    {
        IntVector2 WaitPosition { get; set; }
        bool IsAttacking { get; set; }
        IOneUnitController AttackTarget { get; set; }
    }
}
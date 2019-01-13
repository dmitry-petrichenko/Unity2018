using Units.ExternalAPI;

namespace Scripts.Units.Player
{
    public interface IPlayerController : IPlayer
    {
        IOneUnitController o { get; }
    }
}
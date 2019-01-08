using System;

namespace ZScripts.Units.Player
{
    public interface IPlayerController  : IOneUnitController, IOneUnitServicesContainer
    {
        event Action<IntVector2> PositionChanged;
    }
}
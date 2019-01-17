using System;
using Scripts;

namespace Units.OneUnit.Base
{
    public interface INoWayEventRouter
    {
        event Action<IntVector2> NoWayToAttackPointHandler;
        event Action<IntVector2> NoWayToPointHandler;
    }
}
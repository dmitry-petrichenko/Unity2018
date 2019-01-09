﻿using System;

namespace Scripts.Units
{
    public interface INoWayEventRouter
    {
        event Action<IntVector2> NoWayToAttackPointHandler;
        event Action<IntVector2> NoWayToPointHandler;
    }
}
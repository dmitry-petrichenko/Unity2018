﻿using System.Collections.Generic;
using Scripts;

namespace Units.OneUnit.Base
{
    public interface IMoveStepByStepController
    {
        void Wait();
        void Wait(IntVector2 position);
        void MoveTo(List<IntVector2> path);
        void Cancel();
        void SetOnPosition(IntVector2 position);
        
        IntVector2 Position { get; }
        bool IsMoving { get; }
        IntVector2 Destination { get; set; }
    }
}
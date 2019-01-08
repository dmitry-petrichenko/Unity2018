using System;
using UnityEngine;
using ZScripts.Units.Settings;

namespace ZScripts.Units
{
    public interface IOneUnitMotionController
    {
        void MoveToPosition(IntVector2 position);
        void SetOnPosition(IntVector2 position);
        IntVector2 Position { get; }
        void Initialize(IUnitSettings unitSettings);
        bool IsMoving { get; }

        event Action MoveStart;
        event Action MoveComplete;
    }
}
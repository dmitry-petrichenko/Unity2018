using System;
using UnityEngine;
using Scripts.Units.Settings;

namespace Scripts.Units
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
using System;
using Scripts;

namespace Units.OneUnit.StatesControllers.Base.GameObject.Motion
{
    public interface IOneUnitMotionController
    {
        void MoveToPosition(IntVector2 position);
        void SetOnPosition(IntVector2 position);
        IntVector2 Position { get; }
        bool IsMoving { get; }

        event Action MoveStart;
        event Action MoveComplete;
    }
}
using System;
using Scripts;
using Units.OneUnit.Base.GameObject.Motion;

namespace Tests.Scripts.Units.UnitGameObject
{
    public class MotionControllerMock : IOneUnitMotionController
    {
        public void MoveToPosition(IntVector2 position)
        {
            MoveStart?.Invoke();
            MoveComplete?.Invoke();
        }

        public void SetOnPosition(IntVector2 position)
        {
        }

        public IntVector2 Position { get; }
        public bool IsMoving { get; }
        public event Action MoveStart;
        public event Action MoveComplete;
    }
}
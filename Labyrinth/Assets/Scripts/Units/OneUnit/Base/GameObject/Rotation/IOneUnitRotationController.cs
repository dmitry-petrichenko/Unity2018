using System;
using Scripts;

namespace Units.OneUnit.Base.GameObject.Rotation
{
    public interface IOneUnitRotationController : IDisposable
    {
        void Rotate(IntVector2 point1, IntVector2 point2);
    }
}
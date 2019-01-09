using UnityEngine;
using Scripts.Units.Settings;

namespace Scripts.Units.Rotation
{
    public interface IOneUnitRotationController
    {
        void Initialize(IUnitSettings unitSettings);
        void Rotate(IntVector2 point1, IntVector2 point2);
    }
}
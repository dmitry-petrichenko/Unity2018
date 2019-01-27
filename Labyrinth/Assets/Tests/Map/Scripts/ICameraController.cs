using Scripts;

namespace Tests.Map.Scripts
{
    public interface ICameraController
    {
        void UpdateCurrentPosition(IntVector2 position);
        void Follow<T>(T gameObject);
    }
}
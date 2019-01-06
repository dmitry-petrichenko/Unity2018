namespace ZScripts
{
    public interface ICameraController
    {
        void UpdateCurrentPosition(IntVector2 position);
        void Follow<T>(T gameObject);
    }
}
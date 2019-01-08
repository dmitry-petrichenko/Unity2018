namespace ZScripts.Units
{
    public interface IPeacefulBehaviour
    {
        void Initialize(IOneUnitController oneUnitController);
        void Start();
        void Stop();
    }
}
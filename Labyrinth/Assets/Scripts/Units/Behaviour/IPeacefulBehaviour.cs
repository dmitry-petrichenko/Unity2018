using Units.OneUnit;

namespace Scripts.Units
{
    public interface IPeacefulBehaviour
    {
        void Initialize(IOneUnitController oneUnitController);
        void Start();
        void Stop();
    }
}
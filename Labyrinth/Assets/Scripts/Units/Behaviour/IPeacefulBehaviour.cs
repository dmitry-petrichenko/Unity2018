using System;
using Units.OneUnit;

namespace Scripts.Units
{
    public interface IPeacefulBehaviour : IDisposable
    {
        void Initialize(IOneUnitController oneUnitController);
        void Start();
        void Stop();
    }
}
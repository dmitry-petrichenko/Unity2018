using System;
using Units.OneUnit;

namespace Scripts.Units
{
    public interface IUnitAction : IDisposable
    {
        void Start();
        void Stop();
        void Destroy();
        void Initialize(IOneUnitController oneUnitController);
        
        event Action OnComplete;
    }
}
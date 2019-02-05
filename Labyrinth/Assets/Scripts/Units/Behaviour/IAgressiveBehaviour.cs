using System;
using Units.OneUnit;

namespace Scripts.Units
{
    public interface IAgressiveBehaviour : IDisposable
    {
        event Action Complete;
        
        void Cancel();
        void Start(IOneUnitController target);
        void Initialize(IOneUnitController oneUnitController);
    }
}
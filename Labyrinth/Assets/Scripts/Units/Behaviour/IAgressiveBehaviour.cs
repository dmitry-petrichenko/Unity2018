using System;

namespace ZScripts.Units
{
    public interface IAgressiveBehaviour
    {
        event Action Complete;
        
        void Cancel();
        void Start(IOneUnitController target);
        void Initialize(IOneUnitController oneUnitController);
    }
}
using System;

namespace ZScripts.Units
{
    public interface IUnitAction
    {
        void Start();
        void Stop();
        void Destroy();
        void Initialize(IOneUnitController oneUnitController);
        
        event Action OnComplete;
    }
}
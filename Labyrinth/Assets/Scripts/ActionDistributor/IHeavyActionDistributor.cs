using System;

namespace Scripts.ActionDistributor
{
    public interface IHeavyActionDistributor
    {
        void InvokeDistributed(Action action);
        bool HasActions();
    }
}
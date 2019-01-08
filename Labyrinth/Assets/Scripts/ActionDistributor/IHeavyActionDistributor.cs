using System;

namespace ZScripts.ActionDistributor
{
    public interface IHeavyActionDistributor
    {
        void InvokeDistributed(Action action);
        bool HasActions();
    }
}
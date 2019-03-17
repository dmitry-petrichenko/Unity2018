using System;
using Units.OneUnit.StatesControllers;

namespace Scripts.Units.Events
{
    public interface IDeadNotifier
    {
        void AddDeadListener(IPositional unit, Action listener);
        void RemoveDeadListener(IPositional unit, Action listener);
        void NotifyDead();
    }
}
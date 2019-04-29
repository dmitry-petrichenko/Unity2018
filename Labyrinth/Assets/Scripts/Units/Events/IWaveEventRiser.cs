using System;
using Units.OneUnit;
using Units.OneUnit.StatesControllers;

namespace Scripts.Units.Events
{
    public interface IWaveEventRiser
    {
        void AddPositionChangedHandler(Action handler, IPositional subscriber);
        void RemovePositionChangedHandler(Action handler, IPositional subscriber);
    }
}
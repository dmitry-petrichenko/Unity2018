using System;
using Units.OneUnit;

namespace Scripts.Units.Events
{
    public interface IWaveEventRiser
    {
        void AddPositionChangedHandler(Action handler, IOneUnitController subscriber);
        void RemovePositionChangedHandler(Action handler, IOneUnitController subscriber);
    }
}
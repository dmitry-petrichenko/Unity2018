using System;

namespace Units.OneUnit.StatesControllers.Base.GameObject.Health
{
    public interface IOneUnitHealthController : IDisposable
    {
        void SetHealthBarValue(float value);
        void SetHealthBarVisible(bool value);
    }
}
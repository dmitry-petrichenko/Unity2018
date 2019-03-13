using System;
using Scripts;

namespace Units.OneUnit.StatesControllers.Base
{
    public interface IApplyDamageController : IDisposable
    {
        void ApplyDamageOnPosition(IntVector2 position);
    }
}
using System;
using Scripts;

namespace Units.OneUnit.Base
{
    public interface IApplyDamageController : IDisposable
    {
        void ApplyDamageOnPosition(IntVector2 position);
    }
}
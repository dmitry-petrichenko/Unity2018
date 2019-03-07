using Scripts.Units.Events;
using Units.OneUnit.Info;

namespace Units.OneUnit
{
    public interface IOneUnitController : IOneUnitApi
    {
        IUnitEvents UnitEvents { get; }
        IUnitInfoExternal DynamicInfo { get; }
    }
}
using Scripts._Client.Units.SingleUnit.Player;
using _Dto.Unit;

namespace Scripts._Client.Units.SingleUnit.Resolver
{
    public interface IUnitViewObjectResolver
    {
        IUnityUnitViewObject ResolveUnitViewObject(IUnitDto dto);
    }
    
    public class UnitViewObjectResolver : IUnitViewObjectResolver
    {
        public IUnityUnitViewObject ResolveUnitViewObject(IUnitDto dto)
        {
            switch (dto.Type)
            {
                case UnitType.Player:
                    return new PlayerViewObject();
                default:
                    return null;   
            }
        }
    }
}
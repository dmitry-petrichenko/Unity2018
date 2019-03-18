using Scripts;
using Units.OneUnit.Info;
using Units.OneUnit.StatesControllers.Base;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class FreePointToGoResolver : IFreePointToGoResolver
    {
        private readonly IBaseActionController _baseActionController;
        private readonly IUnitInfoExternal _unitInfo;
        
        public FreePointToGoResolver(
            IBaseActionController baseActionController,
            IUnitInfoExternal unitInfo)
        {
            _baseActionController = baseActionController;
            _unitInfo = unitInfo;
        }

        public IntVector2 GetFreePoint()
        {
            
        }
    }
}
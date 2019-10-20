using Scripts._Client.Units.SingleUnit.Api;
using _Dto.Unit;

namespace Scripts._Client.Units.SingleUnit.ViewControllers
{
    public interface IViewUnitRotationController : IUnitViewComponent {}
    
    public class ViewUnitRotationController : IViewUnitRotationController
    {
        public delegate ViewUnitRotationController Factory(IRotationApi rotationApi);
        
        private readonly IRotationApi _rotationApi;

        public ViewUnitRotationController(IRotationApi rotationApi)
        {
            _rotationApi = rotationApi;
        }

        public void Update(IUnitDto dto)
        {
            _rotationApi.SetRotation(dto.RotationValue);
        }
    }
}
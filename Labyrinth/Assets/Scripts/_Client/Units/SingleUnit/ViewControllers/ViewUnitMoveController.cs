using Scripts._Client.Units.SingleUnit.Api;
using _Dto.Unit;

namespace Scripts._Client.Units.SingleUnit.ViewControllers
{
    public interface IViewUnitMoveController : IUnitViewComponent {}
    
    public class ViewUnitMoveController : IViewUnitMoveController
    {
        public delegate ViewUnitMoveController Factory(IMotionApi motionApi);

        private readonly IMotionApi _motionApi;

        public ViewUnitMoveController(IMotionApi motionApi)
        {
            _motionApi = motionApi;
        }

        public void Update(IUnitDto dto)
        {
            _motionApi.SetOnPosition(dto.Position);
        }
    }
}
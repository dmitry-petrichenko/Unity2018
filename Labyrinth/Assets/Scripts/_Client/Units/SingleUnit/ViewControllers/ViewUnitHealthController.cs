using Scripts._Client.Units.SingleUnit.Api;
using _Dto.Unit;

namespace Scripts._Client.Units.SingleUnit.ViewControllers
{
    public interface IViewUnitHealthController : IUnitViewComponent {}
    
    public class ViewUnitHealthController : IViewUnitHealthController
    {
        public delegate ViewUnitHealthController Factory(IHealthBarApi healthBarApi);

        private readonly IHealthBarApi _healthBarApi;

        public ViewUnitHealthController(IHealthBarApi healthBarApi)
        {
            _healthBarApi = healthBarApi;
        }

        public void Update(IUnitDto dto)
        {
        }
    }
}
using Scripts._Client.Units.SingleUnit.Api;
using Units.OneUnit.StatesControllers.Base.GameObject.Animation;
using _Dto.Unit;

namespace Scripts._Client.Units.SingleUnit.ViewControllers
{
    public interface IViewUnitAnimationController : IUnitViewComponent {}
    
    public class ViewUnitAnimationController : IViewUnitAnimationController
    {
        public delegate ViewUnitAnimationController Factory(IAnimationApi animationApi);
        
        private readonly IUnitScript _script;
        
        public ViewUnitAnimationController(IAnimationApi animationApi)
        {
            _script = animationApi.GetAnimationScript();
        }

        public void Update(IUnitDto dto)
        {
            switch (dto.AnimationType)
            {
                case UnitAnimationType.Walk:
                    _script.PlayWalkAnimation();
                    break;
            }
        }
    }
}
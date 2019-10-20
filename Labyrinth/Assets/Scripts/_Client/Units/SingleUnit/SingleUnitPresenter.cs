using System.Collections.Generic;
using Scripts._Client.Units.SingleUnit.ViewControllers;
using _Dto.Unit;

namespace Scripts._Client.Units.SingleUnit
{
    public interface ISingleUnitPresenter
    {
        void Update(IUnitDto unitDto);
    }

    public class SingleUnitPresenter : ISingleUnitPresenter
    {
        private readonly ICertainViewUnitObject _certainViewUnitObject;
        
        private readonly IViewUnitAnimationController _viewUnitAnimationController;
        private readonly IViewUnitHealthController _viewUnitHealthController;
        private readonly IViewUnitRotationController _viewUnitRotationController;
        private readonly IViewUnitMoveController _viewUnitMoveController;

        private readonly List<IUnitViewComponent> _components;
        
        public SingleUnitPresenter(
            IUnitDto unitDto,
            CertainViewUnitObject.Factory viewUnitObjectFactory,
            ViewUnitMoveController.Factory viewUnitMoveControllerFactory,
            ViewUnitRotationController.Factory viewUnitRotationControllerFactory,
            ViewUnitHealthController.Factory viewUnitHealthControllerFactory,
            ViewUnitAnimationController.Factory viewUnitAnimationControllerFactory
            )
        {
            _components = new List<IUnitViewComponent>();
            _certainViewUnitObject = viewUnitObjectFactory.Invoke(unitDto);
            _viewUnitAnimationController = viewUnitAnimationControllerFactory.Invoke(_certainViewUnitObject);
            _components.Add(_viewUnitAnimationController);
            _viewUnitHealthController = viewUnitHealthControllerFactory.Invoke(_certainViewUnitObject);
            _components.Add(_viewUnitHealthController);
            _viewUnitRotationController = viewUnitRotationControllerFactory.Invoke(_certainViewUnitObject);
            _components.Add(_viewUnitRotationController);
            _viewUnitMoveController = viewUnitMoveControllerFactory.Invoke(_certainViewUnitObject);
            _components.Add(_viewUnitMoveController);
        }


        public void Update(IUnitDto unitDto)
        {
            foreach (var component in _components)
            {
                component.Update(unitDto);
            }
        }
    }
}
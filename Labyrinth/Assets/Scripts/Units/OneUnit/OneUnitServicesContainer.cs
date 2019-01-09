using Units;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public class OneUnitServicesContainer : IOneUnitServicesContainer
    {
        private IOneUnitMotionController _oneUnitMotionController;
        private IOneUnitAnimationController _oneUnitAnimationController;
        private IOneUnitRotationController _oneUnitRotationController;
        private IUnitStateInfo _unitStateInfo;

        public OneUnitServicesContainer(IOneUnitServices services)
        {
            _oneUnitRotationController = services.OneUnitRotationController;
            _oneUnitMotionController = services.OneUnitMotionController;
            _oneUnitAnimationController = services.OneUnitAnimationController;
            _unitStateInfo = services.UnitStateInfo;
        }

        public void Initialize()
        {
            _oneUnitMotionController.Initialize(UnitSettings);
            _oneUnitAnimationController.Initialize(UnitSettings);
            _oneUnitRotationController.Initialize(UnitSettings);

            MotionController = _oneUnitMotionController;
            AnimationController = _oneUnitAnimationController;
            RotationController = _oneUnitRotationController;
            UnitStateInfo = _unitStateInfo;
        }
        
        public IOneUnitMotionController MotionController { get; set; }
        public IOneUnitAnimationController AnimationController { get; set; }
        public IOneUnitRotationController RotationController { get; set; }
        public IUnitSettings UnitSettings { get; set; }
        public IUnitStateInfo UnitStateInfo { get; set; }
    }
}
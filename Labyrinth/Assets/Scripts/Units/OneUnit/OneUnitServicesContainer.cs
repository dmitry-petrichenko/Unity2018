using Units;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public class OneUnitServicesContainer : IOneUnitServicesContainer
    {
        private IUnitStateInfo _unitStateInfo;
        
        public OneUnitServicesContainer(
            IOneUnitMotionController oneUnitMotionController,
            IOneUnitAnimationController oneUnitAnimationController,
            IOneUnitRotationController oneUnitRotationController,
            IUnitStateInfo unitStateInfo,
            IUnitSettings unitSettings
            )
        {
            RotationController = oneUnitRotationController;
            MotionController = oneUnitMotionController;
            AnimationController = oneUnitAnimationController;
            UnitStateInfo = unitStateInfo;
            UnitSettings = unitSettings;
        }

        public void Initialize(string settingsPath)
        {
            UnitSettings.Initialize(settingsPath);
            MotionController.Initialize(UnitSettings);
            AnimationController.Initialize(UnitSettings);
            RotationController.Initialize(UnitSettings);
        }
        
        public IOneUnitMotionController MotionController { get; }
        public IOneUnitAnimationController AnimationController { get; }
        public IOneUnitRotationController RotationController { get; }
        public IUnitSettings UnitSettings { get; }
        public IUnitStateInfo UnitStateInfo { get; }
    }
}
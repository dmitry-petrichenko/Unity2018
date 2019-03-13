using Scripts;
using Units.OneUnit.StatesControllers.Base.GameObject;
using Units.OneUnit.StatesControllers.Base.GameObject.Health;
using Units.OneUnit.StatesControllers.Base.GameObject.Motion;
using Units.OneUnit.StatesControllers.Base.GameObject.Rotation;
using Xunit;

namespace Tests.Scripts.Units.UnitGameObject
{
    public class UnitGameObjectTests
    {
        private IUnitGameObjectController _unitGameObjectController;
        
        private IOneUnitRotationController _rotationControllerMock;
        private AnimationControllerMock _unitAnimationController;
        private IOneUnitMotionController _unitMotionController;
        private IOneUnitHealthController _unitHealthController;

        public UnitGameObjectTests()
        {
            _rotationControllerMock = new RotationControllerMock();
            _unitAnimationController = new AnimationControllerMock();
            _unitMotionController = new MotionControllerMock();
            _unitHealthController = new HealthControllerMock();
            
            _unitGameObjectController = new UnitGameObjectController(
                _rotationControllerMock,
                _unitAnimationController,
                _unitMotionController,
                _unitHealthController);
        }
        
        [Fact]
        //[tested method]_[expected input]_[expected behavior]
        public void MoveTo_AnyPosition_ShouldCallWalkAnimation()
        {
            // Arrange
            bool playWalkAnimationCalled = false;
            bool moveTileCompleteCalled = false;
            _unitAnimationController.PlayWalkAnimationEvent += () => playWalkAnimationCalled = true;
            _unitGameObjectController.MoveTileComplete += () => moveTileCompleteCalled = true;

            // Act
            _unitGameObjectController.MoveTo(new IntVector2());

            // Assert 
            Assert.True(playWalkAnimationCalled);
            Assert.True(moveTileCompleteCalled);
        }
    }
}
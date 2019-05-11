using Scripts;
using Scripts.Units.Events;
using Telerik.JustMock;
using Units.OneUnit.StatesControllers.Base;
using Units.OneUnit.StatesControllers.Hostile;
using Xunit;

namespace Tests.Scripts.Units.Overtaker
{
    public class TargetOvertakerTests
    {
        private readonly IWayHostileController _wayHostileController;
        private readonly IBaseActionController _baseActionController;
        
        private TargetOvertaker _targetOvertaker;
        
        public TargetOvertakerTests()
        {
            _wayHostileController = Mock.Create<IWayHostileController>();
            _baseActionController = Mock.Create<IBaseActionController>();
        }

        [Fact]
        public void Overtake_OneUnit_ShouldRaiseCompleteIfUnitInRange()
        {
            // Arrange
            bool actualComplete = false;
            var unitEvents = Mock.Create<IUnitEvents>();
            Mock.Arrange(() => _baseActionController.Position).Returns(new IntVector2(0, 1));
            OneUnitControllerMock oneUnitController = new OneUnitControllerMock(unitEvents);
            oneUnitController.SetOnPosition(new IntVector2(0,0));
            _targetOvertaker = new TargetOvertaker(_wayHostileController, _baseActionController);

            // Act
            _targetOvertaker.OvertakeComplete += () => actualComplete = true;
            _targetOvertaker.Overtake(oneUnitController);

            // Assert 
            Assert.True(actualComplete);
        }
    }
}
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Units.Events;
//using Telerik.JustMock;
using Units.OneUnit.Base;
using Units.OneUnit.Base.GameObject;
using Xunit;

namespace Tests.Scripts.Units
{
    public class DeathControllerTests
    {
        private IUnitGameObjectController _unitGameObjectController;
        private IEventDispatcher _eventDispatcher;
        private IDeathController _deathController;
        
        public DeathControllerTests()
        {
            //_unitGameObjectController = Mock.Create<IUnitGameObjectController>();
            _eventDispatcher = new EventDispatcher();
            _deathController = new DeathController(_unitGameObjectController, _eventDispatcher);
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(true, true)]
        public void HealthEndedHandler_WorkCorrect(bool expectedResult, bool dispatchEvent)
        {

            // Arrange
            bool dieCalled = false;
            //Mock.Arrange(() => _unitGameObjectController.Die()).DoInstead(() => { dieCalled = true; });
            
            // Act
            if (dispatchEvent) _eventDispatcher.DispatchEvent(UnitEventsTypes.HEALTH_ENDED);

            // Assert 
            Assert.Equal(expectedResult, dieCalled);
        }
    }
}
namespace Tests.Scripts.Units
{/*
    public class DeathControllerTests
    {
        private IUnitGameObjectController _unitGameObjectController;
        private IEventDispatcher _eventDispatcher;
        private IDeathController _deathController;
        
        public DeathControllerTests()
        {
            _unitGameObjectController = Mock.Create<IUnitGameObjectController>();
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
            Mock.Arrange(() => _unitGameObjectController.Die()).DoInstead(() => { dieCalled = true; });
            
            // Act
            if (dispatchEvent) _eventDispatcher.DispatchEvent(UnitEventsTypes.HEALTH_ENDED);

            // Assert 
            Assert.Equal(expectedResult, dieCalled);
        }
    }*/
}
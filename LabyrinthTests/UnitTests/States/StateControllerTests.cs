using Telerik.JustMock;
using Units.OneUnit.State;
using Units.OneUnit.State.States;
using Xunit;

namespace Tests.Scripts.Units.States
{
    public class StateControllerTests
    {
        private IPlacidState _placidState;
        private IHostileState _hostileState;
        private IDeadState _deadState;
        private StateController _stateController;
        private IStateControllerExternal _stateControllerExternal;
        private IStateControllerInternal _stateControllerInternal;

        public StateControllerTests()
        {
            _placidState = Mock.Create<IPlacidState>();
            _hostileState = Mock.Create<IHostileState>();
            _deadState = Mock.Create<IDeadState>();
        }
        
        [Fact]
        public void SetState_AnyState_ShouldActivateDeactivate()
        {
            // Arrange
            bool deactivateCalled = false;
            bool activateCalled = false;
            InitializeStateController();
            Mock.Arrange(() => _placidState.Deactivate()).DoInstead(() => { deactivateCalled = true; });
            Mock.Arrange(() => _hostileState.Activate()).DoInstead(() => { activateCalled = true; });

            // Act
            _stateControllerInternal.SetState(_stateController.GetHostileState());

            // Assert 
            Assert.True(deactivateCalled);
            Assert.True(activateCalled);
        }
        
        [Fact]
        public void Dispose_ShouldSetCurrentStateNull()
        {
            // Arrange
            //InitializeStateController();

            // Act
            //_stateController.Dispose();

            // Assert 
            //Assert.True(_stateController.CurrentState == null);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetState_DeadState_ShouldSetCorrectState(bool set)
        {
            // Arrange
            InitializeStateController();

            // Act
            if (set)
                _stateControllerInternal.SetState(_stateController.GetDeadState());
            else
                _stateControllerInternal.SetState(_stateController.GetHostileState());

            // Assert 
            if (set)
                Assert.True(_stateControllerExternal.CurrentState == _deadState);
            else
                Assert.False(_stateControllerExternal.CurrentState == _deadState);
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetState_PlacidState_ShouldSetCorrectState(bool set)
        {
            // Arrange
            InitializeStateController();

            // Act
            if (set)
                _stateControllerInternal.SetState(_stateController.GetPlacidState());
            else
                _stateControllerInternal.SetState(_stateController.GetHostileState());

            // Assert 
            if (set)
                Assert.True(_stateControllerExternal.CurrentState == _placidState);
            else
                Assert.False(_stateControllerExternal.CurrentState == _placidState);
        }
        
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetState_HostileState_ShouldSetCorrectState(bool set)
        {
            // Arrange
            InitializeStateController();

            // Act
            if (set)
                _stateControllerInternal.SetState(_stateController.GetHostileState());
            else
                _stateControllerInternal.SetState(_stateController.GetPlacidState());

            // Assert 
            if (set)
                Assert.True(_stateControllerExternal.CurrentState == _hostileState);
            else
                Assert.False(_stateControllerExternal.CurrentState == _hostileState);
        }

        private void InitializeStateController()
        {
            _stateController = new StateController(
                _placidState,
                _hostileState,
                _deadState);

            _stateControllerExternal = _stateController;
            _stateControllerInternal = _stateController;
        }
    }
}
using System;
using Scripts;
using Scripts.Units.StateInfo.BaseState;
using Telerik.JustMock;
using Units.OneUnit.Base;
using Xunit;

namespace Tests.Scripts.Units.States
{
    public class StateControllerTests
    {
        private IBaseActionControllerInternal _baseActionController;
        private StateController _stateController;

        public StateControllerTests()
        {
            _baseActionController = Mock.Create<IBaseActionControllerInternal>();
        }

        [Theory]
        [InlineData(true, false, true)]
        [InlineData(false, true, false)]
        //tested method]_[expected input]_[expected behavior]
        public void SetAttackState_ShouldRiseCorrectNoWayEvents(bool attackState, bool walkCalled, bool attackCalled)
        {
            // Arrange
            _stateController = new StateController();
            _stateController.InitializeBaseActionController(_baseActionController);
            bool noWayToWalkCalled = false;
            bool noWayToAttackCalled = false;
            Mock.Arrange(() => _baseActionController.RaiseNoWayToWalkDestination(Arg.IsAny<IntVector2>()))
                .DoInstead(() => { noWayToWalkCalled = true; });
            Mock.Arrange(() => _baseActionController.RaiseNoWayToAttackDestination(Arg.IsAny<IntVector2>()))
                .DoInstead(() => { noWayToAttackCalled = true; });

            // Act
            if (attackState)
            {
                _stateController.SetAttackState();
            }
            _stateController.CurrentState.RaiseNoWayToDestination(new IntVector2());

            // Assert 
            Assert.Equal(noWayToWalkCalled, walkCalled);
            Assert.Equal(noWayToAttackCalled, attackCalled);
        }
        
        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        //tested method]_[expected input]_[expected behavior]
        public void SetAnyState_ShouldRiseCorrectEvents(bool attackState, bool walkState)
        {
            // Arrange
            _stateController = new StateController();
            _stateController.InitializeBaseActionController(_baseActionController);
            bool moveCompleteCalled = false;
            bool nexTileOccupiedCalled = false;
            Mock.Arrange(() => _baseActionController.RaiseMovePathComplete())
                .DoInstead(() => { moveCompleteCalled = true; });
            Mock.Arrange(() => _baseActionController.RaiseNextTileOccupied(Arg.IsAny<IntVector2>()))
                .DoInstead(() => { nexTileOccupiedCalled = true; });

            // Act
            if (attackState)
            {
                _stateController.SetAttackState();
            }
            _stateController.CurrentState.RaiseNextTileOccupied(new IntVector2());
            _stateController.CurrentState.RaiseMovePathComplete();

            // Assert 
            Assert.True(nexTileOccupiedCalled);
            Assert.True(moveCompleteCalled);
        }
        
        [Fact]
        public void SetAnyState_ShouldThrowExeptionOnNotInitialized()
        {
            // Arrange
            _stateController = new StateController();
            bool exeptionCalled = false;

            // Act
            try
            {
                _stateController.SetAttackState();
            }
            catch (Exception e)
            {
                exeptionCalled = true;
            }

            // Assert 
            Assert.True(exeptionCalled);
        }
        
        [Fact]
        public void GetAnyState_ShouldThrowExeptionOnNotInitialized()
        {
            // Arrange
            _stateController = new StateController();
            bool exeptionCalled = false;

            // Act
            try
            {
                _stateController.GetAttackState();
            }
            catch (Exception e)
            {
                exeptionCalled = true;
            }

            // Assert 
            Assert.True(exeptionCalled);
        }
    }
}
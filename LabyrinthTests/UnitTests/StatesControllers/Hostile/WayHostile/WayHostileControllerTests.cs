using System.Collections.Generic;
using Scripts;
using Telerik.JustMock;
using Units.OneUnit.StatesControllers.Base;
using Units.OneUnit.StatesControllers.Hostile;
using Xunit;

namespace UnitTests.StatesControllers.Hostile.WayHostile
{
    public class WayHostileControllerTests
    {
        private readonly IFreePointToGoResolver _freePointToGoResolver;
        private readonly IWaitObstacleController _waitObstacleController;
        private readonly IBaseActionController _baseActionController;
        private readonly IWayHostileControllerParameters _wayHostileControllerParameters;

        private IWayHostileController _wayHostileController;

        public WayHostileControllerTests()
        {
            _freePointToGoResolver = Mock.Create<IFreePointToGoResolver>();
            _waitObstacleController = Mock.Create<IWaitObstacleController>();
            _baseActionController = Mock.Create<IBaseActionController>();
            _wayHostileControllerParameters = Mock.Create<IWayHostileControllerParameters>();

            _wayHostileController = new WayHostileController(_freePointToGoResolver, _baseActionController, _waitObstacleController);
        }

        [Theory]
        [ClassData(typeof(WayHostileMoveToTestData))]
        public void MoveToPosition_ShouldRiseMoveToPositionCompleteCorrect(IntVector2 attackPosition, IntVector2 returnedFreePoint, bool expectedCall)
        {
            // Aggange
            bool moveToPositionCompleteCalled = false;
            _wayHostileController.MoveToPositionComplete += () => moveToPositionCompleteCalled = true;
            Mock.Arrange(() => _wayHostileControllerParameters.AttackPosition).Returns(attackPosition);
            Mock.Arrange(() => _freePointToGoResolver.GetFreePoint(Arg.IsAny<IntVector2>())).Returns(returnedFreePoint);

            Mock.Arrange(() => _baseActionController.MoveToPosition(Arg.IsAny<IntVector2>())).DoNothing();

            // Act
            _wayHostileController.MoveToPosition(attackPosition);
            Mock.Raise(() => _baseActionController.MovePathComplete += null);

            // Assert
            Assert.True(expectedCall == moveToPositionCompleteCalled);
        }

        [Theory]
        [ClassData(typeof(WayHostileMoveToTestData))]
        public void MoveToPosition_ShouldCallWaitCorrect(IntVector2 attackPosition, IntVector2 returnedFreePoint, bool expectedCall)
        {
            // Aggange
            bool waitCalled = false;
            Mock.Arrange(() => _freePointToGoResolver.GetFreePoint(Arg.IsAny<IntVector2>())).Returns(returnedFreePoint);
            Mock.Arrange(() => _waitObstacleController.Wait(Arg.IsAny<IntVector2>())).DoInstead(() => { waitCalled = true; });

            Mock.Arrange(() => _baseActionController.MoveToPosition(Arg.IsAny<IntVector2>())).DoNothing();

            // Act
            _wayHostileController.MoveToPosition(attackPosition);
            Mock.Raise(() => _baseActionController.MovePathComplete += null);

            // Assert
            Assert.True(expectedCall != waitCalled);
        }

        [Theory]
        [ClassData(typeof(OstacleStateChangedTestData))]
        public void MoveToPosition_NoWayOstacleStateChanged_ShouldCallWaitCorrect(IntVector2 attackPosition, List<IntVector2> interimPoints,
            int expectedWaitCalledCount, int expectedMoveCompleteCalledCount)
        {
            // Aggange
            int waitCalledCount = 0;
            int moveCompleteCalledCount = 0;
            ArrangeGetFreePointReturnValue(interimPoints[0]);
            _wayHostileController.MoveToPositionComplete += () => moveCompleteCalledCount++;
            Mock.Arrange(() => _waitObstacleController.Wait(Arg.IsAny<IntVector2>())).DoInstead(() => { waitCalledCount++; });
            Mock.Arrange(() => _baseActionController.MoveToPosition(Arg.IsAny<IntVector2>())).DoNothing();

            // Act
            _wayHostileController.MoveToPosition(attackPosition);
            Mock.Raise(() => _baseActionController.NoWayToDestination += null, Arg.IsAny<IntVector2>());
            Mock.Raise(() => _baseActionController.MovePathComplete += null);

            ArrangeGetFreePointReturnValue(interimPoints[1]);
            Mock.Raise(() => _waitObstacleController.OstacleStateChanged += null);
            Mock.Raise(() => _baseActionController.MovePathComplete += null);

            ArrangeGetFreePointReturnValue(interimPoints[2]);
            Mock.Raise(() => _waitObstacleController.OstacleStateChanged += null);
            Mock.Raise(() => _baseActionController.MovePathComplete += null);

            // Assert
            Assert.Equal(expectedWaitCalledCount, waitCalledCount);
            Assert.Equal(expectedMoveCompleteCalledCount, moveCompleteCalledCount);
        }

        private void ArrangeBaseActionControllerMoveToRiseComplete()
        {
            Mock.Arrange(() => _baseActionController.MoveToPosition(Arg.IsAny<IntVector2>())).DoInstead(RiseMoveCompleteEvent);

            void RiseMoveCompleteEvent()
            {
                Mock.Raise(() => _baseActionController.MovePathComplete += null);
            }
        }

        private void ArrangeGetFreePointReturnValue(IntVector2 value)
        {
            Mock.Arrange(() => _freePointToGoResolver.GetFreePoint(Arg.IsAny<IntVector2>())).Returns(value);
        }
    }
}
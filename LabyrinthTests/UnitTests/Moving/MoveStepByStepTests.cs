using System;
using System.Collections.Generic;
using Scripts;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Units.OccupatedMap;
using Units.OneUnit.State;
using Units.OneUnit.StatesControllers.Base;
using Units.OneUnit.StatesControllers.Base.GameObject;
using Xunit;

namespace Tests.Scripts.Units.Moving
{
    public class MoveStepByStepTests
    {
        private IUnitGameObjectController _unitGameObjectController;
        private IStateControllerExternal _stateController;
        private IOccupatedPossitionsMap _occupatedPossitionsMap;
        private IMoveStepByStepController _moveStepByStepController;

        public MoveStepByStepTests()
        {
            _occupatedPossitionsMap = Mock.Create<IOccupatedPossitionsMap>();
            _stateController = Mock.Create<IStateControllerExternal>();
            _unitGameObjectController = new UnitGameObjectControllerMock();
        }

        [Theory]
        [ClassData(typeof(MoveStepByStepTestData1))]
        public void MoveTo_AnyPath_ShouldRiseMovePathComplete(List<IntVector2> path, int i1, int i2)
        {
            // Arrange
            bool movePathCompleteCalled = false;
            Mock.Arrange(() => _occupatedPossitionsMap.IsVacantPosition(Arg.IsAny<IntVector2>()))
                .Returns(true);

            _moveStepByStepController = CreateMoveStepByStepController();
            _moveStepByStepController.MovePathComplete += () => movePathCompleteCalled = true;

            // Act
            _moveStepByStepController.MoveTo(path);

            // Assert
            Assert.True(movePathCompleteCalled);
        }

        [Theory]
        [ClassData(typeof(MoveStepByStepTestData2))]
        public void MoveTo_AnyPath_ShouldRiseNextTileOccupied(List<IntVector2> path, IntVector2 occupiedPosition, bool NoWayToDestination)
        {
            // Arrange
            bool nextTileOccupiedCalled = false;
            bool nexNoWayToDestinationCalled = false;
            Mock.Arrange(() => _occupatedPossitionsMap.IsVacantPosition(Arg.IsAny<IntVector2>()))
                .Returns(true);
            Mock.Arrange(() => _occupatedPossitionsMap.IsVacantPosition(occupiedPosition))
                .Returns(false);

            _moveStepByStepController = CreateMoveStepByStepController();
            _moveStepByStepController.NextTileOccupied += i => nextTileOccupiedCalled = true;
            _moveStepByStepController.NoWayToDestination += i => nexNoWayToDestinationCalled = true;

            // Act
            _moveStepByStepController.MoveTo(path);

            // Assert
            if (NoWayToDestination)
            {
                Assert.True(nexNoWayToDestinationCalled);
            }
            else
            {
                Assert.True(nextTileOccupiedCalled);
            }
        }

        [Theory]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(12)]
        public void Cancel_ShouldNotRiseMovePathComplete(int moveCompleteCalledCount)
        {
            // Arrange
            List<IntVector2> path = new List<IntVector2>();
            path.Add(new IntVector2(0, 0));
            path.Add(new IntVector2(1, 1));
            path.Add(new IntVector2(2, 1));
            path.Add(new IntVector2(3, 1));

            int actualMoveCompleteCalledCount = 0;
            Mock.Arrange(() => _occupatedPossitionsMap.IsVacantPosition(Arg.IsAny<IntVector2>()))
                .Returns(true);

            _moveStepByStepController = CreateMoveStepByStepController();
            _moveStepByStepController.MovePathComplete += () => actualMoveCompleteCalledCount++;

            // Act
            _moveStepByStepController.MoveTo(path);
            _moveStepByStepController.Cancel();
            for (int i = 0; i < moveCompleteCalledCount; i++)
            {
                _unitGameObjectController.MoveTo(new IntVector2(0, 0));
            }

            // Assert
            Assert.Equal(1, actualMoveCompleteCalledCount);
        }

        private MoveStepByStepController CreateMoveStepByStepController()
        {
            var moveStepByStepController = new MoveStepByStepController(
                _unitGameObjectController,
                _occupatedPossitionsMap);

            return moveStepByStepController;
        }
    }

    public class UnitGameObjectControllerMock : IUnitGameObjectController
    {
        private IntVector2 _position;
        private bool _isMoving;
        public void Dispose() {}
        public void Wait() {}
        public void Wait(IntVector2 position) {}
        public void Attack(IntVector2 position) {}
        public void Die() {}
        public void SetOnPosition(IntVector2 position) {}
        public void SetHealthBarValue(float value) {}
        public void SetHealthBarVisible(bool value) {}

        public IntVector2 Position => _position;

        public bool IsMoving => _isMoving;

        public event Action MoveTileComplete;
        public event Action MoveTileStart;
        public event Action AttackComplete;
        public event Action DieComplete;


        public void MoveTo(IntVector2 position)
        {
            MoveTileComplete?.Invoke();
        }
    }
}
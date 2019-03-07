using System;
using System.Collections.Generic;
using Scripts;
using Scripts.Units.StateInfo.BaseState;
using Telerik.JustMock;
using Units;
using Units.OneUnit.Base;
using Units.OneUnit.Base.GameObject;
using Xunit;

namespace Tests.Scripts.Units.Moving
{
    public class MoveStepByStepTests
    {
        private IUnitGameObjectController _unitGameObjectController;
        private IStateControllerExternal _stateController;
        private IUnitsTable _unitsTable;
        private IMoveStepByStepController _moveStepByStepController;
        
        public MoveStepByStepTests()
        {
            _unitsTable = Mock.Create<IUnitsTable>();
            _stateController = Mock.Create<IStateControllerExternal>();
            _unitGameObjectController = new UnitGameObjectControllerMock();
        }

        [Theory]
        [ClassData(typeof(MoveStepByStepTestData1))]
        //tested method]_[expected input]_[expected behavior]
        public void MoveTo_AnyPath_ShouldUpdateOccupationMap(List<IntVector2> path, int expectedVacantCalls, int expectedOccupiedCalls)
        {
            // Arrange
            int setVacantCallsCount = 0;
            int setOccupiedCallsCount = 0;
            Mock.Arrange(() => _unitsTable.SetVacant(Arg.IsAny<IntVector2>()))
                .DoInstead(() => { setVacantCallsCount++; });
            Mock.Arrange(() => _unitsTable.SetOccupied(Arg.IsAny<IntVector2>()))
                .DoInstead(() => { setOccupiedCallsCount++; });
            Mock.Arrange(() => _unitsTable.IsVacantPosition(Arg.IsAny<IntVector2>()))
                .Returns(true);
            
            _moveStepByStepController = new MoveStepByStepController(
                _stateController, 
                _unitsTable, 
                _unitGameObjectController);

            // Act
            _moveStepByStepController.MoveTo(path);
            
            // Assert
            Assert.Equal(expectedVacantCalls, setVacantCallsCount);
            Assert.Equal(expectedOccupiedCalls, setOccupiedCallsCount);
        }
        
        [Theory]
        [ClassData(typeof(MoveStepByStepTestData1))]
        public void MoveTo_AnyPath_ShouldRiseMovePathComplete(List<IntVector2> path, int i1, int i2)
        {
            // Arrange
            bool movePathCompleteCalled = false;
            Mock.Arrange(() => _unitsTable.IsVacantPosition(Arg.IsAny<IntVector2>()))
                .Returns(true);
            Mock.Arrange(() => _stateController.CurrentState.RaiseMovePathComplete())
                .DoInstead(() => { movePathCompleteCalled = true; });
            
            _moveStepByStepController = new MoveStepByStepController(
                _stateController, 
                _unitsTable, 
                _unitGameObjectController);

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
            Mock.Arrange(() => _unitsTable.IsVacantPosition(Arg.IsAny<IntVector2>()))
                .Returns(true);
            Mock.Arrange(() => _unitsTable.IsVacantPosition(occupiedPosition))
                .Returns(false);
            Mock.Arrange(() => _stateController.CurrentState.RaiseNextTileOccupied(Arg.IsAny<IntVector2>()))
                .DoInstead(() => { nextTileOccupiedCalled = true; });
            Mock.Arrange(() => _stateController.CurrentState.RaiseNoWayToDestination(Arg.IsAny<IntVector2>()))
                .DoInstead(() => { nexNoWayToDestinationCalled = true; });
            
            _moveStepByStepController = new MoveStepByStepController(
                _stateController, 
                _unitsTable, 
                _unitGameObjectController);

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

            int actualMoveCompleteCalledCount = 0;
            Mock.Arrange(() => _unitsTable.IsVacantPosition(Arg.IsAny<IntVector2>()))
                .Returns(true);
            Mock.Arrange(() => _stateController.CurrentState.RaiseMovePathComplete())
                .DoInstead(() => { actualMoveCompleteCalledCount++; });
            
            _moveStepByStepController = new MoveStepByStepController(
                _stateController, 
                _unitsTable, 
                _unitGameObjectController);

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
    }

    public class UnitGameObjectControllerMock : IUnitGameObjectController
    {
        public void Dispose() {}
        public void Wait() {}
        public void Wait(IntVector2 position) {}
        public void Attack(IntVector2 position) {}
        public void Die() {}
        public void SetOnPosition(IntVector2 position) {}
        public void SetHealthBarValue(float value) {}
        public void SetHealthBarVisible(bool value) {}

        public IntVector2 Position { get; }
        public bool IsMoving { get; }
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
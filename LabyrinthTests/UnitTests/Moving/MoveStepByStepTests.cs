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
        [ClassData(typeof(MoveStepByStepTestData))]
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
        [ClassData(typeof(MoveStepByStepTestData))]
        //tested method]_[expected input]_[expected behavior]
        public void MoveTo_AnyPath_ShouldRiseMovePathComplete(List<IntVector2> path, int i1, int i2)
        {
            // Arrange
            bool movePathCompleteCalled = false;
            IState state = Mock.Create<IState>();
            Mock.Arrange(() => _unitsTable.IsVacantPosition(Arg.IsAny<IntVector2>()))
                .Returns(true);
            Mock.Arrange(() => state.RaiseMovePathComplete())
                .DoInstead(() => { movePathCompleteCalled = true; });
            Mock.Arrange(() => _stateController.CurrentState)
                .Returns(state);
            
            _moveStepByStepController = new MoveStepByStepController(
                _stateController, 
                _unitsTable, 
                _unitGameObjectController);

            // Act
            _moveStepByStepController.MoveTo(path);
            
            // Assert
            Assert.True(movePathCompleteCalled);
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
        public event Action MoveComplete;
        
        public void MoveTo(IntVector2 position)
        {
            MoveComplete?.Invoke();
        }
    }
}
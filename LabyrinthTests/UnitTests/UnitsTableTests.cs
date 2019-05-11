using Scripts;
using Scripts.Units.Events;
using Telerik.JustMock;
using Tests.Scripts.Units.OccupatedMap;
using Units.OccupatedMap;
using Units.OneUnit;
using Xunit;

namespace Tests.Scripts.Units
{
    public class UnitsTableTests
    {
        private OccupatedPossitionsMap _occupatedPossitionsTable;
        
        public UnitsTableTests()
        {    
            _occupatedPossitionsTable = new OccupatedPossitionsMap();
        }

        [Theory]
        [InlineData(false, 2, 3, 2, 3)]
        [InlineData(true, 2, 3, 2, 4)]
        public void IsVacantPosition_Occupied_TrueFalse(bool expectedResult, int x1, int y1, int x2, int y2)
        {
            bool actualResult = false;
            var unit = new OneUnitControllerMock(Mock.Create<IUnitEvents>());
            unit.SetOnPosition(new IntVector2(x1, y1));
            _occupatedPossitionsTable.AddUnit(unit);
            actualResult = _occupatedPossitionsTable.IsVacantPosition(new IntVector2(x2, y2));

            Assert.True(actualResult == expectedResult);
        }
        
        [Theory]
        [InlineData(false, 2, 3, 2, 3)]
        [InlineData(true, 2, 3, 2, 4)]
        public void SetVacant_TrueFalse(bool expectedResult, int x1, int y1, int x2, int y2)
        {
            bool actualResult = false;
            var unit = new OneUnitControllerMock(Mock.Create<IUnitEvents>());
            unit.SetOnPosition(new IntVector2(x1, y1));
            _occupatedPossitionsTable.AddUnit(unit);
            unit.SetOnPosition(new IntVector2(x2, y2));
            actualResult = _occupatedPossitionsTable.IsVacantPosition(new IntVector2(x1, y1));

            Assert.True(actualResult == expectedResult);
        }
    }
}
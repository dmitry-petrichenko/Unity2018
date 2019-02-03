using Scripts;
using Telerik.JustMock;
using Units;
using Xunit;

namespace Tests.Scripts.Units
{
    public class UnitsTableTests
    {
        private OccupatedPossitionsTable _occupatedPossitionsTable;
        
        public UnitsTableTests()
        {    
            _occupatedPossitionsTable = new OccupatedPossitionsTable();
        }

        [Theory]
        [InlineData(false, 2, 3, 2, 3)]
        [InlineData(true, 2, 3, 2, 4)]
        public void IsVacantPosition_Occupied_TrueFalse(bool expectedResult, int x1, int y1, int x2, int y2)
        {
            bool actualResult = false;
            _occupatedPossitionsTable.SetOccupied(new IntVector2(x1, y1));
            actualResult = _occupatedPossitionsTable.IsVacantPosition(new IntVector2(x2, y2));

            Assert.True(actualResult == expectedResult);
        }
        
        [Theory]
        [InlineData(true, 2, 3, 2, 3)]
        [InlineData(false, 2, 3, 2, 4)]
        public void SetVacant_TrueFalse(bool expectedResult, int x1, int y1, int x2, int y2)
        {
            bool actualResult = false;
            _occupatedPossitionsTable.SetOccupied(new IntVector2(x1, y1));
            _occupatedPossitionsTable.SetVacant(new IntVector2(x2, y2));
            actualResult = _occupatedPossitionsTable.IsVacantPosition(new IntVector2(x1, y1));

            Assert.True(actualResult == expectedResult);
        }
    }
}
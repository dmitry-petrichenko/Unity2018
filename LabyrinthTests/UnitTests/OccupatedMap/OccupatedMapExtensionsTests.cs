using System.Collections.Generic;
using System.Linq;
using Scripts;
using Units.OccupatedMap;
using Units.OneUnit;
using Xunit;

namespace Tests.Scripts.Units.OccupatedMap
{
    public class OccupatedMapExtensionsTests
    {
        private IOccupatedPossitionsMap _occupatedPossitionsMap;
        
        public OccupatedMapExtensionsTests()
        {
            _occupatedPossitionsMap = new OccupatedPossitionsMap();
        }

        [Theory]
        [ClassData(typeof(GetRegionUnitsTestData))]
        public void GetUnitsInRegion_AnyPosition_ShouldReturnCorrect(List<OneUnitControllerMock> units, List<IOneUnitController> expectedUnits, IntVector2 point1, IntVector2 point2)
        {
            InitializeMap(units);
            
            var actualUnits = _occupatedPossitionsMap.GetUnitsInRegion(point1, point2);

            var expectedResult = UnitsToPositionsList(expectedUnits);
            var actualResult = UnitsToPositionsList(actualUnits);
            
            Assert.True(actualResult.SequenceEqual(expectedResult));
        }

        private List<IntVector2> UnitsToPositionsList(List<IOneUnitController> list)
        {
            var positions = new List<IntVector2>();
            foreach (var unitControllerMock in list)
            {
                positions.Add(unitControllerMock.Position);
            }

            return positions;
        }

        private void InitializeMap(List<OneUnitControllerMock> list)
        {
            _occupatedPossitionsMap = new OccupatedPossitionsMap();
            foreach (var unitControllerMock in list)
            {
                _occupatedPossitionsMap.AddUnit(unitControllerMock);
            }
        }
    }
}
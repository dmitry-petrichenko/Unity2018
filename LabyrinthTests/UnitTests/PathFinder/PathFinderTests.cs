using System.Collections.Generic;
using System.Linq;
using Scripts;
using Units.PathFinder;
using Xunit;

namespace Tests.Scripts.Units.PathFinder
{
    public class PathFinderTests
    {
        public PathFinderTests()
        {
        }

        [Theory]
        [ClassData(typeof(PathFinderTestData))]
        //tested method]_[expected input]_[expected behavior]
        public void GetPath_BetweenTwoPoints_ShouldReturnCorrectPath(IntVector2 startPoint, IntVector2 endPoint, List<IntVector2> expectedPath)
        {
            // Arrange
            IGrid grid = CreateZigzagGrid();
            IPathFinderController pathFinderController = new PathFinderController(grid);
            // Act
            var actualPath = pathFinderController.GetPath(startPoint, endPoint, null);

            // Assert
            Assert.True(expectedPath.SequenceEqual(actualPath));
        }

        [Fact]
        public void GetPath_BetweenTwoPointsRepeatedly_ShouldReturnCorrectPath()
        {
            // Arrange
            int findPathCorrectXTimes = 0;
            int XTimes = 5;
            var expectedPath = PathFinderTestData.GetList0_0_5_5();
            IGrid grid = CreateZigzagGrid();
            IPathFinderController pathFinderController = new PathFinderController(grid);

            // Act
            for (int i = 0; i < XTimes; i++)
            {
                var actualPath = pathFinderController.GetPath(GetPoint(0, 0), GetPoint(5, 5), null);
                if (expectedPath.SequenceEqual(actualPath))
                {
                    findPathCorrectXTimes++;
                }
            }
            // Assert
            Assert.Equal(findPathCorrectXTimes, XTimes);
        }

        [Theory]
        [ClassData(typeof(PathFinderNoWayTestData))]
        public void GetPath_BetweenTwoPoints_ShouldReturnNoWayToPoint(IntVector2 startPoint, IntVector2 endPoint)
        {
            // Arrange
            bool noWayCalled = false;
            IGrid grid = CreateWallGrid();
            IPathFinderController pathFinderController = new PathFinderController(grid);
            pathFinderController.NoWayToDestinationPoint += v => noWayCalled = true;

            // Act
            pathFinderController.GetPath(startPoint, endPoint, null);

            // Assert
            Assert.True(noWayCalled);
        }

        [Theory]
        [ClassData(typeof(PathFinderDestinationTestData))]
        public void GetPath_BetweenTwoPoints_ShouldReturnDestinationPointBusy(IntVector2 startPoint, IntVector2 endPoint)
        {
            // Arrange
            bool destinationBusyCalled = false;
            IGrid grid = CreateWallGrid();
            IPathFinderController pathFinderController = new PathFinderController(grid);
            pathFinderController.DestinationPointIsNotEmpty += v => destinationBusyCalled = true;

            // Act
            pathFinderController.GetPath(startPoint, endPoint, null);

            // Assert
            Assert.True(destinationBusyCalled);
        }

        private IGrid CreateZigzagGrid()
        {
            bool[,] zigzagGrid;
            zigzagGrid = new [,]
            {
                { true,    true,    false,    true,      true,    true },
                { true,    true,    false,    true,      true,    true },
                { true,    true,    false,    true,     false,    true },
                { true,    true,    true,     true,     false,    true },
                { true,    true,    true,     true,     false,    true },
                { true,    true,    true,     true,     false,    true }
            };

            return new TestGrid(zigzagGrid);
        }

        private IGrid CreateWallGrid()
        {
            bool[,] zigzagGrid;
            zigzagGrid = new [,]
            {
                { true,    true,    false,    true,     true,    true },
                { true,    true,    false,    true,     true,    true },
                { true,    true,    false,    true,     true,    true },
                { true,    true,    false,    true,     true,    true },
                { true,    true,    false,    true,     true,    true },
                { true,    true,    false,    true,     true,    true }
            };

            return new TestGrid(zigzagGrid);
        }

        private IntVector2 GetPoint(int x, int y)
        {
            return new IntVector2(x, y);
        }
    }

    internal class TestGrid : IGrid
    {
        private bool[,] _grid;
        public TestGrid(bool[,] grid)
        {
            _grid = grid;
        }

        public bool GetCell(IntVector2 index)
        {
            if (_grid.GetLength(0) <= index.x || index.x < 0)
            {
                return false;
            }

            if (_grid.GetLength(1) <= index.y || index.y < 0)
            {
                return false;
            }

            return _grid[index.x, index.y];
        }
    }
}
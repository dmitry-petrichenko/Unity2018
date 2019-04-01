using System;
using System.Collections.Generic;
using Scripts;
using Telerik.JustMock;
using Units.OneUnit.StatesControllers.Hostile;
using Units.PathFinder;
using Xunit;

namespace TestProject1
{
    public class FreePointToGoResolverTests
    {
        private IPathFinderController _pathFinderController;
        private IAdjacentPointsResolver _adjacentPointsResolver;
        private IFreePossitionsMap _freePossitionsMap;
        private IWayHostileControllerParameters _parameters;
        private IFreePointToGoResolver _freePointToGoResolver;

        public FreePointToGoResolverTests()
        {
            _pathFinderController = Mock.Create<IPathFinderController>();
            _adjacentPointsResolver = Mock.Create<IAdjacentPointsResolver>();
            _freePossitionsMap = Mock.Create<IFreePossitionsMap>();
            _parameters = Mock.Create<IWayHostileControllerParameters>();

            _freePointToGoResolver = new FreePointToGoResolver(
                _parameters,
                _adjacentPointsResolver,
                _pathFinderController,
                _freePossitionsMap);
        }

        [Theory]
        [ClassData(typeof(GetFreePointNearestTestData))]
        public void GetFreePoint_ShouldReturnNearestPoint(IntVector2 attackPosition,
            IntVector2 unitPosition, List<IntVector2> pathFinderList, List<IntVector2> freeAdjacentUnitPoints, IntVector2 expectedPoint)
        {
            // Aggange
            IntVector2 actualPoint = IntVector2Constant.UNASSIGNET;

            Mock.Arrange(() => _parameters.AttackPosition).Returns(attackPosition);
            Mock.Arrange(() => _parameters.UnitPosition).Returns(unitPosition);

            Mock.Arrange(() =>
                _adjacentPointsResolver.GetFreeAdjacentUnitPoints(
                    Arg.IsAny<IntVector2>(),
                    Arg.IsAny<Predicate<IntVector2>>(),
                    Arg.IsAny<int>()))
                .Returns(freeAdjacentUnitPoints);

            Mock.Arrange(() =>
                _pathFinderController.GetPath(
                    Arg.IsAny<IntVector2>(),
                    Arg.IsAny<IntVector2>(),
                    Arg.IsAny<List<IntVector2>>()))
                .Returns(pathFinderList);

            // Act
            actualPoint = _freePointToGoResolver.GetFreePoint(attackPosition);

            // Assert
            Assert.True(actualPoint.Equals(expectedPoint));
        }

        [Theory]
        [ClassData(typeof(GetFreePointThowNoValidPoints))]
        public void GetFreePoint_ShouldTrowExeptionOnNoAchivabalePoints(IntVector2 attackPosition,
            IntVector2 unitPosition, List<IntVector2> pathFinderList, List<IntVector2> freeAdjacentUnitPoints, IntVector2 expectedPoint, bool exeption)
        {
            // Aggange
            IntVector2 actualPoint = IntVector2Constant.UNASSIGNET;
            bool exeptionWasThrown = false;

            Mock.Arrange(() => _parameters.AttackPosition).Returns(attackPosition);
            Mock.Arrange(() => _parameters.UnitPosition).Returns(unitPosition);

            Mock.Arrange(() =>
                    _adjacentPointsResolver.GetFreeAdjacentUnitPoints(
                        Arg.IsAny<IntVector2>(),
                        Arg.IsAny<Predicate<IntVector2>>(),
                        Arg.IsAny<int>()))
                .Returns(freeAdjacentUnitPoints);

            Mock.Arrange(() =>
                    _pathFinderController.GetPath(
                        Arg.IsAny<IntVector2>(),
                        Arg.IsAny<IntVector2>(),
                        Arg.IsAny<List<IntVector2>>()))
                .Returns(pathFinderList);

            // Act
            try
            {
                actualPoint = _freePointToGoResolver.GetFreePoint(attackPosition);
            }
            catch (Exception e)
            {
                exeptionWasThrown = true;
            }

            // Assert
            Assert.True(exeptionWasThrown == exeption);
        }
    }
}
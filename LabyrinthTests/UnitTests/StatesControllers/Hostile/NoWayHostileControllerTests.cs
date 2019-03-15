using Scripts;
using Telerik.JustMock;
using Units.OneUnit.Info;
using Units.OneUnit.StatesControllers.Hostile;
using Units.PathFinder;
using Xunit;

namespace Tests.Scripts.Units.StatesControllers.Hostile
{
    public class NoWayHostileControllerTests
    {
        private NoWayHostileController _noWayHostileController;

        private IFreePossitionsMap _freePossitionsMap;
        private IUnitInfoExternal _unitInfoExternal;
        private BaseActionControlleMock _baseActionController;
        private IGrid _grid;

        public NoWayHostileControllerTests()
        {
            _freePossitionsMap = Mock.Create<IFreePossitionsMap>();
            _unitInfoExternal = Mock.Create<IUnitInfoExternal>();
            _baseActionController = new BaseActionControlleMock();
            _grid = Mock.Create<IGrid>();
        }
/*
        [Fact]
        public void Activate_ShouldSubscribe()
        {
            // Arrange
            bool deactivateCalled = false;
            bool activateCalled = false;
            _noWayHostileController = CreateNoWayHostileController();

            _noWayHostileController.Activate();
            _baseActionController.RiseNoWayToDestination(new IntVector2(0, 0));

            // Act

            // Assert
            Assert.True(true);
            //Assert.True(deactivateCalled);
            //Assert.True(activateCalled);
        }
*/
        private NoWayHostileController CreateNoWayHostileController()
        {
            var n = new NoWayHostileController(_freePossitionsMap,
                _unitInfoExternal,
                _baseActionController);

            return n;
        }
    }
}
using Scripts;
using Units.OneUnit.StatesControllers;
using Xunit.Abstractions;

namespace Tests.Scripts.Units.Events
{
    public class PositionalMock : IPositional
    {
        private ITestOutputHelper _testOutputHelper;
        private IntVector2 _position;
        
        public PositionalMock(ITestOutputHelper testOutputHelper, IntVector2 position)
        {
            _position = position;
            _testOutputHelper = testOutputHelper;
        }

        public IntVector2 Position => _position;

        public void SomeAction()
        {
            _testOutputHelper.WriteLine(Position.x + " " + Position.y + " notified");
        }
    }
}
using Scripts;
using Scripts.Units.Events;
using Telerik.JustMock;
using Units.OneUnit.StatesControllers.Base;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Scripts.Units.Events
{
    public class DeadNotifierTests
    {
        private IBaseActionController _baseActionController;
        private IDeadNotifier _deadNotifier;

        private ITestOutputHelper _output;
        
        public DeadNotifierTests(ITestOutputHelper output)
        {
            _output = output;
            _baseActionController = Mock.Create<IBaseActionController>();
            
            _deadNotifier = new DeadNotifier(_baseActionController);
        }
        
        [Fact]
        public void NotifyDead_ShouldNotifyCorrect()
        {
            // Arrange
            Mock.Arrange(() => _baseActionController.Position)
                .Returns(new IntVector2(0, 0));

            PositionalMock mock;
            mock = new PositionalMock(_output, new IntVector2(2, 2));
            _deadNotifier.AddDeadListener(mock, mock.SomeAction);
            mock = new PositionalMock(_output, new IntVector2(3, 4));
            _deadNotifier.AddDeadListener(mock, mock.SomeAction);
            mock = new PositionalMock(_output, new IntVector2(1, 0));
            _deadNotifier.AddDeadListener(mock, mock.SomeAction);
            mock = new PositionalMock(_output, new IntVector2(-2, 1));
            _deadNotifier.AddDeadListener(mock, mock.SomeAction);
            mock = new PositionalMock(_output, new IntVector2(1, 3));
            _deadNotifier.AddDeadListener(mock, mock.SomeAction);
            
            // Act
            _deadNotifier.NotifyDead();

            // Assert
            Assert.True(true);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Scripts;
using Scripts.Units.Events;
using Telerik.JustMock;
using Units.OneUnit;
using Units.OneUnit.Info;
using Units.OneUnit.StatesControllers.Base;
using Xunit;

namespace Tests.Scripts.Units.WaveEventRaiser
{
    public class WaveEventRaiserTests
    {
        private IBaseActionController _baseActionController;
        private IWaveEventRiser _waveEventRiser;
        
        public WaveEventRaiserTests()
        {
            _baseActionController = Mock.Create<IBaseActionController>();
            _waveEventRiser = new global::Scripts.Units.Events.WaveEventRaiser(_baseActionController);
        }
        
        [Fact]
        public void MoveTileStart_ShouldCallHandlersWaveLike()
        {
            // Act
            var uc1 = new OneUnitControllerMock();
            uc1.SetOnPosition(new IntVector2(3, 3));
            _waveEventRiser.AddPositionChangedHandler(uc1.Handler, uc1);
            
            var uc2 = new OneUnitControllerMock();
            uc2.SetOnPosition(new IntVector2(4, 4));
            _waveEventRiser.AddPositionChangedHandler(uc2.Handler, uc2);
            
            var uc3 = new OneUnitControllerMock();
            uc3.SetOnPosition(new IntVector2(2, 2));
            _waveEventRiser.AddPositionChangedHandler(uc3.Handler, uc3);
            
            var uc4 = new OneUnitControllerMock();
            uc4.SetOnPosition(new IntVector2(1, 1));
            _waveEventRiser.AddPositionChangedHandler(uc4.Handler, uc4);

            Mock.Raise(() => _baseActionController.MoveTileStart += null);

            var expectedSequence = new List<IntVector2>();
            expectedSequence.Add(new IntVector2(1,1));
            expectedSequence.Add(new IntVector2(2,2));
            expectedSequence.Add(new IntVector2(3,3));
            expectedSequence.Add(new IntVector2(4,4));
            // Assert
            Assert.True(expectedSequence.SequenceEqual(HandlerSequenceHolder.CalledHandlers));
        }

        private static class HandlerSequenceHolder
        {
            private static List<IntVector2> _сalledHandlers = new List<IntVector2>();

            public static List<IntVector2> CalledHandlers => _сalledHandlers;

            public static void AddCalledHandler(IntVector2 position)
            {
                _сalledHandlers.Add(position);
            }
        }
        
        private class OneUnitControllerMock : IOneUnitController
        {
            private IntVector2 _position;

            public IntVector2 Position => _position;
            public void Dispose()
            {
                throw new System.NotImplementedException();
            }

            public void MoveTo(IntVector2 position)
            {
                throw new System.NotImplementedException();
            }

            public void Wait()
            {
                throw new System.NotImplementedException();
            }

            public void Wait(IntVector2 position)
            {
                throw new System.NotImplementedException();
            }

            public void SetOnPosition(IntVector2 position)
            {
                _position = position;
            }

            public void Attack(IntVector2 position)
            {
                throw new System.NotImplementedException();
            }

            public void TakeDamage(int value)
            {
                throw new System.NotImplementedException();
            }

            public void Handler()
            {
                HandlerSequenceHolder.AddCalledHandler(_position);
            }

            public IUnitEvents UnitEvents { get; }
            public IUnitInfoExternal DynamicInfo { get; }
        }
    }
}
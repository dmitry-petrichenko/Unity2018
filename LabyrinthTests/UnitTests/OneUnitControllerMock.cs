using Scripts;
using Scripts.Units.Events;
using Units.OneUnit;
using Units.OneUnit.Info;

namespace Tests.Scripts.Units
{
    public class OneUnitControllerMock : IOneUnitController
    {
        private IntVector2 _position;
        private IUnitEvents _unitEvents;

        public OneUnitControllerMock(IUnitEvents unitEvents)
        {
            _unitEvents = unitEvents;
        }

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

        public IUnitEvents UnitEvents => _unitEvents;
        public IUnitInfoExternal DynamicInfo { get; }
    }
}
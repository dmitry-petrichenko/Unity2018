using Scripts;
using Scripts.Units.Events;
using Units.OneUnit;
using Units.OneUnit.Info;

namespace Tests.Scripts.Units.OccupatedMap
{
    public class OneUnitControllerMock : IOneUnitController
    {
        private IntVector2 _position;

        public OneUnitControllerMock(IntVector2 position)
        {
            _position = position;
        }

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
            throw new System.NotImplementedException();
        }

        public void Attack(IntVector2 position)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(int value)
        {
            throw new System.NotImplementedException();
        }

        public IntVector2 Position => _position;
        public IUnitEvents UnitEvents { get; }
        public IUnitInfoExternal DynamicInfo { get; }
    }
}
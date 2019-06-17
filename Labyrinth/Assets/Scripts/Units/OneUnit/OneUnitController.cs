using System;
using System.Threading.Tasks;
using Scripts;
using Scripts.CustomDebug;
using Scripts.Units.Events;
using Units.OccupatedMap;
using Units.OneUnit.Info;
using Units.OneUnit.State;

namespace Units.OneUnit
{
    public class OneUnitController : Disposable, IOneUnitController
    {
        private readonly IUnitEvents _unitEvents;
        private readonly IUnitInfoExternal _unitInfoExternal;
        private readonly IStateControllerExternal _stateController;
        private readonly ILifeController _lifeController;
        
        private IOccupatedPossitionsMap _occupatedPossitionsMap;

        public OneUnitController(
            IOccupatedPossitionsMap occupatedPossitionsMap,
            IUnitEvents unitEvents,
            IStateControllerExternal stateController,
            ILifeController lifeController,
            IUnitInfoExternal unitInfoExternal)
        {
            _occupatedPossitionsMap = occupatedPossitionsMap;          
            _unitEvents = unitEvents;
            _unitInfoExternal = unitInfoExternal;
            _stateController = stateController;
            _lifeController = lifeController;
            
            _occupatedPossitionsMap.AddUnit(this);
            _unitEvents.HealthEnded += HealthEndedHandler;
            _unitEvents.DieComplete += DieCompleteHandler;
            _lifeController.HealthEnded += HealthEndedHandler;
        }

        public IUnitInfoExternal DynamicInfo => _unitInfoExternal;
        public IUnitEvents UnitEvents => _unitEvents;
        public IntVector2 Position => _stateController.CurrentState.Position;

        public void SetOnPosition(IntVector2 position)
        {
            if (!_occupatedPossitionsMap.IsVacantPosition(position))
            {
                ApplicationDebugger.ThrowException(
                    String.Format("OneUnitController.SetOnPosition: position {0} {1} has already taken", position.x, position.y)); 
                return;
            }

            _stateController.CurrentState.SetOnPosition(position);
        }
        
        public virtual void Attack(IntVector2 position)
        {
            _stateController.CurrentState.Attack(position);
        }

        public void TakeDamage(int value)
        {
            _stateController.CurrentState.TakeDamage(value);
        }

        public void MoveTo(IntVector2 position)
        { 
            _stateController.CurrentState.MoveTo(position);
        }

        public void Wait()
        {
            _stateController.CurrentState.Wait();
        }

        public void Wait(IntVector2 position)
        {
            _stateController.CurrentState.Wait(position);
        }

        protected override void DisposeInternal()
        {
            _unitEvents.HealthEnded -= HealthEndedHandler;
            _unitEvents.DieComplete -= DieCompleteHandler;
            base.DisposeInternal();
        }
        
        private void HealthEndedHandler()
        {
            _occupatedPossitionsMap.RemoveUnit(this);
            _stateController.CurrentState.Die();
        }

        private void DieCompleteHandler()
        {
            DelayedDispose();
        }

        private async Task DelayedDispose()
        {
            await Task.Delay(1000);
            Dispose();
        }
    }
}
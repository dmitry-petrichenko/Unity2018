using System;
using Scripts.Units.Events;
using Units.OneUnit;
using Units.OneUnit.Info;
using Units.OneUnit.State1E;
using UnityEngine;

namespace Scripts.Units
{
    public class AggressiveBehaviour : Disposable, IAgressiveBehaviour
    {
        public event Action Complete;

        private IOneUnitController _target;
        
        private readonly IAttackController _attackController;
        private readonly IUnitInfoInternal _unitInfo;
        private readonly IUnitEvents _unitEvents;
        private readonly IStateControllerExternal _stateController;

        public AggressiveBehaviour(
            IAttackController attackController,
            IUnitInfoInternal unitInfo,
            IStateControllerExternal stateController,
            IUnitEvents unitEvents
            )
        {
            _attackController = attackController;
            _unitInfo = unitInfo;
            _unitEvents = unitEvents;
            _stateController = stateController;
        }
        
        public void Initialize(IOneUnitController oneUnitController)
        {
            _unitEvents.AttackComplete += AttackCompleteHandler;
        }

        private void AttackCompleteHandler()
        {
            _stateController.CurrentState.Attack(_target.Position);
        }
        
        private void TargetHealthEndedHandler()
        {
            Cancel();
            Complete?.Invoke();
        }

        public void Start(IOneUnitController target)
        {
            _unitInfo.SetAttackTarget(target);
            _target = target;
            
            _target.UnitEvents.HealthEnded += TargetHealthEndedHandler;
            _stateController.CurrentState.Attack(_target.Position);
        }

        public void Cancel()
        {
            _unitEvents.AttackComplete -= AttackCompleteHandler;
            _target.UnitEvents.HealthEnded -= TargetHealthEndedHandler;
            _attackController.Cancel();
        }

        public void DisposeInternal()
        {
            Cancel();
            base.DisposeInternal();
        }
    }
}
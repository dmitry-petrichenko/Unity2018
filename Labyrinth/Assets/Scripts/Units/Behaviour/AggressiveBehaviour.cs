using System;
using Scripts.Extensions;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using Units.OneUnit;

namespace Scripts.Units
{
    public class AggressiveBehaviour : Disposable, IAgressiveBehaviour
    {
        public event Action Complete;

        private IOneUnitController _target;
        
        private readonly IAttackController _attackController;
        private readonly IUnitStateController _stateInfo;
        private readonly IUnitEvents _unitEvents;

        public AggressiveBehaviour(
            IAttackController attackController,
            IUnitStateController stateInfo,
            IUnitEvents unitEvents
            )
        {
            _attackController = attackController;
            _stateInfo = stateInfo;
            _unitEvents = unitEvents;
        }
        
        public void Initialize(IOneUnitController oneUnitController)
        {
            _unitEvents.AttackComplete += AttackCompleteHandler;
        }

        private void AttackCompleteHandler()
        {
            _attackController.Attack(_target.Position);
        }
        
        private void TargetDiedHandler()
        {
            Cancel();
        }

        public void Start(IOneUnitController target)
        {
            _stateInfo.CurrentState.AttackTarget = target;
            _target = target;
            
            _target.UnitEvents.Died += TargetDiedHandler;
            _attackController.Attack(_target.Position);
        }

        public void Cancel()
        {
            _unitEvents.AttackComplete -= AttackCompleteHandler;
            _target.UnitEvents.Died -= TargetDiedHandler;
            _attackController.Cancel();
        }

        public void DisposeInternal()
        {
            Cancel();
            base.DisposeInternal();
        }
    }
}
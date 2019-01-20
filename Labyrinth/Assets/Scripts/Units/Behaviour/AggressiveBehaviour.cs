using System;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using Units.OneUnit;

namespace Scripts.Units
{
    public class AggressiveBehaviour : IAgressiveBehaviour
    {
        public event Action Complete;

        private IOneUnitController _target;
        
        private readonly IAttackController _attackController;
        private readonly IUnitStateInfo _unitStateInfo;
        private readonly IUnitEvents _unitEvents;

        public AggressiveBehaviour(
            IAttackController attackController,
            IUnitStateInfo unitStateInfo,
            IUnitEvents unitEvents
            )
        {
            _attackController = attackController;
            _unitStateInfo = unitStateInfo;
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

        public void Start(IOneUnitController target)
        {
            _unitStateInfo.IsAttacking = true;
            _unitStateInfo.AttackTarget = target;
            _target = target;
            _attackController.Attack(_target.Position);
        }

        public void Cancel()
        {
            _attackController.Cancel();
        }
    }
}